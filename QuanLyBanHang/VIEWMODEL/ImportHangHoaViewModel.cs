using Aspose.Cells;
using Microsoft.Win32;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyBanHang.VIEWMODEL
{
    public class ImportHangHoaViewModel : BaseViewModel
    {
        private SanPham _selectedItem;

        public ObservableCollection<SanPham> List
        {
            get;
            set;
        }

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
            }
        }

        public ICommand TaiMauCommand { get; set; }
        public ICommand ImportCommand { get; set; }

        SanPhamBUS SanPhamBUS { get; set; } = new SanPhamBUS();
        
        public ImportHangHoaViewModel()
        {
            List = new ObservableCollection<SanPham>();

            TaiMauCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    // Get the path of the template file
                    string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
                    string templateFilePath = Path.Combine(strWorkPath, "Templates", "MauImportSanPham.xlsx");
                    
                    // Check if the template file exists
                    if (!File.Exists(templateFilePath))
                    {
                        MessageBox.Show("Template file not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Display the Save File dialog
                    var saveFileDialog = new SaveFileDialog();

                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Save Template File";
                    saveFileDialog.FileName = "MauImportSanPham.xlsx"; // Default file name

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string saveFilePath = saveFileDialog.FileName;

                        try
                        {
                            // Copy the template file to the save file path
                            File.Copy(templateFilePath, saveFilePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while saving the template file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }


                });

            ImportCommand = new RelayCommand<object>(
                p =>
                {
                    return true;
                }, p =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                    openFileDialog.Title = "Select an Excel File";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        string filePath = openFileDialog.FileName;

                        try
                        {
                            // Read the Excel file using any suitable library like EPPlus or NPOI
                            List<SanPham> excelData = ReadExcelFile(filePath);

                            foreach (var  data in excelData)
                            {
                                data.MaSp = SanPhamBUS.TaoMaMoi();

                                if (SanPhamBUS.Them(data))
                                {
                                    List.Add(data);
                                }
                            }

                            OnPropertyChanged(nameof(List));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while reading the Excel file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            );
        }

        private List<SanPham> ReadExcelFile(string filePath)
        {
            List<SanPham> ds = new List<SanPham>();
            try
            {
                // Load the Excel file using Aspose.Cells
                Workbook workbook = new Workbook(filePath);

                // Access the first worksheet
                Worksheet worksheet = workbook.Worksheets[0];

                // Read the data from line 2 onward
                for (int row = 1; row <= worksheet.Cells.MaxDataRow; row++)
                {
                    var sp = new SanPham();

                    if (worksheet.Cells[row, 0].Type != CellValueType.IsNull)
                    {
                        sp.TenSp = worksheet.Cells[row, 0].StringValue;
                    }

                    if (worksheet.Cells[row, 1].Type != CellValueType.IsNull)
                    {
                        sp.MoTa = worksheet.Cells[row, 1].StringValue;
                    }

                    if (worksheet.Cells[row, 2].Type != CellValueType.IsNull)
                    {
                        sp.DVT = worksheet.Cells[row, 2].StringValue;
                    }

                    if (worksheet.Cells[row, 3].Type != CellValueType.IsNull)
                    {
                        sp.HinhAnh = worksheet.Cells[row, 3].StringValue;
                    }

                    if (worksheet.Cells[row, 4].Type != CellValueType.IsNull)
                    {
                        sp.DonGia = (decimal) worksheet.Cells[row, 4].DoubleValue;
                    }

                    if (worksheet.Cells[row, 5].Type != CellValueType.IsNull)
                    {
                        sp.SoLuong = worksheet.Cells[row, 5].IntValue;
                    }

                    if (worksheet.Cells[row, 6].Type != CellValueType.IsNull)
                    {
                        sp.MaLoai = worksheet.Cells[row, 6].StringValue;
                    }

                    ds.Add(sp);
                }
                MessageBox.Show("Added");
            }
            catch (Exception ex)
            {
            }

            return ds;
        }
    }
}
