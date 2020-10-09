use Freezer
go

insert into StorageType (SType) values ('Kasser');
insert into StorageType (SType) values ('Pladder');
insert into StorageType (SType) values ('Stk');
insert into StorageType (SType) values ('Æsker');
insert into StorageType (SType) values ('Poser');

insert into Category (CName, ImageRef) values ('Frugt', '/Images/Fruit.png');
insert into Category (CName, ImageRef) values ('Kager', '/Images/Cake.png');
insert into Category (CName, ImageRef) values ('Brød/Dej', '/Images/Bread.png');
insert into Category (CName, ImageRef) values ('Wienerbrød', '/Images/Pastry.png');
insert into Category (CName, ImageRef) values ('Halvfabrikata', '/Images/SemiFinished.png');
insert into Category (CName, ImageRef) values ('Morgenbrød', '/Images/BreakfastBread.png');
insert into Category (CName, ImageRef) values ('Kransekage', '/Images/Kransekage.png');
insert into Category (CName, ImageRef) values ('Mel', '/Images/Flour.png');
insert into Category (CName, ImageRef) values ('Div/Fast food', '/Images/Misc.png');
insert into Category (CName, ImageRef) values ('Grønt', '/Images/Greens.png');

insert into Passwords (Pass, PassType, Set_On_Valid) values ('428821350E9691491F616B754CD8315FB86D797AB35D843479E732EF90665324', 'App', 1);

insert into Passwords (Pass, PassType, Set_On_Valid) values ('8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918', 'Admin', 1);
go

create procedure Update_Product @oldName varchar(50), @newName varchar(50)
as
begin
	update Product set PName = @newName where PName = @oldName
end
go

create procedure Get_Storages_By_Category @category varchar(50)
as
begin
	select distinct SType from StorageType join ProductAmount on StorageType.SType = ProductAmount.StorageType join Product on Product.PName = ProductAmount.Product where Product.Category = @category
end
go

create procedure Update_Amount @amount int, @productName varchar(50), @storageType varchar(50)
as
if ((select Amount from ProductAmount where Product = @productName and StorageType = @storageType) = 0 and @amount < 0)
	begin
		return
	end
if ((select Amount from ProductAmount where Product = @productName and StorageType = @storageType) = 999 and @amount > 0)
	begin
		return
	end
begin
	update ProductAmount set Amount = Amount + @amount where Product = @productName and StorageType = @storageType
end
go

--If submitted password exists, returns 1, otherwise returns -1
create procedure Is_Login_Valid @Input varchar(128), @PassType varchar(50)
as
if exists(select Pass from Passwords where Pass = @Input and PassType = @PassType)
	begin
		update Passwords set Set_On_Valid = 1 where Set_On_Valid = 1 and PassType = @PassType --Makes SQL think a row has been affected so that it will return 1 instead of -1
	end
go