using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.Business.Exceptions.Category
{
    public class CategoryNullException : Exception
    {
        public CategoryNullException() : base("Category object is null. Expected a valid category.")
        {
        }
        public CategoryNullException(string? message) : base(message)
        {
        }
    }
}
