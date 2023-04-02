create database LOGINs
use LOGINs
create table ACOUNT(
UserLogin varchar(20) not null,
UserName nvarchar(47),
Passwords varchar(20) not null,
PowerAcount bit default 0,
primary key(UserLogin)
)
insert into ACOUNT values
('Admin','admin','admin',1),
('Nhanvien',N'Phạm Văn A','123',default)
select * from  acount
create table book
(
	ID int identity primary key,
	bookName nvarchar(100) not null,
	author nvarchar(100) not null, --tác giả
	bookLoan float not null,--giá thuê/ngày
	bookPrice float not null,-- giá bán
	totalQuantity int not null, --số lượng
	actualQuantity int  not null, -- số lượng trong kho
	bookType nvarchar(100)
)