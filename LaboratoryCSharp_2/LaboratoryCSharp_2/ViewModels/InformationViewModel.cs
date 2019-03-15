using System.Windows.Input;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools;
using LaboratoryCSharp_2.Tools.Managers;
using LaboratoryCSharp_2.Tools.Navigation;

namespace LaboratoryCSharp_2.ViewModels
{
    internal class InformationViewModel : BaseViewModel
    {
        private Person _person;
        private string _name;
        private string _surname;
        private string _email;
        private string _birthday;
        private string _isAdult;
        private string _chineseSign;
        private string _sunSign;
        private string _isBirthday;
        internal Person Person
        {
            get => _person;
            private set { _person = value; OnPropertyChanged();}
        }

        public string Name
        {

            get =>_name;
            private set
            {
                _name = value;
                OnPropertyChanged();
            }
              
        }
        public string Surname
        {
            get => _surname;
            private set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            private set
            { _email = value; OnPropertyChanged(); }
        }
        public string Birthday
        {
            get => _birthday;
            private set
            { _birthday = value; OnPropertyChanged(); }
            

        }
        public string IsAdult
        {
            get => _isAdult;
            private set
            { _isAdult = value; OnPropertyChanged(); }

         
        }


        public string SunSign
        {
            get => _sunSign;
            private set
            { _sunSign = value; OnPropertyChanged(); }
        } 

        public string ChineseSign
        {
            get => _chineseSign;
            private set
            { _chineseSign = value; OnPropertyChanged(); }
        }

        public string IsBirthday
        {
            get => _isBirthday;
            private set
            { _isBirthday = value; OnPropertyChanged(); }
        }

        private ICommand _backCommand;
        private ICommand _closeCommand;
        public ICommand CloseCommand => _closeCommand?? (_closeCommand = new RelayCommand<object>(CloseCommandImplementation));

        private void CloseCommandImplementation(object obj)
        {
            StationManager.CloseApp();
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand =
                           new RelayCommand<object>(BackCommandImplementation));
            }
        }

        private void BackCommandImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.Registration);
        }

        public override void Refresher()
        {
            Person = StationManager.CurrentPerson;
            Name = _person.Name;
            Surname = _person.Surname;
            Email = _person.Email;
            Birthday = _person.Birthday.ToShortDateString();
            IsAdult = _person.IsAdult? "Yes" : "No";
            IsBirthday = _person.IsBirthday? "Yes" : "No";
            ChineseSign = _person.ChineseSign;
            SunSign = _person.SunSign;

        }
    }
}
