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

        public PatternViewModel()
        {
            
        }

        public void RandomRecipe()
        {
            Patterns = new ObservableCollection<CustomPattern>();
            List<MyObject> recipes = RecipeAPI.GetRandomRecipes();

            for (int i = 0; i < 6; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = ImageSource.FromFile("unfav.png"),
                    Description = recipes[i].Description,
                    IsFavorite = false,
                    Ingredients = recipes[i].Ingredients,
                    CookingInstructions = recipes[i].CookingInstructions

                };
                Patterns.Add(pattern);
            }
        }

        public ObservableCollection<CustomPattern> SearchRecipesByIngredients()
        {
            ObservableCollection<CustomPattern> patterns = new ObservableCollection<CustomPattern>();
            string[] ingrid = { "potato", "tomatos" };
            List<MyObject> recipes = RecipeAPI.SearchRecipesByIngredients(ingrid);

            for (int i = 0; i < 6; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = recipes[i].Name,
                    ImageSource = recipes[i].Image,
                    ImageButton = ImageSource.FromFile("unfav.png"),
                    IsFavorite = false,
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
