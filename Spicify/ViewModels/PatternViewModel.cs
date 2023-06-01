using Spicify.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using static Spicify.RecipeAPI;

namespace Spicify.ViewModels
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CustomPattern> Patterns { get; set; }

        public PatternViewModel() { }

        public ObservableCollection<CustomPattern> RandomRecipe()
        {
            Patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = RecipeAPI.GetRandomRecipes();
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();
            for (int i = 0; i < recipes.Count; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = ImageSource.FromFile("unfav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = false,
                    Ingredients = recipes[i].Ingredients,
                    CookingInstructions = recipes[i].CookingInstructions,
                    RecipeID = recipes[i].RecipeId

                };
                patterns.Add(pattern);
            }

            return patterns;
        }

        public ObservableCollection<CustomPattern> SearchRecipesByIngredients(string[] ingredients)
        {
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = RecipeAPI.SearchRecipesByIngredients(ingredients);

            for (int i = 0; i < recipes.Count; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = ImageSource.FromFile("unfav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = false,
                    Ingredients = recipes[i].Ingredients,
                    CookingInstructions = recipes[i].CookingInstructions,
                    RecipeID = recipes[i].RecipeId
                };
                patterns.Add(pattern);
            }

            return patterns;
        }

        public ObservableCollection<CustomPattern> SearchRecipes(string query)
        {
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = SearchRecipesByName(query);

            for (int i = 0; i < recipes.Count; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = ImageSource.FromFile("unfav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = false,
                    Ingredients = recipes[i].Ingredients,
                    CookingInstructions = recipes[i].CookingInstructions,
                    RecipeID = recipes[i].RecipeId
                };
                patterns.Add(pattern);
            }

            return patterns;
        }

        public ObservableCollection<CustomPattern> SearchRecipesInfoFavorite(List<int> ids)
        {
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = GetRecipeInformation(ids);

            for (int i = 0; i < recipes.Count; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = ImageSource.FromFile("fav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = false,
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
