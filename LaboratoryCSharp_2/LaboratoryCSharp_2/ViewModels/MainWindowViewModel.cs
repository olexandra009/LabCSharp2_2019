using System.Windows;
using LaboratoryCSharp_2.Tools.Managers;

namespace LaboratoryCSharp_2.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel, ILoaderOwner
    {

        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;


        #endregion


        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }

        public override void Refresher()
        {
        }

    }
}