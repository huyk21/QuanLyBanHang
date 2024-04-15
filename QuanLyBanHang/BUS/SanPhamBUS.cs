using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;
using QuanLyBanHang.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyBanHang.BUS
{
    public class SanPhamBUS : KetNoi
    {
        public SanPhamBUS()
            : base()
        {

        }

        public string TaoMaMoi()
        {
            var list = LayDS();
            int maxMa = 0;

            foreach (var obj in list)
            {
                if (int.TryParse(obj.MaSp.Substring(2), out int _out))
                {
                    if (_out > maxMa)
                        maxMa = _out;
                }
            }

            return "SP" + (maxMa + 1).ToString("000");
        }

        public List<SanPham> TimTheoTen(string w = "")
        {
            List<SanPham> ds = new List<SanPham>();

            string sql = "select masp, tensp, hinhanh, mota, dvt, DONGIA, SOLUONG, sanpham.MaLoai, TenLoai  from sanpham INNER JOIN LoaiSanPham ON sanpham.MaLoai = LoaiSanPham.MaLoai";

            if (!string.IsNullOrWhiteSpace(w))
            {
                sql += " WHERE sanpham.tensp LIKE N'%{0}%'".Format(w);
            }
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }


        public Tuple<List<SanPham>, int> TimTheoKhoangGiaPaging(decimal tuGia, decimal denGia, int currentPage = 1, int rowsPerPage = 10)
        {
            List<SanPham> origin = LayDS();

            origin = origin.Where(x => x.DonGia >= tuGia && x.DonGia <= denGia).ToList();

            // Phân trang
            var result = origin
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();

            return new Tuple<List<SanPham>, int>(result, origin.Count);
        }

        public List<SanPham> LayDS()
        {
            List<SanPham> origin = new List<SanPham>();

            string sql = "select masp, tensp, hinhanh, mota, dvt, DONGIA, SOLUONG, sanpham.MaLoai, TenLoai from sanpham INNER JOIN LoaiSanPham ON sanpham.MaLoai = LoaiSanPham.MaLoai";

            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                origin.Add(CreateObj(r));
            }

            return origin;
        }

        public Tuple<List<SanPham>, int> LayDSPaging(int currentPage = 1, int rowsPerPage = 10)
        {
            List<SanPham> origin = LayDS();

            // Phân trang
            var result =  origin
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();

            return new Tuple<List<SanPham>, int>(result, origin.Count);
        }

        private SanPham CreateObj(DataRow r)
        {
            SanPham obj = new SanPham
            {
                MaSp = r.Field<string>("MaSP"),
                TenSp = r.Field<string>("TenSP"),
                MoTa = r.Field<string>("MoTa"),
                DVT = r.Field<string>("DVT"),
                DonGia = r.Field<decimal?>("DONGIA"),
                SoLuong = r.Field<int?>("SOLUONG"),
                MaLoai = r.Field<string>("MaLoai"),
                TenLoai = r.Field<string>("TenLoai"),
                HinhAnh = r.Field<string>("HinhAnh")
                
            };

            return obj;
        }

        public SanPham LaySanPham(string maSp)
        {
            string sql = "select masp, tensp, hinhanh, mota, dvt, DONGIA, SOLUONG, sanpham.MaLoai, TenLoai from sanpham INNER JOIN LoaiSanPham ON sanpham.MaLoai = LoaiSanPham.MaLoai where masp = N'{0}'".Format(maSp);
            DataTable data = DocDuLieu(sql);
            
            if (data.Rows.Count > 0)
            {
                return CreateObj(data.Rows[0]);
            }
            return null;
        }

        public bool Them(SanPham obj)
        {
            string sql =
                "insert into SANPHAM values(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', {5}, {6}, N'{7}')".Format(
                    obj.MaSp, obj.TenSp, obj.MoTa, obj.DVT, obj.HinhAnh,  obj.DonGia, obj.SoLuong, obj.MaLoai);
            return ThucThiLenh(sql);
        }

        public bool Xoa(string masp)
        {
            string sql = "DELETE FROM SANPHAM WHERE MASP = N'{0}'".Format(masp);
            return ThucThiLenh(sql);
        }

        public bool Sua(string masp, SanPham obj)
        {
            string sql = ("UPDATE SANPHAM SET " +
                         "TENSP = N'{1}', " +
                         "MOTA = N'{2}', " +
                         "DVT = N'{3}', " +
                         "HINHANH = N'{4}', " +
                         "DONGIA = {5}, " +
                         "SOLUONG = {6}, " +
                         "MALOAI = N'{7}' " +
                         "WHERE MASP = '{0}'").Format(masp, obj.TenSp, obj.MoTa, obj.DVT, obj.HinhAnh, obj.DonGia, obj.SoLuong, obj.MaLoai);
            return ThucThiLenh(sql);
        }

        public int TongSoSanPham()
        {
            string sql = "SELECT COUNT(*) FROM SANPHAM";
            return (int)ThucThiLenhLayKetQua(sql);
        }

        public List<SanPham> CacSanPhamSapHetHang()
        {
            List<SanPham> ds = new List<SanPham>();

            string sql = "SELECT TOP 5 masp, tensp, hinhanh, mota, dvt, DONGIA, SOLUONG, sanpham.MaLoai, TenLoai from sanpham INNER JOIN LoaiSanPham ON sanpham.MaLoai = LoaiSanPham.MaLoai WHERE SOLUONG <= 5";

            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        public Tuple<List<SanPham>, int> TimTheoTen(string tenSp, int currentPage, int rowsPerPage)
        {
            List<SanPham> origin = LayDS();
            
            origin = origin.Where(x => x.TenSp.ToLower().Contains(tenSp.ToLower())).ToList();

            // Phân trang
            var result = origin
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();

            return new Tuple<List<SanPham>, int>(result, origin.Count);
        }

        public Tuple<List<SanPham>, int> TimTheoLoai(string maLoai, int currentPage, int rowsPerPage)
        {
            List<SanPham> origin = LayDS();
            origin = origin.Where(x => x.MaLoai == maLoai).ToList();
            // Phân trang
            var result = origin
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();

            return new Tuple<List<SanPham>, int>(result, origin.Count);
        }
    }
}
