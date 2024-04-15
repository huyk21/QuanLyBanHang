using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;



namespace QuanLyBanHang.DTO
{
    public class CTHoaDon : BaseViewModel
    {
        private string _maHd;
        private string _maSp;
        private decimal? _donGia;
        private decimal? _sl;
        private string _tenSp;
        private decimal _tienThue;

        public string MaHd { get => _maHd; set { _maHd = value; OnPropertyChanged(); } }
        public string MaSp { get => _maSp; set { _maSp = value; OnPropertyChanged(); } }
        public decimal? DonGia { get => _donGia; set { _donGia = value; OnPropertyChanged(); } }
        public decimal? Sl { get => _sl; set { _sl = value; OnPropertyChanged(); } }
        public string TenSp { get => _tenSp; set { _tenSp = value; OnPropertyChanged(); } }
        public decimal TienThue { get => _tienThue; set { _tienThue = value; OnPropertyChanged(); } }
    }
}