create database DVTestMail
go

---------------------------------------------------------------

create table Employee
(
	id uniqueidentifier primary key not null,
	Login nvarchar(40) not null,
	Password nvarchar(40) not null,
	FirstName nvarchar(40),
	LastName nvarchar(40)
)

create table Letter
(	
	id uniqueidentifier primary key not null,
	head nvarchar(40) not null,
	contentmessage nvarchar(max) not null, 
	sender uniqueidentifier references Employee(id) not null,
	createdate smalldatetime not null,
)

create table Recepient
(
	LetterID uniqueidentifier references Letter(id),
	EmployeeID uniqueidentifier references Employee(id),
	senddate smalldatetime not null,
	isRead bit not null
	primary key(LetterID, EmployeeID)
)
go

---------------------------------------------------------------------

-- #Region user

create procedure CreateUser
	@id uniqueidentifier,
	@Login nvarchar(40),
	@Password nvarchar(40),
	@FirstName nvarchar(40) = '',
	@LastName nvarchar(40) = ''
as
	insert into Employee values (@id, @Login, @Password, @FirstName, @LastName)
	select id, Login, FirstName, LastName from Employee where id = @id
go	

create procedure SignIn
	@Login nvarchar(40),
	@Password nvarchar(40)
as
	select id, login, FirstName, LastName From Employee where login = @Login and Password = @Password
go

create procedure GetUserInfo
	@id uniqueidentifier
as
	select id, login, FirstName, LastName from Employee where id = @id
go
	
-- #Region letter
create procedure CreateLetter
	@id uniqueidentifier,
	@head nvarchar(40),
	@contentmessage nvarchar(max),
	@sender uniqueidentifier
as
	declare @datecreate smalldatetime
	set @datecreate = Cast(GetDate() as smalldatetime)
	insert into Letter values (@id, @head, @contentmessage, @sender, @datecreate)
	select * from Letter where id = @id
go

create procedure SendLetter
	@letterID uniqueidentifier,
	@empoyeeID uniqueidentifier
as
	declare @datesend smalldatetime
	set @datesend = Cast(GetDate() as smalldatetime)
	insert into Recepient(@letterID, @empoyeeID, @datesend, 0)
	
	
create procedure ReadLetter
	@letterID uniqueidentifier,
	@userID uniqueidentifier
as
	update Recepient set IsRead = 1 where LetterID = @leterID and EmployeeID = @userID
	
	
create procedure GetNewLetters
	@userID uniqueidentifier
as
	select l.id, l.head, l.contentmessage, l.sender, r.senddate from Letter as l join Recepient as r on l.id = r.id where r.empoyeeID = @userID and r.IsRead = 0
	
create procedure GetAllLetters
	@userID uniqueidentifier
as
	select l.id, l.head, l.contentmessage, l.sender, r.senddate from Letter as l join Recepient as r on l.id = r.id where r.EmployeeID = @userID
	

create procedure GetSendedLetters
	@userID uniqueidentifier
as 
	select l.id, l.head, l.contentmessage, l.sender, r.senddate from Letter as l join Recepient as r on l.id = r.id where l.sender = @userID