using Spicify.Models;
using Spicify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spicify.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Recipe : ContentPage
    {
        public Recipe()
        {

            PatternViewModel viewModel = new PatternViewModel();
            viewModel.Patterns = viewModel.SearchRecipesByIngredients();

            this.BindingContext = viewModel;

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

            this.Content = scrollView;
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

        private void ImageChangeTapped(object sender, EventArgs e)
        {
            Image imageButton = (Image)sender;
            CustomPattern pattern = (CustomPattern)imageButton.BindingContext;

            if (pattern.IsFavorite)
            {
                pattern.ImageButton = ImageSource.FromFile("unfav.png");
                pattern.IsFavorite = false;
            }
            else
            {
                pattern.ImageButton = ImageSource.FromFile("fav.png");
                pattern.IsFavorite = true;
            }
        }
    }
}