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
	State varchar(50),
	Name varchar(50),
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






