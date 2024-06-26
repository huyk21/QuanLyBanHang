USE [master]
GO
/****** Object:  Database [QLBH]    Script Date: 4/15/2024 11:00:50 PM ******/
CREATE DATABASE [QLBH]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLBH', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QLBH.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLBH_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QLBH_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QLBH] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLBH].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLBH] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLBH] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLBH] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLBH] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLBH] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLBH] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLBH] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLBH] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLBH] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLBH] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLBH] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLBH] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLBH] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLBH] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLBH] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLBH] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLBH] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLBH] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLBH] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLBH] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLBH] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLBH] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLBH] SET RECOVERY FULL 
GO
ALTER DATABASE [QLBH] SET  MULTI_USER 
GO
ALTER DATABASE [QLBH] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLBH] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLBH] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLBH] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLBH] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLBH] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLBH', N'ON'
GO
ALTER DATABASE [QLBH] SET QUERY_STORE = ON
GO
ALTER DATABASE [QLBH] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QLBH]
GO
/****** Object:  User [tester]    Script Date: 4/15/2024 11:00:50 PM ******/
CREATE USER [tester] FOR LOGIN [tester] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[CAUHINH]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAUHINH](
	[SoLSanPhamMoiTrang] [int] NULL,
	[MoLaiManHinhCuoi] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTHOADON]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTHOADON](
	[MAHD] [nvarchar](20) NOT NULL,
	[MASP] [nvarchar](20) NOT NULL,
	[DONGIA] [numeric](18, 4) NULL,
	[SL] [numeric](10, 0) NULL,
	[TIENTHUE] [numeric](18, 4) NULL,
 CONSTRAINT [PK_CTHOADON] PRIMARY KEY CLUSTERED 
(
	[MAHD] ASC,
	[MASP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOADON]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[MAHD] [nvarchar](20) NOT NULL,
	[NGAYLAP] [datetime] NULL,
	[NGAYXUAT] [datetime] NULL,
	[MANV] [nvarchar](20) NULL,
	[MAKH] [nvarchar](20) NULL,
	[TONGTIENTRUOCTHUE] [numeric](18, 4) NULL,
	[TIENTHUE] [numeric](18, 4) NULL,
	[TONGTIENSAUTHUE] [numeric](18, 4) NULL,
	[CHIETKHAU] [numeric](18, 4) NULL,
	[TONGTIENPHAITRA] [numeric](18, 4) NULL,
 CONSTRAINT [PK_HOADON] PRIMARY KEY CLUSTERED 
(
	[MAHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MAKH] [nvarchar](20) NOT NULL,
	[TENKH] [nvarchar](100) NULL,
	[SDT] [nvarchar](50) NULL,
	[DIACHI] [nvarchar](100) NULL,
	[EMAIL] [nvarchar](100) NULL,
	[DIEMTICHLUY] [int] NULL,
	[NGAYSINH] [datetime] NULL,
 CONSTRAINT [PK_KHACHHANG] PRIMARY KEY CLUSTERED 
(
	[MAKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOAISANPHAM]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAISANPHAM](
	[MALOAI] [nvarchar](20) NOT NULL,
	[TENLOAI] [nvarchar](100) NULL,
 CONSTRAINT [PK_DANHMUCSANPHAM] PRIMARY KEY CLUSTERED 
(
	[MALOAI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MANV] [nvarchar](20) NOT NULL,
	[HOTEN] [nvarchar](100) NOT NULL,
	[SDT] [nvarchar](50) NULL,
	[DIACHI] [nvarchar](100) NULL,
	[EMAIL] [nvarchar](100) NULL,
	[NGAYSINH] [datetime] NULL,
 CONSTRAINT [PK_NHANVIEN] PRIMARY KEY CLUSTERED 
(
	[MANV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QUYENTRUYCAP]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUYENTRUYCAP](
	[MAQUYEN] [nvarchar](20) NOT NULL,
	[TENQUYEN] [nvarchar](50) NULL,
 CONSTRAINT [PK_QUYENTRUYCAP] PRIMARY KEY CLUSTERED 
(
	[MAQUYEN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SANPHAM]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SANPHAM](
	[MASP] [nvarchar](20) NOT NULL,
	[TENSP] [nvarchar](100) NULL,
	[MOTA] [nvarchar](200) NULL,
	[DVT] [nvarchar](100) NULL,
	[HINHANH] [nvarchar](200) NULL,
	[DONGIA] [numeric](18, 0) NULL,
	[SOLUONG] [int] NULL,
	[MALOAI] [nvarchar](20) NULL,
 CONSTRAINT [PK_SANPHAM] PRIMARY KEY CLUSTERED 
(
	[MASP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 4/15/2024 11:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[MATK] [nvarchar](20) NOT NULL,
	[MATKHAU] [nvarchar](200) NULL,
	[NGAYLAP] [datetime] NULL,
	[THOIGIANDANGNHAP] [datetime] NULL,
	[THOIGIANDANGXUAT] [datetime] NULL,
	[MAQUYEN] [nvarchar](20) NULL,
	[MANV] [nvarchar](20) NULL,
 CONSTRAINT [PK_TAIKHOAN] PRIMARY KEY CLUSTERED 
(
	[MATK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CAUHINH] ([SoLSanPhamMoiTrang], [MoLaiManHinhCuoi]) VALUES (4, 1)
GO
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1656127969', N'SP001', CAST(50000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(5000.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1656130556', N'SP001', CAST(50000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(5000.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1656130715', N'SP001', CAST(50000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(5000.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1656130761', N'SP001', CAST(50000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(5000.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1656131344', N'SP001', CAST(50000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(5000.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1656132889', N'SP001', CAST(50000.0000 AS Numeric(18, 4)), CAST(2 AS Numeric(10, 0)), CAST(5000.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1662597814', N'SP001', CAST(50000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(5000.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1662597815', N'SP001', CAST(100000.0000 AS Numeric(18, 4)), CAST(5 AS Numeric(10, 0)), CAST(0.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1662597815', N'SP002', CAST(50000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(0.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1662597816', N'SP001', CAST(100000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(0.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1662597817', N'SP002', CAST(50000.0000 AS Numeric(18, 4)), CAST(1 AS Numeric(10, 0)), CAST(0.0000 AS Numeric(18, 4)))
INSERT [dbo].[CTHOADON] ([MAHD], [MASP], [DONGIA], [SL], [TIENTHUE]) VALUES (N'HD1662597819', N'SP004', CAST(20000.0000 AS Numeric(18, 4)), CAST(10 AS Numeric(10, 0)), CAST(0.0000 AS Numeric(18, 4)))
GO
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1656127969', CAST(N'2022-06-25T00:00:00.000' AS DateTime), CAST(N'2022-06-25T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(50000.0000 AS Numeric(18, 4)), CAST(5000.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1656130556', CAST(N'2022-06-25T00:00:00.000' AS DateTime), CAST(N'2022-06-25T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(50000.0000 AS Numeric(18, 4)), CAST(5000.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1656130715', CAST(N'2022-06-25T00:00:00.000' AS DateTime), CAST(N'2022-06-25T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(50000.0000 AS Numeric(18, 4)), CAST(5000.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1656130761', CAST(N'2022-06-25T00:00:00.000' AS DateTime), CAST(N'2022-06-25T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(50000.0000 AS Numeric(18, 4)), CAST(5000.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1656131344', CAST(N'2022-06-25T00:00:00.000' AS DateTime), CAST(N'2022-06-25T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(50000.0000 AS Numeric(18, 4)), CAST(5000.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1656132889', CAST(N'2023-05-17T00:00:00.000' AS DateTime), CAST(N'2022-06-25T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(100000.0000 AS Numeric(18, 4)), CAST(10000.0000 AS Numeric(18, 4)), CAST(110000.0000 AS Numeric(18, 4)), CAST(10000.0000 AS Numeric(18, 4)), CAST(100000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1662597814', CAST(N'2023-05-18T00:00:00.000' AS DateTime), CAST(N'2022-09-08T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(50000.0000 AS Numeric(18, 4)), CAST(5000.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(55000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1662597815', CAST(N'2023-05-19T00:00:00.000' AS DateTime), CAST(N'1970-01-01T00:00:00.000' AS DateTime), N'NV003', N'KH002', CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(550000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1662597816', CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(N'1970-01-01T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(100000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1662597817', CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(N'1970-01-01T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(50000.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1662597818', CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(N'1970-01-01T00:00:00.000' AS DateTime), N'NV001', N'KH002', CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)))
INSERT [dbo].[HOADON] ([MAHD], [NGAYLAP], [NGAYXUAT], [MANV], [MAKH], [TONGTIENTRUOCTHUE], [TIENTHUE], [TONGTIENSAUTHUE], [CHIETKHAU], [TONGTIENPHAITRA]) VALUES (N'HD1662597819', CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(N'1970-01-01T00:00:00.000' AS DateTime), N'NV001', N'KH000', CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(0.0000 AS Numeric(18, 4)), CAST(200000.0000 AS Numeric(18, 4)))
GO
INSERT [dbo].[KHACHHANG] ([MAKH], [TENKH], [SDT], [DIACHI], [EMAIL], [DIEMTICHLUY], [NGAYSINH]) VALUES (N'KH000', N'Khách lẻ', N'03943949', N'Hà Nội', N'un@gmail.com', 0, CAST(N'1990-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[KHACHHANG] ([MAKH], [TENKH], [SDT], [DIACHI], [EMAIL], [DIEMTICHLUY], [NGAYSINH]) VALUES (N'KH002', N'Tuyết Nhung', N'0907999777', N'TP HCM', N'tuyet@gmail.com', 0, CAST(N'1999-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[KHACHHANG] ([MAKH], [TENKH], [SDT], [DIACHI], [EMAIL], [DIEMTICHLUY], [NGAYSINH]) VALUES (N'NKH003', N'Nga', N'', N'', N'', 0, CAST(N'2024-04-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[LOAISANPHAM] ([MALOAI], [TENLOAI]) VALUES (N'DM001', N'Tiêu dùng')
INSERT [dbo].[LOAISANPHAM] ([MALOAI], [TENLOAI]) VALUES (N'DM002', N'Điện tử 1')
INSERT [dbo].[LOAISANPHAM] ([MALOAI], [TENLOAI]) VALUES (N'DM003', N'Tươi sống')
INSERT [dbo].[LOAISANPHAM] ([MALOAI], [TENLOAI]) VALUES (N'DM004', N'Trái cây')
GO
INSERT [dbo].[NHANVIEN] ([MANV], [HOTEN], [SDT], [DIACHI], [EMAIL], [NGAYSINH]) VALUES (N'NV001', N'Tu', N'090887996', N'TP HCM', N'trang@gmail.com', CAST(N'1990-07-28T00:00:00.000' AS DateTime))
INSERT [dbo].[NHANVIEN] ([MANV], [HOTEN], [SDT], [DIACHI], [EMAIL], [NGAYSINH]) VALUES (N'NV002', N'Quân', N'0979777999', N'Hà Nội', N'quan@gmail.com', CAST(N'2023-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[NHANVIEN] ([MANV], [HOTEN], [SDT], [DIACHI], [EMAIL], [NGAYSINH]) VALUES (N'NV003', N'Trưng', N'0979777444', N'TP HCM', N'trong@gmail.com', CAST(N'2023-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[NHANVIEN] ([MANV], [HOTEN], [SDT], [DIACHI], [EMAIL], [NGAYSINH]) VALUES (N'NV004', N'Đạt', N'0979779999', N'Hải Phòng', N'dat@gmail.com', CAST(N'2023-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[NHANVIEN] ([MANV], [HOTEN], [SDT], [DIACHI], [EMAIL], [NGAYSINH]) VALUES (N'NV005', N'Tuấn', N'0927934739', N'Hà Nội', N'tuan@gmail.com', CAST(N'2023-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[NHANVIEN] ([MANV], [HOTEN], [SDT], [DIACHI], [EMAIL], [NGAYSINH]) VALUES (N'NV006', N'Minh', N'02947522343', N'HCM', N'manh@gmail.com', CAST(N'2023-05-11T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[QUYENTRUYCAP] ([MAQUYEN], [TENQUYEN]) VALUES (N'Q001', N'Quản trị')
INSERT [dbo].[QUYENTRUYCAP] ([MAQUYEN], [TENQUYEN]) VALUES (N'Q002', N'Người dùng')
GO
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP001', N'Áo len', N'Áo len', N'Chiếc', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\aothun.jpg', CAST(10000 AS Numeric(18, 0)), 2, N'DM001')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP002', N'Áo khoác', N'Áo khoác', N'Chiếc', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\aosomi.jpg', CAST(50000 AS Numeric(18, 0)), 0, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP004', N'Quần Jeans', N'Quần denim', N'Chiếc', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\quanjeans.jpg', CAST(20000 AS Numeric(18, 0)), 5, N'DM001')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP005', N'Nón Snapback', N'Nón thời trang', N'Chiếc', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\nonsnapback.jpg', CAST(8000 AS Numeric(18, 0)), 20, N'DM001')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP006', N'Balo Du Lịch', N'Balo phượt', N'Cái', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\balo.jpg', CAST(50000 AS Numeric(18, 0)), 1, N'DM001')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP007', N'Áo Sơ Mi', N'Áo Sơ Mi trắng', N'Chiếc', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\aosomi.jpg', CAST(15000 AS Numeric(18, 0)), 12, N'DM001')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP008', N'Quần Khaki', N'Quần vải Khaki', N'Chiếc', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\quankhaki.jpg', CAST(25000 AS Numeric(18, 0)), 8, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP010', N'Xiaomi', N'Điện thoại xiaomi', N'Cái', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\dienthoai.jpg', CAST(300000 AS Numeric(18, 0)), 30, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP011', N'Iphone14', N'Điện thoại', N'Cái', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\dienthoai.jpg', CAST(60000 AS Numeric(18, 0)), 25, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP012', N'Samsung', N'Điện thoại', N'Cái', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\dienthoai.jpg', CAST(40000 AS Numeric(18, 0)), 10, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP013', N'Samsung2', N'Điện thoại', N'Cái', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\dienthoai.jpg', CAST(5000000 AS Numeric(18, 0)), 20, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP015', N'Samsung4', N'Điện thoại', N'Cái', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\dienthoai.jpg', CAST(150000 AS Numeric(18, 0)), 15, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP016', N'Xiaomi2', N'Điện thoại', N'Cái', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\dienthoai.jpg', CAST(700000 AS Numeric(18, 0)), 10, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP017', N'Xiaomi3', N'Điện thoại', N'Cái', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\dienthoai.jpg', CAST(1200000 AS Numeric(18, 0)), 4, N'DM002')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP019', N'Áo khoác2', N'áo', N'Chiếc', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\aosomi.jpg', CAST(1231232 AS Numeric(18, 0)), 10, N'DM001')
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [MOTA], [DVT], [HINHANH], [DONGIA], [SOLUONG], [MALOAI]) VALUES (N'SP020', N'áo khoác 3', N'áo', N'Chiếc', N'C:\Users\Huy\source\repos\QuanLyBanHang\QuanLyBanHang\Images\aosomi.jpg', CAST(100 AS Numeric(18, 0)), 10, N'DM001')
GO
INSERT [dbo].[TAIKHOAN] ([MATK], [MATKHAU], [NGAYLAP], [THOIGIANDANGNHAP], [THOIGIANDANGXUAT], [MAQUYEN], [MANV]) VALUES (N'NV001', N'202cb962ac59075b964b07152d234b70', CAST(N'2021-01-01T00:00:00.000' AS DateTime), NULL, NULL, N'Q001', N'NV001')
GO
ALTER TABLE [dbo].[CTHOADON]  WITH CHECK ADD  CONSTRAINT [FK_CTHOADON_HOADON] FOREIGN KEY([MAHD])
REFERENCES [dbo].[HOADON] ([MAHD])
GO
ALTER TABLE [dbo].[CTHOADON] CHECK CONSTRAINT [FK_CTHOADON_HOADON]
GO
ALTER TABLE [dbo].[CTHOADON]  WITH CHECK ADD  CONSTRAINT [FK_CTHOADON_SANPHAM] FOREIGN KEY([MASP])
REFERENCES [dbo].[SANPHAM] ([MASP])
GO
ALTER TABLE [dbo].[CTHOADON] CHECK CONSTRAINT [FK_CTHOADON_SANPHAM]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_KHACHHANG] FOREIGN KEY([MAKH])
REFERENCES [dbo].[KHACHHANG] ([MAKH])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_KHACHHANG]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_NHANVIEN]
GO
ALTER TABLE [dbo].[TAIKHOAN]  WITH CHECK ADD  CONSTRAINT [FK_TAIKHOAN_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[TAIKHOAN] CHECK CONSTRAINT [FK_TAIKHOAN_NHANVIEN]
GO
ALTER TABLE [dbo].[TAIKHOAN]  WITH CHECK ADD  CONSTRAINT [FK_TAIKHOAN_QUYENTRUYCAP] FOREIGN KEY([MAQUYEN])
REFERENCES [dbo].[QUYENTRUYCAP] ([MAQUYEN])
GO
ALTER TABLE [dbo].[TAIKHOAN] CHECK CONSTRAINT [FK_TAIKHOAN_QUYENTRUYCAP]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CTHOADON', @level2type=N'COLUMN',@level2name=N'MAHD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CTHOADON', @level2type=N'COLUMN',@level2name=N'MASP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CTHOADON', @level2type=N'COLUMN',@level2name=N'DONGIA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CTHOADON', @level2type=N'COLUMN',@level2name=N'SL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CTHOADON', @level2type=N'COLUMN',@level2name=N'TIENTHUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CTHOADON'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'MAHD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'NGAYLAP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'NGAYXUAT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'MANV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'MAKH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'TONGTIENTRUOCTHUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'TIENTHUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'TONGTIENSAUTHUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'CHIETKHAU'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON', @level2type=N'COLUMN',@level2name=N'TONGTIENPHAITRA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HOADON'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KHACHHANG', @level2type=N'COLUMN',@level2name=N'MAKH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KHACHHANG', @level2type=N'COLUMN',@level2name=N'TENKH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KHACHHANG', @level2type=N'COLUMN',@level2name=N'SDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KHACHHANG', @level2type=N'COLUMN',@level2name=N'DIACHI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KHACHHANG', @level2type=N'COLUMN',@level2name=N'EMAIL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KHACHHANG', @level2type=N'COLUMN',@level2name=N'DIEMTICHLUY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KHACHHANG', @level2type=N'COLUMN',@level2name=N'NGAYSINH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KHACHHANG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LOAISANPHAM', @level2type=N'COLUMN',@level2name=N'MALOAI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LOAISANPHAM', @level2type=N'COLUMN',@level2name=N'TENLOAI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LOAISANPHAM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NHANVIEN', @level2type=N'COLUMN',@level2name=N'MANV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NHANVIEN', @level2type=N'COLUMN',@level2name=N'HOTEN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NHANVIEN', @level2type=N'COLUMN',@level2name=N'SDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NHANVIEN', @level2type=N'COLUMN',@level2name=N'DIACHI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NHANVIEN', @level2type=N'COLUMN',@level2name=N'EMAIL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NHANVIEN', @level2type=N'COLUMN',@level2name=N'NGAYSINH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NHANVIEN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'QUYENTRUYCAP', @level2type=N'COLUMN',@level2name=N'MAQUYEN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'QUYENTRUYCAP', @level2type=N'COLUMN',@level2name=N'TENQUYEN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'QUYENTRUYCAP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SANPHAM', @level2type=N'COLUMN',@level2name=N'MASP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SANPHAM', @level2type=N'COLUMN',@level2name=N'TENSP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SANPHAM', @level2type=N'COLUMN',@level2name=N'MOTA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SANPHAM', @level2type=N'COLUMN',@level2name=N'DVT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SANPHAM', @level2type=N'COLUMN',@level2name=N'HINHANH'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SANPHAM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TAIKHOAN', @level2type=N'COLUMN',@level2name=N'MATK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TAIKHOAN', @level2type=N'COLUMN',@level2name=N'MATKHAU'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TAIKHOAN', @level2type=N'COLUMN',@level2name=N'NGAYLAP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TAIKHOAN', @level2type=N'COLUMN',@level2name=N'THOIGIANDANGNHAP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TAIKHOAN', @level2type=N'COLUMN',@level2name=N'THOIGIANDANGXUAT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TAIKHOAN', @level2type=N'COLUMN',@level2name=N'MAQUYEN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TAIKHOAN', @level2type=N'COLUMN',@level2name=N'MANV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'TRIAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TAIKHOAN'
GO
USE [master]
GO
ALTER DATABASE [QLBH] SET  READ_WRITE 
GO
