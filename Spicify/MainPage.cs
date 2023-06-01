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
            SearchPage searchPage = new SearchPage();
            Recipe recipePage = new Recipe();
            LoginRegisterPage loginRegisterPage = new LoginRegisterPage();
            FavoritePage favoritePage = new FavoritePage();

            var searchNavigation = new NavigationPage(searchPage)
            {
                Title = "Search",
                IconImageSource = "search_icon.png"
            };

            var recipeNavigation = new NavigationPage(recipePage)
            {
                Title = "Recipe"
            };

            var regNavigation = new NavigationPage(loginRegisterPage)
            {
                Title = "SignUpIn"
            };
            var favNavigation = new NavigationPage(favoritePage)
            {
                Title = "Favorite"
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
                        },
                        new Button
                        {
                            Text = "SignUpIn",
                            Command = new Command(() =>
                            {
                                Detail = regNavigation;
                                IsPresented = false;
                            })
                        },
                        new Button
                        {
                            Text = "Favorite",
                            Command = new Command(() =>
                            {
                                Detail = favNavigation;
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