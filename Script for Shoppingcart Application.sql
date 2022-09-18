--creating a Country table
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CountryCode')
CREATE TABLE CountryCode (
    Id INT IDENTITY(1,1),
    Country varchar(20) NOT NULL,
    code varchar(20) NOT NULL,
    PRIMARY KEY(Id)
);

--creating a Register table
IF not Exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='Registration')
CREATE TABLE Registration (
    Id INT IDENTITY(1,1),
    FirstName varchar(20) NOT NULL,
    LastName varchar(20) NOT NULL,
    Email varchar(30) NOT NULL,
    PhoneNumber varchar(20) NOT NULL,
    Password varchar(20) NOT NULL,
    isRemoved bit NOT NULL,
    PRIMARY KEY(Id)
);