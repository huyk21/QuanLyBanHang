using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyBanHang.Utils;

namespace QuanLyBanHang.BUS
{
    public class LoaiSanPhamBUS : KetNoi
    {
        public LoaiSanPhamBUS()
            : base() 
        {
            
        }

        public string TaoMaMoi()
        {
            var list = LayDS();
            int maxMa = 0;

            foreach (var obj in list)
            {
                if (int.TryParse(obj.MaLoai.Substring(2), out int _out))
                {
                    if (_out > maxMa)
                        maxMa = _out;
                }
            }

            return "DM" + (maxMa + 1).ToString("000");
        }

        public List<LoaiSanPham> LayDS()
        {
            List<LoaiSanPham> ds = new List<LoaiSanPham>();
            string sql = "SELECT * FROM LoaiSanPham";
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        public Tuple<List<LoaiSanPham>, int> LayDSPaging(int currentPage = 1, int rowsPerPage = 10)
        {
            List<LoaiSanPham> ds = LayDS();
            var result = ds
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();
            return new Tuple<List<LoaiSanPham>, int>(result, ds.Count);
        }

        private LoaiSanPham CreateObj(DataRow r)
        {
            LoaiSanPham obj = new LoaiSanPham
            {
                MaLoai = r.Field<string>("MaLoai"),
                TenLoai = r.Field<string>("TenLoai")
            };

            return obj;
        }

        public LoaiSanPham LayLoaiSanPham(string maLoai)
        {
            string sql = "SELECT * FROM LoaiSanPham WHERE MALOAI = N'{0}'"
                .Format(maLoai);
            DataTable tb = DocDuLieu(sql);
            if (tb.Rows.Count <= 0)
                return null;
            return CreateObj(tb.Rows[0]);
        }

        public bool Them(LoaiSanPham obj)
        {
            string sql = "INSERT INTO LoaiSanPham VALUES('N{0}', N'{1}')"
                .Format(obj.MaLoai,
                obj.TenLoai);
            return ThucThiLenh(sql);
        }

        public bool Xoa(string manv)
        {
            string sql = "DELETE FROM LoaiSanPham WHERE MALOAI = N'{0}'"
                .Format(manv);
            return ThucThiLenh(sql);
        }

        public bool Sua(string maLoai, LoaiSanPham obj)
        {
            string sql = "UPDATE LoaiSanPham SET TENLOAI = N'{1}'  WHERE MALOAI = N'{0}'".Format(maLoai, obj.TenLoai);
            return ThucThiLenh(sql);
        }
    }
}
