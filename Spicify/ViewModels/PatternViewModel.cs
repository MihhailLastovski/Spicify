using Spicify.Service;
using Spicify.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Spicify.RecipeAPI;

namespace Spicify.ViewModels
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        private Database database = new Database("database.db3");

        public ObservableCollection<CustomPattern> Patterns { get; set; }

        public PatternViewModel() { }

        public async Task<ObservableCollection<CustomPattern>> RandomRecipe()
        {
            Patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = RecipeAPI.GetRandomRecipes();
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();

            List<int> favoriteRecipeIds = await database.GetFavoriteRecipeIdsAsync(Database.CurrentUser.Id);

            for (int i = 0; i < recipes.Count; i++)
            {
                bool isFavorite = favoriteRecipeIds.Contains(recipes[i].RecipeId);

                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = isFavorite ? ImageSource.FromFile("fav.png") : ImageSource.FromFile("unfav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = isFavorite,
                    Ingredients = recipes[i].Ingredients,
                    CookingInstructions = recipes[i].CookingInstructions,
                    RecipeID = recipes[i].RecipeId
                };

                patterns.Add(pattern);
            }

            return patterns;
        }


        public async Task<ObservableCollection<CustomPattern>> SearchRecipesByIngredients(string[] ingredients)
        {
            Patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = RecipeAPI.SearchRecipesByIngredients(ingredients);
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();

            List<int> favoriteRecipeIds = await database.GetFavoriteRecipeIdsAsync(Database.CurrentUser.Id);

            for (int i = 0; i < recipes.Count; i++)
            {
                bool isFavorite = favoriteRecipeIds.Contains(recipes[i].RecipeId);

                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = isFavorite ? ImageSource.FromFile("fav.png") : ImageSource.FromFile("unfav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = isFavorite,
                    Ingredients = recipes[i].Ingredients,
                    CookingInstructions = recipes[i].CookingInstructions,
                    RecipeID = recipes[i].RecipeId
                };

                patterns.Add(pattern);
            }

            return patterns;
        }

        public async Task<ObservableCollection<CustomPattern>> SearchRecipes(string query)
        {
            Patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = RecipeAPI.SearchRecipesByName(query);
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();

            List<int> favoriteRecipeIds = await database.GetFavoriteRecipeIdsAsync(Database.CurrentUser.Id);

            for (int i = 0; i < recipes.Count; i++)
            {
                bool isFavorite = favoriteRecipeIds.Contains(recipes[i].RecipeId);

                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = isFavorite ? ImageSource.FromFile("fav.png") : ImageSource.FromFile("unfav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = isFavorite,
                    Ingredients = recipes[i].Ingredients,
                    CookingInstructions = recipes[i].CookingInstructions,
                    RecipeID = recipes[i].RecipeId
                };

                patterns.Add(pattern);
            }

            return patterns;
        }

        public async Task<ObservableCollection<CustomPattern>> SearchRecipesInfoFavorite(List<int> ids)
        {
            Patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = RecipeAPI.GetRecipeInformation(ids);
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();

            List<int> favoriteRecipeIds = await database.GetFavoriteRecipeIdsAsync(Database.CurrentUser.Id);

            for (int i = 0; i < recipes.Count; i++)
            {
                bool isFavorite = favoriteRecipeIds.Contains(recipes[i].RecipeId);

                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = isFavorite ? ImageSource.FromFile("fav.png") : ImageSource.FromFile("unfav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = isFavorite,
                    Ingredients = recipes[i].Ingredients,
                    CookingInstructions = recipes[i].CookingInstructions,
                    RecipeID = recipes[i].RecipeId
                };

                patterns.Add(pattern);
            }

            return patterns;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
