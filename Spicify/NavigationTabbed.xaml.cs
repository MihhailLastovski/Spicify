using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spicify.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spicify
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationTabbed : TabbedPage
    {
        public NavigationTabbed()
        {
            NavigationPage navigationPage = new NavigationPage(new Recipe());
            navigationPage.Title = "Recipe";
            Children.Add(navigationPage);
            NavigationPage navigationPage2 = new NavigationPage(new SearchPage());
            navigationPage2.Title = "Search";
            Children.Add(navigationPage2);
            BarBackgroundColor = Color.Black;
            BarTextColor = Color.White;
        }
    }
}