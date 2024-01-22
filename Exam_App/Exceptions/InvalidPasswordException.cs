using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_App.Exceptions
{
    internal class InvalidPasswordException:Exception
    {
        public InvalidPasswordException(string message) : base(message)
        {
            
        }
    }
}
