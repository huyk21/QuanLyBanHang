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
    public class CTHoaDonBUS : KetNoi
    {
        public CTHoaDonBUS()
            : base()
        {

        }

        public List<CTHoaDon> LayDS(string maHD)
        {
            List<CTHoaDon> ds = new List<CTHoaDon>();
            string sql = "SELECT CTHOADON.*, SANPHAM.TENSP " +
                         "FROM CTHOADON INNER JOIN SANPHAM " +
                         "ON CTHOADON.MASP = SANPHAM.MASP";

            sql += " WHERE CTHOADON.MAHD = '" + maHD + "'";

            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        private CTHoaDon CreateObj(DataRow r)
        {
            CTHoaDon obj = new CTHoaDon
            {
                MaHd = r.Field<string>("MaHD"),
                MaSp = r.Field<string>("MaSP"),
                TenSp = r.Field<string>("TenSP"),
                DonGia = r.Field<decimal>("DonGia"),
                Sl = r.Field<decimal>("SL"),
                TienThue = r.Field<decimal>("TienThue")
            };

            return obj;
        }

        public bool Them(string mahd, CTHoaDon ct)
        {
            string sql = string.Format(@"INSERT INTO CTHoaDon
                               (MaHD
                               ,MaSP
                               ,DonGia
                               ,SL
                               ,TienThue)      
                         VALUES
                               ('{0}'
                               ,'{1}'
                               ,{2}
                               ,{3}, {4})", mahd, ct.MaSp, ct.DonGia, ct.Sl, ct.TienThue);
            return ThucThiLenh(sql);
        }

        public bool Xoa(string mahd)
        {
            string sql = "DELETE FROM CTHOADON WHERE MAHD = '{0}'".Format(mahd);
            return ThucThiLenh(sql);
        }

        public List<CTHoaDon> LayDS()
        {
            List<CTHoaDon> ds = new List<CTHoaDon>();
            string sql = "SELECT CTHOADON.*, SANPHAM.TENSP " +
                         "FROM CTHOADON INNER JOIN SANPHAM " +
                         "ON CTHOADON.MASP = SANPHAM.MASP";
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }
    }
}
