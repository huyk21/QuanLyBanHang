using QuanLyBanHang.BUS;
using QuanLyBanHang.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyBanHang.VIEWMODEL
{
    public class CauHinhViewModel : BaseViewModel
    {
        CauHinhBUS _cauHinhBUS = new CauHinhBUS();
        public ICommand DongCommand { get;set;}
        public ICommand LuuCommand { get;set;}
        public ICommand WindowClosingCommand { get;set;}
        public CauHinhViewModel()
        {
            LuuCommand = new RelayCommand<object>(
            (p) => { 

                if (SoLSanPhamMoiTrang <= 0)
                    return false;

                return true; 
            }, (p) =>
            {
                var obj = new DTO.CauHinh()
                {
                    SoLSanPhamMoiTrang = SoLSanPhamMoiTrang,
                    MoLaiManHinhCuoi = MoLaiManHinhCuoi
                };

                _cauHinhBUS.CapNhatCauHinh(obj);
            });

            DongCommand = new RelayCommand<CauHinhWindow>(
                (p) => {  
                    return true; 
                }, (p) => {
                    p.Close();
                }
            );
            
            WindowClosingCommand = new RelayCommand<CauHinhWindow>(
                   (p) => {
                       return true;
                   }, (p) => {
                       LuuCommand.Execute(null);
                   }
            );

            LoadSettings();
        }

        private void LoadSettings()
        {
            var cauHinh = _cauHinhBUS.LayThongSoCauHinh();
            SoLSanPhamMoiTrang = cauHinh.SoLSanPhamMoiTrang;
            MoLaiManHinhCuoi = cauHinh.MoLaiManHinhCuoi;
        }

        private int _soLSanPhamMoiTrang;
        private bool _moLaiManHinhCuoi;

        public int SoLSanPhamMoiTrang { get => _soLSanPhamMoiTrang; set { _soLSanPhamMoiTrang = value; OnPropertyChanged(); } }
        public bool MoLaiManHinhCuoi { get => _moLaiManHinhCuoi; set { _moLaiManHinhCuoi = value; OnPropertyChanged(); } }
    }
}
