using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHang
{
    public partial class FormQuanLyHoaDon : Form
    {
        private string sTrangThai;
        
        private KetNoi _ketNoi = new KetNoi();

        public FormQuanLyHoaDon()
        {
            InitializeComponent();
        }

        private void FormQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            sTrangThai = "THEM";
            HienThiLuoiHoaDon();
        }

        private void HienThiLuoiHoaDon()
        {
            // Lấy danh sách hóa đơn vào lưới
            string sQuery = "SELECT * FROM HoaDon";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("HoaDon", sQuery, parameters);
            dgHoaDon.DataSource = ds.Tables["HoaDon"];
        }

        private void grpTaiKhoan_Enter(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LamMoiGiaoDien();
        }

        private void LamMoiGiaoDien()
        {
            sTrangThai = "THEM";
            txtMaHD.Text = "";
            txtMaNV.Text = "";
            dtNgayBan.Value = DateTime.Now;
            txtMaKH.Text = "";
            txtTongTien.Text = "";
            txtGiamGia.Text = "";
            txtTongTienTra.Text = "";
        }

        private void dgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgHoaDon.CurrentRow == null || dgHoaDon.CurrentRow.IsNewRow)
                return;

            if (e.RowIndex < 0)
            {
                return;
            }

            txtMaHD.Text = dgHoaDon.CurrentRow.Cells["MaHD"].Value.ToString();
            txtMaNV.Text = dgHoaDon.CurrentRow.Cells["MaNV"].Value.ToString();
            dtNgayBan.Value = DateTime.Parse(dgHoaDon.CurrentRow.Cells["NgayBan"].Value.ToString());
            txtMaKH.Text = dgHoaDon.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTongTien.Text = dgHoaDon.CurrentRow.Cells["TongTien"].Value.ToString();
            sTrangThai = "SUA";
        }

        private void dtNgayBan_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
