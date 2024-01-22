using Exam_App.Enum;
using System;

namespace Exam_App.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueApp = true;

            while (continueApp)
            {
                try
                {
                    Console.WriteLine("==============Menu===========>");
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Login");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter your name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter your surname: ");
                            string surname = Console.ReadLine();
                            Console.Write("Enter your password: ");
                            string password = Console.ReadLine();
                            UserService.Register(name, surname, password);
                            break;

                        case "2":
                            Console.Write("Enter your username: ");
                            string username = Console.ReadLine();
                            Console.Write("Enter your password: ");
                            string loginPassword = Console.ReadLine();
                            if (UserService.Login(username, loginPassword))
                            {
                                Console.WriteLine("Login successful!");

                                bool inBlogMenu = true;
                                while (inBlogMenu)
                                {
                                    Console.WriteLine("================BLOG MENU==================");
                                    Console.WriteLine("1. Add Blog");
                                    Console.WriteLine("2. Delete Blog");
                                    Console.WriteLine("3. Blog Detail");
                                    Console.WriteLine("4. Get All Blog");
                                    Console.WriteLine("5. Filtered Blog");
                                    Console.WriteLine("6. Exit Program");
                                    string blogChoice = Console.ReadLine();

                                    switch (blogChoice)
                                    {
                                        case "1":
                                            Console.Write("Enter Blog Title: ");
                                            string title = Console.ReadLine();
                                            Console.Write("Enter Blog Description: ");
                                            string description = Console.ReadLine();

                                            string blogType;
                                            bool validChoice = false;

                                            do
                                            {
                                                Console.WriteLine("Select Blog Type:");
                                                Console.WriteLine("1. Programming");
                                                Console.WriteLine("2. Educational");
                                                Console.WriteLine("3. Thriller");

                                                blogType = Console.ReadLine();

                                                if (blogType == "1" || blogType == "2" || blogType == "3")
                                                {
                                                    validChoice = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid choice. Please enter a valid number (1, 2, or 3) for Blog Type.");
                                                    validChoice = false;
                                                }
                                            } while (!validChoice);

                                            Blog newBlog = new Blog(title, description, (BlogType)int.Parse(blogType));
                                            BlogService.AddBlog(newBlog);
                                            Console.WriteLine("Blog added successfully.");
                                            break;

                                        case "2":
                                            Console.Write("Enter Blog ID to Delete: ");
                                            if (int.TryParse(Console.ReadLine(), out int blogId))
                                            {
                                                BlogService.RemoveBlog(blogId);
                                                Console.WriteLine("Blog deleted successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid input. Please enter a valid Blog ID.");
                                            }
                                            break;

                                        case "3":
                                            Console.Write("Enter Blog ID for Details: ");
                                            if (int.TryParse(Console.ReadLine(), out int detailId))
                                            {
                                                Blog blog = BlogService.GetBlogById(detailId);
                                                if (blog != null)
                                                {
                                                    Console.WriteLine($"Title: {blog.Title}");
                                                    Console.WriteLine($"Description: {blog.Description}");
                                                    Console.WriteLine($"Type: {blog.Type}");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Blog not found.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid input. Please enter a valid Blog ID.");
                                            }
                                            break;

                                        case "4":
                                            BlogService.GetAllBlogs();
                                            break;

                                        case "5":
                                            Console.Write("Enter a keyword to filter blogs: ");
                                            string keyword = Console.ReadLine();
                                            BlogService.FilterBlogs(keyword);
                                            break;

                                        case "6":
                                            Console.WriteLine("Exiting the program. Goodbye!");
                                            continueApp = false;
                                            break;

                                        default:
                                            Console.WriteLine("Invalid choice. Please select a valid option.");
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Login failed. Invalid username or password.");
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please choose a valid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
