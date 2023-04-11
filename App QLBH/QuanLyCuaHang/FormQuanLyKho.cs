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
    public partial class FormQuanLyKho : Form
    {
        private string sTrangThai;
        private KetNoi _ketNoi = new KetNoi();

        public FormQuanLyKho()
        {
            InitializeComponent();
        }

        private bool formDangLoad = false;
        private void FormQuanLyKho_Load(object sender, EventArgs e)
        {
            formDangLoad = true;
            sTrangThai = "THEM";
            HienThiComboNCC();
            HienThiLuoiSanPham();
            HienThiLuoiPhieuNhapKho();
            formDangLoad = false;
        }

        private void HienThiLuoiPhieuNhapKho()
        {
            // Lấy danh sách sản phẩm vào lưới
            string sQuery = "SELECT MaPhieu, TenSP, SanPham.MaSP, NgayNhap, DonGiaNhap, SoLuongNhap, (SoLuongNhap * DonGiaNhap) AS ThanhTien, MaNCC FROM PhieuNhapKho, SanPham WHERE PhieuNhapKho.MaSP = SanPham.MaSP";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("PhieuNhapKho", sQuery, parameters);
            dgPhieuNhap.DataSource = ds.Tables["PhieuNhapKho"];
        }

        private void HienThiLuoiSanPham()
        {
            // Lấy danh sách sản phẩm vào lưới
            string sQuery = "SELECT SanPham.MaSP, TenSP, (ISNULL(SoLuong, 0) + ISNULL(NHAP.SLNhap, 0)) - ISNULL(XUAT.SLXuat, 0)  AS TongSL FROM SanPham \r\n\tLEFT JOIN ( SELECT MASP, SUM(ISNULL(SoLuongNhap, 0)) AS SLNhap FROM PhieuNhapKho GROUP BY MASP) NHAP\r\n\tON SanPham.MaSP = NHAP.MaSP\r\n\tLEFT JOIN ( SELECT MASP, SUM(ISNULL(SoLuong, 0)) AS SLXuat FROM ChiTietHoaDon GROUP BY MaSP) XUAT\r\n\tON SanPham.MaSP = XUAT.MaSP";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("SanPham", sQuery, parameters);
            dgSanPham.DataSource = ds.Tables["SanPham"];
        }

        private void HienThiComboNCC()
        {
            string sQuery = "SELECT * FROM NhaCungCap";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("NhaCungCap", sQuery, parameters);
            cboNCC.DataSource = ds.Tables["NhaCungCap"];
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "MaNCC";
            
        }
        
        private void dgSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgSanPham.CurrentRow == null || dgSanPham.CurrentRow.IsNewRow)
                return;
            
            if (e.RowIndex < 0)
                return;
            
            maPhieuDuocChon = 0;
            
            DataGridViewRow row = this.dgSanPham.Rows[e.RowIndex];
            txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = row.Cells["TenSP"].Value.ToString();
            numericSLTrongKho.Value = Convert.ToInt32(row.Cells["TongSL"].Value);
            dtNgayNhap.Value = DateTime.Now;
            
        }

        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formDangLoad && cboNCC.SelectedValue != null)
            {
                txtMaNCC.Text = cboNCC.SelectedValue.ToString();
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
        
        private int LayMaPhieuNhapMoi()
        {
            string sQuery = "SELECT MAX(MaPhieu) AS MaPhieu FROM PhieuNhapKho";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("PhieuNhapKho", sQuery, parameters);

            var ketQua = ds.Tables["PhieuNhapKho"].Rows.Count > 0;
            
            if (!ketQua)
                return 101;
            int maPhieu = Convert.ToInt32(ds.Tables["PhieuNhapKho"].Rows[0]["MaPhieu"]);
            return maPhieu + 1;
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (numericSLNhap.Value <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0");
                return;
            }

            if (!double.TryParse(txtDGNhap.Text, out double dgNhap))
            {
                MessageBox.Show("Đơn giá nhập phải là kiểu số !");
                return;
            }

            string sMaSP = txtMaSP.Text;

            if (!MaSPDaTonTai(sMaSP))
            {
                MessageBox.Show("Mã sản phẩm không tồn tại !");
                return;
            }

            string sMaNCC = txtMaNCC.Text;
            int iMaPhieu = LayMaPhieuNhapMoi();
            DateTime ngayNhap = dtNgayNhap.Value;
            int iSoLuongNhap = Convert.ToInt32(numericSLNhap.Value);

            string sQuery = "INSERT INTO PhieuNhapKho (MaPhieu, MaSP, MaNCC, NgayNhap, SoLuongNhap, DonGiaNhap) VALUES (@MaPhieu, @MaSP, @MaNCC, @NgayNhap, @SoLuongNhap, @DonGiaNhap)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@MaPhieu", iMaPhieu);
            parameters.Add("@MaSP", sMaSP);
            parameters.Add("@MaNCC", sMaNCC);
            parameters.Add("@NgayNhap", ngayNhap);
            parameters.Add("@SoLuongNhap", iSoLuongNhap);
            parameters.Add("@DonGiaNhap", dgNhap);
            
            int iSoDongAnhHuong = _ketNoi.ThucThiTruyVan(sQuery, parameters);

            if (iSoDongAnhHuong > 0)
            {
                MessageBox.Show("Nhập kho thành công !");
                HienThiLuoiPhieuNhapKho();
                HienThiLuoiSanPham();
            } 
            else
            {
                MessageBox.Show("Nhập kho thất bại !");
            }
        }

        private int maPhieuDuocChon = 0;
        
        private void dgPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPhieuNhap.CurrentRow == null || dgPhieuNhap.CurrentRow.IsNewRow)
                return;
            
            if (e.RowIndex  < 0)
                return;
            
            DataGridViewRow r = dgPhieuNhap.Rows[e.RowIndex];
            maPhieuDuocChon = Convert.ToInt32(r.Cells["MaPhieu"].Value);
            txtMaNCC.Text = r.Cells["MaNCC"].Value.ToString();
            cboNCC.SelectedValue = txtMaNCC.Text;
            cboNCC.Refresh();
            txtMaSP.Text = r.Cells["PNMaSP"].Value.ToString();
            txtTenSP.Text = r.Cells["PNTenSP"].Value.ToString();
            numericSLNhap.Value = Convert.ToInt32(r.Cells["SoLuongNhap"].Value);
            txtDGNhap.Text = r.Cells["DonGiaNhap"].Value.ToString();
            dtNgayNhap.Value = Convert.ToDateTime(r.Cells["NgayNhap"].Value);
            
            for (int i = 0; i < dgSanPham.Rows.Count; i++)
            {
                if (dgSanPham.Rows[i] == null || dgSanPham.Rows[i].IsNewRow)
                    continue;
                
                if (dgSanPham.Rows[i].Cells["MaSP"].Value.ToString() == r.Cells["PNMaSP"].Value.ToString())
                {
                    numericSLTrongKho.Value = Convert.ToInt32(dgSanPham.Rows[i].Cells["TongSL"].Value);
                }    
            }
        }

        private void btnSuaPhieuNhapKho_Click(object sender, EventArgs e)
        {
            if (maPhieuDuocChon <= 0)
            {
                MessageBox.Show("Không có phiếu nào được chọn !");
                return;
            }

            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numericSLNhap.Value <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0");
                return;
            }

            if (!double.TryParse(txtDGNhap.Text, out double dgNhap))
            {
                MessageBox.Show("Đơn giá nhập phải là kiểu số !");
                return;
            }

            string sMaSP = txtMaSP.Text;

            if (!MaSPDaTonTai(sMaSP))
            {
                MessageBox.Show("Mã sản phẩm không tồn tại !");
                return;
            }

            string sMaNCC = txtMaNCC.Text;
            DateTime ngayNhap = dtNgayNhap.Value;
            int iSoLuongNhap = Convert.ToInt32(numericSLNhap.Value);

            string sQuery = "UPDATE PhieuNhapKho SET MaSP = @MASP, MaNCC = @MaNCC, NgayNhap = @NgayNhap, SoLuongNhap = @SoLuongNhap, DonGiaNhap = @DonGiaNhap WHERE MaPhieu = @MaPhieu";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@MaPhieu", maPhieuDuocChon);
            parameters.Add("@MaSP", sMaSP);
            parameters.Add("@MaNCC", sMaNCC);
            parameters.Add("@NgayNhap", ngayNhap);
            parameters.Add("@SoLuongNhap", iSoLuongNhap);
            parameters.Add("@DonGiaNhap", dgNhap);

            int iSoDongAnhHuong = _ketNoi.ThucThiTruyVan(sQuery, parameters);

            if (iSoDongAnhHuong > 0)
            {
                MessageBox.Show("Cập nhật phiếu nhập kho thành công !");
                HienThiLuoiPhieuNhapKho();
                HienThiLuoiSanPham();
            }
            else
            {
                MessageBox.Show("Cập nhật phiếu nhập kho thất bại !");
            }
        }
    }
}
