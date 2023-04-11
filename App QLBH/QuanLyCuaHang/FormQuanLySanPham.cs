using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHang
{
    public partial class FormQuanLySanPham : Form
    {
        private string sTrangThai;
        private KetNoi _ketNoi = new KetNoi();

        public FormQuanLySanPham()
        {
            InitializeComponent();
        }

        private void FormQuanLySanPham_Load(object sender, EventArgs e)
        {
            LamMoiGiaoDien();
            HienThiLuoiSanPham();
        }

        private void HienThiLuoiSanPham()
        {
            // Lấy danh sách sản phẩm vào lưới
            string sQuery = "SELECT * FROM SanPham";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("SanPham", sQuery, parameters);
            dgSanPham.DataSource = ds.Tables["SanPham"];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LamMoiGiaoDien();
        }

        private void LamMoiGiaoDien()
        {
            sTrangThai = "THEM";
            cboTimTheo.SelectedIndex = 0;
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            numericSL.Value = 0;
            txtDGBan.Text = "";
            if (cboTimTheo.Items.Count > 0)
                cboTimTheo.SelectedIndex = 0;
            txtNoiDung.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            sTrangThai = "SUA";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sMaSP = txtMaSP.Text;
            
            if (string.IsNullOrEmpty(sMaSP))
            {
                MessageBox.Show("Không có sản phẩm nào đang được chọn !");
                return;
            }

            // Kiểm tra sản phẩm tồn tại
            if (!MaSPDaTonTai(sMaSP))
            {
                MessageBox.Show("Sản phẩm không tồn tại !");
                return;
            }

            // Xóa sản phẩm
            bool traLoi = MessageBox.Show("Bạn có muốn xoá hàng hoá này không ?", "Xoá", MessageBoxButtons.YesNo) == DialogResult.Yes;
            
            if (traLoi)
            {
                string sQuery = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@MaSP", sMaSP);
                
                int iSoDongBiAnhHuong = _ketNoi.ThucThiTruyVan(sQuery, parameters);

                if (iSoDongBiAnhHuong > 0)
                {
                    MessageBox.Show("Xoá thành công !");
                    HienThiLuoiSanPham();
                }
                else
                {
                    MessageBox.Show("Xoá thất bại !");
                }
            }
        }

        private bool MaSPDaTonTai(string sMaSP)
        {
            string sQuery = "SELECT * FROM SanPham WHERE MaSP = @MaSP";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@MaSP", sMaSP);
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("SanPham", sQuery, parameters);

            var ketQua = ds.Tables["SanPham"].Rows.Count > 0;
            return ketQua;
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Lưu thông tin sản phẩm
            string sMaSP = txtMaSP.Text;
            string sTenSP = txtTenSP.Text;
            int iSoLuong = (int)numericSL.Value;
            string sDGBan = txtDGBan.Text;

            if (string.IsNullOrEmpty(sMaSP) 
                || string.IsNullOrEmpty(sTenSP) 
                || string.IsNullOrEmpty(sDGBan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mã sản phẩm, tên sản phẩm và giá bán !");
                return;
            }
            
            if (!double.TryParse(sDGBan, out double dgBan))
            {
                MessageBox.Show("Đơn giá bán phải là kiểu số !");
                return;
            }

            string sQuery = "";
            Dictionary<string, object> parameters;
            
            if (sTrangThai == "THEM")
            {
                
                if (MaSPDaTonTai(sMaSP))
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại ! Không thể thêm mới sản phẩm !");
                    return;
                }
                
                sQuery = "INSERT INTO SanPham (MaSP, TenSP, DonGiaBan, SoLuong) VALUES(@MaSP, @TenSP, @DonGiaBan, @SoLuong)";
            }
            else if (sTrangThai == "SUA")
            {
                
                if (!MaSPDaTonTai(sMaSP))
                {
                    MessageBox.Show("Mã sản phẩm này không tồn tại ! Không thể sửa thông tin sản phẩm !");
                    return;
                }

                sQuery = "UPDATE SanPham SET TenSP = @TenSP, DonGiaBan = @DonGiaBan, SoLuong = @SoLuong";
            }

            parameters = new Dictionary<string, object>();
            parameters.Add("@MaSP", sMaSP);
            parameters.Add("@TenSP", sTenSP);
            parameters.Add("@DonGiaBan", dgBan);
            parameters.Add("@SoLuong", iSoLuong);
            
            int iSoLuongDongAnhHuong = _ketNoi.ThucThiTruyVan(sQuery, parameters);
            
            if (iSoLuongDongAnhHuong > 0)
            {
                MessageBox.Show("Cập nhật thông tin sản phẩm thành công !");   
                HienThiLuoiSanPham();
            } 
            else
            {
                MessageBox.Show("Không có sản phẩm nào được cập nhật !");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LamMoiGiaoDien();
        }

        private void dgSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgSanPham.CurrentRow == null || dgSanPham.CurrentRow.IsNewRow)
                return;
            DataGridViewRow row = dgSanPham.Rows[e.RowIndex];
            txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = row.Cells["TenSP"].Value.ToString();
            numericSL.Value = (int)row.Cells["SoLuong"].Value;
            txtDGBan.Text = row.Cells["DonGiaBan"].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboTimTheo.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm !");
                return;
            }

            if (string.IsNullOrEmpty(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung tìm kiếm !");
                return;
            }
            
            string sNoiDung = txtNoiDung.Text;
            
            string sQuery = "";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            
            // Tìm theo mã
            if (cboTimTheo.SelectedIndex == 1)
            {
                sQuery = "SELECT * FROM SanPham WHERE MaSP LIKE @MaSP";
                
                parameters = new Dictionary<string, object>()
                {
                    { "@MaSP", sNoiDung }
                };
               
            }

            // Tìm theo tên
            if (cboTimTheo.SelectedIndex == 2)
            {
                sQuery = "SELECT * FROM SanPham WHERE TenSP LIKE @TenSP";
                parameters = new Dictionary<string, object>()
                {
                    {"@TenSP", sNoiDung }
                };
            }

            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("SanPham", sQuery, parameters);
            dgSanPham.DataSource = ds.Tables["SanPham"];
            dgSanPham.Refresh();
        }

        private void txtTenSP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
