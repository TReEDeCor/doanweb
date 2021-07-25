use master
Drop Database NoiThat
----------
create database NoiThat
GO
use NoiThat
GO

CREATE TABLE NGUOIDUNG
(
	MaKH INT IDENTITY(1,1),
	HoTen nVarchar(50) NOT NULL,
	Taikhoan Varchar(50) UNIQUE,
	Matkhau Varchar(50) NOT NULL,
	Email Varchar(100) UNIQUE,
	Diachi nVarchar(200),
	Dienthoai Varchar(50),	
	Ngaysinh DATETIME,
	Gioitinh BIT
	CONSTRAINT PK_Nguoidung PRIMARY KEY(MaKH)
)
GO
INSERT INTO [dbo].[NGUOIDUNG]
([HoTen],[Taikhoan],[Matkhau],[Email],[Diachi],[Dienthoai],[Ngaysinh],[Gioitinh])
     VALUES(N'Võ Hoàng Cẩm Tú','tustus','123456a','camtu5794@gmail.com',N'Quận 9','0164412887',26-03-2000,1),
			(N'Admin','admin','123456a','admin@gmail.com',N'Quận 9','0164412887',26-03-2000,1)
GO

Create Table NHACUNGCAP
(
	MaNCC int identity(1,1),
	TenNCC nvarchar(50) NOT NULL,
	Diachi NVARCHAR(200),
	DienThoai VARCHAR(50),
	CONSTRAINT PK_Nhacungcap PRIMARY KEY(MaNCC)
)
GO
INSERT INTO [dbo].[NHACUNGCAP]([TenNCC],[Diachi],[DienThoai])
     VALUES (N'Lippy','Paris','1220202'),
			(N'Jessica','Italy','1554795'),
			(N'Merin','Belgium','2551576')
GO

Create Table LOAISANPHAM
(
	MaLoaiSP int Identity(1,1),
	TenLoaiSP nvarchar(100),
	AnhLoaiSP varchar(50),
	Mota NVarchar(Max),
	CONSTRAINT PK_Loaisanpham PRIMARY KEY(MaLoaiSP)
)
GO
INSERT INTO [dbo].[LOAISANPHAM]([TenLoaiSP],[AnhLoaiSP],[Mota])
     VALUES(N'Sopha Da Nhập Khẩu','sophaSP.jpg',N'Sopha da thật 100%. Đẳng cấp - Sang trọng. Nhẹ nhàng - Tinh tế.'),
	 (N'Bàn Trà Hiện Đại - Sang Trọng','bantraSP.jpg',N'Nhập khẩu cao cấp. Tiện nghi - Bền Bỉ. Hiện đại - Khác biệc.'),
	 (N'Bàn Ăn Cổ  Điển - Ấm Cúng','bananSP.jpg',N'Sang trọng - Phóng Khoáng. Nghệ thuật - Ấm cúng. Hiện đại - Khác biệt')
GO


CREATE TABLE SANPHAM
(
	MaSP INT IDENTITY(1,1),
	MaLoaiSP INT,
	MaNCC INT,
	TenSP NVARCHAR(100) NOT NULL,
	Giaban Decimal(18,0) CHECK (Giaban>=0),
	Mota NVarchar(Max),
	AnhSP VARCHAR(50),
	Ngaycapnhat DATETIME,
	Soluongton INT ,
	TrangThai BIT,
	CONSTRAINT FK_Loaisanpham FOREIGN KEY(MaLoaiSP) REFERENCES LOAISANPHAM(MaLoaiSP),
	CONSTRAINT FK_Nhacungcap FOREIGN KEY(MaNCC) REFERENCES NHACUNGCAP(MaNCC),
	CONSTRAINT PK_Sanpham PRIMARY KEY(MaSP),
	
)
GO
INSERT INTO [dbo].[SANPHAM] ([MaLoaiSP],[MaNCC],[TenSP],[Giaban],[Mota],[AnhSP],[Ngaycapnhat],[Soluongton],[TrangThai])
     VALUES (1,1,N'Sopha ND27',3000,N'Khích thướng, Make in USA','sp1.jpg',24-05-2021,5,1),
			(1,1,N'Sopha A26',1500,N'Khích thướng, Make in Italy','sp2.jpg',24-05-2021,5,1),
			(1,1,N'Sopha MF25',1890,N'Khích thướng, Make in USA','sp3.jpg',24-05-2021,5,1),
			(1,1,N'Sopha BK79',2000,N'Khích thướng, Make in Belgium','sp4.jpg',24-05-2021,5,1),

			(1,1,N'Sopha NWF',1570,N'Khích thướng, Make in USA','sopha1.jpg',24-05-2021,5,1),
			(1,1,N'Sopha BWE',7000,N'Khích thướng, Make in Italy','sopha2.jpg',24-05-2021,5,1),
			(1,1,N'Sopha MDG',7500,N'Khích thướng, Make in USA','sopha3.jpg',24-05-2021,5,1),
			(1,1,N'Sopha KDF',8200,N'Khích thướng, Make in Belgium','sopha4.png',24-05-2021,5,1),

			(2,1,N'Bàn SA',5900,N'Khích thướng, Make in USA','bantra1.jpg',24-05-2021,5,1),
			(2,1,N'Bàn WMD',4899,N'Khích thướng, Make in Italy','bantra2.jpg',24-05-2021,5,1),
			(2,1,N'Bàn AD72',7900,N'Khích thướng, Make in USA','bantra3.jpg',24-05-2021,5,1),
			(2,1,N'Bàn H85',3000,N'Khích thướng, Make in Belgium','bantra4.jpg',24-05-2021,5,1),

			(3,1,N'Bàn gỗ DR',2700,N'Khích thướng, Make in USA','banan1.jpg',24-05-2021,5,1),
			(3,1,N'Bàn gỗ WV',2899,N'Khích thướng, Make in Italy','banan1.jpg',24-05-2021,5,1),
			(3,1,N'Bàn gỗ HG',3000,N'Khích thướng, Make in USA','banan1.jpg',24-05-2021,5,1),
			(3,1,N'Bàn tròn trắng',6700,N'Khích thướng, Make in Belgium','banan3.jpg',24-05-2021,5,1)


