using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;


namespace QuanLyBanHang.DTO
{
    public class SanPham : BaseViewModel
    {
        private string _maSp;
        private string _tenSp;
        private string _hinhAnh;
        private string _moTa;
        private string _dVT;
        private decimal? _donGia = 0;
        private int? _soLuong = 0;
        private string _maLoai;
        private string _tenLoai;

        public SanPham()
        {
            CthoaDon = new HashSet<CTHoaDon>();
        }

        public string MaSp { get => _maSp; set { _maSp = value; OnPropertyChanged(); } }
        public string TenSp { get => _tenSp; set { _tenSp = value; OnPropertyChanged(); } }
        public string HinhAnh { get => _hinhAnh; set { _hinhAnh = value; OnPropertyChanged(); } }
        public string MoTa { get => _moTa; set { _moTa = value; OnPropertyChanged(); } }
        public string DVT { get => _dVT; set { _dVT = value; OnPropertyChanged(); } }
        public decimal? DonGia { get => _donGia; set { _donGia = value; OnPropertyChanged(); } }
        public int? SoLuong { get => _soLuong; set { _soLuong = value; OnPropertyChanged(); } }
        public string MaLoai { get => _maLoai; set { _maLoai = value; OnPropertyChanged(); } }
        public string TenLoai { get => _tenLoai; set { _tenLoai = value; OnPropertyChanged(); } }
        public virtual ICollection<CTHoaDon> CthoaDon { get; set; }
    }
}