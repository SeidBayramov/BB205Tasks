using Exam_App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class User
{
    private static int _idCounter = 0;
    private static List<User> Users = new List<User>();

    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public User(string name, string surname, string password)
    {
        if (string.IsNullOrWhiteSpace(name) || ContainsDigit(name))
        {
            throw new InvalidNameException("Invalid Name");
        }
        if (string.IsNullOrWhiteSpace(surname) || ContainsDigit(surname))
        {
            throw new InvalidSurNameException("Invalid Surname");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || !password[0].ToString().Equals(password[0].ToString().ToUpper()) || !ContainsDigit(password))
        {
            throw new InvalidPasswordException("Invalid Password");
        }

        _idCounter++;
        Id = _idCounter;
        Name = name;
        Surname = surname;
        Username = GenerateUsername(name, surname);
        Password = password;

        Users.Add(this);
    }

    public void PasswordValid(string password)
    {
        if (IsPasswordValid(password))
        {
            Password = password;
        }
        else
        {
            Console.WriteLine("Invalid password. Password should contain at least 1 digit, 1 uppercase letter, 1 lowercase letter, and be at least 8 characters long.");
        }
    }

    private bool IsPasswordValid(string password)
    {
        return Regex.IsMatch(password, "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$");
    }

    private bool ContainsDigit(string input)
    {
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                return true;
            }
        }
        return false;
    }

    private string GenerateUsername(string name, string surname)
    {
        string lowercaseName = name.ToLower();
        string lowercaseSurname = surname.ToLower();
        return $"{lowercaseName}.{lowercaseSurname}";
    }

    public static bool Login(string username, string password)
    {
        User user = Users.FirstOrDefault(u => u.Username == username);

        if (user != null)
        {
            if (user.Password == password)
            {
                Console.WriteLine("Login successful.");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid password. Login failed.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("User not found. Login failed.");
            return false;
        }
    }
}