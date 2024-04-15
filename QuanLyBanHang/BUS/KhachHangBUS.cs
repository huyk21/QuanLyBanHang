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
    public class KhachHangBUS : KetNoi
    {
        public KhachHangBUS()
            : base()
        {

        }

        public string TaoMaMoi()
        {
            var list = LayDS();
            int maxMa = 0;

            foreach (var obj in list)
            {
                if (int.TryParse(obj.MaKh.Substring(2), out int _out))
                {
                    if (_out > maxMa)
                        maxMa = _out;
                }
            }

            return "KH" + (maxMa + 1).ToString("000");
        }

        public List<KhachHang> LayDS()
        {
            List<KhachHang> ds = new List<KhachHang>();
            string sql = "SELECT * FROM KhachHang";
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        public Tuple<List<KhachHang>, int> LayDSPaging(int currentPage = 1, int rowsPerPage = 10)
        {
            List<KhachHang> ds = LayDS();
            var result = ds
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();
            return new Tuple<List<KhachHang>, int>(result, ds.Count);
        }

        private KhachHang CreateObj(DataRow r)
        {
            KhachHang obj = new KhachHang
            {
                MaKh = r.Field<string>("MaKH"),
                TenKh = r.Field<string>("TenKH"),
                Sdt = r.Field<string>("SDT"),
                DiaChi = r.Field<string>("DIACHI"),
                Email = r.Field<string>("EMAIL"),
                DiemTichLuy = r.Field<int?>("DIEMTICHLUY"),
                NgaySinh = r.Field<DateTime?>("NGAYSINH")
            };

            return obj;
        }

        public KhachHang LayKhachHang(string maKh)
        {
            string sql = "SELECT * FROM KHACHHANG WHERE MAKH = N'{0}'"
                .Format(maKh);
            DataTable tb = DocDuLieu(sql);
            if (tb.Rows.Count <= 0)
                return null;
            return CreateObj(tb.Rows[0]);
        }

        public bool Them(KhachHang obj)
        {
            string sql = "INSERT INTO KHACHHANG VALUES('N{0}', N'{1}', N'{2}', N'{3}', N'{4}', {5}, {6})"
                .Format(obj.MaKh,
                        obj.TenKh, obj.Sdt, obj.DiaChi, obj.Email, obj.DiemTichLuy, obj.NgaySinh.ToISOString());
            return ThucThiLenh(sql);
        }

        public bool Xoa(string makh)
        {
            string sql = "DELETE FROM KHACHHANG WHERE MAKH = N'{0}'"
                .Format(makh);
            return ThucThiLenh(sql);
        }

        public bool Sua(string makh, KhachHang obj)
        {
            string sql = "UPDATE KHACHHANG SET TENKH = N'{0}', SDT = N'{1}', DIACHI = N'{2}', EMAIL = N'{3}', NGAYSINH = {4} WHERE MAKH = N'{0}'"
                .Format(makh, obj.TenKh, obj.Sdt, obj.DiaChi, obj.Email, obj.NgaySinh.ToISOString());
            return ThucThiLenh(sql);
        }
    }
}
