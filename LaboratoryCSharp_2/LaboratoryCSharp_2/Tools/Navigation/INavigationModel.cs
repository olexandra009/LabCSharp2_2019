
namespace LaboratoryCSharp_2.Tools.Navigation
{
    internal enum ViewType
    {
        Registration,
        ListOfUsers,
        Filtration
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
