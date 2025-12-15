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
            List<(string username, string password)> accounts =
                new List<(string username, string password)>();

            if (!File.Exists(path))
                return accounts;

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line == null || line.Length == 0)
                    continue;
                string[] parts = line.Split(' ');
                if (parts.Length != 2)
                    continue;
                accounts.Add((parts[0], parts[1]));
            }
            return accounts;
        }

        private bool KiemTraRongVaDauCach(string text)
        {
            // 1. Kiem tra chuoi co NULL khong
            if (text == null)
                return false;
            // 2. Kiem tra chuoi co rong khong
            if (text.Length == 0)
                return false;
            // 3. Kiem tra chuoi co dau cach khong
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    return false;
            }
            return true;
        }

        private bool KiemTraTaiKhoanVaMatKhau(string username, string password)
        {
            List<(string username, string password)> accounts = ReadAccounts();

            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].username == username &&
                    accounts[i].password == password)
                {
                    return true;
                }
            }
            return false;
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

            if (!KiemTraRongVaDauCach(username))
            {
                MessageBox.Show(
                    "Tên tài khoản không được rỗng hoặc chứa dấu cách",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!KiemTraRongVaDauCach(password))
            {
                MessageBox.Show(
                    "MẬt khẩu không được rỗng hoặc chứa dấu cách",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (KiemTraTaiKhoanVaMatKhau(username, password))
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
