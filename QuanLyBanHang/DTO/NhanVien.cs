using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;


namespace QuanLyBanHang.DTO
{
    public class NhanVien : BaseViewModel
    {
        private string _maNv;
        private string _hoTen;
        private string _sDT;
        private string _diaChi;
        private string _email;
        private DateTime? _ngaySinh;

        public NhanVien()
        {
        }

        public string MaNv { get => _maNv; set { _maNv = value; OnPropertyChanged(); } }
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        public string SDT { get => _sDT; set { _sDT = value; OnPropertyChanged(); } }
        public string DiaChi { get => _diaChi; set { _diaChi = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public DateTime? NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }
    }
}