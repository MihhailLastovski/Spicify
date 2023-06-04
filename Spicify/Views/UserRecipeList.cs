using Spicify.Models;
using Spicify.Service;
using Spicify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Spicify.Views
{
    public class UserRecipeList : ContentPage
    {
        private PatternViewModel viewModel;
        private FlexLayout flexLayout;

        public UserRecipeList()
        {
            ToolbarItem refreshButton = new ToolbarItem
            {
                Text = "Refresh",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            refreshButton.Clicked += RefreshButtonClicked;
            ToolbarItems.Add(refreshButton);
            ToolbarItem addRecipe = new ToolbarItem
            {
                Text = "Add recipe",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            addRecipe.Clicked += AddRecipe_Clicked; ;
            ToolbarItems.Add(addRecipe);
            InitializeAsync();

        }

        private async void AddRecipe_Clicked(object sender, EventArgs e)
        {
            AddRecipePage addRecipePage = new AddRecipePage();
            await Navigation.PushAsync(addRecipePage);
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
        private async void RefreshButtonClicked(object sender, EventArgs e)
        {
            await InitializeAsync();
            flexLayout.SetBinding(BindableLayout.ItemsSourceProperty, new Binding("Patterns"));
        }
        private async Task InitializeAsync()
        {
            Database database = new Database("database.db3");

            viewModel = new PatternViewModel();
            viewModel.Patterns = await viewModel.UserRecipes();

            BindingContext = viewModel;

            ScrollView scrollView = new ScrollView();

            flexLayout = new FlexLayout
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
                    HeightRequest = 65,
                    TextColor = Color.Black,
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

                relativeLayout.Children.Add(stackLayout,
                    Constraint.RelativeToParent((parent) => parent.Width - frame.WidthRequest),
                    Constraint.RelativeToParent((parent) => parent.Height - frame.HeightRequest));

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                TapGestureRecognizer tapGestureRecognizerFrame = new TapGestureRecognizer();
                tapGestureRecognizerFrame.Tapped += FrameRecipeOpen;
                frame.GestureRecognizers.Add(tapGestureRecognizerFrame);
                stackLayout.Children.Add(nameLabel);
                stackLayout.Children.Add(image);

                frame.Content = relativeLayout;

                return new ContentView { Content = frame };
            });
            BindableLayout.SetItemTemplate(flexLayout, itemTemplate);

            scrollView.Content = flexLayout;

            this.Content = scrollView;
        }
    }
}