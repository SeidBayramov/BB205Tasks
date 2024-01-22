using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.DTOs.Exceptions.Category
{
    public class CategoryNullException : Exception
    {
        public CategoryNullException() : base("Category null di") { }

        public CategoryNullException(string? message) : base(message)
        {
        }
    }
}
