using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ChucNangChinh;

namespace DangNHap
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void pbAppDN_Click(object sender, EventArgs e)
        {

        }

        private void txtTK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lilDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy dangKyForm = new frmDangKy();
            dangKyForm.ShowDialog();
        }

        // Duong dan den file luu tai khoan
        private string path = Application.StartupPath + "\\account.txt";

        // Doc danh sach account tu file
        private List<(string username, string password)> ReadAccounts()
        {
            var accounts = new List<(string, string)>();

            if (!File.Exists(path))
                return accounts;

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(' ');
                if (parts.Length != 2) continue;

                accounts.Add((parts[0], parts[1]));
            }

            return accounts;
        }

        // Kiem tra chuoi khong chua dau cach
        private bool CheckNoSpace(string text)
        {
            return !string.IsNullOrWhiteSpace(text) && !text.Contains(" ");
        }

        // Kiem tra dang nhap hop le
        private bool LoginValid(string username, string password)
        {
            var accounts = ReadAccounts();
            return accounts.Any(a => a.username == username && a.password == password);
        }

        // CheckBox hien/an mat khau
        private void chbMK_CheckedChanged(object sender, EventArgs e)
        {
            txtMK.PasswordChar = chbMK.Checked ? '\0' : '*';
        }

        /*
        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn thoát không?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }

        }
        */

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTK.Text.Trim();
            string password = txtMK.Text.Trim();

            // Kiem tra thong tin rong
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Bạn nhập chưa đủ thông tin", "Thông báo");
                return;
            }

            // Kiem tra dau cach
            if (!CheckNoSpace(username) || !CheckNoSpace(password))
            {
                MessageBox.Show("Tên tài khoản và mật khẩu không được chứa dấu cách", "Thông báo");
                return;
            }

            // Kiem tra dang nhap
            if (LoginValid(username, password))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                // Mo form chinh frmQLBN
                ChucNangChinh.frmChucNangChinh homeForm = new ChucNangChinh.frmChucNangChinh();
                homeForm.Show();

                // Khi form chinh dong thi thoat chuong trinh
                homeForm.FormClosed += (s, args) => Application.Exit();

                // An form dang nhap
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo");
            }
        }
    }
}
