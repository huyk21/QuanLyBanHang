using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace QuanLyBanHang.BUS
{
    public class ThongKeBUS : KetNoi
    {
        public ThongKeBUS()
            : base()
        {
        }

        public int TongSanPhamDangBan()
        {
            string sql = "SELECT Count(MaSP) as TongSP FROM SanPham";
            return ThucThiLenhLayKetQua(sql);
        }
        
        public int TongDonHangMoiTrongTuan(DateTime time)
        {
            string sql = string.Format("SELECT Count(MAHD) as TongHD FROM HOADON WHERE DATEPART(WEEK, NgayLap) = DATEPART(WEEK, CONVERT(DATE, '{0}'))"
                , time.ToString("yyyy/MM/dd"));
            return ThucThiLenhLayKetQua(sql);
        }
        

        public int TongDonHangMoiTrongThang(DateTime time)
        {
            string sql = string.Format("SELECT Count(MAHD) as TongHD FROM HOADON WHERE DATEPART(MONTH, NgayLap) = DATEPART(MONTH, CONVERT(DATE, '{0}|'))"
                , time.ToString("yyyy/MM/dd"));
            return ThucThiLenhLayKetQua(sql);
        }

        public List<SanPham> TopSanPhamSapHetHang(int top, int lessThan)
        {
            List<SanPham> dsSP = new List<SanPham>();

            string sql = string.Format("SELECT TOP {0} * FROM SANPHAM WHERE SoLuong <= {1}", top, lessThan);
            
            DataTable tbKQ =  DocDuLieu(sql);
            
            for(int i = 0; i < tbKQ.Rows.Count; i++)
            {
                dsSP.Add(new SanPham()
                {
                    MaSp = tbKQ.Rows[i].Field<string>("MaSP"),
                    TenSp = tbKQ.Rows[i].Field<string>("TenSP"),
                    MoTa = tbKQ.Rows[i].Field<string>("MoTa"),
                    DVT = tbKQ.Rows[i].Field<string>("DVT"),
                    HinhAnh = tbKQ.Rows[i].Field<string>("HinhAnh"),
                    DonGia = tbKQ.Rows[i].Field<decimal>("DonGia"),
                    SoLuong = tbKQ.Rows[i].Field<int>("SoLuong"),
                });
            }
            return dsSP;
        }
    }
}