GO

CREATE TABLE DONDATHANG
(
	MaDH INT IDENTITY(1,1),
	MaKH INT,
	Ngaydat Datetime,
	Ngaygiao Datetime,	
	Sdtnhanhang INT NOT NULL,
	Diachigiaohang NVARCHAR(100) NOT NULL,
	Ghichu VARCHAR(200),
	Dathanhtoan bit,
	Thanhtien Decimal(18,0),
	CONSTRAINT PK_Dondathang PRIMARY KEY(MaDH),
	CONSTRAINT FK_Nguoidung1 FOREIGN KEY(MaKH) REFERENCES NGUOIDUNG(MaKH),
)
GO

Create Table CHITIETDATHANG
(
	MaDH INT,
	MaSP INT,
	Soluong Int Check(Soluong>0),
	Dongia Decimal(18,0) Check(Dongia>=0),
	Tonggia Decimal(18,0),
	CONSTRAINT PK_CTDatHang PRIMARY KEY(MaDH,MaSP),
	CONSTRAINT FK_Dondathang FOREIGN KEY(MaDH) REFERENCES DONDATHANG(MaDH),
	CONSTRAINT FK_Sanpham1 FOREIGN KEY(MaSP) REFERENCES SANPHAM(MaSP),
)
GO
CREATE TABLE SLIDER
(
	MaSlider INT IDENTITY(1,1),
	LoaiSlider nvarchar(50),
	TenSP NVARCHAR(100),
	CapSP NVARCHAR(100),
	AnhSP VARCHAR(50),
	Vitri int,
	Noidung1 NVARCHAR(MAX),
	Noidung2 NVARCHAR(MAX),
	Noidung3 NVARCHAR(MAX),
	CONSTRAINT PK_MaSlider PRIMARY KEY(MaSlider),
)
Go
INSERT INTO [dbo].[SLIDER] ([LoaiSlider],[TenSP],[CapSP],[AnhSP],[Vitri],[Noidung1],[Noidung2],[Noidung3])
						VALUES('DMSP',N'BÀN ĂN',N'CAO CẤP','dm1.png',3,N'Sang Trọng - Phóng Khoáng',N'Nghệ Thuật - Ấm Cúng',N'Hiện Đại - Khác Biệt'),
	 ('DMSP',N'BÀN TRÀ',N'PHONG CÁCH','dm2.png',2,N'Nhập khẩu cao cấp',N'Tiện Nghi - Bền Bỉ',N'Hiện Đại - Khác Biệt'),
	 ('DMSP',N'SOPHA DA',N'CAO CẤP','dm3.png',1,N'Sopha da thật 100%',N'Đẳng cấp - Sang Trọng',N'Nhẹ Nhàng - Tinh Tế'),

	 ('Banner',N'SANG TRỌNG',N'Đẳng cấp','Tinmain1.jpg',null,N'SANG TRỌNG ĐẲNG CẤP','Thương hiệu nội thất uy tín VIỆT NAM. TReES Decor mang lại cho bạn một chiếc áo mới cho gia đình bạn .Một không gian mới .Một ý tưởng mới',null),
	 ('Banner',N'TReES Decor',N'Không gian xanh - Đậm chất riêng','Tinmain2.jpg',2,N'SKhông gian xanh . Đậm chất riêng của bạn',null,null),
	 ('Banner',N'KIỆT TÁC',N'Nhu cầu của bạn là ý tưởng nghệ thuật của chúng tôi','Tinmain3.jpg',null,N'KIỆT TÁC NGHỆ THUẬT THẾ GIỚI',N'Nhu cầu của bạn là ý tưởng nghệ thuật của chúng tôi. Mỗi tác phẩm mang trong mình cái hồn rất riêng, Những sãn phẩm chất lượng đến từ nhiều thương hiệu nổi tiếng.',null),

	 ('Slider',null,'active','ban1.jpg',null,null,null,null),
	 ('Slider',null,null,'ban2.jpg',null,null,null,null),
	 ('Slider',null,null,'ban3.jpg',null,null,null,null)
		
GO
create table Admin
(	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) NOT NULL,
	HoTen nvarchar(50),

)
GO
Insert into Admin values ('admin','123456','GiaPhuc')
Insert into Admin values ('staff','123456','GiaPhuc1')
go

create table LIENHE
(	Malh int primary key,
	HoTen nvarchar(50) NOT NULL,
	Email varchar(50),
	Phone varchar(50),
	Ghichu nvarchar(200),
)
GO



