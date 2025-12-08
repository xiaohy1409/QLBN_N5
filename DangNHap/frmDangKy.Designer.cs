namespace DangNHap
{
    partial class frmDangKy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangKy));
            this.lblMK = new System.Windows.Forms.Label();
            this.lblTK = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.txtMK = new System.Windows.Forms.TextBox();
            this.txtTK = new System.Windows.Forms.TextBox();
            this.pbAppDK = new System.Windows.Forms.PictureBox();
            this.chbMK = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbAppDK)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMK
            // 
            this.lblMK.AutoSize = true;
            this.lblMK.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMK.Location = new System.Drawing.Point(116, 318);
            this.lblMK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMK.Name = "lblMK";
            this.lblMK.Size = new System.Drawing.Size(120, 26);
            this.lblMK.TabIndex = 9;
            this.lblMK.Text = "Mật khẩu:";
            // 
            // lblTK
            // 
            this.lblTK.AutoSize = true;
            this.lblTK.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTK.Location = new System.Drawing.Point(112, 260);
            this.lblTK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTK.Name = "lblTK";
            this.lblTK.Size = new System.Drawing.Size(123, 26);
            this.lblTK.TabIndex = 10;
            this.lblTK.Text = "Tài khoản:";
            // 
            // btnDangKy
            // 
            this.btnDangKy.AutoSize = true;
            this.btnDangKy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.Location = new System.Drawing.Point(276, 370);
            this.btnDangKy.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(166, 38);
            this.btnDangKy.TabIndex = 13;
            this.btnDangKy.Text = "Đăng Ký";
            this.btnDangKy.UseVisualStyleBackColor = true;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // txtMK
            // 
            this.txtMK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMK.Location = new System.Drawing.Point(238, 318);
            this.txtMK.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtMK.Name = "txtMK";
            this.txtMK.PasswordChar = '*';
            this.txtMK.Size = new System.Drawing.Size(362, 30);
            this.txtMK.TabIndex = 12;
            this.txtMK.TextChanged += new System.EventHandler(this.txtMK_TextChanged);
            // 
            // txtTK
            // 
            this.txtTK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTK.Location = new System.Drawing.Point(238, 260);
            this.txtTK.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtTK.Name = "txtTK";
            this.txtTK.Size = new System.Drawing.Size(362, 30);
            this.txtTK.TabIndex = 11;
            this.txtTK.TextChanged += new System.EventHandler(this.txtTK_TextChanged);
            // 
            // pbAppDK
            // 
            this.pbAppDK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbAppDK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbAppDK.Image = ((System.Drawing.Image)(resources.GetObject("pbAppDK.Image")));
            this.pbAppDK.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbAppDK.InitialImage")));
            this.pbAppDK.Location = new System.Drawing.Point(246, 21);
            this.pbAppDK.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pbAppDK.Name = "pbAppDK";
            this.pbAppDK.Size = new System.Drawing.Size(244, 198);
            this.pbAppDK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAppDK.TabIndex = 14;
            this.pbAppDK.TabStop = false;
            // 
            // chbMK
            // 
            this.chbMK.AutoSize = true;
            this.chbMK.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbMK.Location = new System.Drawing.Point(459, 350);
            this.chbMK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbMK.Name = "chbMK";
            this.chbMK.Size = new System.Drawing.Size(138, 23);
            this.chbMK.TabIndex = 15;
            this.chbMK.Text = "Hiện mật khẩu";
            this.chbMK.UseVisualStyleBackColor = true;
            this.chbMK.CheckedChanged += new System.EventHandler(this.chbMK_CheckedChanged);
            // 
            // frmDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(692, 435);
            this.Controls.Add(this.chbMK);
            this.Controls.Add(this.lblMK);
            this.Controls.Add(this.lblTK);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.txtMK);
            this.Controls.Add(this.txtTK);
            this.Controls.Add(this.pbAppDK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmDangKy";
            this.Text = "Đăng ký";
            ((System.ComponentModel.ISupportInitialize)(this.pbAppDK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMK;
        private System.Windows.Forms.Label lblTK;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.TextBox txtMK;
        private System.Windows.Forms.TextBox txtTK;
        private System.Windows.Forms.PictureBox pbAppDK;
        private System.Windows.Forms.CheckBox chbMK;
    }
}