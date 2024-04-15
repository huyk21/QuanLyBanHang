using Microsoft.Xaml.Behaviors.Core;
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
    public class QLNhanVienViewModel : BaseViewModel
    {
        public ObservableCollection<NhanVien> List
        {
            get => _list;
            set { _list = value; OnPropertyChanged(); }
        }

       
        CauHinhBUS CauHinhBUS { get; set; } = new CauHinhBUS();

        NhanVienBUS NhanVienBUS { get; set; }
        public QLNhanVienViewModel()
        {
            var settings = CauHinhBUS.LayThongSoCauHinh();

            if (settings != null && settings.SoLSanPhamMoiTrang > 0)
                RowsPerPage = settings.SoLSanPhamMoiTrang;

            NhanVienBUS = new NhanVienBUS();
            LoadDSNhanVien();
            UpdatePagingCombo();
            ThemCommand = new RelayCommand<object>(
            p => true, p =>
            {
                MaNv = NhanVienBUS.TaoMaMoi();
                HoTen = "";
                SDT = "";
                DiaChi = "";
                Email = "";
                NgaySinh = DateTime.Now;
            });

            LuuCommand = new RelayCommand<object>(p =>
            {
                if (string.IsNullOrEmpty(MaNv))
                    return false;

                if (!MaNv.StartsWith("NV") || MaNv.Length != 5)
                    return false;

                if (string.IsNullOrEmpty(HoTen))
                    return false;
                return true;
            }, p =>
            {
                var obj = new NhanVien()
                {
                    MaNv = MaNv,
                    HoTen = HoTen,
                    SDT = SDT,
                    DiaChi = DiaChi,
                    Email = Email,
                    NgaySinh = NgaySinh
                };

                if (NhanVienBUS.LayNhanVien(MaNv) == null)
                {
                    var result = NhanVienBUS.Them(obj);
                    if (result)  List.Add(obj);
                }
                else 
                {
                    var result = NhanVienBUS.Sua(MaNv, obj);
                    
                    if (result && SelectedItem != null)
                    {
                        SelectedItem.HoTen = HoTen;
                        SelectedItem.SDT = SDT;
                        SelectedItem.DiaChi = DiaChi;
                        SelectedItem.Email = Email;
                        SelectedItem.NgaySinh = NgaySinh;
                    }
                }
                OnPropertyChanged("List");
            });

            XoaCommand = new RelayCommand<object>(p =>
            {
                if (string.IsNullOrEmpty(MaNv))
                    return false;

                if (NhanVienBUS.LayNhanVien(MaNv) == null)
                    return false;

                return true;
            }, p =>
            {
                NhanVienBUS.Xoa(MaNv);

                if (SelectedItem != null)
                {
                    List.Remove(SelectedItem);
                    MaNv = "";
                    HoTen = "";
                    SDT = "";
                    DiaChi = "";
                    Email = "";
                    NgaySinh = DateTime.Now;
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
                        LoadDSNhanVien();
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

        private void LoadDSNhanVien()
        {
            var ds = NhanVienBUS.LayDSPaging(CurrentPage, RowsPerPage);
            List = new ObservableCollection<NhanVien>(ds.Item1);
            OnPropertyChanged(nameof(List));
            TotalItems = ds.Item2;
            UpdatePagingInfo();
        }

        private NhanVien _selectedNhanVien;
        private ObservableCollection<NhanVien> _list;
        private string _maNv;
        private string _hoTen;
        private string _sDT;
        private string _diaChi;
        private string _email;
        private DateTime? _ngaySinh;

        public NhanVien SelectedItem
        {
            get
            {
                return _selectedNhanVien;
            }

            set
            {
                _selectedNhanVien = value;
                OnPropertyChanged();

                if (_selectedNhanVien != null)
                {
                    MaNv = _selectedNhanVien.MaNv;
                    HoTen = _selectedNhanVien.HoTen;
                    SDT = _selectedNhanVien.SDT;
                    DiaChi = _selectedNhanVien.DiaChi;
                    Email = _selectedNhanVien.Email;
                    NgaySinh = _selectedNhanVien.NgaySinh;
                }
            }
        }

        public string MaNv { get => _maNv; set { _maNv = value; OnPropertyChanged(); } }
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        public string SDT { get => _sDT; set { _sDT = value; OnPropertyChanged(); } }
        public string DiaChi { get => _diaChi; set { _diaChi = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public DateTime? NgaySinh { get => _ngaySinh; set { _ngaySinh = value; OnPropertyChanged(); } }

        public ICommand ThemCommand { get; set; }
        public ICommand LuuCommand { get; set; }
        public ICommand XoaCommand { get; set; }
    }
}
