using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ChucNangChinh
{
    public partial class frmChucNangChinh : Form
    {
        // Khởi tạo hàng đợi
        MyLinkedQueue hangDoi = new MyLinkedQueue();
        bool daThayDoiDuLieu = false;
        string maDangSua = "";

        public frmChucNangChinh()
        {
            InitializeComponent();
            KhoiTaoGiaoDien();
            CapNhatThongKe();
        }

        void KhoiTaoGiaoDien()
        {
            Color primaryColor = Color.FromArgb(0, 122, 204); 
            Color successColor = Color.ForestGreen;             
            Color dangerColor = Color.IndianRed;              
            Color textDark = Color.FromArgb(64, 64, 64);       

            // Cài đặt Form
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            cbbKhoa.Items.Clear();
            cbbKhoa.Items.AddRange(new string[] { "Nội khoa", "Ngoại khoa", "Nhi khoa", "Cấp cứu", "Tai Mũi Họng" });
            if (cbbKhoa.Items.Count > 0) cbbKhoa.SelectedIndex = 0;

            dgvDanhSach.ColumnCount = 5;
            dgvDanhSach.Columns[0].Name = "Mã Số";
            dgvDanhSach.Columns[1].Name = "Họ Tên";
            dgvDanhSach.Columns[2].Name = "Ngày Giờ";
            dgvDanhSach.Columns[3].Name = "Khoa";
            dgvDanhSach.Columns[4].Name = "Ưu Tiên";

            dgvDanhSach.BackgroundColor = Color.White;
            dgvDanhSach.BorderStyle = BorderStyle.None;

            dgvDanhSach.EnableHeadersVisualStyles = false;
            dgvDanhSach.ColumnHeadersDefaultCellStyle.BackColor = primaryColor; 
            dgvDanhSach.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; 
            dgvDanhSach.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDanhSach.ColumnHeadersHeight = 40; 

            // Rows (Dòng dữ liệu)
            dgvDanhSach.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 241); 
            dgvDanhSach.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDanhSach.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDanhSach.RowTemplate.Height = 30; 
            dgvDanhSach.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); 
            // Layout
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSach.ReadOnly = true;

          
            SetupButton(btnThem, "THÊM BỆNH NHÂN", primaryColor);
            SetupButton(btnGoiKham, "GỌI KHÁM", successColor);
            SetupButton(btnXoa, "XÓA", dangerColor);

            // Cấu hình Tooltip
            ToolTip tt = new ToolTip();
            tt.IsBalloon = true; // Tạo hình bong bóng cho đẹp
            tt.ToolTipIcon = ToolTipIcon.Info; // Thêm icon chữ i nhỏ
            tt.SetToolTip(numUuTien, "Số càng nhỏ độ ưu tiên càng cao (0 là ưu tiên nhất)");
            tt.SetToolTip(cbbKhoa, "Chọn khoa chuyên môn cần khám");
            tt.SetToolTip(btnGoiKham, "Hệ thống sẽ gọi bệnh nhân có độ ưu tiên cao nhất");
        }

        void SetupButton(Button btn, string text, Color bg)
        {
            btn.Text = text;
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            //btn.Size = new Size(150, 40); 
        }

        // Hàm vẽ lại dữ liệu từ Linked List lên Bảng
        // Hàm hiển thị nhận thêm tham số 'tuKhoa', mặc định là rỗng (nghĩa là hiện tất cả)
        void HienThiLenBang(string tuKhoa = "")
        {
            dgvDanhSach.Rows.Clear(); // Xóa dữ liệu cũ trên bảng

            // Đổi từ khóa sang chữ thường để tìm không phân biệt hoa/thường (VD: "An" tìm ra "an")
            string tuKhoaThuong = tuKhoa.ToLower().Trim();

            Node current = hangDoi.GetHead();
            while (current != null)
            {
                BenhNhan bn = current.Data;

                // KIỂM TRA ĐIỀU KIỆN LỌC
                // 1. Kiểm tra Tên (chuyển về chữ thường rồi so sánh)
                bool timThayTen = bn.HoTen.ToLower().Contains(tuKhoaThuong);
                // 2. Kiểm tra Mã số
                bool timThayMa = bn.MaSo.ToLower().Contains(tuKhoaThuong);

                // Nếu không nhập gì (tuKhoa rỗng) HOẶC tìm thấy Tên HOẶC tìm thấy Mã
                if (string.IsNullOrEmpty(tuKhoa) || timThayTen || timThayMa)
                {
                    dgvDanhSach.Rows.Add(
                        bn.MaSo,
                        bn.HoTen,
                        bn.NgayGio.ToString("dd/MM/yyyy HH:mm"),
                        bn.Khoa,
                        bn.DoUuTien
                    );
                }

                current = current.Next;
            }
        }

        // ------------------- CÁC SỰ KIỆN NÚT BẤM -------------------

        // 1. Nút THÊM (Enqueue)
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(txtMa.Text) || string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên bệnh nhân!", "Thiếu thông tin");
                return;
            }

            if (hangDoi.ContainsMaSo(txtMa.Text))
            {
                MessageBox.Show($"Mã số '{txtMa.Text}' đã tồn tại trong danh sách chờ!", "Trùng mã số");
                txtMa.Focus();
                return;
            }

            BenhNhan bn = new BenhNhan(
                txtMa.Text,
                txtTen.Text,
                dtpNgayKham.Value,
                cbbKhoa.Text,
                (int)numUuTien.Value
            );

            hangDoi.Enqueue(bn); // Thêm vào cuối hàng đợi
            HienThiLenBang();
            CapNhatThongKe();

            if (dgvDanhSach.Rows.Count > 0)
            {
                dgvDanhSach.ClearSelection();
                dgvDanhSach.Rows[dgvDanhSach.Rows.Count - 1].Selected = true;
            }

            // Reset ô nhập liệu
            txtMa.Clear(); txtTen.Clear(); txtMa.Focus();
            daThayDoiDuLieu = true;
        }

        // 2. Nút GỌI KHÁM (Dequeue theo Ưu tiên)
        private void btnGoiKham_Click(object sender, EventArgs e)
        {
            if (hangDoi.IsEmpty())
            {
                MessageBox.Show("Hàng đợi đang trống!");
                // Xóa trắng thông tin nếu không còn ai
                lblChiTietMa.Text = "...";
                lblChiTietTen.Text = "...";
                lblChiTietKhoa.Text = "...";
                lblChiTietUuTien.Text = "...";
                return;
            }

            // 1. Lấy người ưu tiên nhất ra khỏi hàng đợi
            BenhNhan bnDuocGoi = hangDoi.DequeuePriority();

            if (bnDuocGoi != null)
            {
                // 2. Hiển thị thông tin người được gọi lên các Label chi tiết
                lblChiTietMa.Text = bnDuocGoi.MaSo;
                lblChiTietTen.Text = bnDuocGoi.HoTen;
                lblChiTietKhoa.Text = bnDuocGoi.Khoa;
                lblChiTietUuTien.Text = bnDuocGoi.DoUuTien.ToString();

                // 3. Cập nhật lại bảng danh sách (người này sẽ biến mất khỏi bảng)
                HienThiLenBang(txtTimKiem.Text);
                CapNhatThongKe();
                daThayDoiDuLieu = true;
                dgvDanhSach.ClearSelection();

                MessageBox.Show($"Mời bệnh nhân: {bnDuocGoi.HoTen} vào phòng khám!", "Thông báo gọi khám");
            }
        }

        // 3. Nút XÓA (Xóa người đang chọn trên bảng)
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count > 0)
            {
                // Lấy mã số của dòng đang chọn
                string maCanXoa = dgvDanhSach.SelectedRows[0].Cells[0].Value.ToString();
                string tenCanXoa = dgvDanhSach.SelectedRows[0].Cells[1].Value.ToString();

                DialogResult hoi = MessageBox.Show($"Bạn chắc chắn muốn xóa bệnh nhân '{tenCanXoa}' (Mã: {maCanXoa})?",
                                                   "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (hoi == DialogResult.Yes)
                {
                    bool kq = hangDoi.RemoveByMaSo(maCanXoa);
                    if (kq)
                    {
                        MessageBox.Show("Đã xóa bệnh nhân thành công!");
                        CapNhatThongKe();
                        HienThiLenBang();
                        dgvDanhSach.ClearSelection();
                        //lblTrangThai.Text = "Trạng thái: Sẵn sàng";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bệnh nhân trong dữ liệu gốc!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng trên bảng để xóa!");
            }
        }
        private void frmChucNangChinh_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (daThayDoiDuLieu == true)
            {
                DialogResult result = MessageBox.Show(
                    "Dữ liệu đã thay đổi nhưng chưa được lưu. Bạn có muốn lưu trước khi thoát không?",
                    "Cảnh báo dữ liệu",
                    MessageBoxButtons.YesNoCancel, // 3 lựa chọn: Có, Không, Hủy
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    // Người dùng chọn "Có" -> Gọi hàm Lưu rồi mới thoát
                    btnLuu_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    // Người dùng chọn "Hủy" -> Không thoát nữa, ở lại chương trình
                    e.Cancel = true;
                }
                // Nếu chọn "No" -> Thoát luôn, chấp nhận mất dữ liệu (không cần làm gì thêm)
            }
        }
        void CapNhatThongKe()
        {
            int tongSo = 0;
            int uuTienCao = 0; // Đếm số người có mức ưu tiên 0 (Nặng nhất)

            Node current = hangDoi.GetHead();
            while (current != null)
            {
                tongSo++;
                if (current.Data.DoUuTien == 0) uuTienCao++;
                current = current.Next;
            }

            // Hiển thị lên Label
            lblThongKe.Text = $"Tổng số chờ: {tongSo} | Ưu tiên cao (Mức 0): {uuTienCao}";
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            HienThiLenBang(txtTimKiem.Text);
        }

        private void dgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            //if (dgvDanhSach.SelectedRows.Count > 0)
            //{
            //    DataGridViewRow row = dgvDanhSach.SelectedRows[0];

            //    // 1. Đổ dữ liệu từ bảng vào các ô nhập liệu
            //    txtMa.Text = row.Cells[0].Value.ToString();
            //    txtTen.Text = row.Cells[1].Value.ToString();

            //    // Xử lý ngày tháng (chuyển từ chuỗi về dạng Date)
            //    dtpNgayKham.Value = DateTime.ParseExact(row.Cells[2].Value.ToString(), "dd/MM/yyyy HH:mm", null);

            //    cbbKhoa.Text = row.Cells[3].Value.ToString();
            //    numUuTien.Value = int.Parse(row.Cells[4].Value.ToString());

            //    // 2. Quan trọng: Ghi nhớ Mã số cũ để lát nữa biết đang sửa ai
            //    maDangSua = txtMa.Text;
            //}
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (hangDoi.IsEmpty())
            {
                MessageBox.Show("Danh sách trống, không có gì để lưu!");
                return;
            }

            // 2. Mở file tên là "data.txt" để ghi (nếu chưa có nó sẽ tự tạo)
            // StreamWriter là công cụ giúp viết chữ vào file
            StreamWriter sw = new StreamWriter("data.txt");

            // 3. Duyệt danh sách liên kết
            Node current = hangDoi.GetHead();
            while (current != null)
            {
                BenhNhan bn = current.Data;

                // Tạo dòng chữ theo định dạng: Mã|Tên|Ngày|Khoa|ƯuTiên
                string dongDuLieu = $"{bn.MaSo}|{bn.HoTen}|{bn.NgayGio}|{bn.Khoa}|{bn.DoUuTien}";

                // Ghi dòng này vào file
                sw.WriteLine(dongDuLieu);

                current = current.Next;
            }

            // 4. Đóng file lại (Rất quan trọng! Nếu quên bước này file sẽ rỗng)
            sw.Close();

            MessageBox.Show("Đã lưu dữ liệu thành công vào file data.txt!");
            daThayDoiDuLieu = false;
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem file có tồn tại không
            if (!File.Exists("data.txt"))
            {
                MessageBox.Show("Chưa có dữ liệu nào được lưu trước đó!");
                return;
            }

            // 2. Xóa sạch hàng đợi hiện tại để nạp mới (tránh bị trùng lặp)
            hangDoi = new MyLinkedQueue();

            // 3. Đọc tất cả các dòng trong file
            string[] tatCaDong = File.ReadAllLines("data.txt");

            foreach (string dong in tatCaDong)
            {
                // dong sẽ giống như: "BN01|Nguyen Van A|..."
                // Cắt chuỗi dựa vào dấu '|'
                string[] thongTin = dong.Split('|');

                // Kiểm tra xem có đủ 5 phần thông tin không (tránh lỗi file hỏng)
                if (thongTin.Length == 5)
                {
                    // Tạo lại đối tượng Bệnh Nhân
                    BenhNhan bn = new BenhNhan(
                        thongTin[0], // Mã
                        thongTin[1], // Tên
                        DateTime.Parse(thongTin[2]), // Ngày giờ (chuyển từ chuỗi sang DateTime)
                        thongTin[3], // Khoa
                        int.Parse(thongTin[4]) // Độ ưu tiên (chuyển từ chuỗi sang số)
                    );

                    // Thêm vào hàng đợi
                    hangDoi.Enqueue(bn);
                }
            }

            // 4. Cập nhật lại giao diện
            HienThiLenBang(txtTimKiem.Text);
            CapNhatThongKe();// Nhớ cập nhật cả số lượng thống kê

            MessageBox.Show("Đã tải dữ liệu xong!");
            daThayDoiDuLieu = false;
        }
    }


    // KHU VỰC CẤU TRÚC DỮ LIỆU TỰ ĐỊNH NGHĨA 

    // 1. Class Dữ liệu
    public class BenhNhan
    {
        public string MaSo { get; set; }
        public string HoTen { get; set; }
        public DateTime NgayGio { get; set; }
        public string Khoa { get; set; }
        public int DoUuTien { get; set; } 

        public BenhNhan(string ma, string ten, DateTime ngay, string khoa, int uuTien)
        {
            MaSo = ma; HoTen = ten; NgayGio = ngay; Khoa = khoa; DoUuTien = uuTien;
        }
    }

    // 2. Class Node (Mắt xích)
    public class Node
    {
        public BenhNhan Data;
        public Node Next;
        public Node(BenhNhan data) { Data = data; Next = null; }
    }

    // 3. Class Hàng đợi (Linked List thủ công + Logic Ưu tiên)
    public class MyLinkedQueue
    {
        private Node head;
        private Node tail;

        public MyLinkedQueue() { head = null; tail = null; }
        public bool IsEmpty() { return head == null; }
        public Node GetHead() { return head; }

        // [MỚI] Hàm kiểm tra trùng Mã Số
        public bool ContainsMaSo(string maSo)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.MaSo == maSo) return true;
                current = current.Next;
            }
            return false;
        }

        // Chức năng: Thêm vào CUỐI danh sách (Enqueue thường)
        public void Enqueue(BenhNhan bn)
        {
            Node newNode = new Node(bn);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
        }

        // Chức năng: Lấy ra theo ĐỘ ƯU TIÊN (Priority Queue)
        public BenhNhan DequeuePriority()
        {
            if (head == null) return null;

            // Tìm Node có độ ưu tiên cao nhất (Số DoUuTien nhỏ nhất)
            Node current = head;
            Node prev = null;

            Node maxPrioNode = head;      
            Node prevMaxPrioNode = null;  
            int minPrioValue = head.Data.DoUuTien;

            while (current != null)
            {
                // Nếu tìm thấy số nhỏ hơn (ưu tiên cao hơn)
                if (current.Data.DoUuTien < minPrioValue)
                {
                    minPrioValue = current.Data.DoUuTien;
                    maxPrioNode = current;
                    prevMaxPrioNode = prev;
                }

                prev = current;
                current = current.Next;
            }

            // Lấy dữ liệu ra
            BenhNhan result = maxPrioNode.Data;

            // Xóa Node đó khỏi danh sách
            if (maxPrioNode == head)
            {
                head = head.Next;
                if (head == null) tail = null;
            }
            else if (maxPrioNode == tail) 
            {
                prevMaxPrioNode.Next = null;
                tail = prevMaxPrioNode;
            }
            else // Nếu ở giữa
            {
                prevMaxPrioNode.Next = maxPrioNode.Next;
            }

            return result;
        }

        // Chức năng: Xóa bất kỳ theo Mã Số
        public bool RemoveByMaSo(string maSo)
        {
            if (head == null) return false;

            // Trường hợp xóa đầu
            if (head.Data.MaSo == maSo)
            {
                head = head.Next;
                if (head == null) tail = null;
                return true;
            }

            // Trường hợp xóa giữa hoặc cuối
            Node current = head;
            while (current.Next != null)
            {
                if (current.Next.Data.MaSo == maSo)
                {
                    current.Next = current.Next.Next; 
                    if (current.Next == null) tail = current; 
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // cập nhật
        public bool CapNhat(string maSoCu, BenhNhan bnMoi)
        {
            Node current = head;
            while (current != null)
            {
                // Tìm thấy người có mã số cũ
                if (current.Data.MaSo == maSoCu)
                {
                    current.Data = bnMoi; // Thay thế bằng dữ liệu mới
                    return true; // Báo cáo: Sửa thành công
                }
                current = current.Next;
            }
            return false; // Báo cáo: Không tìm thấy người này
        }
    }
}