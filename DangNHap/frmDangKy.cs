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

namespace DangNHap
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        // Duong dan den file luu tru tai khoan "account.txt"
        private string path = Application.StartupPath + "\\account.txt";

        // Ham doc danh sach account tu file
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

        // Kiem tra account da ton tai chua
        private bool AccountExists(string username)
        {
            var accounts = ReadAccounts();
            return accounts.Any(a => a.username == username);
        }

        // Luu account moi vao file
        private void SaveAccount(string username, string password)
        {
            File.AppendAllText(path, username + " " + password + Environment.NewLine);
        }
        /*
        private void frmDangKy_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string username = txtTK.Text;
            string password = txtMK.Text;

            // Kiem tra rong
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Bạn nhập chưa đủ thông tin", "Thông báo");
                return;
            }

            // Kiem tra dau cach trong tai khoan
            if (!CheckNoSpace(username))
            {
                MessageBox.Show("Tên tài khoản không được chứa dấu cách", "Thông báo");
                return;
            }

            // Kiem tra dau cach trong mat khau
            if (!CheckNoSpace(password))
            {
                MessageBox.Show("Mật khẩu không được chứa dấu cách", "Thông báo");
                return;
            }

            // Kiem tra trung tai khoan
            if (AccountExists(username))
            {
                MessageBox.Show("Tài khoản này đã tồn tại", "Thông báo");
                return;
            }

            // Luu tai khoan
            SaveAccount(username, password);
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

