CREATE DATABASE Department

USE Department

CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(30) NOT NULL CHECK (LEN(Name) >= 3),
    Surname NVARCHAR(50) NOT NULL CHECK (LEN(Surname) >= 3) DEFAULT 'XXX',
    Salary INT CHECK (Salary >= 200),
    Degree NVARCHAR(10)
);

INSERT INTO Employees (Name, Surname, Salary, Degree)
VALUES ('SAID', 'BAYRAMOV', 800, 'junior'),('RUFET','QULIYEV',300,'middle'),('AYDAN','AGAYEVA',450,'junior'),('RENA','BAYRAMOVA',500,'middle'),('Rasul','Rustemli',1200,'senior')




SELECT * FROM Employees WHERE Salary < 400;

SELECT CONCAT(Name, ' ', Surname) AS Fullname FROM Employees;

SELECT Id, CONCAT(Name, ' ', Surname) AS Fullname, Salary FROM Employees


SELECT * FROM Employees WHERE Degree = 'junior'


SELECT  MAX(Salary) FROM Employees


SELECT MIN(Salary) FROM Employees 


SELECT AVG(Salary) AS Avarage_Salary FROM Employees


SELECT * FROM Employees WHERE Salary > (SELECT AVG(Salary) FROM Employees)
 

SELECT * FROM Employees WHERE Surname LIKE '%ov' OR Surname LIKE '%ova'
