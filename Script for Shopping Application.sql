--creating a Name table
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CountryCode')
CREATE TABLE CountryCode (
    Id INT IDENTITY(1,1),
    Name varchar(20) NOT NULL,
    code varchar(20) NOT NULL,
    PRIMARY KEY(Id)
);

--creating a Roles table
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Roles')
CREATE TABLE Roles (
    Id INT IDENTITY(1,1),
    Name varchar(20) NOT NULL,
    DateCreated datetime default getdate(),
    PRIMARY KEY(Id)
);

--creating a Users table
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Users')
CREATE TABLE Users (
    Id INT IDENTITY(1,1) unique,
	AccountNumber varchar(10) not null,
    FirstName varchar(20) NOT NULL,
    LastName varchar(20) NOT NULL,
    Email varchar(30) NOT NULL,
    PhoneNumber varchar(20) NOT NULL,
    Password varchar(20) NOT NULL,
	Role varchar(25) Not null,
    isRemoved bit NOT NULL default 0,
	Address1 varchar(100),
	Address2 varchar(100),
	District int,
	State int,
	Countrty int,
	Pincode varchar(10),
    PRIMARY KEY(AccountNumber),
);

--creating a Transaction table
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Transactions')
CREATE TABLE Transactions (
    Id INT IDENTITY(1,1),
    Accountnumber varchar(10) NOT NULL,
    Type varchar(30) NOT NULL,
    Mode varchar(20) NOT NULL,
	Amount money Not null,
	TransactionTo int not null,
	Date datetime default GetDate(),
    PRIMARY KEY(Id),
	CONSTRAINT fk_Users1 FOREIGN KEY (AccountNumber)  
	REFERENCES Users(AccountNumber)
);


--creating a Otp validation
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='OTPValidation')
CREATE TABLE OTPValidation (
    Id INT IDENTITY(1,1),
    Email varchar(30) NOT NULL,
    Otp varchar(10) NOT NULL,
    Validate bit not null,
	DateCreated datetime not null,
    PRIMARY KEY(Id),
	
	);


--creating a Categerious
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Categerious')
CREATE TABLE Categerious (
    Id INT IDENTITY(1,1),
    Name varchar(20) NOT NULL,
    DateCreated datetime default getdate(),
    PRIMARY KEY(Id)
);


--creating a Products
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Products')
CREATE TABLE Products (
    Id INT IDENTITY(1,1),
    CategeoryId int NOT NULL,
	Make varchar(100) not null,
	Model varchar(100) NOT NULL,
	Price money not null,
    DateCreated datetime default getdate(),
    PRIMARY KEY(Id),
	foreign key(CategeoryId) references Categerious(Id)
);


--creating a Coupons
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Coupons')
CREATE TABLE Coupons (
    Id INT IDENTITY(1,1),
    Name varchar(20) NOT NULL,
	Validupto int not null,
    DateCreated datetime default getdate(),
    PRIMARY KEY(Id)
);

IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='States')
CREATE TABLE States (
    Id INT IDENTITY(1,1),
	Country int not null,
    StateName varchar(20) NOT NULL,
    PRIMARY KEY(Id),
	foreign key (country) references CountryCode(id)
);

IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Districts')
CREATE TABLE Districts (
    Id INT IDENTITY(1,1),
	Statecode int not null,
    District varchar(20) NOT NULL,
    PRIMARY KEY(Id),
	foreign key (Statecode) references states(id)
);