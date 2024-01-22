using Microsoft.VisualBasic;
using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace APP
{
    class Company
    {
        public string Name { get; set; }
        public User[] user = new User[100];
        private int userCount = 0;

        public Company(string name)
        {
            Name = name;
        }
        public void Register(string name, string surname, string password)
        {
            if (userCount >= user.Length)
            {
                Console.WriteLine("User storage is full. Cannot register more users.");
                return;
            }

            if (!Regex.IsMatch(name, "^[A-Za-z]+$"))
            {
                Console.WriteLine("Invalid characters in the name. Only letters are allowed.");
                return;
            }

            if (!Regex.IsMatch(surname, "^[A-Za-z]+$"))
            {
                Console.WriteLine("Invalid characters in the surname. Only letters are allowed.");
                return;
            }

            string email = $"{name.ToLower()}.{surname.ToLower()}@gmail.com";
            string username = $"{name.ToLower()}.{surname.ToLower()}";


            if (!Regex.IsMatch(password, "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$"))
            {
                Console.WriteLine("Password should contain at least 1 digit, 1 uppercase letter, 1 lowercase letter, and be at least 8 characters long.");
                return;
            }

            foreach (User u in user)
            {
                if (u.Username == username)
                {
                    Console.WriteLine("Username already exists. Please choose a different one.");
                    return;
                }
            }

            User newUser = new User
            {
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                Password = password
            };

            user[userCount] = newUser;
            userCount++;
            Console.WriteLine("User registered successfully.");
        
    }

        public bool Login(string username, string password)
        {
            foreach (User u in user)
            {
                if (u.Username == username && u.Password == password)
                {
                    Console.WriteLine("Login successful.");
                    return true;
                }
            }

            Console.WriteLine("Username or password is incorrect.");
            return false;
        }

        public User[] GetAllUsers()
        {
            return user;
        }

        public User GetUserByUsername(string username)
        {
            foreach (User u in user)
            {
                if (u != null && u.Username == username)
                {
                    return u;
                }
            }

            return null;
        }
        public void UpdateUserA(string username, string newName)
        {
            for (int i = 0; i < userCount; i++)
            {
                if (user[i] != null && user[i].Username == username)
                {
                    user[i].Name = newName;
                    Console.WriteLine("User data updated successfully.");
                    Console.WriteLine($"New Name:{newName}");
                    return;
                }
            }

            Console.WriteLine("User not found.");
        }
        public void UpdateUserB(string username, string newSurname)
        {
            for (int i = 0; i < userCount; i++)
            {
                if (user[i] != null && user[i].Username == username)
                {
                    user[i].Surname = newSurname;
                    Console.WriteLine("User data updated successfully.");
                    Console.WriteLine($"New Name:{newSurname}");
                    return;
                }
            }

            Console.WriteLine("User not found.");
        }
        public void UpdateUserC(string username, string newUsername)
        {
            for (int i = 0; i < userCount; i++)
            {
                if (user[i] != null && user[i].Username == username)
                {
                    user[i].Username =newUsername;
                    Console.WriteLine("User data updated successfully.");
                    return;
                }
            }

            Console.WriteLine("User not found.");
        }
        public void UpdateUserD(string username, string newEmail)
        {
            for (int i = 0; i < userCount; i++)
            {
                if (user[i] != null && user[i].Username == username)
                {
                    user[i].Email = newEmail;
                    Console.WriteLine("User data updated successfully.");
                    Console.WriteLine($"New email:{newEmail}");
                    return;
                }
            }

            Console.WriteLine("User not found.");
        }
        public void DeleteUser(string username)
        {
            for (int i = 0; i < userCount; i++)
            {
                if (user[i] != null && user[i].Username == username)
                {
                    for (int j = i; j < userCount - 1; j++)
                    {
                        user[j] = user[j + 1];
                    }

                    user[userCount - 1] = null;
                    userCount--;
                    Console.WriteLine("User deleted.");
                    return;
                }
            }

            Console.WriteLine("User not found.");
        }
    }
}

