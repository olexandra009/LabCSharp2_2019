using System;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools.DataStorage;
using LaboratoryCSharp_2.Tools.Navigation;


namespace LaboratoryCSharp_2.Tools.Managers
{
    internal static class StationManager
    {
     
        internal static Person CurrentPerson { get; set; }

        internal static INavigatable CurrentModel { get; set; }
        internal static INavigatable EditModel { get; set; }


        private static IDataStorage _dataStorage;

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            CurrentPerson = new Person("Person", "Default", "a.grm@ggg.vom", DateTime.Today);

            _dataStorage = dataStorage;
            //DataStorage.AddUser(StationManager.CurrentPerson);
        }
     
        internal static void CloseApp()
        {
            Environment.Exit(1);
        }
    }
}
