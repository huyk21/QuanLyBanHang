using QuanLyBanHang.BUS;
using QuanLyBanHang.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace QuanLyBanHang.VIEWMODEL
{
    public class LoginViewModel : BaseViewModel
    {

        private string _maTK;
        private string _matKhau;
        private bool _luuLaiMatKhau;

        TaiKhoanBUS TaiKhoanBUS { get; set; }
        public bool IsLogin { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public LoginViewModel()
        {
            TaiKhoanBUS = new TaiKhoanBUS();

            NapLaiMatKhauDaLuu();

            IsLogin = false;

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { // Debugging code
            

                MatKhau = p.Password;

               
                
            });
            CloseCommand = new RelayCommand<Window>(p => true, p =>
            {
                p.Close();
            });
        }

        private void NapLaiMatKhauDaLuu()
        {
            if (File.Exists("saved_login.txt"))
            {
                try
                {
                    string encrypted = File.ReadAllText("saved_login.txt");
                 

                    string decrypted = Encryptor.DecryptString(Globals.AES_KEY, encrypted);
                  

                    string[] parts = decrypted.Split(new[] { '|' }, StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        MaTK = parts[0];
                        MatKhau = parts[1];
                        LuuLaiMatKhau = true;
                    }
                    else
                    {
                        // Handle the case where the decrypted string does not contain expected parts.
                        MessageBox.Show("Decrypted data does not contain expected parts separated by '|'.");
                    }
                }
                catch (Exception ex)
                {
                    // Show the exception message
                    MessageBox.Show($"An error occurred while loading saved password: {ex.Message}");
                }
            }
        }




        public string MaTK { get => _maTK; set { _maTK = value; OnPropertyChanged(); } }
        public string MatKhau { get => _matKhau; set { _matKhau = value; OnPropertyChanged(); } }
        public bool LuuLaiMatKhau { get => _luuLaiMatKhau; set { _luuLaiMatKhau = value; OnPropertyChanged(); } }

        private void Login(Window p)
        {
            if (p == null)
                return;

            var tk = TaiKhoanBUS.DangNhap(MaTK, Encryptor.MD5Hash(MatKhau));

            if (tk == null)
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu !");
                IsLogin = false;
            }
            else
            {
                Globals.CurrentNhanVien = tk;
                IsLogin = true;

                if (_luuLaiMatKhau)
                {
                    LuuMatKhau();
                   
                }

                p.Close();
            }
        }

        private void LuuMatKhau()
        {
            try
            {
                string encrypted = Encryptor.EncryptString(Globals.AES_KEY, $"{MaTK}|{MatKhau}");
                File.WriteAllText("saved_login.txt", encrypted);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving credentials: {ex.Message}");
            }
        }

    }
}
