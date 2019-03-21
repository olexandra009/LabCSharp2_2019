
namespace LaboratoryCSharp_2.Tools.Navigation
{
    internal enum ViewType
    {
        Registration,
        ListOfUsers
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
