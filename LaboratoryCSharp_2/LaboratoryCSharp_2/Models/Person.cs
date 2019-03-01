using System;

namespace LaboratoryCSharp_2.Models
{
    internal class Person
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;
        private readonly bool _isAdult;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        private readonly bool _isBirthday;

        internal ref readonly bool IsAdult => ref _isAdult;

        internal ref readonly string SunSign => ref _sunSign;

        internal ref readonly string ChineseSign => ref _chineseSign;

        internal ref readonly bool IsBirthday => ref _isBirthday;

        internal Person(string name, string surname, string email)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthday = DateTime.Today;
            _isAdult = (AgeCalculator(_birthday) >= 18);
            _isBirthday = (_birthday.Day == DateTime.Today.Day && _birthday.Month == DateTime.Today.Month);
            _chineseSign = Enum.GetName(typeof(EastHoroscopeList), _birthday.Year % 12);
            _sunSign = CreateWestHoroscope(_birthday.Day, _birthday.Month);

        }

        internal Person(string name, string surname, DateTime birthday)
        {
            _name = name;
            _surname = surname;
            _birthday = birthday;
            _isAdult = (AgeCalculator(birthday) >= 18);
            _isBirthday = (birthday.Day == DateTime.Today.Day && birthday.Month == DateTime.Today.Month);
            _chineseSign = Enum.GetName(typeof(EastHoroscopeList), birthday.Year % 12);
            _sunSign = CreateWestHoroscope(birthday.Day, birthday.Month);


        }
        internal Person(string name, string surname, string email, DateTime birthday)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthday = birthday;
            _isAdult = (AgeCalculator(birthday) >= 18);
            _isBirthday = (birthday.Day == DateTime.Today.Day && birthday.Month == DateTime.Today.Month);
            _chineseSign = Enum.GetName(typeof(EastHoroscopeList), birthday.Year % 12);
            _sunSign = CreateWestHoroscope(birthday.Day, birthday.Month);


        }
        internal string Name
        {
            get => _name;
            set => _name = value;
        }
        internal string Surname
        {
            get => _surname;
            set => _surname = value;
        }
        internal string Email
        {
            get => _email;
            set => _email = value;
        }
        internal DateTime Birthday
        {
            get => _birthday;
            set => _birthday = value;
    
        }

        private int AgeCalculator(DateTime birthday)
        {
             return ((DateTime.Today.Month >= birthday.Month 
                      && DateTime.Today.Day >=birthday.Day)
                ? DateTime.Today.Year - birthday.Year
                : DateTime.Today.Year - birthday.Year - 1);
         
        }
        private string CreateWestHoroscope(int dayBirth, int monthBirth)
        {

            if (dayBirth < 18) return Enum.GetName(typeof(WestHoroscopeList), monthBirth);
            if (dayBirth > 23) return Enum.GetName(typeof(WestHoroscopeList), monthBirth);
            switch (monthBirth)
            {
                case 2:
                    return (dayBirth == 18) ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);

                case 1:
                case 3:
                case 4:
                    return dayBirth == 20 ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);

                case 5:
                case 6:
                case 12:
                    return dayBirth == 21 ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), (monthBirth + 1) % 12);

                case 7:
                case 11:
                case 9:
                    return dayBirth == 22 ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);

                case 8:
                    return dayBirth == 23 ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);
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
