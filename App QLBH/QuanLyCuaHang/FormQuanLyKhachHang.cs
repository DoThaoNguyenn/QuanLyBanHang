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
    public partial class FormQuanLyKhachHang : Form
    {
        private string sTrangThai;
        private KetNoi _ketNoi = new KetNoi();

        public FormQuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void FormQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            sTrangThai = "THEM";
            HienThiLuoiKhachHang();
        }

        private void HienThiLuoiKhachHang()
        {
            // Lấy danh sách khách hàng vào lưới
            string sQuery = "SELECT * FROM KhachHang";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("KhachHang", sQuery, parameters);
            dgKhachHang.DataSource = ds.Tables["KhachHang"];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaTrongGiaoDien();
        }

        private void XoaTrongGiaoDien()
        {
            sTrangThai = "THEM";
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            if (cboTimTheo.Items.Count > 0)
                cboTimTheo.SelectedIndex = 0;
            txtNoiDung.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sMaKH = txtMaKH.Text;
            if (string.IsNullOrEmpty(sMaKH))
            {
                MessageBox.Show("Không có khách hàng nào được chọn !");
                return;
            }

            if (!TonTaiMaKH(sMaKH))
            {
                MessageBox.Show("Không tìm thấy mã khách hàng được chọn !");
                return;
            }

            // Xoá khách hàng có mã sMaKH
            string sQuery = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@MaKH", sMaKH);
            
            int iKetQua = _ketNoi.ThucThiTruyVan(sQuery, parameters);
            
            if (iKetQua > 0)
            {
                MessageBox.Show("Xoá khách hàng thành công !");
                HienThiLuoiKhachHang();
            }
            else
            {
                MessageBox.Show("Xoá khách hàng thất bại !");
            }

        }

        private bool TonTaiMaKH(string sMaKH)
        {
            string sQuery = "SELECT * FROM KhachHang WHERE MaKH = @MaKH";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@MaKH", sMaKH);
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("KhachHang", sQuery, parameters);
            if (ds.Tables["KhachHang"].Rows.Count > 0)
                return true;
            return false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            sTrangThai = "SUA";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Mã khách hàng không được để trống !");
                return;
            }

            if (!int.TryParse(txtMaKH.Text, out int iMaKH))
            {
                MessageBox.Show("Mã khách hàng phải là số nguyên !");
                return;
            }
            
            if (string.IsNullOrEmpty(txtTenKH.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống !");
                return;
            }

            string sQuery = "";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            
            if (sTrangThai == "THEM")
            {
                if (TonTaiMaKH(txtMaKH.Text))
                {
                    MessageBox.Show("Mã khách hàng đã tồn tại ! Vui lòng chọn mã khác !");
                    return;
                }

                sQuery = "INSERT INTO KhachHang VALUES (@MaKH, @TenKH, @DiaChi, @SDT)";
            }

            if (sTrangThai == "SUA")
            {
                if (!TonTaiMaKH(txtMaKH.Text))
                {
                    MessageBox.Show("Mã khách hàng không tồn tại ! Vui lòng kiểm tra lại giá trị !");
                    return;
                }

                sQuery = "UPDATE KhachHang SET TenKH = @TenKH, DiaChi = @DiaChi, SDT = @SDT WHERE MaKH = @MaKH";
            }
            
            //int iMaKH = Convert.ToInt32(txtMaKH.Text);
            string sTenKH = txtTenKH.Text;
            string sSDT = txtSDT.Text;
            string sDiaChi = txtDiaChi.Text;

            parameters.Add("@MaKH", iMaKH);
            parameters.Add("@TenKH", sTenKH);
            parameters.Add("@DiaChi", sDiaChi);
            parameters.Add("@SDT", sSDT);
            
            int iKetQua = _ketNoi.ThucThiTruyVan(sQuery, parameters);
            
            if (iKetQua > 0)
            {
                MessageBox.Show("Cập nhật thông tin khách hàng thành công !");
                HienThiLuoiKhachHang();
                XoaTrongGiaoDien();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin khách hàng không thành công !");
            } 
                
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            XoaTrongGiaoDien();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboTimTheo.SelectedIndex == 0)
            {
                MessageBox.Show("Hãy chọn tiêu chí tìm kiếm !");
                return;
            }

            if (string.IsNullOrEmpty(txtNoiDung.Text))
            {
                MessageBox.Show("Hãy nhập vào nội dung tìm kiếm !");
                return;
            }

            string sQuery = "";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            // Mã
            if (cboTimTheo.SelectedIndex == 1)
            {
                int iMaKH = Convert.ToInt32(txtNoiDung.Text);
                
                sQuery = "SELECT * FROM KhachHang WHERE MaKH LIKE @MaKH";
                
                parameters = new Dictionary<string, object>()
                {
                    {"@MaKH", iMaKH}
                };
            }

            // Tên
            if (cboTimTheo.SelectedIndex == 2)
            {
                string sTenKH = txtNoiDung.Text;
                sQuery = "SELECT * FROM KhachHang WHERE TenKH LIKE @TenKH";

                parameters = new Dictionary<string, object>()
                {
                    {"@TenKH", sTenKH}
                };
            }

            // SĐT
            if (cboTimTheo.SelectedIndex == 3)
            {
                string sSDT = txtNoiDung.Text;
                sQuery = "SELECT * FROM KhachHang WHERE SDT LIKE @SDT";

                parameters = new Dictionary<string, object>()
                {
                    {"@SDT", sSDT}
                };
            }

            // Địa chỉ
            if (cboTimTheo.SelectedIndex == 4)
            {
                string sDiaChi = txtNoiDung.Text;
                sQuery = "SELECT * FROM KhachHang WHERE DiaChi LIKE %@MaKH%";

                parameters = new Dictionary<string, object>()
                {
                    {"@DiaChi", sDiaChi }
                };
            }

            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("KhachHang", sQuery, parameters);
            dgKhachHang.DataSource = ds.Tables["KhachHang"];
        }

        private void dgKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgKhachHang.CurrentRow == null || dgKhachHang.CurrentRow.IsNewRow)
                return;

            if (e.RowIndex < 0)
                return;
            
            DataGridViewRow r = dgKhachHang.Rows[e.RowIndex];

            txtMaKH.Text = r.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = r.Cells["TenKH"].Value.ToString();
            txtDiaChi.Text = r.Cells["DiaChi"].Value.ToString();
            txtSDT.Text = r.Cells["SDT"].Value.ToString();
            
        }
    }
}
