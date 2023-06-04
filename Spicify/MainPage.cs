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
        private UserRecipeList userRecipeList;

        public MainPage()
        {
            searchPage = new SearchPage();
            recipePage = new Recipe();
            loginRegisterPage = new LoginRegisterPage();
            favoritePage = new FavoritePage();
            userRecipeList = new UserRecipeList();

            var buttonStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex("#2196F3") },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.MarginProperty, Value = new Thickness(0, 10) }
                }
            };

            var menuButtonStyle = new Style(typeof(Button))
            {
                BasedOn = buttonStyle,
                Setters = {
                    new Setter { Property = Button.WidthRequestProperty, Value = 200 },
                    new Setter { Property = Button.FontSizeProperty, Value = 18 }
                }
            };

            NavigationPage searchNavigation = new NavigationPage(searchPage)
            {
                Title = "Search",
            };

            NavigationPage recipeNavigation = new NavigationPage(recipePage)
            {
                Title = "Recipe"
            };

            NavigationPage favNavigation = new NavigationPage(favoritePage)
            {
                Title = "Favorite"
            };

            NavigationPage userRecipeNavigation = new NavigationPage(userRecipeList)
            {
                Title = "Your Recipes"
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
                            }),
                            Style = menuButtonStyle
                        },
                        new Button
                        {
                            Text = "Recipe",
                            Command = new Command(() =>
                            {
                                Detail = recipeNavigation;
                                IsPresented = false;
                            }),
                            Style = menuButtonStyle
                        },
                        new Button
                        {
                            Text = "Favorite",
                            Command = new Command(() =>
                            {
                                Detail = favNavigation;
                                IsPresented = false;
                            }),
                            Style = menuButtonStyle
                        },
                        new Button
                        {
                            Text = "Your Recipes",
                            Command = new Command(() =>
                            {
                                Detail = userRecipeNavigation;
                                IsPresented = false;
                            }),
                            Style = menuButtonStyle
                        },
                        new Button
                        {
                            Text = "Logout",
                            Command = new Command(() =>
                            {
                                Database.CurrentUser = null;
                                Application.Current.MainPage = loginRegisterPage;
                            }),
                            Style = menuButtonStyle
                        }

                    },
                    Margin = new Thickness(20)
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
