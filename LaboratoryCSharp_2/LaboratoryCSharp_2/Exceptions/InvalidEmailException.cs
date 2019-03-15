using System;

namespace LaboratoryCSharp_2.Exceptions
{
   internal class InvalidEmailException : ArgumentException
    {
        public string Value { get; }
        public InvalidEmailException(string message, string val)
            : base(message)
        {
            Value = val;
        }
    }
}
