create database Cinema
use Cinema

create table Movies(
Id int primary key identity,
[Name] nvarchar(50) not null,
Rate decimal(3,1) check(Rate>= 0 and Rate<=10),
DirectorId int foreign key references Directors(Id)
)

create table Directors(
Id int primary key identity,
[Name] nvarchar(50) not null,
Surname nvarchar(50) not null,
Age int check(Age>=18)
)

create table Genres(
Id int primary key identity,
[Name] nvarchar(50) not null,
MovieId int foreign key references Movies(Id)
)

create table Actors(
Id int primary key identity,
[Name] nvarchar(50) not null,
Surname nvarchar(50) not null,
Age int check(Age>=18),
DirectorId int foreign key references Directors(Id)
)

CREATE TABLE MovieActors (
Id int primary key  identity,
MovieId INT FOREIGN KEY REFERENCES Movies(Id),
ActorId INT FOREIGN KEY REFERENCES Actors(Id)
)

create table MoviesGenres(
Id int primary key identity,
MovieId int foreign key references Movies(Id),
GenreId int foreign key references Genres(Id)
)



INSERT INTO Movies (Name, Rate)
VALUES
    ('Harry Potter', 8.5),
    ('Fight Club', 7.2),
    ('Interesteller', 9.0)


INSERT INTO Directors (Name, Surname, Age)
VALUES
    ('J.K', 'Rowling', 40),
    ('David', 'Fincher', 55),
    ('Cristofer', 'Nolan', 48)


INSERT INTO Genres (Name)
VALUES
    ('Action'),
    ('Dram'),
    ('Scientc Fiction')


INSERT INTO Actors (Name, Surname, Age)
VALUES
    ('Danie', 'Radcillfe', 25),
    ('Brad', 'Pitt', 45),
    ('Matthew', 'McConaughey', 35)


INSERT INTO MovieActors (MovieId, ActorId)
VALUES (1, 1),(2,2),(3,3)

INSERT INTO MoviesGenres(MovieId,GenreId)
VALUES(1, 1),(2,2),(3,3)



--1
select [Name] from Movies
where Rate>8

--2
select [Name],Rate from Movies


--3
select 'Actor' as"Category", CONCAT([Name], ' ', Surname) as "Full Name", Age
FROM Actors
WHERE Age > 40
UNION ALL
select 'Director' as "Category", CONCAT([Name], ' ', Surname) as "Full Name", Age
FROM Directors
WHERE Age > 40

--4
create view  MovieDetails as
select M.Name as "Movie Name", M.Rate as "Movie Rate", CONCAT(D.Name, ' ', D.Surname) as "Director Full Name"
from Movies M
JOIN Directors D on M.DirectorId = D.Id

SELECT * FROM MovieDetails

--5
SELECT CONCAT(D.Name, ' ', D.Surname) as "Director Full Name", COUNT(*) as "Number of Movies Directed"
FROM Directors as D
JOIN Movies as M ON D.Id = M.Id
GROUP BY CONCAT(D.Name, ' ', D.Surname)

--6
create view MovieCastDetails as
select M.Name as "Movie Name", M.Rate as "Movie Rate", CONCAT(D.Name, ' ', D.Surname) as "Director Full Name", CONCAT(A.Name, ' ', A.Surname) as "Actor Full Name"
FROM Movies M
JOIN Directors D ON M.DirectorId = D.Id
JOIN MovieActors MA ON M.Id = MA.MovieId
JOIN Actors A ON MA.ActorId = A.Id

SELECT * FROM MovieCastDetails

--7
select CONCAT(A.Name, ' ', A.Surname) as "Actor Full Name", M.Name as "Movie Name", G.Name as "Genre Name", CONCAT(D.Name, ' ', D.Surname) as "Director Full Name", M.Rate as "Movie Rate"
FROM Actors A
join MovieActors MA on  A.Id = MA.ActorId
join Movies M on MA.MovieId = M.Id
join Genres G on M.Id = G.Id
join Directors D on M.Id= D.Id
