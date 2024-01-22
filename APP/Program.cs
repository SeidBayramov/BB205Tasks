using System;
using System.Xml.Serialization;

namespace APP
{
    class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company("AMAZON");

            while (true)
            {
                Console.WriteLine("========Menu========");
                Console.WriteLine("1. Register a user");
                Console.WriteLine("2. Login a user");
                Console.WriteLine("3. See all users in the company");
                Console.WriteLine("4. Get one user by username");
                Console.WriteLine("5. Update user's data");
                Console.WriteLine(" a.Update name");
                Console.WriteLine(" b.Update surname");
                Console.WriteLine(" c.Update username");
                Console.WriteLine(" d.Update email");
                Console.WriteLine("6. Delete user from the company");
                Console.WriteLine("7. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter surname: ");
                        string surname = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();
                        company.Register(name, surname, password);
                        break;
                    case "2":
                        Console.Write("Enter username: ");
                        string loginUsername = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string loginPassword = Console.ReadLine();
                        company.Login(loginUsername, loginPassword);
                        break;
                    case "3":
                        var allUsers = company.GetAllUsers();
                        foreach (var user in allUsers)
                        {
                            if (user != null)
                            {
                                Console.WriteLine($"Name: {user.Name}, Surname: {user.Surname}, Username: {user.Username}, Email: {user.Email}");
                            }
                        }
                        break;
                    case "4":
                        Console.Write("Enter username: ");
                        string getUsername = Console.ReadLine();
                        var userByUsername = company.GetUserByUsername(getUsername);
                        if (userByUsername != null)
                        {
                            Console.WriteLine($"Name: {userByUsername.Name}, Surname: {userByUsername.Surname}, Username: {userByUsername.Username}, Email: {userByUsername.Email}");
                        }
                        else
                        {
                            Console.WriteLine("User not found.");
                        }
                        break;

                    case "5":
                        {
                            Console.Write("Enter update choices:  \n\r a. Update name\r\n b. Update surname\r\n c. Update username\r\n d. Update email \n");

                            string choiceA = Console.ReadLine();

                            switch (choiceA)
                            {
                                case "a":
                                    {
                                        Console.Write("Enter  username: ");
                                        string updateUsername = Console.ReadLine();
                                        Console.Write("Enter new name: ");
                                        string newName = Console.ReadLine();
                                        company.UpdateUserA(updateUsername, newName);

                                        break;
                                    }

                                case "b":

                                    {
                                        Console.Write("Enter  username: ");
                                        string updateUsername = Console.ReadLine();
                                        Console.Write("Enter new Surname: ");
                                        string newsurname = Console.ReadLine();
                                        company.UpdateUserB(updateUsername, newsurname);
                                        break;
                                    }


                                case "c":
                                    {

                                        Console.Write("Enter old username: ");
                                        string updateUsername = Console.ReadLine();
                                        Console.Write("Enter new username: ");
                                        string newusername = Console.ReadLine();
                                        company.UpdateUserC(updateUsername, newusername);
                                        break;
                                    }

                                case "d":
                                    {

                                        Console.Write("Enter  username: ");
                                        string updateUsername = Console.ReadLine();
                                        Console.Write("Enter new nameEmail: ");
                                        string newemail = Console.ReadLine();
                                        company.UpdateUserD(updateUsername, newemail);
                                        break;
                                    }

                            }
                            break;
                        }
                    case "6":
                        Console.Write("Enter username to delete: ");
                        string deleteUsername = Console.ReadLine();
                        company.DeleteUser(deleteUsername);
                        break;
                    case "7":
                        return;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }
    }
}
