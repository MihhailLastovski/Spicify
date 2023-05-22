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
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
