create database employeeDBTraining
GO
use employeeDBTraining

GO
	create table deptInfo
	(
		deptNo int identity(10, 10), -- unique number identity(starts, increment by)
		deptName varchar(20) not null, -- this should be 2 chars minimum, and should be unique
		deptLocation varchar(20) not null, -- this could only be New York, Chicago, Texas

		constraint pk_deptno primary key (deptNo), 
		constraint chk_deptName_length check (len(deptName) >= 2),
		constraint unk_deptName unique(deptName),
		constraint chk_deptLocation_values check (deptLocation in ('New York', 'Chicago', 'Texas'))

	)

	insert into deptInfo values('HR','Texas');
	insert into deptInfo values('Accountant','New York');
	insert into deptInfo values('Developer', 'Chicago');
	insert into deptInfo values('Trainer','New York');
	----------------------------------------------------------------
	GO
	create table employeeInfo
	(	
		empNo int identity(10,10),
		empName varchar(50) not null,
		empDesignation varchar(20) not null, 
		empSalary int not null,
		empIsActive bit not null,
		empDept int not null,
		empSSN char(9) not null,

		constraint pk_empNo primary key (empNo),
		constraint fk_empDept foreign key (empDept) references deptInfo(deptNo),
		constraint unk_deptType check (empDesignation in ('HR', 'Developer', 'Accountant', 'Trainer')),
		constraint chck_salary check (empSalary >= 8000 and empSalary <= 25000),
		constraint chck_nameLength check (len(empName) >= 3),
		constraint unk_empSSN unique (empSSN),

	);

	insert into employeeInfo values('Betty Draper','HR', 12000, 1, 30, 456897895);
	insert into employeeInfo values('Don Draper', 'Accountant', 8456, 1, 20, 458325698);
	insert into employeeInfo values('Peggy Olson', 'Accountant', 12456, 1, 10, 789451212);
	insert into employeeInfo values('Joannie SuchNSuch', 'Trainer', 14000, 1, 10, 236547898);
	insert into employeeInfo values('Sol Star', 'Accountant', 8600, 1, 40, 567812262);
	insert into employeeInfo values('Joanie Stubbs', 'Developer', 24000, 0, 30, 729435681);
	insert into employeeInfo values('Wild Bill Hickock', 'Trainer', 23000, 0,20, 584231565);
	insert into employeeInfo values('Calamity Jane',  'Trainer',21002, 0,10, 894256789);
	insert into employeeInfo values('Alma Garrett', 'Accountant', 16540, 1, 30, 896124565);
	insert into employeeInfo values('Dan Dority', 'Trainer', 16523, 1, 40, 456853978);
	insert into employeeInfo values('Omar Little', 'Developer', 25000, 0, 10, 321854565);
	insert into employeeInfo values('Bunk Moreland', 'HR',24000, 1, 10, 123854592);
	insert into employeeInfo values('Mr. Prezbo', 'Trainer', 8001, 1, 40,  458216987);
	insert into employeeInfo values('The Greek', 'Developer', 25000, 1, 20, 584527436);
	insert into employeeInfo values('Marlo Stanfield', 'Accountant', 25000, 1, 30, 458458795);
	insert into employeeInfo values('Avon Barksdale', 'Accountant', 25000, 0, 40, 564824932);
	insert into employeeInfo values('Stringer Bell', 'HR', 24999, 0, 20,  456287844);
	insert into employeeInfo values('Fred Flinstone', 'Trainer', 8001, 1, 30, 458264112);
	insert into employeeInfo values('Buffy Summers', 'Developer', 15000, 1, 40,  458246845);
	insert into employeeInfo values('Rupert Giles', 'Trainer', 12000, 1,20, 458621466);
	insert into employeeInfo values('William The Bloody', 'HR', 9000, 1, 10, 548886555);


	----------------------------------
	select * from deptInfo; 
	select * from employeeInfo;

	select empNo, empName from employeeInfo -- columns
	select empNo as [Employee Number], empName as Name from employeeInfo order by Name -- with alias, order by == sorting

	select empNo as [Employee Number], empName as Name from employeeInfo where empNo > 25 order by Name desc -- where == filtering
		
	select empNo as [Employee Number], empName as Name from employeeInfo where empSalary > 20000 order by Name desc

	select empNo as [Employee Number], empName as Name from employeeInfo where empSalary > 20000 and empIsActive = 1 order by Name desc

	select count(empNo) as [Total Employees] from employeeInfo

	select sum(empSalary) as [Total Employees] from employeeInfo where empDept = 20
	select sum(empSalary) as [Total Employees] from employeeInfo where empIsActive = 0


	select avg(empSalary) as [Total Employees] from employeeInfo

	select min(empSalary) as [Total Employees] from employeeInfo
	select max(empSalary) as [Total Employees] from employeeInfo

	select empDept, sum(empSalary) [Salary], count(empNo) as [Total Employees] from employeeInfo group by empDept

	select * from employeeInfo where empName like 'T%' -- starts with T   '_' and '%' are wildcard characters

	select * from employeeInfo where empName like '%T' -- ends with T

	select * from employeeInfo where empName like '_T%' -- second character as T

	select distinct empDept from employeeInfo -- returns unique values from that field/column

	select UPPER(empName) as Names from employeeInfo

	select lower(empName) as names from employeeInfo

	select SUBSTRING(empName, 1,3) from employeeInfo

	select 'Hello' + empName from employeeInfo

	select CONCAT('Hello ', substring(empName, 1, 3)) from employeeInfo

	-- task --

	-- output the email address of every employee from deptNo 20
	-- email format: firstName_first2CharactersOfDesignation@myorganization.co.us
	-- make sure address is all lowercase
	select lower(CONCAT(empName, '_', SUBSTRING(empDesignation,1,2), '@myorganization.co.us')) as 'Email' from employeeInfo 
	where empDept = 20

	select LTRIM(empName) from employeeInfo
	select LTRIM('    Nikhil)

	--drop table deptInfo;
	--drop table employeeInfo; 
	
	

	--sequence is the solution to the behavior of the deptNo ID being created, even if invalid entries are made
	-- ie if you add an invalid city, it won't appear, but the next valid entry will have an id 10+ value than it 'should'
	-- sequence object ccan also be shared by multiple tables

