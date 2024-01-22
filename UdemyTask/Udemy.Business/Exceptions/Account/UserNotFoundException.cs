using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.Business.Exceptions.Account
{
    public class UserNotFoundException:Exception
    {
        public UserNotFoundException():base("UserNameorEmail ve ya password sehvdir")
        {

        }

        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
