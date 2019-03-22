using System;
using System.ComponentModel.DataAnnotations;
using LaboratoryCSharp_2.Exceptions;

namespace LaboratoryCSharp_2.Models
{
    [Serializable]
    internal class Person
    {
        #region Fields

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;
        private readonly bool _isAdult;
        private readonly bool _isBirthday;

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        internal DateTime Birthday
        {
            get => _birthday;
            set => _birthday = value;
        }

        public string BirthdayDate => _birthday.ToShortDateString();

        public string IsBirthdayToday => _isBirthday ? "+" : "-";

        public string Adult => _isAdult ? "+" : "-";
        internal ref readonly bool IsAdult => ref _isAdult;

        public string SunSign { get; }

        public string ChineseSign { get; }

        internal ref readonly bool IsBirthday => ref _isBirthday;

        #endregion

        #region Constructors

        internal Person(string name, string surname, string email) : this(name, surname, email, DateTime.Today)
        {
        }

        internal Person(string name, string surname, DateTime? birthday) : this(name, surname, null, birthday)
        {
        }

        internal Person(string name, string surname, string email, DateTime? birthday)
        {
            InputValidation(birthday, email, name, surname);
            _name = name;
            _surname = surname;
            _email = email;
            _birthday = (DateTime) birthday;
            _isAdult = AgeCalculator(_birthday) >= 18;
            _isBirthday = _birthday.Day == DateTime.Today.Day && _birthday.Month == DateTime.Today.Month;
            ChineseSign = Enum.GetName(typeof(EastHoroscopeList), _birthday.Year % 12);
            SunSign = CreateWestHoroscope(_birthday.Day, _birthday.Month);
        }

        #endregion

        private void InputValidation(DateTime? birthday, string email, string name, string surname)
        {
            if (birthday == null) throw new PersonBirthDateException("Input correct date of birthday", null);
            if (birthday > DateTime.Today)
                throw new PersonBirthDateException(
                    $"Error: the date of birthday in future. {Environment.NewLine}Please, input correct date of birthday",
                    birthday);
            var last = new DateTime(DateTime.Today.Year - 135, DateTime.Today.Month, DateTime.Today.Day);
            if (birthday < last)
                throw new PersonBirthDateException(
                    $"Error:person cannot be older than 135 years.{Environment.NewLine}Please, enter correct information about your birthday." +
                    $"{Environment.NewLine}  (If you are vampire, please compute your year of birthday plus twelve while entered information will be correct)",
                    birthday);
            if (!new EmailAddressAttribute().IsValid(email))
                throw new InvalidEmailException("Input correct email address", email);
            if (string.IsNullOrWhiteSpace(name))
                throw new IncorrectArgumentException("Input correct name", name);
            if (string.IsNullOrWhiteSpace(surname))
                throw new IncorrectArgumentException("Input correct surname", surname);
        }

        private int AgeCalculator(DateTime birthday)
        {
            return DateTime.Today.Month >= birthday.Month
                   && DateTime.Today.Day >= birthday.Day
                ? DateTime.Today.Year - birthday.Year
                : DateTime.Today.Year - birthday.Year - 1;
        }

        private string CreateWestHoroscope(int dayBirth, int monthBirth)
        {
            if (dayBirth < 18) return Enum.GetName(typeof(WestHoroscopeList), monthBirth);
            if (dayBirth > 23 && monthBirth == 12)
                return Enum.GetName(typeof(WestHoroscopeList), (monthBirth + 1) % 12
                );
            if (dayBirth > 23) return Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);
            switch (monthBirth)
            {
                case 2:
                    return dayBirth > 18
                        ? Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1)
                        : Enum.GetName(typeof(WestHoroscopeList), monthBirth);

                case 1:
                case 3:
                case 4:
                    return dayBirth > 20
                        ? Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1)
                        : Enum.GetName(typeof(WestHoroscopeList), monthBirth);

                case 5:
                case 6:
                case 12:
                    return dayBirth > 21
                        ? Enum.GetName(typeof(WestHoroscopeList), (monthBirth + 1) % 12)
                        : Enum.GetName(typeof(WestHoroscopeList), monthBirth);

                case 7:
                case 11:
                case 9:
                    return dayBirth > 22
                        ? Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1)
                        : Enum.GetName(typeof(WestHoroscopeList), monthBirth);

                case 8:
                case 10:
                    return dayBirth > 23
                        ? Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1)
                        : Enum.GetName(typeof(WestHoroscopeList), monthBirth);
                default:
                    return "Something went wrong";
            }
        }

        private enum WestHoroscopeList
        {
            Copricorn = 1,
            Aquarius,
            Pisces,
            Aries,
            Taurus,
            Gemini,
            Cancer,
            Leo,
            Virgio,
            Libra,
            Scorpio,
            Sagittarius
        }

        private enum EastHoroscopeList
        {
            Monkey,
            Cock,
            Dog,
            Pig,
            Rat,
            Bull,
            Tiger,
            Cat,
            Dragon,
            Snake,
            Horse,
            Goat
        }
    }
}