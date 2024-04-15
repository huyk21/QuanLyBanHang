using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;
using QuanLyBanHang.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace QuanLyBanHang.BUS
{
    public class HoaDonBUS : KetNoi
    {
        public HoaDonBUS()
            : base()
        {
        }

        public string TaoMaMoi()
        {
            var list = LayDS();
            double maxMa = 0;

            foreach (var obj in list)
            {
                if (double.TryParse(obj.MaHd.Substring(2), out double _out))
                {
                    if (_out > maxMa)
                        maxMa = _out;
                }
            }

            return "HD" + (maxMa + 1).ToString("000000");
        }

        public List<HoaDon> LayDS()
        {
            List<HoaDon> ds = new List<HoaDon>();
            string sql = "SELECT HOADON.*, NHANVIEN.HoTen, KhachHang.TenKH FROM HoaDon " +
                         "INNER JOIN NHANVIEN ON HOADON.MANV = NHANVIEN.MANV INNER " +
                         "JOIN KHACHHANG ON HOADON.MAKH = KHACHHANG.MAKH";
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        public Tuple<List<HoaDon>, int> LayDSPaging(int currentPage = 1, int rowsPerPage = 10)
        {
            List<HoaDon> ds = LayDS();

            var result =  ds
                .Skip((currentPage - 1) * rowsPerPage)
                .Take(rowsPerPage).ToList();

            return new Tuple<List<HoaDon>, int> (result, ds.Count);
        }

        public List<HoaDon> Tim(DateTime tuNgay, DateTime denNgay)
        {
            List<HoaDon> ds = new List<HoaDon>();
            string sql = "SELECT HOADON.*, NHANVIEN.HoTen, KhachHang.TenKH FROM HoaDon " +
                         "INNER JOIN NHANVIEN ON HOADON.MANV = NHANVIEN.MANV INNER " +
                         "JOIN KHACHHANG ON HOADON.MAKH = KHACHHANG.MAKH WHERE NgayLap >= '" + tuNgay.ToString("yyyy/MM/dd") + "' AND NgayLap <= '" + denNgay.ToString("yyyy/MM/dd") + "'";
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        private HoaDon CreateObj(DataRow r)
        {
            HoaDon obj = new HoaDon
            {
               MaHd = r.Field<string>("MaHD"),
               NgayLap = r.Field<DateTime>("NgayLap"),
               NgayXuat = r.Field<DateTime>("NgayXuat"),
               MaKh = r.Field<string>("MaKH"),
               TenKH = r.Field<string>("Tenkh"),
               MaNv = r.Field<string>("MaNV"),
               TenNV = r.Field<string>("HoTen"),
               TongTienTruocThue = r.Field<decimal>("TongTienTruocThue"),
               TongTienSauThue = r.Field<decimal>("TongTienSauThue"),
               ChietKhau = r.Field<decimal>("ChietKhau"),
               TongTienPhaiTra = r.Field<decimal>("TongTienPhaiTra"),
            };

            obj.CthoaDon = new CTHoaDonBUS().LayDS(obj.MaHd);

            return obj;
        }

        private CTHoaDonBUS ctHD = new CTHoaDonBUS();

        public bool Them(HoaDon hd)
        {
            string sql = string.Format(@"INSERT INTO HoaDon
                               (MaHD
                               ,NgayLap
                               ,NgayXuat
                               ,MaNV
                               ,MaKH
                               , TongTienTruocThue
                                , TienThue
                                , TongTienSauThue, ChietKhau, TongTienPhaiTra
                            )
                         VALUES
                               ('{0}'
                               ,{1}
                               ,{2}
                               ,'{3}'
                               ,'{4}', {5}, {6}, {7}, {8}, {9})"
            , hd.MaHd, hd.NgayLap.ToISOString(), hd.NgayXuat.ToISOString(), hd.MaNv, hd.MaKh, hd.TongTienTruocThue, hd.TienThue, hd.TongTienSauThue, hd.ChietKhau, hd.TongTienPhaiTra);

            string sqlSubtractSLSP = "UPDATE SANPHAM SET SoLuong = SoLuong - {0} WHERE MaSP = '{1}'";

            using (var scope = new TransactionScope())
            {
                var ret = ThucThiLenh(sql);

                if (!ret)
                    return ret;

                List<CTHoaDon> dsCT = hd.CthoaDon.ToList();


                for (int i = 0; i < dsCT.Count; i++)
                {
                    ret = ctHD.Them(hd.MaHd, dsCT[i]);

                    if (!ret)
                        break;

                    // Cập nhật lại số lượng sản phẩm
                    ret = ThucThiLenh(sqlSubtractSLSP.Format(dsCT[i].Sl ?? 0, dsCT[i].MaSp));

                    if (!ret)
                        break;
                }

                if (ret)
                    scope.Complete();

                return ret;
            }
        }

        public bool Xoa(string mahd)
        {
            string sqlXoaHoaDon = "DELETE FROM HOADON WHERE MAHD = '{0}'".Format(mahd);

            using (var scope = new TransactionScope())
            {
                bool ret = false;

                // Cập nhật lại số lượng sản phẩm
                var dsCT = ctHD.LayDS(mahd);
                
                for (int i = 0; i < dsCT.Count; i++)
                {
                    string sqlAddSLSP = "UPDATE SANPHAM SET SoLuong = SoLuong + {0} WHERE MaSP = '{1}'";
                    ret = ThucThiLenh(sqlAddSLSP.Format(dsCT[i].Sl ?? 0, dsCT[i].MaSp));
                    if (!ret)
                        break;
                }

                if (ret)
                {
                    ret = ctHD.Xoa(mahd);

                    if (!ret)
                        return ret;

                    ret = ThucThiLenh(sqlXoaHoaDon);

                    if (ret)
                        scope.Complete();
                }
                return ret;
            }
        }

        public HoaDon LayHoaDon(string maHd)
        {
            
            string sql = "SELECT HOADON.*, NHANVIEN.HoTen, KhachHang.TenKH FROM HoaDon " +
                         "INNER JOIN NHANVIEN ON HOADON.MANV = NHANVIEN.MANV INNER " +
                         "JOIN KHACHHANG ON HOADON.MAKH = KHACHHANG.MAKH WHERE MaHd = N'" + maHd + "'";
            DataTable data = DocDuLieu(sql);

            if (data.Rows.Count > 0)
            {
                return CreateObj(data.Rows[0]);
            }
            return null;
        }

        public List<HoaDon> LayDSHoaDonTrongTuan()
        {
            List<HoaDon> ds = new List<HoaDon>();
            string sql = "SELECT HOADON.*, NHANVIEN.HOTEN, KHACHHANG.TENKH FROM HOADON\r\n                         INNER JOIN NHANVIEN ON HOADON.MANV = NHANVIEN.MANV INNER \r\n                         JOIN KHACHHANG ON HOADON.MAKH = KHACHHANG.MAKH\r\nWHERE NGAYLAP >= DATEADD(DAY, 1-DATEPART(dw, GETDATE()), CONVERT(DATE,GETDATE())) \r\nAND NGAYLAP <  DATEADD(DAY, 8-DATEPART(dw, GETDATE()), CONVERT(DATE,GETDATE()));";
            DataTable data = DocDuLieu(sql);
            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }
    }
}
