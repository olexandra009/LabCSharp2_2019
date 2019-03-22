using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools;
using LaboratoryCSharp_2.Tools.Managers;
using LaboratoryCSharp_2.Tools.Navigation;

namespace LaboratoryCSharp_2.ViewModels
{
   internal class AllUsersListViewModel: BaseViewModel
    {

        #region Fields
        private ObservableCollection<Person> _users;
        private Person _selectedUser;
        private int _selectedSortingType;
        private bool _reverseSorting;
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


        public int SelectedSortingType
        {
            get => _selectedSortingType;
            set
            {
                _selectedSortingType = value;
                OnPropertyChanged();
            }
        }



        public bool ReverseSorting
        {
            get => _reverseSorting;
            set
            {
                _reverseSorting = value;
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

        #endregion
        
        #region Constructor
        internal AllUsersListViewModel()
        {
            _users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }
        #endregion

        #region Commands
        private ICommand _createUserCommand;
        private ICommand _editUserCommand;
        private ICommand _deleteUserCommand;
        private ICommand _sortingCommand;
        private ICommand _filtrationCommand;
        private ICommand _closeCommand;

        public ICommand CreateUserCommand =>
            _createUserCommand ?? (_createUserCommand =
                new RelayCommand<object>(CreateUserCommandImplementation));

        public ICommand DeleteUserCommand =>
            _deleteUserCommand ?? (_deleteUserCommand =
                new RelayCommand<object>(DeleteUserCommandImplementation));

        public ICommand EditUserCommand =>
            _editUserCommand ?? (_editUserCommand =
                new RelayCommand<object>(EditUserCommandImplementation));

        public ICommand SortingCommand =>
            _sortingCommand ?? (_sortingCommand =
                new RelayCommand<object>(SortingCommandImplementation));

        public ICommand FiltrationCommand =>
            _filtrationCommand ?? (_filtrationCommand =
                new RelayCommand<object>(FiltrationCommandImplementation));

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
            StationManager.ViewsNavigatable[2].Refresher();
            NavigationManager.Instance.Navigate(ViewType.Filtration);

        }

        private void SortingCommandImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            StationManager.DataStorage.SortUser(SelectedSortingType, ReverseSorting);
            StationManager.ViewsNavigatable[0].Refresher();
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
           StationManager.ViewsNavigatable[0].Refresher();
        }

        private void CreateUserCommandImplementation(object obj)
        {
            StationManager.CurrentPerson = null;
            StationManager.ViewsNavigatable[1].Refresher();
            NavigationManager.Instance.Navigate(ViewType.Registration);
                
         }


        public override void Refresher()
        {
            Users = new ObservableCollection<Person>(StationManager.DataStorage.UsersList);
        }
    }
}
