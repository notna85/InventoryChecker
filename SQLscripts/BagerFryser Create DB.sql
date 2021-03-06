use master;
go

drop database if exists BagerFryser;
go

create database BagerFryser;
go

use BagerFryser
go

create table Product(
PName varchar(50) not null,
Category varchar(50) not null
)

create table Category(
CName varchar(50) not null,
ImageRef varchar(50)
)

create table ProductAmount(
Product varchar(50) not null,
StorageType varchar(50) not null,
Amount int not null
)

create table StorageType(
SType varchar(50) not null
)

create table Passwords(
Pass varchar(128) not null,
PassType varchar(50) not null,
Set_On_Valid int not null
)
go

alter table Product
add primary key (PName);
go

alter table Category
add primary key (CName);
go

alter table StorageType
add primary key (SType);
go

alter table ProductAmount
add constraint PK_ProductAmount primary key (Product, StorageType);
go

alter table Product
add foreign key (Category) references Category(CName) on delete cascade on update cascade;
go

alter table ProductAmount
add foreign key (Product) references Product(PName) on delete cascade on update cascade;
go 

alter table ProductAmount
add foreign key (StorageType) references StorageType(SType) on delete cascade on update cascade;
go

alter table Passwords
add primary key (Pass)
go