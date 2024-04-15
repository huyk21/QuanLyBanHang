using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DTO
{
    public class LoaiSanPham : BaseViewModel
    {
        private string _maLoai;
        private string _tenLoai;

        public string MaLoai { get => _maLoai; set { _maLoai = value; OnPropertyChanged(); } }
        public string TenLoai { get => _tenLoai; set { _tenLoai = value; OnPropertyChanged(); } }
    }
}
