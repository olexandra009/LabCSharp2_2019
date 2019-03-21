using System.Collections.Generic;
using LaboratoryCSharp_2.Models;

namespace LaboratoryCSharp_2.Tools.DataStorage
{
    internal interface IDataStorage
    {
        //bool UserExists(string login);
        //User GetUserByLogin(string login);

        void AddUser(Person user);
        void DeleteUser(Person user);
        List<Person> UsersList { get; }
    }
}
