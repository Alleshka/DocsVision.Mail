create database DVMail
go

use DVMail
create table UserLogin
(
	id uniqueidentifier primary key not null,
	userLogin nvarchar(40) not null unique,
	userPassword nvarchar(40) not null,
)

create table UserInfo
(
	id uniqueidentifier primary key references UserLogin(id) not null,
	FirstName nvarchar(40) not null,
	LastName nvarchar(40) not null,
)

create table Letter		
(
	id uniqueidentifier primary key not null,
	head nvarchar(40) not null,
	senddate datetime2	not null,		
	sender uniqueidentifier references UserLogin(id) not null,
	recipient uniqueidentifier references UserLogin(id) not null,	
	contentmessage nvarchar(max) not null,
	IsRead bit not null,
)		
go

--------------- #region USER -----------------------------

-- Create user
create procedure CreateUser
	@id uniqueidentifier,
	@Login nvarchar(40),
	@Password nvarchar(40), 
	@FirstName nvarchar(40),
	@LastName nvarchar(40)
as
	insert into UserLogin(id, userLogin, userPassword)
	values(@id, @Login, @Password)

	insert into UserInfo(id, FirstName, LastName)
	values(@id, @FirstName, @LastName)
go

-- Delete user
create procedure DeleteUser
	@id uniqueidentifier
as
	delete Letter where sender = @id -- Not sure
	delete UserInfo where id = @id
	delete UserLogin where id = @id
go

create procedure SignIn
	@Login nvarchar(40),
	@Password nvarchar(40)
as
	select id from UserLogin where userLogin = @Login and userPassword = @Password
go

create procedure GetUserInfo
	@id uniqueidentifier
as
	select * from UserInfo where id = @id
go

--------------- #region Letter -----------------------------

create procedure AddLetter
	@id uniqueidentifier,
	@head nvarchar(40),
	@sender uniqueidentifier,
	@recipient uniqueidentifier,
	@content nvarchar(max)
as 
	declare @currentdate datetime2
	set @currentdate = GetDate()
	insert into Letter(id, head, senddate, sender, recipient, contentmessage, IsRead)
	values (@id, @head, @currentdate, @sender, @recipient, @content, 0)
go

create procedure GetLettersFromMe
	@id uniqueidentifier
as
	select * from Letter where sender = @id
go

create procedure GetAllLettersForMe
	@id uniqueidentifier
as 
	select * from Letter where recipient = id
go

create procedure GetNewLettersFromMe
	@id uniqueidentifier
as
	select * from Letter where recipient = id and IsRead = 0
go

create procedure ReadLetter
	@id uniqueidentifier
as
	update Letter set IsRead = 1 where id = @id
go