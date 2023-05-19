using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Spicify.ViewModels
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CustomPattern> Patterns { get; set; }

        public PatternViewModel()
        {
            Patterns = new ObservableCollection<CustomPattern>();

            for (int i = 0; i < 4; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = RecipeAPI.GetRandomRecipe().Name,
                    ImageSource = RecipeAPI.GetRandomRecipe().Image,
                    ImageButton = null,
                    IsFavorite = false,
                    
                };
                Patterns.Add(pattern);
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
