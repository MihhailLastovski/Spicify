using Spicify.Service;
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
        private SearchPage searchPage;
        private Recipe recipePage;
        private LoginRegisterPage loginRegisterPage;
        private FavoritePage favoritePage;

        public MainPage()
        {
            searchPage = new SearchPage();
            recipePage = new Recipe();
            loginRegisterPage = new LoginRegisterPage();
            favoritePage = new FavoritePage();

            var searchNavigation = new NavigationPage(searchPage)
            {
                Title = "Search",
                IconImageSource = "search_icon.png"
            };

            var recipeNavigation = new NavigationPage(recipePage)
            {
                Title = "Recipe"
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
                            Text = "Favorite",
                            Command = new Command(() =>
                            {
                                Detail = favNavigation;
                                IsPresented = false;
                            })
                        },
                        new Button
                        {
                            Text = "Logout",
                            Command = new Command(() =>
                            {
                                Database.CurrentUser = null;
                                Application.Current.MainPage = loginRegisterPage;
                            })
                        }

                    }
                }
            };

            Detail = searchNavigation;

            MasterBehavior = MasterBehavior.Popover;

            MasterBehavior = Device.Idiom == TargetIdiom.Phone
                ? MasterBehavior.Popover
                : MasterBehavior.Split;

            NavigationPage.SetHasNavigationBar(this, false);

            CheckLoginStatus();
        }

        private void CheckLoginStatus()
        {
            if (Database.CurrentUser != null)
            {
                Detail = new NavigationPage(recipePage);
            }
        }
    }
}