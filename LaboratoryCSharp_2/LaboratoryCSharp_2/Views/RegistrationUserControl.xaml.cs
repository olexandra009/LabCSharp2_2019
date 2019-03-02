
using LaboratoryCSharp_2.Tools.Navigation;
using LaboratoryCSharp_2.ViewModels;

namespace LaboratoryCSharp_2.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationUserControl.xaml
    /// </summary>
    public partial class RegistrationUserControl : INavigatable
    {
        internal RegistrationUserControl()
        {
            InitializeComponent();
            DataContext = new RegistrationUserControlViewModel();
        }


        public void Refresher()
        {
            
        }
    }
}
