using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHang
{
    public class KetNoi
    {
        private static string sCon = "Data Source=.;Initial Catalog=DATABASE_CSDL_NHOM04;Integrated Security=True;";

        public static SqlConnection _db;
        public static SqlConnection TaoDoiTuongKetNoi()
        {
            if (_db == null) // Nếu đối tượng kết nối chưa được khởi tạo
                _db = new SqlConnection(sCon);
            return _db;
        }

        public int ThucThiTruyVan(string sQuery, Dictionary<string, object > parameters)
        {
            // Mở kết nối
            SqlConnection con = TaoDoiTuongKetNoi();
            if (con.State != System.Data.ConnectionState.Open) 
                con.Open();

            SqlCommand cmd = new SqlCommand(sQuery, con);
            
            foreach(var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
            // Lấy kết quả trả về câu truy vấn
            // <= 0 Truy vấn không thành công
            // > 0 Truy vấn thành công
            int result = cmd.ExecuteNonQuery();
            // Đóng truy vấn
            con.Close();
            return result;
        }

        public DataSet ThucThiTruyVanLayKetQua(string sTenBang, string sQuery, Dictionary<string, object> parameters)
        {
            // Mở kết nối
            SqlConnection con = TaoDoiTuongKetNoi();
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();

            // Khởi tạo đối tượng command
            SqlCommand cmd = new SqlCommand(sQuery, con);

            // Gán tham số truy vấn
            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }

            // Khởi tạo đối tượng adapter để thực thi truy vấn
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            // Khởi tạo đối tượng ds để chứa kết quả truy vấn
            DataSet ds = new DataSet();
            adapter.Fill(ds, sTenBang);
            con.Close();
            // Trả về kết quả truy vấn
            return ds;
        }
    }
}
