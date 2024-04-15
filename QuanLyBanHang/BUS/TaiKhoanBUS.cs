using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;
using QuanLyBanHang.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.BUS
{
    public class TaiKhoanBUS : KetNoi
    {
        public TaiKhoanBUS()
            : base()
        {

        }

        public string TaoMaMoi()
        {
            var list = LayDS();
            int maxMa = 0;

            foreach (var obj in list)
            {
                if (int.TryParse(obj.MaTk.Substring(2), out int _out))
                {
                    if (_out > maxMa)
                        maxMa = _out;
                }
            }

            return "TK" + (maxMa + 1).ToString("000");
        }

        private NhanVienBUS nhanVien = new NhanVienBUS();

        public List<TaiKhoan> LayDS()
        {
            List<TaiKhoan> ds = new List<TaiKhoan>();
            string sql = "SELECT * FROM TaiKhoan";
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        public Tuple<List<TaiKhoan>, int> LayDSPaging(int currentPage = 1, int rowsPerPage = 10)
        {
            List<TaiKhoan> ds = LayDS();

            var result = ds
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();
            return new Tuple<List<TaiKhoan>, int>(result, ds.Count);
        }

        private TaiKhoan CreateObj(DataRow r)
        {
            TaiKhoan obj = new TaiKhoan
            {
                MaTk = r.Field<string>("MaTK"),
                MatKhau = r.Field<string>("Matkhau"),
                NgayLap = r.Field<DateTime>("NgayLap"),
                ThoiGianDangNhap = r.Field<DateTime?>("ThoiGianDangNhap"),
                ThoiGianDangXuat = r.Field<DateTime?>("ThoiGianDangXuat"),  
                MaQuyen = r.Field<string>("MaQuyen"),
                MaNv = r.Field<string>("MaNV")
            };

            return obj;
        }

        public TaiKhoan LayThongTinTK(string matk)
        {
            string sql = "SELECT * FROM TaiKhoan WHERE MATK = N'{0}'"
                .Format(matk);
            DataTable tb = DocDuLieu(sql);
            if (tb.Rows.Count > 0)
                return CreateObj(tb.Rows[0]);
            return null;
        }

        public NhanVien DangNhap(string matk, string matkhau)
        {
            string sql = "SELECT MaNV FROM TaiKhoan WHERE MATK = '{0}' AND MATKHAU = '{1}'".Format(matk, matkhau);
            DataTable tb = DocDuLieu(sql);
            if (tb.Rows.Count <= 0)
                return null;
            return nhanVien.LayNhanVien(tb.Rows[0].Field<string>("MaNV"));
        }


        public bool Them(TaiKhoan obj)
        {
            string sql = "INSERT INTO TAIKHOAN VALUES('{0}', '{1}', {2}, {3}, {4}, '{5}', '{6}')"
                .Format(obj.MaTk,
                obj.MatKhau, obj.NgayLap.ToISOString(), null, null, obj.MaQuyen, obj.MaNv);

            return ThucThiLenh(sql);
        }


        public bool Xoa(string matk)
        {
            string sql = "DELETE FROM TAIKHOAN WHERE MATK = '{0}'".Format(matk);
            return ThucThiLenh(sql);
        }

        public bool SuaQuyen(string matk, string maquyen)
        {
            string sql = "UPDATE TAIKHOAN SET MAQUYEN = '{1}' WHERE MATK = '{0}'"
                .Format(matk, maquyen);
            return ThucThiLenh(sql);
        }

        public bool SuaMatKhau(string matk, string matkhau)
        {
            string sql = "UPDATE TAIKHOAN SET MATKHAU = '{1}' WHERE MATK = '{0}'"
                .Format(matk, matkhau);
            return ThucThiLenh(sql);
        }
    }
}
