using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;
using QuanLyBanHang.GUI;
using QuanLyBanHang.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace QuanLyBanHang.VIEWMODEL
{
    public class MainViewModel : BaseViewModel
    {
        private static MainViewModel _instance;

        public static MainViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainViewModel();
                return _instance;
            }
        }


        private ObservableCollection<ProductAboutToRunOut> _productsAboutToRunOut;
        public ObservableCollection<ProductAboutToRunOut> ProductsAboutToRunOut
        {
            get => _productsAboutToRunOut;
            set
            {
                _productsAboutToRunOut = value;
                OnPropertyChanged();
            }
        }
        // Method to load products about to run out
        public void LoadProductsAboutToRunOut()
        {
            // Assume you have a method in SanPhamBUS that gets products about to run out
            var products = _sanPhamBus.CacSanPhamSapHetHang()
                .Select(sp => new ProductAboutToRunOut
                {
                    Name = sp.TenSp,
                    Quantity = sp.SoLuong ?? 0
                }).ToList();

            ProductsAboutToRunOut = new ObservableCollection<ProductAboutToRunOut>(products);
        }
        public int _soSPDangBan;
        public int _soDonHangTrongTuan;
        
        public ICommand LoadWindowCommand { get; set; }
        
        public MainViewModel()
        {

           

            LoadProductsAboutToRunOut();
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null) return;

                p.Hide();
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    NapCuaSoMoLanCuoi();
                }
                else
                {
                    p.Close();
                    
                }
            });
           
          
            LoadSoSanPhamDangBan();
            LoadSoDonHangTrongTuan();
            LayThongTinHangTon();
        }

        
        


        

        public class ProductAboutToRunOut
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }
        private void NapCuaSoMoLanCuoi()
        {
            var settings = new CauHinhBUS().LayThongSoCauHinh();

            if (settings.MoLaiManHinhCuoi == true)
            {
                if (!File.Exists("last_window.txt"))
                    return;

                var lastScreen = File.ReadAllText("last_window.txt").Trim();

                if (lastScreen == "NhanVien")
                {
                    NhanVienCommand.Execute(null);
                }
                else if (lastScreen == "KhachHang")
                {
                    KhachHangCommand.Execute(null);
                }
                else if (lastScreen == "HangHoa")
                {
                    QLHangHoaCommand.Execute(null);
                }
                else if (lastScreen == "HoaDon")
                {
                    QLHoaDonCommand.Execute(null);
                }
                else if (lastScreen == "ThongKe")
                {
                    BaoCaoCommand.Execute(null);
                }
                else if (lastScreen == "LoaiSanPham")
                {
                    LoaiSanPhamCommand.Execute(null);
                }
                else if (lastScreen == "ImportSanPham")
                {
                    ImportSanPhamCommand.Execute(null);
                }
            }
        }

        public int SoSPHetHang { get => _soSPHetHang; set { _soSPHetHang = value; OnPropertyChanged(); } }

       
        public void LayThongTinHangTon()
        {
            SoSPHetHang = _sanPhamBus.CacSanPhamSapHetHang()
                .Count();;
        }

        HoaDonBUS _hoaDonBUS = new HoaDonBUS();
        public void LoadSoDonHangTrongTuan()
        {
            List<HoaDon> dsHoaDon = _hoaDonBUS.LayDSHoaDonTrongTuan();
            SoDonHangTrongTuan = dsHoaDon.Count;
            OnPropertyChanged("SoDonHangTrongTuan");
        }

        SanPhamBUS _sanPhamBus = new SanPhamBUS();
        private int _soSPHetHang;

        public void LoadSoSanPhamDangBan()
        {
            SoSPDangBan = _sanPhamBus.TongSoSanPham();

            Application.Current.Dispatcher.Invoke(() =>
            {
                OnPropertyChanged("SoSPDangBan");
            });
        }

        public ICommand NhanVienCommand { get; set; } = new RelayCommand<object>(p => true, p =>
        {
            LuuManHinh("NhanVien");
            NhanVienWindow w = new NhanVienWindow();
            w.ShowDialog();
        });

        public ICommand KhachHangCommand { get; set; } = new RelayCommand<object>(p => true, p =>
        {
            LuuManHinh("KhachHang");
            KhachHangWindow w = new KhachHangWindow();
            w.ShowDialog();
        });

        public ICommand QLHangHoaCommand { get; set; } = new RelayCommand<object>(p => true, p =>
        {
            LuuManHinh("HangHoa");
            HangHoaWindow w = new HangHoaWindow();
            w.ShowDialog();
        });

        public ICommand QLHoaDonCommand { get; set; } = new RelayCommand<object>(p => true, p =>
        {
            LuuManHinh("HoaDon");
            DonHangWindow w = new DonHangWindow();
            w.ShowDialog();
           
        });

        public ICommand BaoCaoCommand { get; set; } = new RelayCommand<object>(p => true, p =>
        {
            LuuManHinh("BaoCao");
            ThongKeWindow w = new ThongKeWindow();
            w.ShowDialog();
        });

        public ICommand CauHinhCommand { get; set; } = new RelayCommand<object>(p => true, p =>
        {
            CauHinhWindow w = new CauHinhWindow();
            w.ShowDialog();
        });

        public ICommand LoaiSanPhamCommand { get; set; } = new RelayCommand<object>(p => true, p =>
        {
            LuuManHinh("LoaiSanPham");
            LoaiSanPhamWindow w = new LoaiSanPhamWindow();
            w.ShowDialog();
        });

        public ICommand ImportSanPhamCommand { get; set; } = new RelayCommand<object>(p => true, p =>
        {
            LuuManHinh("ImportSanPham");
            ImportHangHoaWindow w = new ImportHangHoaWindow();
            w.ShowDialog();
        });
      

       

        private static void LuuManHinh(string windowName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("last_window.txt"))
                {
                    writer.WriteLine(windowName);
                }
            } catch (Exception ex)
            {
                return;
            }
        }

        public int SoSPDangBan { get => _soSPDangBan; set { _soSPDangBan = value; OnPropertyChanged(); } }
        public int SoDonHangTrongTuan { get => _soDonHangTrongTuan; set { _soDonHangTrongTuan = value; OnPropertyChanged(); } }
    }
}
