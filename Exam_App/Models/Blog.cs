using Exam_App.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_App.Models
{
    public class Blog
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }


        private static int blogIdCounter = 0;
        private static List<Blog> Blogs = new List<Blog>();
        public BlogType Type;


        public Blog(string title, string description,BlogType blogType)
        {
           blogIdCounter++;
            Id = blogIdCounter; 
            Title = title;
            Description = description;
            this.Type = blogType;
            Blogs.Add(this);
        }
    }

}

