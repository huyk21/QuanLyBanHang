using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Utils
{
    public static class DateTimeExtends
    {
        public static string ToISOString(this DateTime time)
        {
            return "'" + time.ToString("yyyy/MM/dd") + "'";
        }

        public static string ToISOString(this DateTime? time)
        {
            if (!time.HasValue) return "'" + "1970/01/01" + "'";
            return "'" + time.Value.ToString("yyyy/MM/dd") + "'";
        }
    }
}
