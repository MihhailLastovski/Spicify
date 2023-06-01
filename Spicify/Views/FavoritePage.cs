using Spicify.Service;
using Spicify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Spicify.Views
{
    public class FavoritePage : ContentPage
    {
        public FavoritePage()
        {

                InitializeAsync();


        }
        public async void InitializeAsync()
        {
            SearchPage searchPage = new SearchPage();
            PatternViewModel viewModel = new PatternViewModel();
            int currentUserId = 1;
            searchPage.InitializeScrollView(viewModel, viewModel.SearchRecipesInfoFavorite(await new Database("database.db3").GetFavoriteRecipeIdsAsync(currentUserId)));
           
        }
    }
}