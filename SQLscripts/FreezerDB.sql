use master;

go

drop database if exists Freezer;

go

create database Freezer;

go

use Freezer

--DROP table IF EXISTS Product;

--DROP table IF EXISTS Category;

--DROP table IF EXISTS ProductAmount;

--DROP table IF EXISTS StorageType;


create table Product(
PName varchar(50) not null,
Category varchar(50) not null
)

create table Category(
CName varchar(50) not null
)

create table ProductAmount(
Product varchar(50) not null,
StorageType varchar(50) not null,
Amount int not null
)

create table StorageType(
SType varchar(50) not null
)

go

alter table Product
add primary key (PName);

go

alter table category
add primary key (CName);

go

alter table StorageType
add primary key (SType);

go

alter table ProductAmount
add constraint PK_ProductAmount primary key (Product, StorageType);

go

alter table Product
add foreign key (Category) references Category(CName);

go

alter table ProductAmount
add foreign key (Product) references Product(PName);

go 

alter table ProductAmount
add foreign key (StorageType) references StorageType(SType);