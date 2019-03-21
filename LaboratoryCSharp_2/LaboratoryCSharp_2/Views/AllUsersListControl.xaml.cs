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
using LaboratoryCSharp_2.Tools.Navigation;
using LaboratoryCSharp_2.ViewModels;

namespace LaboratoryCSharp_2.Views
{
    /// <summary>
    /// Логика взаимодействия для AllUsersListControl.xaml
    /// </summary>
    public partial class AllUsersListControl : INavigatable
    {
        private BaseViewModel _model;
        internal AllUsersListControl()
        {
      
           InitializeComponent();
           _model = new AllUsersListViewModel();
           DataContext = _model;

        }

        public void Refresher()
        {
            _model.Refresher();
        }
    }
}
