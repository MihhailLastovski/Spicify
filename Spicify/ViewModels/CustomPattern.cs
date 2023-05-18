using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Spicify.ViewModels
{
    public class CustomPattern : INotifyPropertyChanged
    {
        private string nameLabel;
        private ImageSource imageSource;
        private ImageSource imageButton;
        private bool isFavorite;

        public string NameLabel
        {
            get { return nameLabel; }
            set
            {
                nameLabel = value;
                OnPropertyChanged("NameLabel");
            }
        }

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("Source");
            }
        }

        public ImageSource ImageButton
        {
            get { return imageButton; }
            set
            {
                imageButton = value;
                OnPropertyChanged("Image");
            }
        }

        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                isFavorite = value;
                OnPropertyChanged("Favorite");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
