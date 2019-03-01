
namespace LaboratoryCSharp_2.Tools.Navigation
{
    internal enum ViewType
    {
        Registration,
        Information
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
