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
    public partial class ReceiptPage : ContentPage
    {
        public ReceiptPage()
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
                    HeightRequest = 150,
                    Margin = new Thickness(2),
                    BorderColor = Color.Black,
                    CornerRadius = 10
                };

                StackLayout stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical
                };

                Label nameLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    FontSize = 18
                };
                nameLabel.SetBinding(Label.TextProperty, "NameLabel");

                Image image = new Image
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 65
                };
                image.SetBinding(Image.SourceProperty, "ImageSource");

                Image imageButton = new Image
                {
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.End,
                    HeightRequest = 25
                };
                imageButton.SetBinding(Image.SourceProperty, "ImageButton");

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
            PatternViewModel viewModel = (PatternViewModel)BindingContext;

            ImageSource currentImage = viewModel.ImageButton;

            if (currentImage.Equals(ImageSource.FromFile("fav.png")))
            {
                viewModel.ImageButton = ImageSource.FromFile("unfav.png").ToString();
            }
            else if (currentImage.Equals(ImageSource.FromFile("unfav.png")))
            {
                viewModel.ImageButton = ImageSource.FromFile("fav.png").ToString();
            }
        }
    }
}