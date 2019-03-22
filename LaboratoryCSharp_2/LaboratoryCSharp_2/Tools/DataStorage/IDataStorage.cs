using System.Collections.Generic;
using LaboratoryCSharp_2.Models;

namespace LaboratoryCSharp_2.Tools.DataStorage
{
    internal interface IDataStorage
    {
       
        void AddUser(Person user);
        void DeleteUser(Person user);
        void SortUser(int option, bool order);
        List<Person> UsersList { get; }
    }
}
