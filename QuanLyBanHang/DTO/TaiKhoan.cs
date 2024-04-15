using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;


namespace QuanLyBanHang.DTO
{
    public class TaiKhoan : BaseViewModel
    {
        public string MaTk { get; set; }
        public string MatKhau { get; set; }
        public DateTime? NgayLap { get; set; }
        public DateTime? ThoiGianDangNhap { get; set; }
        public DateTime? ThoiGianDangXuat { get; set; }
        public string MaQuyen { get; set; }
        public string MaNv { get; set; }

        public virtual NhanVien MaNvNavigation { get; set; }
        public virtual QuyenTruyCap MaQuyenNavigation { get; set; }
    }
}