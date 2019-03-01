
using LaboratoryCSharp_2.Tools.Navigation;
using LaboratoryCSharp_2.ViewModels;

namespace LaboratoryCSharp_2.Views
{
    /// <summary>
    /// Логика взаимодействия для InformationControl.xaml
    /// </summary>
   public partial class InformationControl :  INavigatable
    {
        private BaseViewModel model;
        internal InformationControl()
        {
            InitializeComponent();
            model = new InformationViewModel();
            DataContext = model;
        }

        public void Refresher()
        {
            model.Refresher();
        }
    }
}
