using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHang
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        
        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            FormQuanLyTaiKhoan frm = new FormQuanLyTaiKhoan();
            frm.ShowDialog();
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            FormQuanLyKho frm = new FormQuanLyKho();
            frm.ShowDialog();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            FormQuanLySanPham frm = new FormQuanLySanPham();
            frm.ShowDialog();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FormQuanLyKhachHang frm = new FormQuanLyKhachHang();
            frm.ShowDialog();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            FormQuanLyHoaDon frm = new FormQuanLyHoaDon();
            frm.ShowDialog();
        }
    }
}
