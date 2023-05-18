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
        private string _imageButton;
        public string ImageButton
        {
            get { return _imageButton; }
            set
            {
                if (_imageButton != value)
                {
                    _imageButton = value;
                    OnPropertyChanged(nameof(ImageButton));
                }
            }
        }
        public ObservableCollection<CustomPattern> Patterns { get; set; }

        public PatternViewModel()
        {
            Patterns = new ObservableCollection<CustomPattern>();

            for (int i = 0; i < 20; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = (i + 1).ToString(),
                    ImageSource = ImageSource.FromFile("unfav.png"),
                    ImageButton = ImageSource.FromFile("unfav.png"),
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
