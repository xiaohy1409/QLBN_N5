using System;
using System.IO;
using System.Windows.Forms;

namespace ChucNangChinh
{
    public partial class frmLichSu : Form
    {
        public frmLichSu()
        {
            InitializeComponent();
            LoadHistory();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadHistory()
        {
            dgv.Rows.Clear();
            if (!File.Exists("history.txt")) return;

            string[] lines = File.ReadAllLines("history.txt");
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                // NgayGoi|MaSo|HoTen|NgayHen|Khoa|DoUuTien
                if (parts.Length >= 6)
                {
                    dgv.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
                }
                else
                {
                    
                    dgv.Rows.Add(line, "", "", "", "", "");
                }
            }
        }

        private void frmLichSu_Load(object sender, EventArgs e)
        {

        }
    }
}
