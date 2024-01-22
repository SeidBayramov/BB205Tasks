using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.Business.Exceptions.Category
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException() : base("There is no such category.")
        {
        }
        public CategoryNotFoundException(string? message) : base(message)
        {
        }
    }
}
