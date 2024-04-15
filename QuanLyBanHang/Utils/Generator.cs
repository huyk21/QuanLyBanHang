using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Utils
{
    public static class Generator
    {
        public static string TaoMaHoaDon()
        {
            var current = DateTime.Now;
            return string.Format("HD{0}"
                , (int)current.Subtract(new DateTime(1970, 1, 1))
                    .TotalSeconds);
        }
    }
}
