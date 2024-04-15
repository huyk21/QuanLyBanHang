using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;
using QuanLyBanHang.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace QuanLyBanHang.VIEWMODEL
{
    public class QLDonHangViewModel : BaseViewModel
    {
        public ObservableCollection<HoaDon> List
        {
            get;
            set;
        }
        public event Action DataUpdated;


        private void NotifyDataUpdated()
        {

            DataUpdated?.Invoke();
            
            MainViewModel.Instance.LoadSoDonHangTrongTuan();
            



        }
        public ObservableCollection<CTHoaDon> ListCT { get; set; }
        public CTHoaDon SelectedCT
        {
            get => _selectedCT;
            set
            {
                _selectedCT = value;
                OnPropertyChanged();
                if (_selectedCT != null)
                {
                    // TODO : 
                }
            }
        }
        public ObservableCollection<NhanVien> ListNV { get; set; }
        public NhanVien SelectedNV { get; set; }
        public ObservableCollection<KhachHang> ListKH { get; set; }
        public KhachHang SelectedKH { get; set; }
        public ObservableCollection<SanPham> ListSP { get; set; }
        public SanPham SelectedSP { get; set; }

        HoaDonBUS HoaDonBUS { get; set; } = new HoaDonBUS();
        CTHoaDonBUS CTHoaDonBUS { get; set; } = new CTHoaDonBUS();
        NhanVienBUS NhanVienBUS { get; set; } = new NhanVienBUS();
        KhachHangBUS KhachHangBUS { get; set; } = new KhachHangBUS();
        SanPhamBUS SanPhamBUS { get; set; } = new SanPhamBUS();
        CauHinhBUS CauHinhBUS { get; set; } = new CauHinhBUS();
        public QLDonHangViewModel()
        {
            var settings = CauHinhBUS.LayThongSoCauHinh();

            if (settings != null && settings.SoLSanPhamMoiTrang > 0)
                RowsPerPage = settings.SoLSanPhamMoiTrang;

            NgayLap = DateTime.Now;
            LoadDSHoaDon();
            UpdatePagingCombo();
            LoadDSNhanVien();
            LoadDSKhachHang();
            LoadDSSanPham();

            ThemCTCommand = new RelayCommand<object>(
                p =>
                {
                    if (SelectedItem != null)
                        return false;

                    if (SelectedSP == null)
                        return false;

                    if (!SoLuong.HasValue)
                        return false;

                    if (SoLuong <= 0)
                        return false;

                    if (ListCT == null)
                        return false;

                    return true;
                }, p =>
                {

                    var obj = ListCT.Where(x => x.MaSp == SelectedSP.MaSp).FirstOrDefault();

                    if (obj != null)
                    {
                        obj.MaSp = SelectedSP.MaSp;
                        obj.TenSp = SelectedSP.TenSp;
                        obj.Sl = (obj.Sl ?? 0) + SoLuong;
                        obj.DonGia = SelectedSP.DonGia;
                        
                    }
                    else
                    {
                        ListCT.Add(new CTHoaDon()
                        {
                            MaHd = MaHd,
                            MaSp = SelectedSP.MaSp,
                            TenSp = SelectedSP.TenSp,
                            Sl = SoLuong,
                            DonGia = SelectedSP.DonGia
                        });
                       
                    }
                    SoLuong = 0;
                    OnPropertyChanged(nameof(SoLuong));
                    NotifyDataUpdated();
                }
            );

            XoaCTCommand = new RelayCommand<object>(
                p =>
                {
                    if (SelectedItem != null)
                        return false;

                    if (SelectedCT == null)
                        return false;

                    return true;
                }, p =>
                {
                    ListCT.Remove(SelectedCT);
                    OnPropertyChanged(nameof(ListCT));
                    NotifyDataUpdated();
                }

            );

            ThemCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    MaHd = HoaDonBUS.TaoMaMoi();
                    NgayLap = DateTime.Now;

                    if (Globals.CurrentNhanVien != null)
                    {
                        SelectedNV = Globals.CurrentNhanVien;
                        OnPropertyChanged(nameof(SelectedNV));
                        MaNv = SelectedNV.MaNv;
                        OnPropertyChanged(nameof(MaNv));
                        NotifyDataUpdated();
                    }
                        
                    MaKh = "";
                    SelectedItem = null;
                    OnPropertyChanged(nameof(SelectedItem));
                    ListCT = new ObservableCollection<CTHoaDon>();
                    OnPropertyChanged(nameof(ListCT));
                    SelectedSP = null;
                    OnPropertyChanged(nameof(SelectedSP));
                    SoLuong = 0;
                    OnPropertyChanged(nameof(SoLuong));
                });

            LuuCommand = new RelayCommand<object>(
               p =>
               {
                   if (string.IsNullOrEmpty(MaHd))
                       return false;

                   if (string.IsNullOrEmpty(MaKh))
                       return false;

                   if (string.IsNullOrEmpty(MaNv))
                       return false;

                   return true;
               }, p =>
               {
                   var obj = HoaDonBUS.LayHoaDon(MaHd);

                   var newObj = new HoaDon()
                   {
                       MaHd = MaHd,
                       NgayLap = NgayLap,
                       MaNv = MaNv,
                       MaKh = MaKh,
                       TongTienPhaiTra = ListCT.Sum(x => x.Sl * x.DonGia) ?? 0,
                       CthoaDon = ListCT.ToList()
                   };


                   if (obj == null)
                   {
                       // Thêm mới hoá đơn
                       var result = HoaDonBUS.Them(newObj);

                       if (result)
                       {
                           List.Add(newObj);
                           OnPropertyChanged(nameof(List));
                           NotifyDataUpdated();
                       }
                   }
               });

            XoaCommand = new RelayCommand<object>(
               p =>
               {
                   if (string.IsNullOrEmpty(MaHd))
                       return false;

                   if (SelectedItem == null)
                       return false;

                   return true;
               }, p =>
               {
                   var result = HoaDonBUS.Xoa(SelectedItem.MaHd);
                   if (result)
                   {
                       List.Remove(SelectedItem);
                       OnPropertyChanged(nameof(List));
                       MaHd = HoaDonBUS.TaoMaMoi();
                       NgayLap = DateTime.Now;
                       MaNv = "";
                       MaKh = "";
                       ListCT = new ObservableCollection<CTHoaDon>();
                       OnPropertyChanged(nameof(ListCT));
                       SelectedSP = null;
                       OnPropertyChanged(nameof(SelectedSP));
                       SoLuong = 0;
                       OnPropertyChanged(nameof(SoLuong));
                       NotifyDataUpdated();
                   }
               });

            ComboBoxPagingSelectionChangedCommand = new RelayCommand<ComboBox>(
                p =>
                {
                    return true;
                }, p =>
                {
                    if (p != null)
                    {
                        LoadDSHoaDon();
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
                        //LoadDSSanPham();
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
                        //LoadDSSanPham();
                    }
                }
            );


            TimCommand = new RelayCommand<object>(
              p =>
              {
                  if (TuNgay > DenNgay)
                      return false;

                  if (!TuNgay.HasValue || !DenNgay.HasValue)
                      return false;

                  return true;
              }, p =>
              {
                  List = new ObservableCollection<HoaDon>(HoaDonBUS.Tim(TuNgay.Value, DenNgay.Value));
                  OnPropertyChanged(nameof(List));
              });

            TatCaCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    List = new ObservableCollection<HoaDon>(HoaDonBUS.LayDS());
                    OnPropertyChanged(nameof(List));
                }
            );
        }

        private void LoadDSSanPham()
        {
            var list = SanPhamBUS.LayDS();
            ListSP = new ObservableCollection<SanPham>(list);
        }

        private void LoadDSKhachHang()
        {
            var list = KhachHangBUS.LayDS();
            ListKH = new ObservableCollection<KhachHang>(list);
        }

        private void LoadDSNhanVien()
        {
            var list = NhanVienBUS.LayDS();
            ListNV = new ObservableCollection<NhanVien>(list);
        }

        private void LoadDSHoaDon()
        {

            var ds = HoaDonBUS.LayDSPaging(CurrentPage, RowsPerPage);
            List = new ObservableCollection<HoaDon>(ds.Item1);
            OnPropertyChanged(nameof (List));
            TotalItems = ds.Item2;
            OnPropertyChanged(nameof(TotalPages));
            UpdatePagingInfo();
        }

        private HoaDon _selectedItem;
        private string _maHd;
        private DateTime? _ngayLap;
        private DateTime? _ngayXuat;
        private string _maNv;
        private string _maKh;
        private decimal _tongTienPhaiTra = 0;
        private int? _soLuong = 0;
        private CTHoaDon _selectedCT;
        private DateTime? _tuNgay;
        private DateTime? _denNgay;

        public HoaDon SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
                if (_selectedItem != null)
                {
                    MaHd = _selectedItem.MaHd;
                    MaNv = _selectedItem.MaNv;
                    MaKh = _selectedItem.MaKh;
                    NgayLap = _selectedItem.NgayLap;
                    TongTienPhaiTra = _selectedItem.TongTienPhaiTra;
                    ListCT = new ObservableCollection<CTHoaDon>(CTHoaDonBUS.LayDS(_selectedItem.MaHd));
                    OnPropertyChanged("ListCT");
                }
            }
        }

        public DateTime? TuNgay { get => _tuNgay; set { _tuNgay = value; OnPropertyChanged(); } }
        public DateTime? DenNgay { get => _denNgay; set { _denNgay = value; OnPropertyChanged(); } }
        public int? SoLuong { get => _soLuong; set { _soLuong = value; OnPropertyChanged(); } }
        public string MaHd { get => _maHd; set { _maHd = value; OnPropertyChanged(); } }
        public DateTime? NgayLap { get => _ngayLap; set { _ngayLap = value; OnPropertyChanged(); } }
        public string MaNv { get => _maNv; set { _maNv = value; OnPropertyChanged(); } }
        public string MaKh { get => _maKh; set { _maKh = value; OnPropertyChanged(); } }
        public decimal TongTienPhaiTra { get => _tongTienPhaiTra; set { _tongTienPhaiTra = value; OnPropertyChanged(); } }

        public ICommand ThemCTCommand { get; set; }
        public ICommand XoaCTCommand { get; set; }
        public ICommand ThemCommand { get; set; }
        public ICommand LuuCommand { get; set; }
        public ICommand XoaCommand { get; set; }
        public ICommand TimCommand { get; set; }
        public ICommand TatCaCommand { get; set; }
    }
}
