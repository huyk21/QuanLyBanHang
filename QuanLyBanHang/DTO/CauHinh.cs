using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DTO
{
    public class CauHinh : BaseViewModel
    {
        private int _soLSanPhamMoiTrang = 0;
        private bool _moLaiManHinhCuoi;

        public int SoLSanPhamMoiTrang { get => _soLSanPhamMoiTrang; set { _soLSanPhamMoiTrang = value; OnPropertyChanged(); } }
        public bool MoLaiManHinhCuoi { get => _moLaiManHinhCuoi; set { _moLaiManHinhCuoi = value; OnPropertyChanged(); } }
    }
}
