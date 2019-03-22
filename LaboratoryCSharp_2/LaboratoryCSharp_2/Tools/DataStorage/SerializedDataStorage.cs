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
        private  List<Person> _users;
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
                string name = Enum.GetName(typeof(Names), random.Next(0, 10));
                string surname = Enum.GetName(typeof(Surnames), random.Next(0, 10));
                string email = name+surname+"_"+i+"@gmail.com";
                int year = DateTime.Today.Year - random.Next(3, 120);
                int month = random.Next(1, 13);
                int day = random.Next(1, 29);
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

        public void SortUser(int option, bool reverseOrder)
        {
            List<Person> orderedEnumerable;
            switch (option)
            {
                case 0:
                    orderedEnumerable = !reverseOrder
                        ? new List<Person>(_users.OrderBy(person => person.Name))
                        : new List<Person>(_users.OrderByDescending(person => person.Name));
                    break;
                case 1:
                    orderedEnumerable = !reverseOrder
                        ? new List<Person>(_users.OrderBy(person => person.Surname))
                        : new List<Person>(_users.OrderByDescending(person => person.Surname));
                    break;
                case 2:
                    orderedEnumerable = !reverseOrder
                        ? new List<Person>(_users.OrderBy(person => person.Email))
                        : new List<Person>(_users.OrderByDescending(person => person.Email));
                    break;
                case 3:
                    orderedEnumerable = !reverseOrder
                        ? new List<Person>(_users.OrderBy(person => person.Birthday))
                        : new List<Person>(_users.OrderByDescending(person => person.Birthday));
                    break;
                case 4:
                    orderedEnumerable = !reverseOrder
                        ? new List<Person>(_users.OrderBy(person => person.SunSign))
                        : new List<Person>(_users.OrderByDescending(person => person.SunSign));
                    break;
                case 5:
                    orderedEnumerable = !reverseOrder
                        ? new List<Person>(_users.OrderBy(person => person.ChineseSign))
                        : new List<Person>(_users.OrderByDescending(person => person.ChineseSign));
                    break;
                case 6:
                    orderedEnumerable = !reverseOrder
                        ? new List<Person>(_users.OrderBy(person => person.IsAdult))
                        : new List<Person>(_users.OrderByDescending(person => person.IsAdult));
                    break;
                case 7:
                    orderedEnumerable = !reverseOrder
                        ? new List<Person>(_users.OrderBy(person => person.IsBirthday))
                        : new List<Person>(_users.OrderByDescending(person => person.IsBirthday));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(null);
             }

            _users = orderedEnumerable;
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
