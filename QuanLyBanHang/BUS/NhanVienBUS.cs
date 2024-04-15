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
    public class NhanVienBUS : KetNoi
    {
        public NhanVienBUS()
            :base()
        {

        }

        public string TaoMaMoi()
        {
            var list = LayDS();
            int maxMa = 0;

            foreach (var obj in list)
            {
                if (int.TryParse(obj.MaNv.Substring(2), out int _out))
                {
                    if (_out > maxMa)
                        maxMa = _out;
                }
            }

            return "NV" + (maxMa + 1).ToString("000");
        }

        public List<NhanVien> LayDS()
        {
            List<NhanVien> ds = new List<NhanVien>();
            string sql = "SELECT * FROM NhanVien";
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        public Tuple<List<NhanVien>, int> LayDSPaging(int currentPage = 1, int rowsPerPage = 10)
        {
            List<NhanVien> ds = LayDS();
            var result = ds
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();
            return new Tuple<List<NhanVien>, int>(result, ds.Count);
        }

        private NhanVien CreateObj(DataRow r)
        {
            NhanVien obj = new NhanVien
            {
                MaNv = r.Field<string>("MaNV"),
                HoTen = r.Field<string>("HoTen"),
                SDT = r.Field<string>("SDT"),
                DiaChi = r.Field<string>("DiaChi"),
                Email = r.Field<string>("Email"),
                NgaySinh = r.Field<DateTime>("NgaySinh")
            };

            return obj;
        }

        public NhanVien LayNhanVien(string manv)
        {
            string sql = "SELECT * FROM NHANVIEN WHERE MANV = N'{0}'"
                .Format(manv);
            DataTable tb = DocDuLieu(sql);
            if (tb.Rows.Count <= 0)
                return null;
            return CreateObj(tb.Rows[0]);
        }

        public bool Them(NhanVien obj)
        {
            string sql = "INSERT INTO NHANVIEN VALUES(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', {5})"
                .Format(obj.MaNv,
                obj.HoTen, obj.SDT, obj.DiaChi, obj.Email, obj.NgaySinh.ToISOString());
            return ThucThiLenh(sql);
        }

        public bool Xoa(string manv)
        {
            string sql = "DELETE FROM NHANVIEN WHERE MANV = '{0}'"
                .Format(manv);
            return ThucThiLenh(sql);
        }

        public bool Sua(string manv, NhanVien obj)
        {
            string sql = "UPDATE NHANVIEN SET HOTEN = N'{1}', SDT = N'{2}', DIACHI = N'{3}', EMAIL = N'{4}', NGAYSINH = {5}  WHERE MANV = N'{0}'".Format(manv, obj.HoTen, obj.SDT, obj.DiaChi,
                obj.Email, obj.NgaySinh.ToISOString());
            return ThucThiLenh(sql);
        }
    }
}
