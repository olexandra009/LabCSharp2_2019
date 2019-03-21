using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LaboratoryCSharp_2.Exceptions;
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
                // if (!CorrectAge()) return false;
               //   if (!CorrectEmail()) return false;
              //  if (!CorrectNameSurname()) return false;
                try
                {

                    StationManager.CurrentPerson = new Person(_name, _surname, _email, (DateTime) _birthday);
                    StationManager.DataStorage.AddUser(StationManager.CurrentPerson);
                    Thread.Sleep(100);
                }
                catch (PersonBirthDateException exp)
                {
                    MessageBox.Show(exp.Message);
                    return false;
                }
                catch (InvalidEmailException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                catch (IncorrectArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                StationManager.CurrentModel.Refresher();
                NavigationManager.Instance.Navigate(ViewType.ListOfUsers);
                if (StationManager.CurrentPerson.IsBirthday)
                    MessageBox.Show("Happy Birthday to you!");
            }
        }

       

       private bool CanExecute(object obj)
        {
            return !String.IsNullOrEmpty(_name) &&
                   !String.IsNullOrEmpty(_surname) &&
                   !String.IsNullOrEmpty(_email) &&
                   (_birthday != null);
        }

        //public override void Refresher()
        //{
            
        //}
        public override void Refresher()
        {
           
            if (StationManager.CurrentPerson == null)
            {
                Name = "";
                Surname = "";
                Email = "";
                Birthday = DateTime.Today;
            }
            else
            {
                Name = StationManager.CurrentPerson.Name;
                Surname = StationManager.CurrentPerson.Surname;
                Email = StationManager.CurrentPerson.Email;
                Birthday = StationManager.CurrentPerson.Birthday;
            }
        }
    }
}