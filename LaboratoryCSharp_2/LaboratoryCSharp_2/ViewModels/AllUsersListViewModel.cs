using System.Collections.ObjectModel;
using System.Windows.Input;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools;
using LaboratoryCSharp_2.Tools.Managers;
using LaboratoryCSharp_2.Tools.Navigation;

namespace LaboratoryCSharp_2.ViewModels
{
    class AllUsersListViewModel: BaseViewModel
    {
        private ObservableCollection<Person> _users;
        private Person _selectedUser;

        public Person SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
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

        internal AllUsersListViewModel()
        {
            _users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);

        }
        private ICommand _createUserCommand;
        private ICommand _editUserCommand;
        private ICommand _deleteUserCommand;

        public ICommand CreateUserCommand
        {
            get
            {

                return _createUserCommand ?? (_createUserCommand =
                           new RelayCommand<object>(CreateUserCommandImplementation));
            }
        }
        public ICommand DeleteUserCommand
        {
            get
            {

                return _deleteUserCommand ?? (_deleteUserCommand =
                           new RelayCommand<object>(DeleteUserCommandImplementation));
            }
        }
        public ICommand EditUserCommand
        {
            get
            {

                return _editUserCommand ?? (_editUserCommand =
                           new RelayCommand<object>(EditUserCommandImplementation));
            }
        }

        private void EditUserCommandImplementation(object obj)
        {
            StationManager.CurrentPerson = SelectedUser;
            StationManager.DataStorage.DeleteUser(SelectedUser);
            StationManager.EditModel.Refresher();
            NavigationManager.Instance.Navigate(ViewType.Registration);
        }

        private void DeleteUserCommandImplementation(object obj)
        {
           StationManager.DataStorage.DeleteUser(SelectedUser);
           StationManager.CurrentModel.Refresher();
       }

        private void CreateUserCommandImplementation(object obj)
        {
            StationManager.CurrentPerson = null;
            StationManager.EditModel.Refresher();
            NavigationManager.Instance.Navigate(ViewType.Registration);
                
         }


        public override void Refresher()
        {
            Users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }
    }
}
