using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DangNHap
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
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

        // Duong dan den file luu tru tai khoan "account.txt"
        private string path = Application.StartupPath + "\\account.txt";

        // Ham doc danh sach account tu file
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

        // Kiem tra account da ton tai chua
        private bool KiemTraTaiKhoan(string username)
        {
            List<(string username, string password)> accounts = ReadAccounts();

            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].username == username)
                {
                    return true;
                }
            }

            return false;
        }

        // Luu account moi vao file
        private void WriteAccountToFile(string username, string password)
        {
            File.AppendAllText(path, username + " " + password + Environment.NewLine);
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string username = txtTK.Text;
            string password = txtMK.Text;

            // Kiem tra dau cach trong tai khoan
            if (!KiemTraRongVaDauCach(username))
            {
                MessageBox.Show("Tên tài khoản không được rống hoặc chứa dấu cách", "Thông báo");
                return;
            }

            // Kiem tra dau cach trong mat khau
            if (!KiemTraRongVaDauCach(password))
            {
                MessageBox.Show("Mật khẩu không được rỗng hoặc chứa dấu cách", "Thông báo");
                return;
            }

            // Kiem tra trung tai khoan
            if (KiemTraTaiKhoan(username))
            {
                MessageBox.Show("Tài khoản này đã tồn tại", "Thông báo");
                return;
            }

            // Luu tai khoan
            WriteAccountToFile(username, password);
            MessageBox.Show("Bạn đã đăng ký tài khoản thành công!", "Thông báo");


            // Dong form Dang Ky
            this.Close();
        }

        private void txtTK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {

        }

        // Check An/Hien mat khau
        private void chbMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chbMK.Checked)
            {
                txtMK.PasswordChar = '\0';
            }
            else
            {
                txtMK.PasswordChar = '*';
            }
        }
    }
}

