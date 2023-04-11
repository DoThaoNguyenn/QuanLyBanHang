namespace QuanLyCuaHang
{
    partial class FormMain
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
            this.mnuQuanLy = new System.Windows.Forms.MenuStrip();
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChiNhanh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhapKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNhapKho = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnSanPham = new System.Windows.Forms.Button();
            this.btnTaiKhoan = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.mnuQuanLy.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuQuanLy
            // 
            this.mnuQuanLy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýToolStripMenuItem});
            this.mnuQuanLy.Location = new System.Drawing.Point(0, 0);
            this.mnuQuanLy.Name = "mnuQuanLy";
            this.mnuQuanLy.Size = new System.Drawing.Size(800, 24);
            this.mnuQuanLy.TabIndex = 0;
            this.mnuQuanLy.Text = "menuStrip1";
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSanPham,
            this.mnuNhanVien,
            this.mnuKhachHang,
            this.mnuTaiKhoan,
            this.mnuChiNhanh,
            this.mnuNhapKho,
            this.mnuHoaDon});
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.quảnLýToolStripMenuItem.Text = "Quản lý";
            // 
            // mnuSanPham
            // 
            this.mnuSanPham.Name = "mnuSanPham";
            this.mnuSanPham.Size = new System.Drawing.Size(180, 22);
            this.mnuSanPham.Text = "Sản phẩm";
            // 
            // mnuNhanVien
            // 
            this.mnuNhanVien.Name = "mnuNhanVien";
            this.mnuNhanVien.Size = new System.Drawing.Size(180, 22);
            this.mnuNhanVien.Text = "Nhân viên";
            // 
            // mnuKhachHang
            // 
            this.mnuKhachHang.Name = "mnuKhachHang";
            this.mnuKhachHang.Size = new System.Drawing.Size(180, 22);
            this.mnuKhachHang.Text = "Khách hàng";
            // 
            // mnuTaiKhoan
            // 
            this.mnuTaiKhoan.Name = "mnuTaiKhoan";
            this.mnuTaiKhoan.Size = new System.Drawing.Size(180, 22);
            this.mnuTaiKhoan.Text = "Tài khoản";
            // 
            // mnuChiNhanh
            // 
            this.mnuChiNhanh.Name = "mnuChiNhanh";
            this.mnuChiNhanh.Size = new System.Drawing.Size(180, 22);
            this.mnuChiNhanh.Text = "Chi nhánh";
            // 
            // mnuNhapKho
            // 
            this.mnuNhapKho.Name = "mnuNhapKho";
            this.mnuNhapKho.Size = new System.Drawing.Size(180, 22);
            this.mnuNhapKho.Text = "Nhập kho";
            // 
            // mnuHoaDon
            // 
            this.mnuHoaDon.Name = "mnuHoaDon";
            this.mnuHoaDon.Size = new System.Drawing.Size(180, 22);
            this.mnuHoaDon.Text = "Hoá đơn";
            // 
            // btnNhapKho
            // 
            this.btnNhapKho.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNhapKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnNhapKho.Location = new System.Drawing.Point(47, 80);
            this.btnNhapKho.Name = "btnNhapKho";
            this.btnNhapKho.Size = new System.Drawing.Size(185, 59);
            this.btnNhapKho.TabIndex = 1;
            this.btnNhapKho.Text = "Nhập kho";
            this.btnNhapKho.UseVisualStyleBackColor = false;
            this.btnNhapKho.Click += new System.EventHandler(this.btnNhapKho_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnKhachHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnKhachHang.Location = new System.Drawing.Point(47, 185);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Size = new System.Drawing.Size(185, 59);
            this.btnKhachHang.TabIndex = 1;
            this.btnKhachHang.Text = "Khách hàng";
            this.btnKhachHang.UseVisualStyleBackColor = false;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnSanPham
            // 
            this.btnSanPham.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSanPham.Location = new System.Drawing.Point(47, 288);
            this.btnSanPham.Name = "btnSanPham";
            this.btnSanPham.Size = new System.Drawing.Size(185, 59);
            this.btnSanPham.TabIndex = 1;
            this.btnSanPham.Text = "Sản phẩm";
            this.btnSanPham.UseVisualStyleBackColor = false;
            this.btnSanPham.Click += new System.EventHandler(this.btnSanPham_Click);
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnTaiKhoan.Location = new System.Drawing.Point(320, 185);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Size = new System.Drawing.Size(185, 59);
            this.btnTaiKhoan.TabIndex = 1;
            this.btnTaiKhoan.Text = "Tài khoản";
            this.btnTaiKhoan.UseVisualStyleBackColor = false;
            this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHoaDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnHoaDon.Location = new System.Drawing.Point(320, 80);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(185, 59);
            this.btnHoaDon.TabIndex = 1;
            this.btnHoaDon.Text = "Hoá đơn";
            this.btnHoaDon.UseVisualStyleBackColor = false;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnHoaDon);
            this.Controls.Add(this.btnTaiKhoan);
            this.Controls.Add(this.btnSanPham);
            this.Controls.Add(this.btnKhachHang);
            this.Controls.Add(this.btnNhapKho);
            this.Controls.Add(this.mnuQuanLy);
            this.MainMenuStrip = this.mnuQuanLy;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý cửa hàng";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.mnuQuanLy.ResumeLayout(false);
            this.mnuQuanLy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem quảnLýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem mnuTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem mnuChiNhanh;
        private System.Windows.Forms.ToolStripMenuItem mnuNhapKho;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDon;
        private System.Windows.Forms.Button btnNhapKho;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnSanPham;
        private System.Windows.Forms.Button btnTaiKhoan;
        private System.Windows.Forms.Button btnHoaDon;
    }
}

