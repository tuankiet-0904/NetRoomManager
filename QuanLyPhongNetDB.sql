create database QuanLyPhongNetDB
go
use QuanLyPhongNetDB
go
 
create table GroupClient
(
	GroupName nvarchar(30) primary key,
	Discription nvarchar(120) 
)
 
go
 
create table GroupUser
(
	GroupName nvarchar(30) primary key,
	TypeName varchar(30),
)
 
go
 
create table TheUser
(
	ID int primary key,
	UserName nvarchar(60),
	Type varchar(30),
	loginPass varchar(30),
	GroupUser nvarchar(30) references GroupUser(GroupName),
	PhoneNumber varchar(11),
	Email varchar(100),
)
 
go
 
create table GroupClientAndGroupUser
(
	GroupUserName nvarchar(30) references GroupUser(GroupName),
	GroupClientName nvarchar(30) references GroupClient(GroupName),
	primary key(GroupUserName, GroupClientName),
	GiaDichVu float
)
 
go
 
create table Client
(
	ClientName varchar(30) primary key,
	GroupClientName nvarchar(30) references GroupClient(GroupName),
	StatusClient varchar(50),
	Note nvarchar(100)
)
 
go
 
create table Category
(
	CategoryName nvarchar(60) primary key,
	CategotyType nvarchar(60) not null
)
 
go
 
create table Drink
(
	DrinkID int identity(1,001) primary key,
	DrinkName nvarchar(100),
	CategoryName nvarchar(60) references Category(CategoryName),
	PriceUnit float,
	UnitLot nvarchar(100),
	InventoryNumber int,
)
 
go
 
create table Food
(
	FoodID int identity(1,001) primary key,
	FoodName nvarchar(100),
	CategoryName nvarchar(60) references Category(CategoryName),
	PriceUnit float,
	UnitLot nvarchar(100),
	InventoryNumber int,
)
 
go
 
create table TheCard
(
	CardID int identity(1,001) primary key,
	CardName nvarchar(100),
	CategoryName nvarchar(60) references Category(CategoryName),
	PriceUnit float,
	UnitLot nvarchar(100),
	InventoryNumber int,
)
 
go
 
create table OrderDrink
(
	ClientName varchar(30) references Client(ClientName),
	DrinkID int references Drink(DrinkID),
	primary key(ClientName, DrinkID),
	Quantity int,
	PriceTotal float
)
 
go
 
create table OrderFood
(
	ClientName varchar(30) references Client(ClientName),
	FoodID int references Food(FoodID),
	primary key(ClientName, FoodID),
	Quantity int,
	PriceTotal float
)
 
go
 
create table OrderCard
(
	ClientName varchar(30) references Client(ClientName),
	CardID int references TheCard(CardID),
	primary key(ClientName, CardID),
	Quantity int,
	PriceTotal float
)
 
go
 
create table Member
(
	MemberID int identity(1,001) primary key,
	AccountName varchar(30),
	Password varchar(30),
	GroupUser nvarchar(30) references GroupUser(GroupName),
	CurrentTime time,
	CurrentMoney float,
	StatusAccount nvarchar(30)
)
 
go
 
create table LoginMember
(
	LoginID int primary key,
	MemberID int references Member(MemberID),
	ClientName varchar(30) references Client(ClientName),
	LoginDate date,
	StartTime time,
	UseTime time,
	LeftTime time,
)
 
go
 
create table MemberInformation
(	
	memberID int references Member(memberID),
	primary key(memberID),
	MemberName nvarchar(100),
	FoundedDate date,
	PhoneNumber varchar(11),
	MemberAddress nvarchar(100),
	Email varchar(100)
)
 
go
 
create table TransactionDiary2
(
	TD_ID int identity(1,001) primary key,
	UserID int references TheUser(ID),
	UserName nvarchar(60),
	memberID int references Member(MemberID),
	TransacDate date,
	AddTime time,
	AddMoney float,
	UseTime time,
	Note nvarchar(120)
)
 
go
 
create table Bill
(
	BillID int identity(1,001) primary key,
	FoundedUserID int references TheUser(ID),
	FoundedUserName nvarchar(60),
	FoundedDate date,
	PriceTotal float,
)
 
