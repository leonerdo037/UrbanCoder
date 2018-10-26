using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanCoder.Classes
{
    class Exceptions
    {
        public class UC_Exception : Exception
        {
            public UC_Exception(string message) : base(message) { }
        }
        public class UC_LoginFailed : Exception
        {
            public UC_LoginFailed(string message) : base(message) { }
        }
        public class UC_TeamNotFound : Exception
        {
            public UC_TeamNotFound(string message) : base(message) { }
        }
        public class UC_UnknownUser : Exception
        {
            public UC_UnknownUser(string message) : base(message) { }
        }
        public class UC_ResourceNotFound : Exception
        {
            public UC_ResourceNotFound(string message) : base(message) { }
        }
    }
}
