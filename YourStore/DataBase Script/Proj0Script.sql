

Drop Table IF Exists OrderDetails;
Drop Table IF Exists Employees;
Drop Table IF Exists Inventories;
Drop Table IF Exists RoleTypes;
Drop Table IF Exists Orders;
Drop Table IF Exists Customers;
Drop Table IF Exists Products;
Drop Table IF Exists Stores;



Create Table Products (
  ID int identity Not Null,
  ProudctName nvarchar(64) Not Null,
  Cost Money Not Null,
  ImagePath nvarchar(256) Null,
  primary key (ID),
)


Insert Into Products Values
('Playstation 1', 100, null),
('Playstation 2', 200,null),
('Playstation 3', 300,null),
('Playstation 5', 500, null),
('Playstation 6', 600,null),
('Playstation 7', 700, null),
('Playstation 8', 800, null),
('Playstation 9', 900, null),
('Playstation 10', 1300, null),
('GameBoy', 49, null),
('GameCube', 80, null),
('Switch', 599, null),
('Xbox 100', 100, null),
('Playstation 4', 500, null),
('Xbox 1', 500, null),
('Wii', 300, null),
('Die 2 times harder', 59, null),--16
('Riven: The Broken Blade', 59.99, null),
('Delete Me', 49.99, null),
('Delete Me 2', 59.99, null),
('Sam Adventure', 10, null),
('Sam Love Edition', 15, null),
('Majhong: Kung Fu Edition', 30, null),
('Majhong: Romance Edition', 30, null),
('Majhong: addiction ', 30, null),
('Liv+', 0.99, null),
('Dummy Life', 0.99, null),
('Gabe Life', 0.99, null),
('Riven: The Broken Blade 2', 59.99, null),
('Overnight', 59.99, null)--30
;

Create Table Stores(
  ID int Identity not null,
  zip int not null,
  StoreName nvarchar(64) not null,
  primary key(ID),
)
Insert Into Stores Values--10
(0,'Dom Warehouse'),
(2,'ConsoleStop'),
(1,'Gamer Life'),
(4,'Game Stop'),
(9,'Store For Fun')
;
Create Table RoleTypes(
  ID int Identity Not Null,
  RoleName nvarchar(64) Not Null,
  Primary Key (ID),
);
Insert Into RoleTypes Values--10
('Admin'),
('Store Manager'),
('Empolyee')
;

Create Table Employees(
  ID int Identity Not Null,
  FirstName nvarchar(64) Not Null,
  LastName nvarchar(64) Not Null,
  StoreID int Not Null,
  UserName nvarchar(64) unique Not Null,
  Pass nvarchar(64) Not Null,
  RoleId int Not Null,
  Zip int not null,

  Primary Key (ID),
	FOREIGN KEY (StoreID) REFERENCES Stores(ID),
	FOREIGN KEY (RoleId) REFERENCES RoleTypes(ID),

);

Insert Into Employees Values--10
('S','L',3, 'emp1','1234',1,1),
('J','Li',1, 'emp2','1234',2,2),
('D','Lin',1, 'emp3','1234',3,4),
('F','Lind',2, 'emp4','1234',3,5),
('Jack','Lindo',3, 'emp5','1234',3,6),
('Yo','Lindo',3, 'emp6','1234',3,2)


;






Create Table Inventories(
  ID int Identity not null,
  ProductID int not null ,
  Quantity int not null,
  StoreID int not null,
  primary key(ID),
     FOREIGN KEY (ProductID) REFERENCES Products(ID),
	 FOREIGN KEY (StoreID) REFERENCES Stores(ID)

);

Insert Into Inventories Values--10
(1,100,1),
(2,10,1),
(3,10,1),
(4,10,1),
(5,130,1),
(6,120,1),
(7,102,1),
(8,30,1),
(6,10,2),
(1,10,2),
(2,60,2),
(3,1,2),
(4,2,2),
(5,3,2),
(1,10,3),
(2,10,3),
(9,10,1),
(9,140,2),
(3,105,3),
(5,10,3),
(7,1,3),
(8,0,3),
(16,100,4),
(17,100,4),
(18,100,4),
(19,100,4),
(20,100,4),
(21,100,4),
(22,100,4),
(22,100,4),
(22,100,5),
(17,100,5),
(18,100,5),
(19,100,5)
;




Create Table Customers (
	ID int Identity Not Null,
	FirstName nvarchar(64) Not Null,
	LastName nvarchar(64) Not Null,
	Zip int Not Null,
	PreferLocationID int  Null,
	PreferProductID int Null ,
	UserName nvarchar(64) unique  Not Null,
	Pass nvarchar(64) Not Null,
	PRIMARY KEY (ID),
	FOREIGN KEY (PreferLocationID) REFERENCES Stores(ID) ,
	FOREIGN KEY (PreferProductID) REFERENCES Products(ID)
);

Insert Into Customers Values--10
('Sam','Lin',0, null, null, 'cust1','1234'),
('Jake','Lin',1, null, null, 'cust2','1234'),
('Dom','Lin',2, null, null, 'cust3','1234'),
('F','Lin',0, null, null, 'cust4','1234'),
('S','Lin',6, null, null, 'cust5','1234')
;
	
Create Table Orders(
  ID int Identity Not Null,
  CustomerID int Not Null,
  StoreID int Not Null,
  DateTimeOrder datetime2,  
  PRIMARY KEY (ID),
	FOREIGN KEY (CustomerID) REFERENCES Customers(ID),
	FOREIGN KEY (StoreID) REFERENCES Stores(ID)

);
Insert Into Orders Values--10
(1,1,GetDate()),
(1,1,GetDate()),
(3,2,GetDate()),
(2,2,GetDate())

;

Create Table OrderDetails(
  ID int Identity Not Null,
  ProductID int Not Null,
  Quantity int Not Null, 
  OrderID int Not Null,
  Primary Key (ID),
	FOREIGN KEY (ProductID) REFERENCES Products(ID),
	FOREIGN KEY (OrderID) REFERENCES Orders(ID),
);

Insert Into OrderDetails Values--10
(1,21,1 ),
(2,21,1 ),
(3,21,1 ),
(1,21,2 ),
(4,21,2 ),
(1,21,3 ),
(1,21,4 )

;


