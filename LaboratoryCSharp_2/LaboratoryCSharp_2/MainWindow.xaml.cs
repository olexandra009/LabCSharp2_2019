using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LaboratoryCSharp_2.Tools.Managers;
using LaboratoryCSharp_2.Tools.Navigation;
using LaboratoryCSharp_2.ViewModels;
using LaboratoryCSharp_2.Views;

namespace LaboratoryCSharp_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            var ovner = new InitializationNavigationModel(this);
            NavigationManager.Instance.Initialize(ovner);
            NavigationManager.Instance.Navigate(ViewType.Registration);
            StationManager.Initialize();
            var infoModel = new InformationControl();
            ovner.ViewsDictionary.Add(ViewType.Information, infoModel);
          
            StationManager.CurrentModel = infoModel;
        }


        public ContentControl ContentControl {
            get { return _contentControl; }
        }
    }
}
