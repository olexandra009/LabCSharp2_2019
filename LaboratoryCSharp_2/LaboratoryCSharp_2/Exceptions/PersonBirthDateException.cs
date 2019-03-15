using System;

namespace LaboratoryCSharp_2.Exceptions
{
   internal class PersonBirthDateException: Exception
    {
        public DateTime? Value { get; }
        public PersonBirthDateException(string message, DateTime? val)
            : base(message)
        {
            Value = val;
        }
    }
}
