﻿using LaboratoryCSharp_2.Tools.Navigation;
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
