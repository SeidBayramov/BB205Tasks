using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam_App.Models
{
    public static class BlogService
    {
        public static void AddBlog(Blog blog)
        {
            BlogDatabase.Blogs.Add(blog);
        }


        public static void RemoveBlog(int id)
        {
            BlogDatabase.Blogs.RemoveAll(blog => blog.Id == id);
        }

        public static Blog GetBlogById(int id)
        {
            return BlogDatabase.Blogs.FirstOrDefault(blog => blog.Id == id);
            
        }

        public static void FilterBlogs(string keyword)
        {
            List<Blog> filteredBlogs = BlogDatabase.Blogs
                .Where(blog => blog.Title.Contains(keyword) || blog.Description.Contains(keyword))
                .ToList();

            if (filteredBlogs.Count > 0)
            {
                Console.WriteLine("Filtered Blogs:");
                foreach (var blog in filteredBlogs)
                {
                    Console.WriteLine($"Id: {blog.Id}, Title: {blog.Title}");
                }
            }
            else
            {
                Console.WriteLine("No matching blogs found.");
            }
        }


        public static void GetAllBlogs()
        {
            foreach (Blog blog in BlogDatabase.Blogs)
            {
                Console.WriteLine($"Title: {blog.Title}, Description: {blog.Description}");
            }
        }

        public static void GetBlogsByValue(string value)
        {
            List<Blog> matchingBlogs = BlogDatabase.Blogs
                .Where(blog => blog.Title.Contains(value) || blog.Description.Contains(value))
                .ToList();

            if (matchingBlogs.Count > 0)
            {
                foreach (var blog in matchingBlogs)
                {
                    Console.WriteLine($"Title: {blog.Title}, Description: {blog.Description}");
                }
            }
            else
            {
                Console.WriteLine("No matching blogs found.");
            }
        }
    }
}
