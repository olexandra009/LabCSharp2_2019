using System.ComponentModel;
using System.Windows;

namespace LaboratoryCSharp_2
{
    internal interface ILoaderOwner : INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}

