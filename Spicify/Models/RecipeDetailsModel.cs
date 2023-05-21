using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Spicify.Models
{
    public class RecipeDetailsModel
    {
        public string Name { get; set; }
        public ImageSource Image { get; set; }
        public string Description { get; set; }
        public ObservableCollection<string> Ingredients { get; set; }
        public string CookingInstructions { get; set; }
    }
}
