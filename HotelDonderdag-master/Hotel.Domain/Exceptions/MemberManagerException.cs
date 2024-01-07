using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class MemberManagerException : Exception
    {
        public MemberManagerException(string? message) : base(message)
        {
        }

        public MemberManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
