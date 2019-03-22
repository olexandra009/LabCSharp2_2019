using LaboratoryCSharp_2.Tools.Navigation;
using LaboratoryCSharp_2.ViewModels;

namespace LaboratoryCSharp_2.Views
{
    /// <summary>
    /// Логика взаимодействия для FiltrationUserControl.xaml
    /// </summary>
    public partial class FiltrationUserControl : INavigatable
    {
        private BaseViewModel _model;
        internal FiltrationUserControl()
        {
            InitializeComponent();
            _model = new FiltrationUserViewModel();
            DataContext = _model;
        }

        public void Refresher()
        {
            _model.Refresher();
        }
    }
}
