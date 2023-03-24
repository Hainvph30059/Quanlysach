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