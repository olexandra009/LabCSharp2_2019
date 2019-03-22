using System.Windows.Controls;
using LaboratoryCSharp_2.Tools.DataStorage;
using LaboratoryCSharp_2.Tools.Managers;
using LaboratoryCSharp_2.Tools.Navigation;
using LaboratoryCSharp_2.ViewModels;
using LaboratoryCSharp_2.Views;

namespace LaboratoryCSharp_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentOwner
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            StationManager.Initialize(new SerializedDataStorage(), 3);
            InitializeViews();
            NavigationManager.Instance.Navigate(ViewType.ListOfUsers);
         
        }

        private void InitializeViews()
        {
            var owner = new InitializationNavigationModel(this);
            NavigationManager.Instance.Initialize(owner);
            var infoModel = new AllUsersListControl();
            var registration = new RegistrationUserControl();
            var filtration = new FiltrationUserControl();
            owner.ViewsDictionary.Add(ViewType.ListOfUsers, infoModel);
            owner.ViewsDictionary.Add(ViewType.Registration, registration);
            owner.ViewsDictionary.Add(ViewType.Filtration, filtration);
            StationManager.ViewsNavigatable[0] = infoModel;
            StationManager.ViewsNavigatable[1] = registration;
            StationManager.ViewsNavigatable[2] = filtration;
        }

        public ContentControl ContentControl => _contentControl;
    }
}
