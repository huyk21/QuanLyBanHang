using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyBanHang.Utils;

namespace QuanLyBanHang.DTO
{
    public class HoaDon
    {
        public HoaDon()
        {
            MaKh = "KH000";
            MaHd = Generator.TaoMaHoaDon();
        }

        public string MaHd { get; set; }
        public DateTime? NgayLap { get; set; }
        public DateTime? NgayXuat { get; set; }
        public string MaNv { get; set; }
        public string MaKh { get; set; }

        public string TenNV { get; set; }
        public string TenKH { get; set; }
        public decimal TongTienTruocThue { get; set; } = 0;
        public decimal TienThue { get
            {
                return CthoaDon.Sum(x => x.Sl.Value * x.TienThue);
            }
        }
        public decimal TongTienSauThue { get; set; } = 0;
        public decimal ChietKhau { get; set; } = 0;
        public decimal TongTienPhaiTra { get; set; } = 0;
        public virtual ICollection<CTHoaDon> CthoaDon { get; set; } = new HashSet<CTHoaDon>();

        public decimal TongTien
        {
            get
            {
                return CthoaDon.Sum(x => x.Sl.Value * x.DonGia.Value);
            }
        }
        
    }
}