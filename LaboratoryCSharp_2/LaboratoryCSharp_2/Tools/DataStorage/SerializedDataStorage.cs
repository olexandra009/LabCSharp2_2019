using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools.Managers;

namespace LaboratoryCSharp_2.Tools.DataStorage
{
    class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _users;
        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = new List<Person>();
                InitializeFirstUsers();
            }
        }

        private enum Names
        {
            Draco,
            George,
            Andromeda,
            Dean,
            Sam,
            Luna,
            Bellatrix,
            Sirius,
            Merope,
            Angelina
        }
        private enum Surnames
        {
            Johnson,
            Gaunt,
            Tonks,
            Winchester,
            Smith,
            Malfoy,
            Black,
            Lestrange,
            Lovegood,
            Thomas
        }

        

        private void InitializeFirstUsers()
        {
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                string name = Enum.GetName(typeof(Names), random.Next(0, 9));
                string surname = Enum.GetName(typeof(Surnames), random.Next(0, 9));
                string email = name+surname+"_"+i+"@gmail.com";
                int year = DateTime.Today.Year - random.Next(3, 120);
                int month = random.Next(1, 12);
                int day = random.Next(1, 28);
              AddUser(new Person(name, surname, email, new DateTime(year, month, day)));
            }

        }

        public void AddUser(Person user)
        {
            _users.Add(user);
            SaveChanges();
        }

        public void DeleteUser(Person user)
        {
            _users.Remove(user);
            SaveChanges();
        }

        public List<Person> UsersList
        {
            get { return _users.ToList(); }
        }
        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }

    }
}
