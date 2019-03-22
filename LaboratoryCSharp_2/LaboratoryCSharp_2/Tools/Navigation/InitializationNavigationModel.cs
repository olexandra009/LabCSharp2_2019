using System;
using LaboratoryCSharp_2.Views;

namespace LaboratoryCSharp_2.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
           
            switch (viewType)
            {
               
                case ViewType.Registration:
                    ViewsDictionary.Add(viewType, new RegistrationUserControl());
                    break;
                case ViewType.ListOfUsers:
                    ViewsDictionary.Add(viewType, new AllUsersListControl());
                    break;
                case ViewType.Filtration:
                    ViewsDictionary.Add(viewType, new FiltrationUserControl());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
