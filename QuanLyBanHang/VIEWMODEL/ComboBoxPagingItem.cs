using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.VIEWMODEL
{
    public class ComboBoxPagingItem : BaseViewModel
    {
        private int _page;
        private string _display;

        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public string Display { get => _display; set { _display = value; OnPropertyChanged(); } }
    }
}
