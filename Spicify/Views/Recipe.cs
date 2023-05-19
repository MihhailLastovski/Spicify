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
                    HeightRequest = 180,
                    BorderColor = Color.Black,
                    CornerRadius = 10,
                    Margin = new Thickness(2),
                };

                StackLayout stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                };

                Label nameLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    FontSize = 18,
                    HeightRequest = 70
                };
                nameLabel.SetBinding(Label.TextProperty, "NameLabel");

                Image image = new Image
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 180,
                    WidthRequest = 130,
                    Aspect = Aspect.AspectFill

                };
                image.SetBinding(Image.SourceProperty, "ImageSource");

                Image imageButton = new Image
                {
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.End,
                    Source = ImageSource.FromFile("unfav.png"),
                };

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += ImageChangeTapped;
                imageButton.GestureRecognizers.Add(tapGestureRecognizer);

                stackLayout.Children.Add(nameLabel);
                stackLayout.Children.Add(image);
                stackLayout.Children.Add(imageButton);

                frame.Content = stackLayout;

                return new ContentView { Content = frame };
            });

            BindableLayout.SetItemTemplate(flexLayout, itemTemplate);

            scrollView.Content = flexLayout;

            this.Content = scrollView;
        }

        private void ImageChangeTapped(object sender, EventArgs e)
        {
            //Image image = (Image)sender;
            //image.Source = RecipeAPI.GetRandomRecipe();

            ////ImageSource imgSrc = image.Source;
            ////ImageSource imgSrcFav = ImageSource.FromFile("fav.png");
            ////ImageSource imgSrcUnFav = ImageSource.FromFile("unfav.png");
            ////if (imgSrc == imgSrcUnFav)
            ////{
            ////    image.Source = ImageSource.FromFile("fav.png");
            ////}
            ////else if (imgSrc == imgSrcFav)
            ////{
            ////    image.Source = ImageSource.FromFile("unfav.png");
            ////}
        }
    }
}