
using LaboratoryCSharp_2.Tools.Navigation;
using LaboratoryCSharp_2.ViewModels;

namespace LaboratoryCSharp_2.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationUserControl.xaml
    /// </summary>
    public partial class RegistrationUserControl : INavigatable
    {
        private BaseViewModel _model;
        internal RegistrationUserControl()
        {
            InitializeComponent();
            _model = new RegistrationUserControlViewModel();
            DataContext = _model;
        }


        public void Refresher()
        {
            _model.Refresher();
        }
    }
}
