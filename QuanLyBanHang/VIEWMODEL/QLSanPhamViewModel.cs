using Microsoft.Win32;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;
using QuanLyBanHang.GUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyBanHang.VIEWMODEL
{
    public class QLSanPhamViewModel : BaseViewModel
    {
        private static QLSanPhamViewModel _instance;

        public static QLSanPhamViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QLSanPhamViewModel();
                return _instance;
            }
        }
        public ObservableCollection<SanPham> List
        {
            get;
            set;
        }



        public ICommand SelectImageCommand { get; private set; }
        public ICommand ThemCommand { get; set; }
        public ICommand LuuCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public ICommand TimSPTheoTenCommand { get;set;}
        public ICommand XemDSTheoLoaiCommand { get;set;}
        public ICommand HienThiTatCaSPCommand { get;set;}
        public ICommand TimTheoKhoangGiaCommand { get;set;}
        SanPhamBUS SanPhamBUS { get; set; }
        LoaiSanPhamBUS LoaiSPBUS { get;set;}
        CauHinhBUS CauHinhBUS { get; set; } = new CauHinhBUS();
        public event Action DataUpdated;
       
        
        private void NotifyDataUpdated()
        {

            DataUpdated?.Invoke();
            MainViewModel.Instance.LoadProductsAboutToRunOut();
            MainViewModel.Instance.LoadSoDonHangTrongTuan();
            MainViewModel.Instance.LoadSoSanPhamDangBan();
          


        }
       
        private string _trangThaiLenh = "";
        public QLSanPhamViewModel()
        {

            SelectImageCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                    Title = "Select an Image"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    HinhAnh = openFileDialog.FileName;  // This assumes `HinhAnh` is a property in your ViewModel
                }
            });
            CurrentPage = 1;
            
            var settings = CauHinhBUS.LayThongSoCauHinh();

            if (settings != null && settings.SoLSanPhamMoiTrang > 0)
                RowsPerPage = settings.SoLSanPhamMoiTrang;

            SanPhamBUS = new SanPhamBUS();
            LoadDSSanPham();
            UpdatePagingCombo();
            LoaiSPBUS = new LoaiSanPhamBUS();
            LoadDSLoaiSP();
            // Thêm mới
            ThemCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                MaSp = SanPhamBUS.TaoMaMoi();
                TenSp = "";
                HinhAnh = "";
                MoTa = "";
                DVT = "";
                DonGia = 0;
                SoLuong = 0;
                MaLoai = "";
            });

            // Lưu
            LuuCommand = new RelayCommand<object>(
            p =>
            {
                if (string.IsNullOrEmpty(MaSp))
                    return false;

                if (string.IsNullOrEmpty(TenSp))
                    return false;

                return true;
            }, p =>
            {
                var obj = SanPhamBUS.LaySanPham(MaSp);
                var newObj = new SanPham()
                {
                    MaSp = MaSp,
                    TenSp = TenSp,
                    HinhAnh = HinhAnh,
                    MoTa = MoTa,
                    DVT = DVT,
                    DonGia = DonGia,
                    SoLuong = SoLuong,
                    MaLoai = MaLoai
                };
                if (obj == null)
                {
                    var result = SanPhamBUS.Them(newObj);
                    if (result) List.Add(newObj);
                }
                else
                {
                    var result = SanPhamBUS.Sua(MaSp, newObj);
                    if (result && SelectedItem != null)
                    {
                        SelectedItem.MaSp = newObj.MaSp;
                        SelectedItem.TenSp = newObj.TenSp;
                        SelectedItem.HinhAnh = newObj.HinhAnh;
                        SelectedItem.MoTa = newObj.MoTa;
                        SelectedItem.DVT = newObj.DVT;
                        SelectedItem.DonGia = newObj.DonGia;
                        SelectedItem.SoLuong = newObj.SoLuong;
                        SelectedItem.MaLoai = newObj.MaLoai;
                        NotifyDataUpdated();
                    }

                }
            });

            // Xoá
            XoaCommand = new RelayCommand<object>(
            p =>
            {

                if (SelectedItem == null)
                    return false;

                if (string.IsNullOrEmpty(TenSp))
                    return false;

                return true;
            }, p =>
            {
                SanPhamBUS.Xoa(SelectedItem.MaSp);
                List.Remove(SelectedItem);
                LoadDSSanPham();
                NotifyDataUpdated();
            });

            ComboBoxPagingSelectionChangedCommand = new RelayCommand<ComboBox>(
                p =>
                               {
                    return true;
                }, p =>
                {
                    if (p != null)
                    {
                        LoadDSSanPham();
                    }
                });

            PrevPageCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    if (CurrentPage > 1)
                    {
                        CurrentPage--;
                        LoadDSSanPham();
                    }
                }
            );

            NextPageCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    if (CurrentPage < TotalPages)
                    {
                        CurrentPage++;
                        LoadDSSanPham();
                    }
                }
            );

            TimSPTheoTenCommand = new RelayCommand<object>(
                p =>
                {
                    if (string.IsNullOrEmpty (TenSp))
                        return false;

                    return true;
                }, p =>
                {
                    _trangThaiLenh = "TimTheoTen";
                    // Tim san pham theo ten
                    CurrentPage = 1;
                    LoadDSSanPham();
                    UpdatePagingCombo();

                }
            );

            XemDSTheoLoaiCommand = new RelayCommand<object>(
                p =>
                {
                    if (string.IsNullOrEmpty(MaLoai))
                        return false;

                    return true;
                }, p =>
                {
                    // Xem danh sach san pham theo loai
                    _trangThaiLenh = "TimTheoLoai";
                    CurrentPage = 1;
                    LoadDSSanPham();
                    UpdatePagingCombo();
                }
            );

            HienThiTatCaSPCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    // Xem danh sach san pham theo loai
                    _trangThaiLenh = "";
                    CurrentPage = 1;
                    LoadDSSanPham();
                    UpdatePagingCombo();
                }
            );

            TimTheoKhoangGiaCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    // Xem danh sach san pham theo loai
                    _trangThaiLenh = "TimTheoKhoangGiaPaging";
                    CurrentPage = 1;
                    LoadDSSanPham();
                    UpdatePagingCombo();
                }
            );
        }
       
        private void LoadDSLoaiSP()
        {
            ListLoaiSP = new ObservableCollection<LoaiSanPham>(LoaiSPBUS.LayDS());
        }

        private void LoadDSSanPham()
        {

            Tuple<List<SanPham>, int> dsSanPham =  null;

            if (_trangThaiLenh == "TimTheoTen")
            {
                dsSanPham = SanPhamBUS.TimTheoTen(TenSp, CurrentPage, RowsPerPage);
            }
            else if (_trangThaiLenh == "TimTheoLoai")
            {
                dsSanPham = SanPhamBUS.TimTheoLoai(MaLoai, CurrentPage, RowsPerPage);
            }
            else if (_trangThaiLenh == "TimTheoKhoangGiaPaging")
            {
                dsSanPham = SanPhamBUS
                    .TimTheoKhoangGiaPaging(TuGia, DenGia, CurrentPage, RowsPerPage);
            }
            else
            {
                dsSanPham = SanPhamBUS.LayDSPaging(CurrentPage, RowsPerPage);
            }
            List = new ObservableCollection<SanPham>(dsSanPham.Item1);
            OnPropertyChanged(nameof(List));
            TotalItems = dsSanPham.Item2;
            UpdatePagingInfo();
        }

        public ObservableCollection<LoaiSanPham> ListLoaiSP { get => _listLoaiSP; set => _listLoaiSP = value; }

        public LoaiSanPham SelectedLoaiSP
        {
            get => _selectedLoaiSP;
            set
            {
                _selectedLoaiSP = value;
                OnPropertyChanged();
                if (value != null)
                    MaLoai = value.MaLoai;
            }
        }

        private SanPham _selectedItem;
        private string _maSp;
        private string _tenSp;
        private string _hinhAnh;
        private string _moTa;
        private string _dvt;
        private decimal? _donGia = 0;
        private int? _soLuong = 0;
        private ObservableCollection<LoaiSanPham> _listLoaiSP;
        private LoaiSanPham _selectedLoaiSP;
        private string _maLoai;
        private decimal _tuGia;
        private decimal _denGia;

        public SanPham SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (_selectedItem != null)
                {
                    MaSp = _selectedItem.MaSp;
                    TenSp = _selectedItem.TenSp;
                    HinhAnh = _selectedItem.HinhAnh;
                    MoTa = _selectedItem.MoTa;
                    DVT = _selectedItem.DVT;
                    DonGia = _selectedItem.DonGia;
                    SoLuong = _selectedItem.SoLuong;
                    MaLoai = _selectedItem.MaLoai;
                    OnPropertyChanged(nameof(MaLoai));
                }
            }
        }

        public string MaSp { get => _maSp; set { _maSp = value; OnPropertyChanged(); } }
        public string TenSp { get => _tenSp; set { _tenSp = value; OnPropertyChanged(); } }
        public string HinhAnh { get => _hinhAnh; set { _hinhAnh = value; OnPropertyChanged(); } }
        public string MoTa { get => _moTa; set { _moTa = value; OnPropertyChanged(); } }
        public string DVT { get => _dvt; set { _dvt = value; OnPropertyChanged(); } }
        public decimal? DonGia { get => _donGia; set { _donGia = value; OnPropertyChanged(); } }
        public int? SoLuong { get => _soLuong; set { _soLuong = value; OnPropertyChanged(); } }
        public string MaLoai { get => _maLoai; set { _maLoai = value; OnPropertyChanged(); } }
        
        public decimal TuGia { get => _tuGia; set { _tuGia = value; OnPropertyChanged(); } }
        public decimal DenGia { get => _denGia; set { _denGia = value; OnPropertyChanged(); } }
    }
}
