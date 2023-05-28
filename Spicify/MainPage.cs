using Spicify.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Spicify
{
    public class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            var searchPage = new SearchPage();
            var recipePage = new Recipe();

            var searchNavigation = new NavigationPage(searchPage)
            {
                Title = "Search",
                IconImageSource = "search_icon.png"
            };

            var recipeNavigation = new NavigationPage(recipePage)
            {
                Title = "Recipe"
            };

            Master = new ContentPage
            {
                Title = "Menu",
                Content = new StackLayout
                {
                    Children = {
                        new Button
                        {
                            Text = "Search",
                            Command = new Command(() =>
                            {
                                Detail = searchNavigation;
                                IsPresented = false;
                            })
                        },
                        new Button
                        {
                            Text = "Recipe",
                            Command = new Command(() =>
                            {
                                Detail = recipeNavigation;
                                IsPresented = false;
                            })
                        }
                    }
                }
            };

            Detail = recipeNavigation;

            MasterBehavior = MasterBehavior.Popover;

            MasterBehavior = Device.Idiom == TargetIdiom.Phone
                ? MasterBehavior.Popover
                : MasterBehavior.Split;

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}