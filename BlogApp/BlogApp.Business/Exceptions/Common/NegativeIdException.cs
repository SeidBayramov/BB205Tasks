using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.DTOs.Exceptions.Common
{
    public class NegativeIdException : Exception
    {
        public NegativeIdException() : base("Id menfi ola bilmez") { }

        public NegativeIdException(string message) : base(message)
        {
        }
    }
}

