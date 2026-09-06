use master;
go

create database uChat;
go

use uChat;
go

create table Account (
	AccountId uniqueidentifier not null primary key,
	AccountNo bigint identity(1, 1) not null,
	Email nvarchar(255) not null unique,
	Username nvarchar(63) not null unique,
	Password nvarchar(511) not null,

	Nickname nvarchar(127) null,
	AccountBio nvarchar(255) null,
	GenderCode tinyint not null, -- 0: No specific, 1: Male, 2: Femail, 3: Croissant


	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset null,

	constraint CK_EmailFormat
	check (Email <> N'' and 
		Email collate Latin1_General_100_BIN2 like N'%_@_%._%' and
		Email is not null),

	constraint CK_GenderCode
	check (GenderCode in (0, 1, 2, 3))
);

create table StaffAccount (
	StaffAccountId uniqueidentifier not null primary key,
	AccountId uniqueidentifier not null foreign key references Account(AccountId),
	PermissionLevel tinyint not null check (PermissionLevel in (0, 1, 2, 3)), -- 0: basic reviewing, 1: reviewer, 2: admin, 3: root
	
	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset null
);