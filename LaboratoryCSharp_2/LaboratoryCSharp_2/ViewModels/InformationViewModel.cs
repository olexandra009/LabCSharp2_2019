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
        private string _chinseSign;
        private string _sunSign;
        private string _isBirthday;
        public Person Person
        {
            get => _person;
            set { _person = value; OnPropertyChanged();}
        }

        public string Name
        {

            get =>_name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
              
        }
        public string Surname
        {
            get => _surname;
            set { _surname = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }
        public string Birthday
        {
            get => _birthday;
            set { _birthday = value; OnPropertyChanged(); }
            

        }
        public string IsAdult
        {
            get => _isAdult;
            set { _isAdult = value; OnPropertyChanged(); }

         
        }


        public string SunSign
        {
            get => _sunSign;
            set { _sunSign = value; OnPropertyChanged(); }
        } 

        public string ChineseSign
        {
            get => _chinseSign;
            set { _chinseSign = value; OnPropertyChanged(); }
        }

        public string IsBirthday
        {
            get => _isBirthday;
            set { _isBirthday = value; OnPropertyChanged(); }
        }

        private ICommand _backCommand;
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
             return _closeCommand?? (_closeCommand = new RelayCommand<object>(CloseCommandImplementation));   
            }
        }

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

        public void BackCommandImplementation(object obj)
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
