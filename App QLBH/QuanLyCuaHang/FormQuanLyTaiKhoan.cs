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
    public partial class FormQuanLyTaiKhoan : Form
    {
        public FormQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private KetNoi _ketNoi = new KetNoi();
        
        private void FormQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            sTrangThai = "THEM";
            
            HienThiLuoiTaiKhoan();
        }

        private void HienThiLuoiTaiKhoan()
        {
            // Lấy danh sách tài khoản vào lưới
            string sQuery = "SELECT * FROM TaiKhoan";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("TaiKhoan", sQuery, parameters);
            dgTaiKhoan.DataSource = ds.Tables["TaiKhoan"];
        }
        
        private bool TenTaiKhoanDaTonTai(string sTenTaiKhoan)
        {
            string sQuery = "SELECT * FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan";
            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@TenTaiKhoan", sTenTaiKhoan }
            };

            DataSet ds = _ketNoi.ThucThiTruyVanLayKetQua("TaiKhoan", sQuery, parameters);
            bool ketQua = ds.Tables["TaiKhoan"].Rows.Count > 0;
            return ketQua;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            string sTenTK = txtTenTaiKhoan.Text;
            string sMatKhau = txtMatKhau.Text;
            
            string sQuery = "";
            
            if (sTrangThai == "THEM")
            {
                if (TenTaiKhoanDaTonTai(sTenTK))
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại ! Không thể thêm mới tài khoản với tên này !");
                    return;
                }
                
                sQuery = "INSERT INTO TaiKhoan VALUES(@TenTaiKhoan, @MatKhau)";
            }

            if (sTrangThai == "SUA")
            {
                if (!TenTaiKhoanDaTonTai(sTenTK))
                {
                    MessageBox.Show("Không tìm thấy tên tài khoản được chọn !");
                    return;
                }
                sQuery = "UPDATE TaiKhoan SET MatKhau = @MatKhau WHERE TenTaiKhoan = @TenTaiKhoan";
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@TenTaiKhoan", sTenTK },
                {"@MatKhau", sMatKhau }
            };
           
            try
            {
                int kq = _ketNoi.ThucThiTruyVan(sQuery, parameters);
                
                if (kq > 0)
                {
                    sTrangThai = "";
                    HienThiLuoiTaiKhoan();
                    MessageBox.Show("Cập nhật dữ liệu thành công !");
                }  
                else
                {
                    MessageBox.Show("Không có dữ liệu nào được lưu !");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xảy ra lỗi trong quá trình thêm mới!");
            }
        }

        private void dgTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgTaiKhoan.CurrentRow == null || dgTaiKhoan.CurrentRow.IsNewRow)
                return;
            
            txtTenTaiKhoan.Text = dgTaiKhoan.Rows[e.RowIndex].Cells["TenTaiKhoan"].Value.ToString();
            txtMatKhau.Text = dgTaiKhoan.Rows[e.RowIndex].Cells["MatKhau"].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Có chắc chắn xoá không?", "Thông báo", MessageBoxButtons.OKCancel);
            
            if (ret !=  DialogResult.OK)
                return;
               
            string sTenTaiKhoan = txtTenTaiKhoan.Text;
            string sQuery = "DELETE FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan";
            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@TenTaiKhoan", sTenTaiKhoan }
            };
                
            try
            {
                int kq = _ketNoi.ThucThiTruyVan(sQuery, parameters);
                    
                if (kq > 0)
                {
                    MessageBox.Show("Xoá thành công!");
                    HienThiLuoiTaiKhoan();
                } else
                {
                    MessageBox.Show("Không có dữ liệu nào được xoá !");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình xoá!");
            } 
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            sTrangThai = "THEM";   
            txtMatKhau.Text = "";
            txtTenTaiKhoan.ReadOnly = false;
            txtTenTaiKhoan.Text = "";
            txtTenTaiKhoan.Focus();
        }
        
        string sTrangThai = "";
        
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenTaiKhoan.Text))
            {
                MessageBox.Show("Không có tài khoản nào được chọn !");
                sTrangThai = "";
                return;
            }

            sTrangThai = "SUA";
            txtTenTaiKhoan.ReadOnly = true;
            txtMatKhau.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            sTrangThai = "";
            txtMatKhau.Text = "";
            txtTenTaiKhoan.ReadOnly = false;
            txtTenTaiKhoan.Text = "";
            txtTenTaiKhoan.Focus();
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
