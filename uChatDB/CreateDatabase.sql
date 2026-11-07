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
	GenderCode tinyint not null, -- 0: Male, 1: Female, 2: Croissant, 3: Rather Not To Say

	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset null,
	LastOnlineDateTime datetimeoffset null

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

create table FileData (
	FileDataId uniqueidentifier not null primary key,
	FileType tinyint not null,

	FileName nvarchar(255) not null default N'untitled',
	FileExtention nvarchar(63) null,
	MimeType nvarchar(63) null,

	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset
);

create table ServerGroup (
	ServerGroupId uniqueidentifier not null primary key,
	ServerGroupNo bigint not null identity(1, 1),
	ServerName nvarchar(63) not null,
	ServerAvatarId uniqueidentifier foreign key references FileData(FileDataId),
	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset
);

create table ChatRoom (
	ChatRoomId uniqueidentifier not null primary key,

	ChatRoomType tinyint not null,	-- 0: one-to-one chat, 1: normal group chat, 2: server-affiliated chat
	ServerGroupId uniqueidentifier foreign key references ServerGroup(ServerGroupId),

	ChatName nvarchar(63),

	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset	-- this value also shows the last message time
);

create table ChatRoomMember (
	ChatRoomMemberId uniqueidentifier not null primary key,
	AccountId uniqueidentifier not null foreign key references Account(AccountId),
	ChatRoomId uniqueidentifier not null foreign key references ChatRoom(ChatRoomId),
	
	Nickname nvarchar(63),
	
	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset,

	unique (AccountId, ChatRoomId)
);

create table ServerMember (
	ServerMemberId uniqueidentifier not null primary key,
	AccountId uniqueidentifier not null foreign key references Account(AccountId),
	ServerGroupId uniqueidentifier not null foreign key references ServerGroup(ServerGroupId),

	Nickname nvarchar(63),

	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset,

	unique (AccountId, ServerGroupId)
);

create table ChatMessage (
	ChatMessageId uniqueidentifier not null primary key,
	ChatMessageNo bigint not null identity(1, 1),

	AffiliatedType tinyint not null,	-- 0: one-to-one chat, 1: normal group chat, 2: server-affiliated
	ChatRoomId uniqueidentifier not null foreign key references ChatRoom(ChatRoomId),
	ServerGroupId uniqueidentifier foreign key references ServerGroup(ServerGroupId),
	ServerMemberId uniqueidentifier foreign key references ServerMember(ServerMemberId),
	ChatRoomMemberId uniqueidentifier foreign key references ChatRoomMember(ChatRoomMemberId),

	TextContent nvarchar(1001) null,
	ModifyStatus tinyint not null default 0,	-- 0: normal, 1: edited, 2: removed
	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset
);

create table ChatMessageAttachment (
	ChatMessageAttachmentId uniqueidentifier not null primary key,
	FileDataId uniqueidentifier not null foreign key references FileData(FileDataId),

	ChatMessageId uniqueidentifier not null foreign key references ChatMessage(ChatMessageId),

	ModifyStatus tinyint not null default 0,	-- 0:normal, 1: removed

	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset
);

create table Friend (
	FriendId uniqueidentifier not null primary key,
	AccountId uniqueidentifier not null foreign key references Account(AccountId),
	FriendAccountId uniqueidentifier not null foreign key references Account(AccountId),
	Type tinyint not null default 0,
	Status tinyint not null default 0,
	CreateDateTime datetimeoffset not null,
	UpdateDateTime datetimeoffset,
);

-- Friend type / status + theyType / theyStatus
-- when 0 / 0 + 0 / 0 : default
-- when 1 / 1 + 0 / 0 : me requested
-- when 0 / 0 + 1 / 1 : they requested
-- when 1 / 1 + 1 / 1 : friend
-- when X / 2 + X / X : me blocked them
-- when X / X + X / 2: they blocked me