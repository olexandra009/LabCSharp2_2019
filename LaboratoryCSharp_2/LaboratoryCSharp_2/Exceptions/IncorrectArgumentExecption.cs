using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryCSharp_2.Exceptions
{
    class IncorrectArgumentException :ArgumentException
    {
        public string Value { get; }
        public IncorrectArgumentException(string message, string val)
            : base(message)
        {
            Value = val;
        }
    }
}