go

--danh Muc
insert into Category values(N'Mì gói', N'Food')
insert into Category values(N'Cơm', N'Food')
insert into Category values(N'Phở', N'Food')
insert into Category values(N'Bún', N'Food')
insert into Category values(N'Nước Ngọt', N'Drink')
insert into Category values(N'Trà', N'Drink')
insert into Category values(N'Bia', N'Drink')
insert into Category values(N'Nước tăng lực', N'Drink')
insert into Category values(N'Nước lọc', N'Drink')
insert into Category values(N'Thẻ Game', N'Card')
insert into Category values(N'Thẻ Điện Thoại', N'Card')
 
go
 
--thuc an
insert into Food values (N'Mì xào trứng',N'Mì gói',10000,N'Dĩa',15)
insert into Food values (N'Mì xào bò',N'Mì gói',20000,N'Dĩa',20)
insert into Food values (N'Mì xào xúc xích',N'Mì gói',15000,N'Dĩa',11)
insert into Food values (N'Cơm chiên trứng',N'Cơm',12000,N'Dĩa',12)
insert into Food values (N'Cơm chiên hải sản',N'Cơm',15000,N'Dĩa',14)
insert into Food values (N'Cơm chiên thịt bò',N'Cơm',20000,N'Dĩa',16)
insert into Food values (N'Bún xào',N'Bún',12000,N'Dĩa',13)
insert into Food values (N'Phở gà',N'Phở',20000,N'Tô',8)
insert into Food values (N'Phở bò',N'Phở',22000,N'Tô',6)
 
go
 
--Nuoc uong
insert into Drink values (N'7 UP',N'Nước Ngọt',10000,N'Chai',35)
insert into Drink values (N'Pepsi',N'Nước Ngọt',10000,N'Chai',25)
insert into Drink values (N'CocaCola',N'Nước Ngọt',10000,N'Chai',40)
insert into Drink values (N'Trà xanh không độ',N'Trà',12000,N'Chai',21)
insert into Drink values (N'Trà C2',N'Trà',8000,N'Chai',32)
insert into Drink values (N'Fanta',N'Nước Ngọt',10000,N'Chai',22)
insert into Drink values (N'Mirinda xá xị',N'Nước Ngọt',10000,N'Chai',20)
insert into Drink values (N'Rồng đỏ',N'Nước Ngọt',10000,N'Hủ',45)
insert into Drink values (N'Bò húc',N'Nước Ngọt',10000,N'Lon',48)
insert into Drink values (N'Sting dâu',N'Nước Ngọt',12000,N'Lon',35)
insert into Drink values (N'Sting vàng',N'Nước Ngọt',12000,N'Chai',47)
insert into Drink values (N'Red Bull',N'Nước Ngọt',12000,N'Lon',50)
insert into Drink values (N'Trà đá',N'Trà',12000,N'Ly',100)
insert into Drink values (N'Bia Tiger',N'Bia',15000,N'Lon',42)
insert into Drink values (N'Lavie',N'Nước lọc',10000,N'Chai',40)
insert into Drink values (N'Monster',N'Nước ngọt',12000,N'Lon',30)
insert into Drink values (N'Mirinda xá xị',N'Nước ngọt',12000,N'Chai',35)
insert into Drink values (N'Mountain Dew',N'Nước ngọt',20000,N'Chai',30)
insert into Drink values (N'WakeUp 247',N'Nước tăng lực',20000,N'Chai',35)
 
go
 
--TheCard cac loai
 
