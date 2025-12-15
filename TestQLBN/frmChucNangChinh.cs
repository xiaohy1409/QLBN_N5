using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ChucNangChinh
{
    public partial class frmChucNangChinh : Form
    {
        MyLinkedQueue hangDoi = new MyLinkedQueue();
        bool daThayDoiDuLieu = false;
        string maDangSua = "";

        public frmChucNangChinh()
        {
            InitializeComponent();
            KhoiTaoGiaoDien();
            CapNhatThongKe();

        }

        // Sự kiện Load Form
        private void frmChucNangChinh_Load(object sender, EventArgs e)
        {
            KhoiTaoGiaoDien();
            MoDuLieu();
            dgvDanhSach.ClearSelection();
        }

        // Hàm thiết lập giao diện ban đầu
        void KhoiTaoGiaoDien()
        {
            Color primaryColor = Color.FromArgb(0, 122, 204);
            Color successColor = Color.ForestGreen;
            Color dangerColor = Color.IndianRed;

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

            dgvDanhSach.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 241);
            dgvDanhSach.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDanhSach.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDanhSach.RowTemplate.Height = 30;
            dgvDanhSach.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSach.ReadOnly = true;

            SetupButton(btnThem, "THÊM BỆNH NHÂN", primaryColor);
            SetupButton(btnGoiKham, "GỌI KHÁM", successColor);
            SetupButton(btnXoa, "XÓA", dangerColor);

            ToolTip tt = new ToolTip();
            tt.IsBalloon = true;
            tt.ToolTipIcon = ToolTipIcon.Info;

            // Giới hạn độ ưu tiên từ 0 đến 7
            numUuTien.Minimum = 1;
            numUuTien.Maximum = 7;
            numUuTien.Value = 1;
            numUuTien.ReadOnly = true;

            // Tooltip mô tả chi tiết mức ưu tiên
            tt.SetToolTip(numUuTien,
                "1: Người bệnh trong tình trạng cấp cứu\n" +
                "2: Trẻ em dưới 06 tuổi\n" +
                "3: Phụ nữ có thai\n" +
                "4: Người khuyết tật đặc biệt nặng\n" +
                "5: Người khuyết tật nặng\n" +
                "6: Người từ đủ 75 tuổi trở lên\n" +
                "7: Người có công với cách mạng"
            );

            tt.SetToolTip(cbbKhoa, "Chọn khoa chuyên môn cần khám");
            tt.SetToolTip(btnGoiKham, "Hệ thống sẽ gọi bệnh nhân có độ ưu tiên cao nhất");
            tt.SetToolTip(btnSua, "Chọn một dòng trong danh sách rồi bấm nút này để sửa thông tin");

            // Cấu hình thời gian chỉ cho chọn ngày giờ sau hiện tại
            dtpNgayKham.Format = DateTimePickerFormat.Custom;
            dtpNgayKham.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpNgayKham.ShowUpDown = true;
            // Đặt MinDate là ngày giờ hiện tại
            dtpNgayKham.MinDate = DateTime.Now;
        }

        // Hàm định dạng nút bấm
        void SetupButton(Button btn, string text, Color bg)
        {
            btn.Text = text;
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        // Hàm hiển thị dữ liệu lên DataGridView
        void HienThiLenBang(string tuKhoa = "")
        {
            dgvDanhSach.Rows.Clear();
            string tuKhoaThuong = tuKhoa.ToLower().Trim();

            Node current = hangDoi.GetHead();
            while (current != null)
            {
                BenhNhan bn = current.Data;
                bool timThayTen = bn.HoTen.ToLower().Contains(tuKhoaThuong);
                bool timThayMa = bn.MaSo.ToLower().Contains(tuKhoaThuong);

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

        // Sự kiện thêm bệnh nhân
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text) || string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên bệnh nhân!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BenhNhan bn = new BenhNhan(
                txtMa.Text,
                txtTen.Text,
                dtpNgayKham.Value,
                cbbKhoa.Text,
                (int)numUuTien.Value
            );

            if (string.IsNullOrEmpty(maDangSua))
            {
                if (hangDoi.ContainsMaSo(txtMa.Text))
                {
                    MessageBox.Show($"Mã số '{txtMa.Text}' đã tồn tại!", "Trùng mã số", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMa.Focus();
                    return;
                }

                hangDoi.Enqueue(bn);
                MessageBox.Show("Thêm mới thành công!");
            }
            else
            {
                bool daXoa = hangDoi.RemoveByMaSo(maDangSua);

                if (!daXoa)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy dữ liệu gốc để sửa!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                hangDoi.Enqueue(bn);

                MessageBox.Show("Cập nhật thông tin thành công!");

                maDangSua = "";          
                txtMa.Enabled = true;     
                btnThem.Text = "THÊM BỆNH NHÂN";
                btnThem.BackColor = Color.FromArgb(0, 122, 204);
            }

            HienThiLenBang(txtTimKiem.Text);
            CapNhatThongKe();

            txtMa.Clear();
            txtTen.Clear();
            dtpNgayKham.Value = DateTime.Now;
            numUuTien.Value = 0;
            daThayDoiDuLieu = true; 
        }

        private void btnGoiKham_Click(object sender, EventArgs e)
        {
            if (hangDoi.IsEmpty())
            {
                MessageBox.Show("Hàng đợi đang trống!");
                lblChiTietMa.Text = "...";
                lblChiTietTen.Text = "...";
                lblChiTietKhoa.Text = "...";
                lblChiTietUuTien.Text = "...";
                return;
            }

            BenhNhan bnDuocGoi = hangDoi.DequeuePriority();

            if (bnDuocGoi != null)
            {
                lblChiTietMa.Text = bnDuocGoi.MaSo;
                lblChiTietTen.Text = bnDuocGoi.HoTen;
                lblChiTietKhoa.Text = bnDuocGoi.Khoa;
                lblChiTietUuTien.Text = bnDuocGoi.DoUuTien.ToString();

                HienThiLenBang(txtTimKiem.Text);
                CapNhatThongKe();
                daThayDoiDuLieu = true;
                dgvDanhSach.ClearSelection();
                dgvDanhSach.CurrentCell = null;

                MessageBox.Show($"Mời bệnh nhân: {bnDuocGoi.HoTen} vào phòng khám!", "Thông báo gọi khám");
            }
        }

        // Sự kiện xóa bệnh nhân
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count > 0)
            {
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
                        dgvDanhSach.CurrentCell = null;
                        daThayDoiDuLieu = true;
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

        // Sự kiện đóng Form
        private void frmChucNangChinh_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (daThayDoiDuLieu == true)
            {
                DialogResult result = MessageBox.Show(
                    "Dữ liệu đã thay đổi nhưng chưa được lưu. Bạn có muốn lưu trước khi thoát không?",
                    "Cảnh báo dữ liệu",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    btnLuu_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        // Hàm cập nhật thống kê
        void CapNhatThongKe()
        {
            int tongSo = 0;
            int uuTienCao = 1;

            Node current = hangDoi.GetHead();
            while (current != null)
            {
                tongSo++;
                if (current.Data.DoUuTien == 1) uuTienCao++;
                current = current.Next;
            }

            lblThongKe.Text = $"Tổng số chờ: {tongSo} | Ưu tiên cao (Mức 1): {uuTienCao}";
        }

        // Sự kiện tìm kiếm
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            HienThiLenBang(txtTimKiem.Text);
        }

        // Sự kiện lưu file
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (hangDoi.IsEmpty())
            {
                MessageBox.Show("Danh sách trống, không có gì để lưu!");
                return;
            }

            StreamWriter sw = new StreamWriter("data.txt");
            Node current = hangDoi.GetHead();
            while (current != null)
            {
                BenhNhan bn = current.Data;
                string dongDuLieu = $"{bn.MaSo}|{bn.HoTen}|{bn.NgayGio}|{bn.Khoa}|{bn.DoUuTien}";
                sw.WriteLine(dongDuLieu);
                current = current.Next;
            }
            sw.Close();

            MessageBox.Show("Đã lưu dữ liệu thành công vào file data.txt!");
            daThayDoiDuLieu = false;
        }

        //hàm mở file và hiển thị lên bảng

        private void MoDuLieu()
        {
            if (!File.Exists("data.txt"))
            {
                //MessageBox.Show("Chưa có dữ liệu nào được lưu trước đó!");
                return;
            }

            hangDoi = new MyLinkedQueue();
            string[] tatCaDong = File.ReadAllLines("data.txt");

            foreach (string dong in tatCaDong)
            {
                string[] thongTin = dong.Split('|');
                if (thongTin.Length == 5)
                {
                    BenhNhan bn = new BenhNhan(
                        thongTin[0],
                        thongTin[1],
                        DateTime.Parse(thongTin[2]),
                        thongTin[3],
                        int.Parse(thongTin[4])
                    );
                    hangDoi.Enqueue(bn);
                }
            }
            HienThiLenBang(txtTimKiem.Text);
            CapNhatThongKe();

            // Xử lý bỏ chọn dòng đầu
            dgvDanhSach.ClearSelection();
            dgvDanhSach.CurrentCell = null;

            daThayDoiDuLieu = false;
        }

        // Sự kiện mở file
        private void btnMo_Click(object sender, EventArgs e)
        {
           MoDuLieu();
        }

        private void cbbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbKhoa.Text == "Cấp cứu")
            {
                numUuTien.Value = 1;
                numUuTien.Enabled = false;
            }
            else
            {
                numUuTien.Enabled = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bệnh nhân trên bảng để sửa!",
                                "Chưa chọn dòng",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = dgvDanhSach.SelectedRows[0];

            txtMa.Text = row.Cells[0].Value.ToString();
            txtTen.Text = row.Cells[1].Value.ToString();

            dtpNgayKham.MinDate = DateTime.ParseExact(row.Cells[2].Value.ToString(), "dd/MM/yyyy HH:mm", null);

            // 2. Lấy chuỗi từ bảng
            string chuoiNgay = row.Cells[2].Value.ToString();
            DateTime ketQua;

            bool thanhCong = DateTime.TryParseExact(
                chuoiNgay,
                "dd/MM/yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out ketQua
            );

            if (thanhCong)
            {
                dtpNgayKham.Value = ketQua;
            }
            else
            {
                dtpNgayKham.Value = DateTime.Now; 
            }

            cbbKhoa.Text = row.Cells[3].Value.ToString();
            numUuTien.Value = int.Parse(row.Cells[4].Value.ToString());

            maDangSua = txtMa.Text; 
            txtMa.Enabled = false;  

            btnThem.Text = "CẬP NHẬT";
            btnThem.BackColor = Color.Orange; 
        }

        private void frmChucNangChinh_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Đã tải dữ liệu xong!");
        }
    }


    // --- KHU VỰC CẤU TRÚC DỮ LIỆU ---
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

    public class Node
    {
        public BenhNhan Data;
        public Node Next;
        public Node(BenhNhan data) { Data = data; Next = null; }
    }

    public class MyLinkedQueue
    {
        private Node head;
        private Node tail;

        public MyLinkedQueue() { head = null; tail = null; }
        public bool IsEmpty() { return head == null; }
        public Node GetHead() { return head; }

        // Kiểm tra tồn tại Mã số
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
        private bool HigherPrio(BenhNhan a, BenhNhan b)
        {
            // TRUE thì người a có độ ưu tiên cao hơn người b
            // FALSE thì người b có độ ưu tiên cao hơn người a

            // 1. So sánh ngày
            if (a.NgayGio.Date < b.NgayGio.Date) return true;
            if (a.NgayGio.Date > b.NgayGio.Date) return false;

            // 2. So sánh độ ưu tiên
            if (a.DoUuTien < b.DoUuTien) return true;
            if (a.DoUuTien > b.DoUuTien) return false;

            // 3. So sánh giờ, phút, giây
            if (a.NgayGio.TimeOfDay < b.NgayGio.TimeOfDay) return true;
            if (a.NgayGio.TimeOfDay > b.NgayGio.TimeOfDay) return false;

            // 4. Bằng nhau hết thì FIFO
            return false;
        }

        // Thêm vào hàng đợi (Enqueue)
        public void Enqueue(BenhNhan bn)
        {
            Node newNode = new Node(bn);
            if (head == null)
            {
                head = tail = newNode;
                return;
            }
            if (HigherPrio(newNode.Data, head.Data))
            {
                newNode.Next = head;
                head = newNode;
                return;
            }

            // Case 3: insert in middle or end
            Node current = head;
            while (current.Next != null &&
                   !HigherPrio(newNode.Data, current.Next.Data))
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            current.Next = newNode;
            if (newNode.Next == null) tail = newNode;
        }

        // Lấy ra theo độ ưu tiên (DequeuePriority)
        public BenhNhan DequeuePriority()
        {
            if (head == null) return null;
            var res = head.Data;
            head = head.Next;
            if (head == null) tail = null;
            return res;
        }


        // Xóa theo Mã số
        public bool RemoveByMaSo(string maSo)
        {
            if (head == null) return false;

            if (head.Data.MaSo == maSo)
            {
                head = head.Next;
                if (head == null) tail = null;
                return true;
            }

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

        //Cập nhật thông tin bệnh nhân
        public bool CapNhat(string maSoCu, BenhNhan bnMoi)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.MaSo == maSoCu)
                {
                    current.Data = bnMoi;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
    }
}