create database ShopDb;
use ShopDb;

create table Orders (
	Id int primary key not null,
	Number nvarchar(max) not null,
	CustomerId int not null,
	EmplyeeId int not null,
	OrderDetailsId int not null,

	constraint FK_Orders_CustomerId foreign key (CustomerId)
		references Customers(Id),
	constraint FK_Orders_Employees foreign key (EmplyeeId)
		references Employees(Id),
	constraint FK_Orders_OrderDetails foreign key (OrderDetailsId)
		references OrderDetails(Id)
);

create table Customers (
	Id int primary key not null,
	[Name] nvarchar(max) not null,
	[Address] nvarchar(max) not null
)

create table Employees (
	Id int primary key not null,
	[Name] nvarchar(max) not null,
	Post nvarchar(max) not null,
	DateOfEmployment datetime not null
);

create table OrderDetails (
	Id int primary key not null,
	OrderDate datetime not null,
	Price float not null,
	ProductId int not null,

	constraint FK_OrderDetailsProduct foreign key(ProductId)
		references Products(Id)
);

create table Products (
	Id int primary key not null,
	[Name] nvarchar(max) not null,
	Price float not null,
	[Description] nvarchar(max) not null 
);