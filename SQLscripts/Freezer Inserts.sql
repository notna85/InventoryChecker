use Freezer
go

insert into StorageType (SType) values ('Kasser');
insert into StorageType (SType) values ('Pladder');
insert into StorageType (SType) values ('Stk');
insert into StorageType (SType) values ('Æsker');

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

--unhashed password is "test"
insert into Passwords (Pass, Set_On_Valid) values ('9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08', 1);

insert into Product (PName, Category) values
('atest2', 'Grønt')
insert into ProductAmount (product, StorageType, Amount) values 
('atest2', 'æsker', 6)

update Product set Pname = 'atest3' where Pname = 'atest2'
delete from Product where PName = 'atest3'
delete from Productamount where product = 'atest2' and StorageType = 'æsker'

exec Update_Amount 1,'atest2','kasser'
exec Update_Amount -1,'b','stk'
exec Update_Amount -1,'c','kasser'

select * from ProductAmount
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
create procedure Is_Login_Valid @Input varchar(128)
as
if exists(select Pass from Passwords where Pass = @Input)
	begin
		update Passwords set Set_On_Valid = 1 where Set_On_Valid = 1 --Makes SQL think a row has been affected so that it will return 1 instead of -1
	end
go