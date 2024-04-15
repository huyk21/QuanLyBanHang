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
    public class CauHinhBUS : KetNoi
    {
        public CauHinhBUS()
            : base()
        {
        }

        public CauHinh LayThongSoCauHinh()
        {
            string sql = "SELECT * FROM CauHinh";
            return CreateObj(DocDuLieu(sql).Rows[0]);
        }

        private CauHinh CreateObj(DataRow r)
        {
            var result = new CauHinh();
            if (r != null )
            {
                result.SoLSanPhamMoiTrang = r.Field<int>("SoLSanPhamMoiTrang");
                result.MoLaiManHinhCuoi = r.Field<bool>("MoLaiManHinhCuoi");
            }
            return result;

        }

        
        public bool CapNhatCauHinh(CauHinh c)
        {
            string sql = "UPDATE CauHinh SET SoLSanPhamMoiTrang = {0}, MoLaiManHinhCuoi = {1}"
                .Format(new object[] { c.SoLSanPhamMoiTrang, c.MoLaiManHinhCuoi ? 1 : 0 });
            return ThucThiLenh(sql);
        }
    }
}
