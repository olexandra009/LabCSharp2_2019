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
        #region MyRegion
        private string _name;
        private string _surname;
        private string _email;
        private DateTime? _birthday;
        #endregion

        #region Properties
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime? Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }
        #endregion

        
        #region Commands
        private ICommand _proceedCommand;
        private ICommand _closeCommand;
        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ??
                                        (_backCommand = new RelayCommand<object>(BackCommandImplementation));

        public ICommand CloseCommand => _closeCommand ??
                                        (_closeCommand = new RelayCommand<object>(CloseCommandImplementation));

        public ICommand ProceedCommand =>
            _proceedCommand ?? (_proceedCommand =
                new RelayCommand<object>(ProceedCommandImplementation, CanExecute));

        #endregion

        private void BackCommandImplementation(object obj)
        {
          if(StationManager.CurrentPerson!=null)
              StationManager.DataStorage.AddUser(StationManager.CurrentPerson);
          StationManager.ViewsNavigatable[0].Refresher();
          NavigationManager.Instance.Navigate(ViewType.ListOfUsers);
        }
        private void CloseCommandImplementation(object obj)
        {
            StationManager.CloseApp();
        }
        
        private async void ProceedCommandImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
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
                StationManager.ViewsNavigatable[0].Refresher();
                NavigationManager.Instance.Navigate(ViewType.ListOfUsers);
            }
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