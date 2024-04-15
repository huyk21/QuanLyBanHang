using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;


namespace QuanLyBanHang.DTO
{
    public class KhachHang : BaseViewModel
    {
        private string _maKh;
        private string _tenKh;
        private string _sdt;
        private string _diaChi;
        private string _email;
        private int? _diemTichLuy;
        private DateTime? _ngaySinh;

        public KhachHang()
        {
            HoaDon = new HashSet<HoaDon>();
        }

        public string MaKh { get => _maKh; set { _maKh = value; OnPropertyChanged(); } }
        public string TenKh { get => _tenKh; set { _tenKh = value; OnPropertyChanged(); } }
        public string Sdt { get => _sdt; set { _sdt = value; OnPropertyChanged(); } }
        public string DiaChi { get => _diaChi; set { _diaChi = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public int? DiemTichLuy { get => _diemTichLuy; set { _diemTichLuy = value; OnPropertyChanged(); } }
        public DateTime? NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }

        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}