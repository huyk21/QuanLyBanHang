using QuanLyBanHang.VIEWMODEL;
using System;
using System.Collections.Generic;


namespace QuanLyBanHang.DTO
{
    public class QuyenTruyCap : BaseViewModel
    {
        public QuyenTruyCap()
        {
        }

        public string MaQuyen { get; set; }
        public string TenQuyen { get; set; }

    }
}