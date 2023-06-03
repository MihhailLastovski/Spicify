using Spicify.Models;
using Spicify.Service;
using Spicify.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Spicify.Views
{
    public class SearchPage : ContentPage
    {
        private Entry searchEntry;
        private Button searchButton;
        private StackLayout resultContainer;
        private StackLayout main;
        private Database database = new Database("database.db3");
        public SearchPage()
        {
            searchEntry = new Entry
            {
                Placeholder = "Enter recipe name or ingredients",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            searchButton = new Button
            {
                Text = "Search",
                HorizontalOptions = LayoutOptions.End
            };
            searchButton.Clicked += OnSearchButtonClicked;

            resultContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Padding = new Thickness(10)
            };

            main = new StackLayout { Children = { searchEntry, searchButton, resultContainer } };

            Content = main;

        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string query = searchEntry.Text.Trim();

            if (!string.IsNullOrEmpty(query))
            {
                resultContainer.Children.Clear();

                PatternViewModel viewModel = new PatternViewModel();
                if (query.Contains(" "))
                {
                    string[] ingredients = query.Split(' ');
                    InitializeScrollView(viewModel, await viewModel.SearchRecipesByIngredients(ingredients));
                }
                else
                {
                    InitializeScrollView(viewModel ,await viewModel.SearchRecipes(query));
                }

                
            }
        }      

        public void InitializeScrollView(PatternViewModel viewModel, ObservableCollection<CustomPattern> fromApi)
        {
            viewModel.Patterns = fromApi;

            this.BindingContext = viewModel;
            ScrollView existingScrollView = main.Children.OfType<ScrollView>().FirstOrDefault();
            if (existingScrollView != null)
            {
                main.Children.Remove(existingScrollView);
            }

            ScrollView scrollView = new ScrollView();

            FlexLayout flexLayout = new FlexLayout
            {
                Wrap = FlexWrap.Wrap,
                JustifyContent = FlexJustify.SpaceEvenly
            };

            flexLayout.SetBinding(BindableLayout.ItemsSourceProperty, new Binding("Patterns"));

            DataTemplate itemTemplate = new DataTemplate(() =>
            {
                Frame frame = new Frame
                {
                    WidthRequest = 150,
                    HeightRequest = 220,
                    BorderColor = Color.Black,
                    CornerRadius = 10,
                    Margin = new Thickness(2),
                };

                RelativeLayout relativeLayout = new RelativeLayout();

                StackLayout stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                };

                Label nameLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    FontSize = 18,
                    HeightRequest = 65
                };
                nameLabel.SetBinding(Label.TextProperty, "NameLabel");

                Image image = new Image
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 190,
                    WidthRequest = 150,
                    Aspect = Aspect.AspectFit,
                };
                image.SetBinding(Image.SourceProperty, "ImageSource");

                Image imageButton = new Image
                {
                    HeightRequest = 45,
                    WidthRequest = 45,
                };
                imageButton.SetBinding(Image.SourceProperty, "ImageButton");

                relativeLayout.Children.Add(stackLayout,
                    Constraint.RelativeToParent((parent) => parent.Width - frame.WidthRequest),
                    Constraint.RelativeToParent((parent) => parent.Height - frame.HeightRequest));

                relativeLayout.Children.Add(imageButton,
                    Constraint.RelativeToParent((parent) => parent.Width - imageButton.WidthRequest),
                    Constraint.RelativeToParent((parent) => parent.Height - imageButton.HeightRequest));

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                TapGestureRecognizer tapGestureRecognizerFrame = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += ImageChangeTapped;
                imageButton.GestureRecognizers.Add(tapGestureRecognizer);
                tapGestureRecognizerFrame.Tapped += FrameRecipeOpen;
                frame.GestureRecognizers.Add(tapGestureRecognizerFrame);
                stackLayout.Children.Add(nameLabel);
                stackLayout.Children.Add(image);

                frame.Content = relativeLayout;

                return new ContentView { Content = frame };
            });


            BindableLayout.SetItemTemplate(flexLayout, itemTemplate);

            scrollView.Content = flexLayout;
            main.Children.Add(scrollView);

            this.Content = main;
        }

        private async void FrameRecipeOpen(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            CustomPattern pattern = (CustomPattern)frame.BindingContext;

            RecipeDetailsModel recipeDetails = new RecipeDetailsModel
            {
                Name = pattern.Data.NameLabel,
                Image = pattern.Data.ImageSource,
                Description = pattern.Data.Description,
                Ingredients = pattern.Data.Ingredients,
                CookingInstructions = pattern.Data.CookingInstructions,
            };

            RecipeDetailsPage detailsPage = new RecipeDetailsPage(new RecipeDetailsViewModel(recipeDetails));
            await Navigation.PushAsync(detailsPage);
        }

        private async void ImageChangeTapped(object sender, EventArgs e)
        {
            Image imageButton = (Image)sender;
            CustomPattern pattern = (CustomPattern)imageButton.BindingContext;

            if (pattern.IsFavorite)
            {
                pattern.ImageButton = ImageSource.FromFile("unfav.png");
                pattern.IsFavorite = false;
                FavoriteRecipe favoriteRecipe = await database.GetFavoriteRecipe(pattern.RecipeID);
                if (favoriteRecipe != null)
                {
                    await database.DeleteFavoriteRecipe(favoriteRecipe);
                }
            }
            else
            {
                pattern.ImageButton = ImageSource.FromFile("fav.png");
                pattern.IsFavorite = true;
                FavoriteRecipe favoriteRecipe = new FavoriteRecipe
                {
                    UserID = Database.CurrentUser.Id,
                    RecipeID = pattern.RecipeID
                };
                await database.AddFavoriteRecipe(favoriteRecipe);
            }

        }
    }
}