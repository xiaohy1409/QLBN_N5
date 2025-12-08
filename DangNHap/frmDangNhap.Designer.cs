namespace DangNHap
{
    partial class frmDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            this.lblMK = new System.Windows.Forms.Label();
            this.lblTK = new System.Windows.Forms.Label();
            this.txtMK = new System.Windows.Forms.TextBox();
            this.txtTK = new System.Windows.Forms.TextBox();
            this.pbAppDN = new System.Windows.Forms.PictureBox();
            this.lilDangKy = new System.Windows.Forms.LinkLabel();
            this.chbMK = new System.Windows.Forms.CheckBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbAppDN)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMK
            // 
            this.lblMK.AutoSize = true;
            this.lblMK.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMK.Location = new System.Drawing.Point(94, 310);
            this.lblMK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMK.Name = "lblMK";
            this.lblMK.Size = new System.Drawing.Size(120, 26);
            this.lblMK.TabIndex = 0;
            this.lblMK.Text = "Mật khẩu:";
            // 
            // lblTK
            // 
            this.lblTK.AutoSize = true;
            this.lblTK.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTK.Location = new System.Drawing.Point(94, 253);
            this.lblTK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTK.Name = "lblTK";
            this.lblTK.Size = new System.Drawing.Size(123, 26);
            this.lblTK.TabIndex = 0;
            this.lblTK.Text = "Tài khoản:";
            // 
            // txtMK
            // 
            this.txtMK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMK.Location = new System.Drawing.Point(230, 308);
            this.txtMK.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtMK.Name = "txtMK";
            this.txtMK.PasswordChar = '*';
            this.txtMK.Size = new System.Drawing.Size(362, 30);
            this.txtMK.TabIndex = 2;
            this.txtMK.TextChanged += new System.EventHandler(this.txtMK_TextChanged);
            // 
            // txtTK
            // 
            this.txtTK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTK.Location = new System.Drawing.Point(230, 250);
            this.txtTK.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtTK.Name = "txtTK";
            this.txtTK.Size = new System.Drawing.Size(362, 30);
            this.txtTK.TabIndex = 1;
            this.txtTK.TextChanged += new System.EventHandler(this.txtTK_TextChanged);
            // 
            // pbAppDN
            // 
            this.pbAppDN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbAppDN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAppDN.Image = ((System.Drawing.Image)(resources.GetObject("pbAppDN.Image")));
            this.pbAppDN.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbAppDN.InitialImage")));
            this.pbAppDN.Location = new System.Drawing.Point(230, 28);
            this.pbAppDN.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pbAppDN.Name = "pbAppDN";
            this.pbAppDN.Size = new System.Drawing.Size(244, 198);
            this.pbAppDN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAppDN.TabIndex = 15;
            this.pbAppDN.TabStop = false;
            this.pbAppDN.Click += new System.EventHandler(this.pbAppDN_Click);
            // 
            // lilDangKy
            // 
            this.lilDangKy.AutoSize = true;
            this.lilDangKy.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lilDangKy.Location = new System.Drawing.Point(152, 352);
            this.lilDangKy.Name = "lilDangKy";
            this.lilDangKy.Size = new System.Drawing.Size(68, 19);
            this.lilDangKy.TabIndex = 16;
            this.lilDangKy.TabStop = true;
            this.lilDangKy.Text = "Đăng ký";
            this.lilDangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lilDangKy_LinkClicked);
            // 
            // chbMK
            // 
            this.chbMK.AutoSize = true;
            this.chbMK.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbMK.Location = new System.Drawing.Point(451, 349);
            this.chbMK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbMK.Name = "chbMK";
            this.chbMK.Size = new System.Drawing.Size(138, 23);
            this.chbMK.TabIndex = 17;
            this.chbMK.Text = "Hiện mật khẩu";
            this.chbMK.UseVisualStyleBackColor = true;
            this.chbMK.CheckedChanged += new System.EventHandler(this.chbMK_CheckedChanged);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.AutoSize = true;
            this.btnDangNhap.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.Location = new System.Drawing.Point(270, 373);
            this.btnDangNhap.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(166, 38);
            this.btnDangNhap.TabIndex = 18;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(692, 435);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.chbMK);
            this.Controls.Add(this.lilDangKy);
            this.Controls.Add(this.lblMK);
            this.Controls.Add(this.lblTK);
            this.Controls.Add(this.txtMK);
            this.Controls.Add(this.txtTK);
            this.Controls.Add(this.pbAppDN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            ((System.ComponentModel.ISupportInitialize)(this.pbAppDN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMK;
        private System.Windows.Forms.Label lblTK;
        private System.Windows.Forms.TextBox txtMK;
        private System.Windows.Forms.TextBox txtTK;
        private System.Windows.Forms.PictureBox pbAppDN;
        private System.Windows.Forms.LinkLabel lilDangKy;
        private System.Windows.Forms.CheckBox chbMK;
        private System.Windows.Forms.Button btnDangNhap;
    }
}

