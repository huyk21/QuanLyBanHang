using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyBanHang.VIEWMODEL
{
    public class TestClass : BaseViewModel
    {
        /// <summary>
        /// The number.
        /// </summary>
        private decimal number;

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public decimal Number
        {
            get => this.number;
            set
            {
                this.number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
    }

    public class BaoCaoViewModel : BaseViewModel
    {
        private DateTime? _tuNgay;
        private DateTime? _denNgay;

        public ObservableCollection<TestClass> DoanhThu
        {
            get;
            set;
        }

        public ObservableCollection<TestClass> HangHoaBan
        {
            get;
            set;
        }

        public ICommand TheoNgayCommand { get; set; }
        public ICommand TheoThangCommand { get; set; }
        public ICommand TheoNamCommand { get; set; }

        public HoaDonBUS HoaDonBUS { get; set; } = new HoaDonBUS();
        public CTHoaDonBUS CTHoaDonBUS { get; set; } = new CTHoaDonBUS();

        public BaoCaoViewModel()
        {
            
            TheoNgayCommand = new RelayCommand<object>(
            p =>
            {
                if (!TuNgay.HasValue || !DenNgay.HasValue)
                    return false;

                return true;
            }
            , p =>
            {
                TaoBaoCaoTheoNgay(TuNgay.Value, DenNgay.Value);
            });

            TheoThangCommand = new RelayCommand<object>(
            p =>
            {
                if (!TuNgay.HasValue || !DenNgay.HasValue)
                    return false;

                return true;
            }
            , p =>
            {
                TaoBaoCaoTheoThang(TuNgay.Value, DenNgay.Value);
            });

            TheoNamCommand = new RelayCommand<object>(
            p =>
            {
                if (!TuNgay.HasValue || !DenNgay.HasValue)
                    return false;

                return true;
            }
            , p =>
            {
                TaoBaoCaoTheoNam(TuNgay.Value, DenNgay.Value);
            });
        }

        private void TaoBaoCaoTheoNam(DateTime tuNgay, DateTime denNgay)
        {
            var bcDoanhThu = HoaDonBUS.LayDS().Where(x => x.NgayLap >= tuNgay && x.NgayLap <= denNgay)
               .GroupBy(x => x.NgayLap.Value.ToString("yyyy"))
               .Select(x => new TestClass()
               {
                   Category = x.Key,
                   Number = x.Sum(y => y.TongTienPhaiTra)
               }).ToList();

            this.DoanhThu = new ObservableCollection<TestClass>(bcDoanhThu);
            OnPropertyChanged(nameof(DoanhThu));

            var bcHangBan = HoaDonBUS.LayDS().Where(x => x.NgayLap >= tuNgay && x.NgayLap <= denNgay)
                .SelectMany(x => x.CthoaDon)
                .GroupBy(x => x.TenSp)
                .Select(x => new TestClass()
                {
                    Category = x.Key,
                    Number = x.Sum(y => y.Sl) ?? 0
                }).ToList();
            this.HangHoaBan = new ObservableCollection<TestClass>(bcHangBan);
            OnPropertyChanged(nameof(HangHoaBan));
        }

        private void TaoBaoCaoTheoThang(DateTime tuNgay, DateTime denNgay)
        {
            var bcDoanhThu = HoaDonBUS.LayDS().Where(x => x.NgayLap >= tuNgay && x.NgayLap <= denNgay)
               .GroupBy(x => x.NgayLap.Value.ToString("MM/yyyy"))
               .Select(x => new TestClass()
               {
                   Category = x.Key,
                   Number = x.Sum(y => y.TongTienPhaiTra)
               }).ToList();

            this.DoanhThu = new ObservableCollection<TestClass>(bcDoanhThu);
            OnPropertyChanged(nameof(DoanhThu));

            var bcHangBan = HoaDonBUS.LayDS().Where(x => x.NgayLap >= tuNgay && x.NgayLap <= denNgay)
                .SelectMany(x => x.CthoaDon)
                .GroupBy(x => x.TenSp)
                .Select(x => new TestClass()
                {
                    Category = x.Key,
                    Number = x.Sum(y => y.Sl) ?? 0
                }).ToList();
            this.HangHoaBan = new ObservableCollection<TestClass>(bcHangBan);
            OnPropertyChanged(nameof(HangHoaBan));
        }

        private void TaoBaoCaoTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            
            var bcDoanhThu = HoaDonBUS.LayDS().Where(x => x.NgayLap >= tuNgay && x.NgayLap <= denNgay)
                .GroupBy(x => x.NgayLap.Value.ToString("dd/MM/yyyy"))
                .Select(x => new TestClass()
                {
                    Category = x.Key,
                    Number = x.Sum(y => y.TongTienPhaiTra)
                }).ToList();

            this.DoanhThu = new ObservableCollection<TestClass>(bcDoanhThu);
            OnPropertyChanged(nameof(DoanhThu));

            var bcHangBan = HoaDonBUS.LayDS().Where(x => x.NgayLap >= tuNgay && x.NgayLap <= denNgay)
                .SelectMany(x => x.CthoaDon)
                .GroupBy(x => x.TenSp)
                .Select(x => new TestClass()
                {
                    Category = x.Key,
                    Number = x.Sum(y => y.Sl) ?? 0
                }).ToList();
            this.HangHoaBan = new ObservableCollection<TestClass>(bcHangBan);
            OnPropertyChanged(nameof(HangHoaBan));
        }

        public DateTime? TuNgay { get => _tuNgay; set { _tuNgay = value; OnPropertyChanged(); } }
        public DateTime? DenNgay { get => _denNgay; set { _denNgay = value; OnPropertyChanged(); } }
    }
}
