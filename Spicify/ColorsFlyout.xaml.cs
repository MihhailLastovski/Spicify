using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spicify
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorsFlyout : ContentPage
    {
        public ListView ListView;

        public ColorsFlyout()
        {
            InitializeComponent();

            BindingContext = new ColorsFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class ColorsFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ColorsFlyoutMenuItem> MenuItems { get; set; }

            public ColorsFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<ColorsFlyoutMenuItem>(new[]
                {
                    new ColorsFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new ColorsFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new ColorsFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new ColorsFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new ColorsFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}