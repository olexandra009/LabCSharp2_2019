using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools;
using LaboratoryCSharp_2.Tools.Managers;
using LaboratoryCSharp_2.Tools.Navigation;

namespace LaboratoryCSharp_2.ViewModels
{
    internal class RegistrationUserControlViewModel : BaseViewModel
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime? _birthday;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get
            {
                return _surname;

            }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get
            {
                return _email;
             }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime? Birthday
        {
            get
            {
                return _birthday;

            }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }
        private ICommand _proceedCommand;
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseCommandImplementation));
            }
        }

        private void CloseCommandImplementation(object obj)
        {
            StationManager.CloseApp();
        }
        public ICommand ProceedCommand
        {
            get
            {
                
                return _proceedCommand ?? (_proceedCommand =
                           new RelayCommand<object>(ProceedCommandImplementation, CanExecute));
            }
        }
        private async void ProceedCommandImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                if (!CorrectAge()) return false;
                if (!CorrectEmail()) return false;
                if (!CorrectNameSurname()) return false;
                Thread.Sleep(100);
                StationManager.CurrentPerson = new Person(_name, _surname, _email, (DateTime) _birthday);
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                StationManager.CurrentModel.Refresher();
                NavigationManager.Instance.Navigate(ViewType.Information);
                if (StationManager.CurrentPerson.IsBirthday)
                    MessageBox.Show("Happy Birthday to you!");
            }
        }

        private bool CorrectNameSurname()
        {
            if (String.IsNullOrWhiteSpace(_name) || String.IsNullOrWhiteSpace(_surname))
            {
                MessageBox.Show("Input correct name or surname");
                return false;
            }

            return true;
        }

        private bool CorrectEmail()
        {
            if (!(new EmailAddressAttribute().IsValid(_email)))
            {
                MessageBox.Show("Input correct email address");
                return false;
            }

            return true;
        }

        private bool CorrectAge()
        {
            if (_birthday == null)
            {
                MessageBox.Show("Input your date of birthday");
                return false;
            }
            if (_birthday > DateTime.Today)
            {
                MessageBox.Show("Input correct date of birthday");
                return false;
            }
            DateTime last = new DateTime(DateTime.Today.Year - 135, DateTime.Today.Month, DateTime.Today.Day);
            if (_birthday< last)
            {
                MessageBox.Show($"Sorry, you cannot be older than 135 years.{Environment.NewLine} Please, enter correct information about your birthday." +
                                $"{Environment.NewLine}  (If you are vampire, please compute your year of birthday plus twelve while entered information will be correct)");
                return false;
            }

            return true;
    }


        private bool CanExecute(object obj)
        {
            return !String.IsNullOrEmpty(_name) &&
                   !String.IsNullOrEmpty(_surname) &&
                   !String.IsNullOrEmpty(_email) &&
                   (_birthday != null);
        }

        public override void Refresher()
        {
            
        }
    }
}