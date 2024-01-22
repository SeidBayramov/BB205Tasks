CREATE DATABASE BB205Relations
USE BB205Relations

CREATE TABLE ROLES (
    RoleId INT PRIMARY KEY identity,
    Name NVARCHAR(50) NOT NULL
)

CREATE TABLE USERS (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50),
	[Password] nvarchar(50),
    RoleId INT FOREIGN KEY  REFERENCES ROLES(RoleID)
)

insert into ROLES (Name) 
values ('admin'), ('moderator'), ('user')

insert into USERS(Username,[Password],RoleId)
values ('seidbayramov', 'seid2004', 1), 
('rufoquliyev','rufo1234', 2), 
('feridbld','ferid2004', 3),
('fidanalizade','fdn2345',2)

SELECT Users.Username, Roles.Name AS Role
FROM USERS
JOIN ROLES ON Users.RoleID = Roles.RoleID
