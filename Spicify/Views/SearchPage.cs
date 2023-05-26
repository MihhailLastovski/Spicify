using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Spicify.Views
{
    public class SearchPage : ContentPage
    {
        public SearchPage()
        {
            SearchBar searchBar = new SearchBar
            {
                Placeholder = "Search items...",
                PlaceholderColor = Color.Orange,
                TextColor = Color.Orange,
                TextTransform = TextTransform.Lowercase,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(SearchBar)),
                FontAttributes = FontAttributes.Italic
            };
            Content= searchBar;
        }
    }
}