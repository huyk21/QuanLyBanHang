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
using System.Windows.Media.Media3D;

namespace QuanLyBanHang.VIEWMODEL
{
    public class LoaiSanPhamViewModel : BaseViewModel
    {
        private string _maLoai;
        private string _tenLoai;
        public string MaLoai { get => _maLoai; set { _maLoai = value; OnPropertyChanged(); } }
        public string TenLoai { get => _tenLoai; set { _tenLoai = value; OnPropertyChanged(); } }

        public ObservableCollection<LoaiSanPham> List { get; set; }
        public ICommand ThemCommand { get; private set; }
        public ICommand XoaCommand { get; private set; }
        public ICommand LuuCommand { get; private set; }

        private LoaiSanPhamBUS _loaiSanPhamBUS = new LoaiSanPhamBUS();
        private LoaiSanPham _selectedItem;
        CauHinhBUS CauHinhBUS { get; set; } = new CauHinhBUS();

        public LoaiSanPhamViewModel()
        {
            var settings = CauHinhBUS.LayThongSoCauHinh();

            if (settings != null && settings.SoLSanPhamMoiTrang > 0)
                RowsPerPage = settings.SoLSanPhamMoiTrang;

            LoadDSLoaiSP();
            UpdatePagingCombo();
            ThemCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    MaLoai = _loaiSanPhamBUS.TaoMaMoi();
                    TenLoai = "";
                    SelectedItem = null;
                }
            );

            LuuCommand = new RelayCommand<object>(
                p =>
                {
                    if (string.IsNullOrEmpty(_maLoai))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(_tenLoai))
                    {
                        return false;
                    }

                    return true;
                }, p =>
                {
                    var old = _loaiSanPhamBUS.LayDS()
                    .Where(x => x.MaLoai == MaLoai).FirstOrDefault();

                    var input = new LoaiSanPham() { MaLoai = MaLoai, TenLoai = TenLoai };

                    if (old != null)
                    {
                        var result = _loaiSanPhamBUS.Sua(MaLoai, input);
                        if (result && SelectedItem != null)
                        {
                            SelectedItem.MaLoai = MaLoai;
                            SelectedItem.TenLoai = TenLoai;
                        }
                    } else
                    {
                        var result = _loaiSanPhamBUS.Them(input);
                        if (result) List.Add(input);
                    }
                }
            );

            XoaCommand = new RelayCommand<object>(
                p =>
                {
                    if (SelectedItem == null)
                        return false;

                    if (string.IsNullOrEmpty(MaLoai))
                        return false;


                    return true;
                }, p =>
                {
                    var result = _loaiSanPhamBUS.Xoa(MaLoai);
                    if (result == true)
                    {
                        List.Remove(SelectedItem);
                    }
                }
            );

            ComboBoxPagingSelectionChangedCommand = new RelayCommand<ComboBox>(
                p =>
                {
                    return true;
                }, p =>
                {
                    if (p != null)
                    {
                        LoadDSLoaiSP();
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
        }

        private void LoadDSLoaiSP()
        {
            var ds = _loaiSanPhamBUS.LayDSPaging(CurrentPage, RowsPerPage);
            List = new ObservableCollection<LoaiSanPham>(ds.Item1);
            OnPropertyChanged(nameof(List));
            TotalItems = ds.Item2;
            OnPropertyChanged(nameof(TotalItems));
            UpdatePagingInfo();
        }

        public LoaiSanPham SelectedItem { 
            get => _selectedItem; 
            set { 
                _selectedItem = value; 
                OnPropertyChanged(); 
                if (value != null)
                {
                    MaLoai = value.MaLoai;
                    TenLoai = value.TenLoai;
                }
             } 
        }

    }
}
