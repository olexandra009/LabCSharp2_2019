using System;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools.DataStorage;
using LaboratoryCSharp_2.Tools.Navigation;


namespace LaboratoryCSharp_2.Tools.Managers
{
    internal static class StationManager
    {

        private static INavigatable[] _viewsNavigatables;

        internal static INavigatable[] ViewsNavigatable
        {
            get => _viewsNavigatables;
        }
        internal static Person CurrentPerson { get; set; }

        private static IDataStorage _dataStorage;

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage, int views)
        {
            CurrentPerson = new Person("Person", "Default", "a.grm@ggg.vom", DateTime.Today);
            _viewsNavigatables = new INavigatable[views];
            _dataStorage = dataStorage;
            //DataStorage.AddUser(StationManager.CurrentPerson);
        }
     
        internal static void CloseApp()
        {
            Environment.Exit(1);
        }
    }
}
