using Exam_App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exam_App.Models
{
    public static class UserService
    {
        public static void Register(string name, string surname, string password)
        {
            string enteredPassword;

            do
            {
                Console.Write("Enter a valid(main) password: ");
                enteredPassword = Console.ReadLine();

                if (IsPasswordValid(enteredPassword))
                {
                    try
                    {
                        User newUser = new User(name, surname, enteredPassword);
                        BlogDatabase.Users.Add(newUser);
                        Console.WriteLine("Successful Register.");
                        foreach (var user in BlogDatabase.Users)
                        {
                            Console.WriteLine(user.Username);
                        }
                    }
                    catch (InvalidPasswordException)
                    {
                        Console.WriteLine("Password is incorrect choice");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid password. Password should contain at least 1 digit, 1 uppercase letter, 1 lowercase letter, and be at least 8 characters long.");
                }
            } while (!IsPasswordValid(enteredPassword));
        }

        public static bool Login(string username, string password)
        {
            User user = BlogDatabase.Users.Find(u => u.Username == username);

            if (user != null && user.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsPasswordValid(string password)
        {
   
            return Regex.IsMatch(password, "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$");
        }
    }
}
