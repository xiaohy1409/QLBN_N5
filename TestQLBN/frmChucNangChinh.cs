using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChucNangChinh
{
    public partial class frmChucNangChinh : Form
    {
        // Khởi tạo hàng đợi
        MyLinkedQueue hangDoi = new MyLinkedQueue();

        public frmChucNangChinh()
        {
            InitializeComponent();
            KhoiTaoGiaoDien();
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
            btn.Size = new Size(150, 40); 
        }

        // Hàm vẽ lại dữ liệu từ Linked List lên Bảng
        void HienThiLenBang()
        {
            dgvDanhSach.Rows.Clear(); // Xóa bảng cũ

            Node current = hangDoi.GetHead();
            while (current != null)
            {
                BenhNhan bn = current.Data;
                // Thêm dòng mới
                dgvDanhSach.Rows.Add(bn.MaSo, bn.HoTen, bn.NgayGio.ToString("dd/MM/yyyy HH:mm"), bn.Khoa, bn.DoUuTien);
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

            if (dgvDanhSach.Rows.Count > 0)
            {
                dgvDanhSach.ClearSelection();
                dgvDanhSach.Rows[dgvDanhSach.Rows.Count - 1].Selected = true;
            }

            // Reset ô nhập liệu
            txtMa.Clear(); txtTen.Clear(); txtMa.Focus();
        }

        // 2. Nút GỌI KHÁM (Dequeue theo Ưu tiên)
        private void btnGoiKham_Click(object sender, EventArgs e)
        {
            if (hangDoi.IsEmpty())
            {
                MessageBox.Show("Hàng đợi đang trống!");
                lblTrangThai.Text = "Trạng thái: Sẵn sàng";
                return;
            }

            // Gọi hàm lấy người có độ ưu tiên cao nhất
            BenhNhan bnDuocGoi = hangDoi.DequeuePriority();

            if (bnDuocGoi != null)
            {
                lblTrangThai.Text = $"Đang gọi: {bnDuocGoi.HoTen} - Khoa {bnDuocGoi.Khoa} (Ưu tiên: {bnDuocGoi.DoUuTien})";
                HienThiLenBang();
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
                        HienThiLenBang();
                        lblTrangThai.Text = "Trạng thái: Sẵn sàng";
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
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn thoát chương trình quản lý không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true; 
            }
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
    }
}