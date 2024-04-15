using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyBanHang.VIEWMODEL
{
    public class QLKhachHangViewModel : BaseViewModel
    {
        public ObservableCollection<KhachHang> List
        {
            get => _list;
            set { _list = value; OnPropertyChanged(nameof(List)); }
        }

        public ICommand ThemCommand { get; set; }
        public ICommand LuuCommand { get; set; }
        public ICommand XoaCommand { get; set; }

        KhachHangBUS KhachHangBUS { get; set; }
        CauHinhBUS CauHinhBUS { get; set; } = new CauHinhBUS();

        public QLKhachHangViewModel()
        {
            var settings = CauHinhBUS.LayThongSoCauHinh();

            if (settings != null && settings.SoLSanPhamMoiTrang > 0)
                RowsPerPage = settings.SoLSanPhamMoiTrang;

            KhachHangBUS = new KhachHangBUS();
            LoadDSKhachHang();
            UpdatePagingCombo();

        }

        private void LoadDSKhachHang()
        {

            var ds = KhachHangBUS.LayDSPaging(CurrentPage, RowsPerPage);
            List = new ObservableCollection<KhachHang>(ds.Item1);

            ThemCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                MaKh = KhachHangBUS.TaoMaMoi();
                TenKh = "";
                Sdt = "";
                DiaChi = "";
                Email = "";
                DiemTichLuy = 0;
                NgaySinh = DateTime.Now;
            });

            LuuCommand = new RelayCommand<object>(p =>
            {
                if (string.IsNullOrEmpty(MaKh))
                    return false;

                if (string.IsNullOrEmpty(TenKh))
                    return false;

                return true;
            }, p =>
            {
                var obj = KhachHangBUS.LayKhachHang(MaKh);

                var newObj = new KhachHang()
                {
                    MaKh = MaKh,
                    TenKh = TenKh,
                    Sdt = Sdt,
                    DiaChi = DiaChi,
                    Email = Email,
                    DiemTichLuy = DiemTichLuy,
                    NgaySinh = NgaySinh
                };

                if (obj == null)
                {
                    var result = KhachHangBUS.Them(newObj);
                    if (result)
                        List.Add(newObj);
                }
                else
                {
                    var result = KhachHangBUS.Sua(MaKh, newObj);

                    if (result && SelectedItem != null)
                    {
                        SelectedItem.MaKh = newObj.MaKh;
                        SelectedItem.TenKh = newObj.TenKh;
                        SelectedItem.Sdt = newObj.Sdt;
                        SelectedItem.DiaChi = newObj.DiaChi;
                        SelectedItem.Email = newObj.Email;
                        SelectedItem.DiemTichLuy = newObj.DiemTichLuy;
                        SelectedItem.NgaySinh = newObj.NgaySinh;
                    }
                }
            });


            XoaCommand = new RelayCommand<object>(p =>
            {
                if (SelectedItem == null)
                    return false;

                return true;
            }, p =>
            {
                if (SelectedItem == null)
                    return;
                KhachHangBUS.Xoa(SelectedItem.MaKh);
                List.Remove(SelectedItem);
            });

            ComboBoxPagingSelectionChangedCommand = new RelayCommand<ComboBox>(
                p =>
                {
                    return true;
                }, p =>
                {
                    if (p != null)
                    {
                        LoadDSKhachHang();
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

            OnPropertyChanged(nameof(List));
            TotalItems = ds.Item2;
            UpdatePagingInfo();
        }

        

        private KhachHang _selectedItem;
        private ObservableCollection<KhachHang> _list;
        private string _maKh;
        private string _tenKh;
        private string _sdt;
        private string _diaChi;
        private string _email;
        private int? _diemTichLuy;
        private DateTime? _ngaySinh;

        public KhachHang SelectedItem
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
                    MaKh = SelectedItem.MaKh;
                    TenKh = SelectedItem.TenKh;
                    Sdt = SelectedItem.Sdt;
                    DiaChi = SelectedItem.DiaChi;
                    Email = SelectedItem.Email;
                    DiemTichLuy = SelectedItem.DiemTichLuy;
                    NgaySinh = SelectedItem.NgaySinh;
                }
            }
        }

        public string MaKh { get => _maKh; set { _maKh = value; OnPropertyChanged(); } }
        public string TenKh { get => _tenKh; set { _tenKh = value; OnPropertyChanged(); } }
        public string Sdt { get => _sdt; set { _sdt = value; OnPropertyChanged(); } }
        public string DiaChi { get => _diaChi; set { _diaChi = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public int? DiemTichLuy { get => _diemTichLuy; set { _diemTichLuy = value; OnPropertyChanged(); } }
        public DateTime? NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }
    }
}
