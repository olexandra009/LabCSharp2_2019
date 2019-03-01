using System.Windows.Controls;
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
            var owner = new InitializationNavigationModel(this);
            NavigationManager.Instance.Initialize(owner);
            NavigationManager.Instance.Navigate(ViewType.Registration);
            StationManager.Initialize();
            var infoModel = new InformationControl();
            owner.ViewsDictionary.Add(ViewType.Information, infoModel);
          
            StationManager.CurrentModel = infoModel;
        }


        public ContentControl ContentControl {
            get { return _contentControl; }
        }
    }
}
