using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.BUS
{
    public class QuyenTruyCapBUS : KetNoi
    {
        public QuyenTruyCapBUS()
            :base()
        {

        }

       
        public string TaoMaMoi ()
        {
            string sql = "SELECT TOP 1 MAQUYEN FROM QUYENTRUYCAP ORDER BY MAQUYEN DESC";
            var ret = ThucThiLenhLayKetQua(sql);

            if (ret > 0)
            {
                string ma = ret.ToString();
                int so = int.Parse(ma.Substring(2));
                so++;
                return "QT" + so.ToString("000");
            }
            else
            {
                return "QT001";
            }
        }

        public List<QuyenTruyCap> LayDS()
        {
            List<QuyenTruyCap> ds = new List<QuyenTruyCap>();
            string sql = "SELECT * FROM QuyenTruyCap";
            DataTable data = DocDuLieu(sql);

            foreach (DataRow r in data.Rows)
            {
                ds.Add(CreateObj(r));
            }
            return ds;
        }

        private QuyenTruyCap CreateObj(DataRow r)
        {
            QuyenTruyCap obj = new QuyenTruyCap
            {
                MaQuyen = r.Field<string>("MaQuyen"),
                TenQuyen = r.Field<string>("TenQuyen")
            };

            return obj;
        }
    }
}