insert into TheCard values (N'Gate 20K',N'Thẻ Game',20000,N'Thẻ',51)
insert into TheCard values (N'Gate 30K',N'Thẻ Game',30000,N'Thẻ',60)
insert into TheCard values (N'Gate 50K',N'Thẻ Game',50000,N'Thẻ',42)
insert into TheCard values (N'Gate 100K',N'Thẻ Game',100000,N'Thẻ',22)
insert into TheCard values (N'VinaGame 20K',N'Thẻ Game',20000,N'Thẻ',35)
insert into TheCard values (N'VinaGame 50K',N'Thẻ Game',50000,N'Thẻ',39)
insert into TheCard values (N'VinaGame 100K',N'Thẻ Game',100000,N'Thẻ',21)
insert into TheCard values (N'Zing 20K',N'Thẻ Game',20000,N'Thẻ',36)
insert into TheCard values (N'Zing 50K',N'Thẻ Game',50000,N'Thẻ',25)
insert into TheCard values (N'Zing 100K',N'Thẻ Game',100000,N'Thẻ',19)
insert into TheCard values (N'VTC_Online 20K',N'Thẻ Game',20000,N'Thẻ',62)
insert into TheCard values (N'VTC_Online 50K',N'Thẻ Game',50000,N'Thẻ',35)
insert into TheCard values (N'VTC_Online 100K',N'Thẻ Game',100000,N'Thẻ',26)
insert into TheCard values (N'VTC_Online 200K',N'Thẻ Game',200000,N'Thẻ',12)
insert into TheCard values (N'Garena 20K',N'Thẻ Game',20000,N'Thẻ',80)
insert into TheCard values (N'Garena 30K',N'Thẻ Game',30000,N'Thẻ',120)
insert into TheCard values (N'Garena 50K',N'Thẻ Game',50000,N'Thẻ',110)
insert into TheCard values (N'Garena 100K',N'Thẻ Game',100000,N'Thẻ',40)
insert into TheCard values (N'Garena 200K',N'Thẻ Game',200000,N'Thẻ',25)
 
--TheCard Dien thoai
 
insert into TheCard values (N'Mobifone 10K',N'Thẻ Điện Thoại',10000,N'Thẻ',76)
insert into TheCard values (N'Mobifone 20K',N'Thẻ Điện Thoại',20000,N'Thẻ',80)
insert into TheCard values (N'Mobifone 50K',N'Thẻ Điện Thoại',50000,N'Thẻ',56)
insert into TheCard values (N'Mobifone 100K',N'Thẻ Điện Thoại',100000,N'Thẻ',40)
insert into TheCard values (N'Mobifone 200K',N'Thẻ Điện Thoại',200000,N'Thẻ',21)
insert into TheCard values (N'Vinaphone 10K',N'Thẻ Điện Thoại',10000,N'Thẻ',54)
insert into TheCard values (N'Vinaphone 20K',N'Thẻ Điện Thoại',20000,N'Thẻ',60)
insert into TheCard values (N'Vinaphone 50K',N'Thẻ Điện Thoại',50000,N'Thẻ',32)
insert into TheCard values (N'Vinaphone 100K',N'Thẻ Điện Thoại',100000,N'Thẻ',16)
insert into TheCard values (N'Viettel 10K',N'Thẻ Điện Thoại',10000,N'Thẻ',84)
insert into TheCard values (N'Viettel 20K',N'Thẻ Điện Thoại',20000,N'Thẻ',69)
insert into TheCard values (N'Viettel 50K',N'Thẻ Điện Thoại',50000,N'Thẻ',54)
insert into TheCard values (N'Viettel 100K',N'Thẻ Điện Thoại',100000,N'Thẻ',32)
insert into TheCard values (N'VietNamMobile 10K',N'Thẻ Điện Thoại',10000,N'Thẻ',45)
insert into TheCard values (N'VietNamMobile 20K',N'Thẻ Điện Thoại',20000,N'Thẻ',54)
insert into TheCard values (N'VietNamMobile 50K',N'Thẻ Điện Thoại',50000,N'Thẻ',25)
insert into TheCard values (N'VietNamMobile 100K',N'Thẻ Điện Thoại',100000,N'Thẻ',14)
 
--Nhom Nguoi Dung
insert into GroupUser values (N'Khách vãng lai','Guest')
insert into GroupUser values (N'Hội viên','Member')
insert into GroupUser values (N'Nhân viên','Staff')
insert into GroupUser values (N'Quản lý','Manager')
 
