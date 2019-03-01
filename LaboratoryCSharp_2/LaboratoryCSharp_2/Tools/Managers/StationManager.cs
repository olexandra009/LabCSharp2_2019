using System;
using LaboratoryCSharp_2.Models;
using LaboratoryCSharp_2.Tools.Navigation;


namespace LaboratoryCSharp_2.Tools.Managers
{
    internal static class StationManager
    {
     
        internal static Person CurrentPerson { get; set; }
        internal static INavigatable CurrentModel { get; set; }
        internal static void Initialize()
        {
            CurrentPerson = new Person("a", "a", "a", DateTime.Today);
        }

        internal static void CloseApp()
        {
            Environment.Exit(1);
        }
    }
}
