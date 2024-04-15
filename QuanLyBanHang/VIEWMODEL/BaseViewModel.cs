using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyBanHang.VIEWMODEL
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private int _currentPage = 1;
        private int _rowsPerPage = 10;
        private int _totalItems;
        private int _totalPages;
        private ObservableCollection<ComboBoxPagingItem> _comboBoxPagingList = new ObservableCollection<ComboBoxPagingItem>();
        private ComboBoxPagingItem _comboBoxPagingSelectedItem;

        public int TotalItems { get => _totalItems; set => _totalItems = value; }
        public int CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(); } }
        public int RowsPerPage { get => _rowsPerPage; set { _rowsPerPage = value; OnPropertyChanged(); } }
        public int TotalPages { get => _totalPages; set { _totalPages = value; OnPropertyChanged(); } }

        public ComboBoxPagingItem ComboBoxPagingSelectedItem { get => _comboBoxPagingSelectedItem; set { _comboBoxPagingSelectedItem = value; OnPropertyChanged(); } }
        public ICommand PrevPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand ComboBoxPagingSelectionChangedCommand { get; set; }

        public void UpdatePagingCombo()
        {
            // Nạp lại danh sách nếu chưa có
            while (ComboBoxPagingList.Count > 0)
            {
                ComboBoxPagingList.RemoveAt(0);
            }

            OnPropertyChanged(nameof(ComboBoxPagingList));

            for (int i = 1; i <= TotalPages; i++)
            {
                ComboBoxPagingList.Add(new ComboBoxPagingItem()
                {
                    Page = i,
                    Display = i + "/" + TotalPages
                });
            }
        }

        public void UpdatePagingInfo()
        {
            TotalPages = TotalItems / RowsPerPage +
                (TotalItems % RowsPerPage == 0 ? 0 : 1);   
        }


        public ObservableCollection<ComboBoxPagingItem> ComboBoxPagingList
        {
            get => _comboBoxPagingList;
            set { _comboBoxPagingList = value; OnPropertyChanged("ComboBoxPagingList"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
