
USE NoiThat
GO

create table Admin
(	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) NOT NULL,
	HoTen nvarchar(50)
)
GO

Insert into Admin values ('admin','123456','GiaPhuc')
Insert into Admin values ('staff','123456','GiaPhuc1')