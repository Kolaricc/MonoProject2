Create table Company(
	Id UniqueIdentifier primary key default newId(),
	Name varchar(50),
	Email varchar(25)
);

Create table Adress(
	Id UniqueIdentifier primary key foreign key references Company(id) default newId(),
	Street varchar(50),
	Number int,
	City varchar(20)
	);
	
Create table Item(
	Id UniqueIdentifier primary key default newId(),
	Category varchar(20),
	Name varchar(25),
	CompanyId UniqueIdentifier foreign key references Company(Id),
	Price decimal(10,2)
);

Create table Employee(
	Id UniqueIdentifier primary key default newId(),
	FirstName varchar(15),
	LastName varchar(20)
);

Create table CompanyEmployee
(
	EmployeeId UniqueIdentifier,
	CompanyId UniqueIdentifier,

	Primary key(CompanyId,EmployeeId),
	Foreign key(CompanyId) References Company(id),
	Foreign key(EmployeeId) References Employee(id)

);
Insert into Company (Name, Email) values ('Company1','company1@mail.com')
Insert into Company (Name, Email) values ('Company2','second@mail.com')
Insert into Company (Name, Email) values ('Company3','comp3@mail.com')
Insert into Company (Name, Email) values ('Company4','forth@mail.com')
Insert into Company (Name, Email) values ('Company5','company5@mail.com')
Insert into Company (Name, Email) values ('Company6','six@mail.com')

Select * from Company

Insert into Adress values ((Select Id from Company where name = 'Company1'),'Street st.',11,'Osijek')
Insert into Adress values ((Select Id from Company where name = 'Company3'),'Lane st.',55,'Vukovar')
Insert into Adress values ((Select Id from Company where name = 'Company5'),'Street ln.',4,'Zagreb')
Insert into Adress values ((Select Id from Company where name = 'Company2'),'Lane ln.',10,'Osijek')

Select * from Adress

Insert into Item values (NewId(),'Office supplys','Paper',(Select Id from Company where name = 'Company1'),0.45)
Insert into Item values (NewId(),'Office supplys','PaperClip',(Select Id from Company where name = 'Company1'),0.99)
Insert into Item values (NewId(),'Office supplys','Eraser',(Select Id from Company where name = 'Company4'),1.00)
Insert into Item values (NewId(),'Office supplys','Marker',(Select Id from Company where name = 'Company2'),2.50)

Select * from Item

Insert into Employee values (newId(),'John','Doe')
Insert into Employee values (newId(),'Jane','Doe')
Insert into Employee values (newId(),'Jack','Jackson')
Insert into Employee values (newId(),'Johhny','Daniels')
Insert into Employee values (newId(),'Michael','Stevens')

Select * from Employee

Insert into CompanyEmployee values((Select Id from Employee where FirstName = 'John' and LastName = 'Doe'),(Select Id from Company where name = 'Company1'))
Insert into CompanyEmployee values((Select Id from Employee where FirstName = 'John' and LastName = 'Doe'),(Select Id from Company where name = 'Company2'))
Insert into CompanyEmployee values((Select Id from Employee where FirstName = 'Jane' and LastName = 'Doe'),(Select Id from Company where name = 'Company2'))
Insert into CompanyEmployee values((Select Id from Employee where FirstName = 'Jack' and LastName = 'Jackson'),(Select Id from Company where name = 'Company5'))
Insert into CompanyEmployee values((Select Id from Employee where FirstName = 'Jack' and LastName = 'Jackson'),(Select Id from Company where name = 'Company3'))
Insert into CompanyEmployee values((Select Id from Employee where FirstName = 'Michael' and LastName = 'Stevens'),(Select Id from Company where name = 'Company4'))

Select Company.Name, Adress.Street, Adress.Number, Adress.City
From Adress
Right join Company On Adress.Id = Company.Id
Order by Adress.City

Select Company.Name, Employee.FirstName, Employee.LastName
From Employee
Inner join CompanyEmployee on Employee.Id=CompanyEmployee.EmployeeId
Inner join Company on Company.id = CompanyEmployee.CompanyId
Order by Employee.FirstName

Select  Company.Name, Count(Item.Id) as 'NumberOfItems' 
From Company
Full Outer join Item on Company.Id = Item.CompanyId
Group by Company.Name
Having COUNT(Item.Id)>=1

Select Top 3 * From Company 

Select * From Company
Order by Name Desc 
Offset 2 rows fetch next 3 rows only

Delete Employee from Employee 
Left join CompanyEmployee on CompanyEmployee.EmployeeId = Employee.id
where CompanyEmployee.EmployeeId is null

Update Item
set CompanyId = 3 where id=3
