using Spicify.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Spicify.ViewModels
{
    public class RecipeDetailsViewModel : INotifyPropertyChanged
    {
        private RecipeDetailsModel recipe;

        public RecipeDetailsModel Recipe
        {
            get { return recipe; }
            set
            {
                recipe = value;
                OnPropertyChanged(nameof(Recipe));
            }
        }

        public RecipeDetailsViewModel(RecipeDetailsModel recipeDetails)
        {
            Recipe = recipeDetails;
        }
        public RecipeDetailsViewModel()
        {
            // Здесь вы должны получить данные рецепта, например, из API или другого источника данных
            // В этом примере просто создается фиктивный рецепт

            Recipe = new RecipeDetailsModel
            {
                Name = "Sample Recipe",
                Image = ImageSource.FromFile("recipe_image.jpg"),
                Description = "This is a sample recipe description.",
                Ingredients = new ObservableCollection<string>
                {
                    "Ingredient 1",
                    "Ingredient 2",
                    "Ingredient 3"
                },
                CookingInstructions = "Sample cooking instructions."
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