--Nguoi Dung
insert into TheUser values (0,N'Phùng Đình Dương','Manager','123456',N'Quản lý','0935014520','duong1906ltv@gmail.com')
insert into TheUser values (1,N'Mai Xuân Nhật','Staff','123456',N'Nhân viên','0354374322','mxnhatqbi01@gmail.com')
insert into TheUser values (2,N'Nguyễn Đặng Tuấn Kiệt','Staff','123',N'Nhân viên','0905676905','tuankietnk2001@gmail.com')
insert into TheUser values (3,N'Nguyễn Đức Chinh','Staff','1234',N'Nhân viên','0981669453','ducchinhbg@gmail.com')
 
--Hoi Vien
insert into Member values ('DragonSilver','1230410',N'Hội viên','5:00',20000,N'Cho Phép')
insert into Member values ('Seraphim','123',N'Hội viên','7:00',28000,N'Cho Phép')
insert into Member values ('0961563202','123',N'Hội viên','3:00',12000,N'Cho Phép')
insert into Member values ('heavenhell8899','123',N'Hội viên','10:00',40000,N'Cho Phép')
insert into Member values ('abc','123',N'Hội viên','0:00',0,N'Hết Thời Gian')
insert into Member values ('xyz','123',N'Hội viên','15:00',60000,N'Cho Phép')
insert into Member values ('tikitaka','123',N'Hội viên','0:00',0,N'Hết Thời Gian')
insert into Member values ('barca','123',N'Hội viên','0:00',0,N'Hết Thời Gian')
insert into Member values ('realmadrid','123',N'Hội viên','0:00',0,N'Hết Thời Gian')
insert into Member values ('Calomama','123',N'Hội viên','9:00',36000,N'Cho Phép')
insert into Member values ('haivl','123',N'Hội viên','12:00',48000,N'Cho Phép')
insert into Member values ('xemvn','123',N'Hội viên','8:00',32000,N'Cho Phép')
insert into Member values ('facebook','123',N'Hội viên','7:00',28000,N'Cho Phép')

--Hoi Vien Info
insert into MemberInformation values(1,null, '2021-06-21', null, null, null)
insert into MemberInformation values(2,null, '2021-06-21', null, null, null)
insert into MemberInformation values(3,null, '2021-06-21', null, null, null)
insert into MemberInformation values(4,null, '2021-06-21', null, null, null)
insert into MemberInformation values(5,null, '2021-06-21', null, null, null)
insert into MemberInformation values(6,null, '2021-06-21', null, null, null)
insert into MemberInformation values(7,null, '2021-06-21', null, null, null)
insert into MemberInformation values(8,null, '2021-06-21', null, null, null)
insert into MemberInformation values(9,null, '2021-06-21', null, null, null)
insert into MemberInformation values(10,null, '2021-06-21', null, null, null)
insert into MemberInformation values(11,null, '2021-06-21', null, null, null)
insert into MemberInformation values(12,null, '2021-06-21', null, null, null)
insert into MemberInformation values(13,null, '2021-06-21', null, null, null)
 
--Nhom may tram
insert into GroupClient values (N'Mặc Định',N'Phòng máy thường')
insert into GroupClient values (N'Máy Lạnh',N'Phòng máy lạnh')
insert into GroupClient values (N'VIP',N'Phòng vip')
insert into GroupClient values (N'Thi Đấu',N'Phòng máy dành cho giải đấu Game')
 
--May Tram
insert into Client values ('MAY1',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY2',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY3',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY4',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY5',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY6',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY7',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY8',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY9',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY10',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY11',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY12',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY13',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY14',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY15',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY16',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY17',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY18',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY19',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY20',N'Mặc Định','DISCONNECT','máy phòng thường')
insert into Client values ('MAY-1',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-2',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-3',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-4',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-5',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-6',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-7',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-8',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-9',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-10',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-11',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-12',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-13',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-14',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-15',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-16',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-17',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-18',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-19',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAY-20',N'Máy Lạnh','DISCONNECT','máy phòng máy lạnh')
insert into Client values ('MAYVIP-1',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-2',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-3',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-4',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-5',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-6',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-7',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-8',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-9',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-10',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-11',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-12',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-13',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-14',N'VIP','DISCONNECT','máy phòng VIP')
insert into Client values ('MAYVIP-15',N'VIP','DISCONNECT','máy phòng VIP')