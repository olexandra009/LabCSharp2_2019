using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools;
using LaboratoryCSharp_2.Tools.Managers;
using LaboratoryCSharp_2.Tools.Navigation;

namespace LaboratoryCSharp_2.ViewModels
{
    internal class FiltrationUserViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<Person> _users;
        private ObservableCollection<Person> _filtrationUsers;
        private bool _isFiltrated;
        private string _option;
        private int _type;
        private Person _selectedUser;
        #endregion

        #region Properties
        public Person SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public string InputValue
        {
            get => _option;
            set
            {
                _option = value;
                OnPropertyChanged();
            }
        }

        public int SelectedFilterType
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Person> Users
        {
            get => _users;
            private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        internal FiltrationUserViewModel()
        {
            Users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
            _isFiltrated = false;
        } 
        #endregion


        public override void Refresher()
        {
            Users = _isFiltrated
                ? _filtrationUsers
                : new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }

        #region Commands
        private ICommand _filtrationCommand;
        private ICommand _backCommand;
        private ICommand _closeCommand;
        private ICommand _createUserCommand;
        private ICommand _editUserCommand;
        private ICommand _deleteUserCommand;

        public ICommand CreateUserCommand =>
            _createUserCommand ?? (_createUserCommand =
                new RelayCommand<object>(CreateUserCommandImplementation));

        public ICommand DeleteUserCommand =>
            _deleteUserCommand ?? (_deleteUserCommand =
                new RelayCommand<object>(DeleteUserCommandImplementation));

        public ICommand EditUserCommand =>
            _editUserCommand ?? (_editUserCommand =
                new RelayCommand<object>(EditUserCommandImplementation));
        public ICommand FiltrationCommand =>
            _filtrationCommand ?? (_filtrationCommand =
                new RelayCommand<object>(FiltrationCommandImplementation));

        public ICommand BackCommand =>
            _backCommand ?? (_backCommand =
                new RelayCommand<object>(BackCommandImplementation));

        public ICommand CloseCommand =>
            _closeCommand ?? (_closeCommand =
                new RelayCommand<object>(CloseCommandImplementation)); 
        #endregion

        private void CloseCommandImplementation(object obj)
        {
            StationManager.CloseApp();
        }

        private void FiltrationCommandImplementation(object obj)
        {
           _isFiltrated = true;
            LoaderManager.Instance.ShowLoader();
            Filtration();

            LoaderManager.Instance.HideLoader();
        }
        private void EditUserCommandImplementation(object obj)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("You chose no user to edit");
                return;
            }
            StationManager.CurrentPerson = SelectedUser;
            StationManager.DataStorage.DeleteUser(SelectedUser);
            StationManager.ViewsNavigatable[1].Refresher();
            NavigationManager.Instance.Navigate(ViewType.Registration);
        }

        private void DeleteUserCommandImplementation(object obj)
        {
            StationManager.DataStorage.DeleteUser(SelectedUser);
            _filtrationUsers.Remove(SelectedUser);
            StationManager.ViewsNavigatable[2].Refresher();
        }

        private void CreateUserCommandImplementation(object obj)
        {
            StationManager.CurrentPerson = null;
            StationManager.ViewsNavigatable[1].Refresher();
            NavigationManager.Instance.Navigate(ViewType.Registration);

        }

        private void Filtration()
        {
            _users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
            _filtrationUsers = new ObservableCollection<Person>();
            switch (_type)
            {
                case 0:
                    foreach (var person in _users)
                        if (string.Equals(person.Name, InputValue, StringComparison.CurrentCultureIgnoreCase))
                            _filtrationUsers.Add(person);
                    break;
                case 1:
                    foreach (var person in _users)
                        if (string.Equals(person.Surname, InputValue, StringComparison.CurrentCultureIgnoreCase))
                            _filtrationUsers.Add(person);
                    break;
                case 2:
                    foreach (var person in _users)
                        if (string.Equals(person.Email, InputValue, StringComparison.CurrentCultureIgnoreCase))
                            _filtrationUsers.Add(person);
                    break;
                case 3:
                    DateTime birth;
                    try
                    {
                        birth = DateTime.Parse(InputValue);
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    foreach (var person in _users)
                        if (person.Birthday == birth)
                            _filtrationUsers.Add(person);
                    break;
                case 4:
                    foreach (var person in _users)
                        if (string.Equals(person.SunSign, InputValue, StringComparison.CurrentCultureIgnoreCase))
                            _filtrationUsers.Add(person);
                    break;
                case 5:
                    foreach (var person in _users)
                        if (string.Equals(person.ChineseSign, InputValue, StringComparison.CurrentCultureIgnoreCase))
                            _filtrationUsers.Add(person);
                    break;
                case 6:
                    foreach (var person in _users)
                        if (string.Equals(person.Adult, InputValue, StringComparison.CurrentCultureIgnoreCase))
                            _filtrationUsers.Add(person);
                    break;
                case 7:
                    foreach (var person in _users)
                        if (string.Equals(person.IsBirthdayToday, InputValue,
                            StringComparison.CurrentCultureIgnoreCase))
                            _filtrationUsers.Add(person);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(null);
            }

            StationManager.ViewsNavigatable[2].Refresher();
        }

        private void BackCommandImplementation(object obj)
        {
            _isFiltrated = false;
            StationManager.ViewsNavigatable[0].Refresher();
            NavigationManager.Instance.Navigate(ViewType.ListOfUsers);
        }
    }
}