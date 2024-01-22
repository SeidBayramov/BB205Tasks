using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Busines.Exceptions.User
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException():base("Bele bir user yoxdu")
        {
            
        }
        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
