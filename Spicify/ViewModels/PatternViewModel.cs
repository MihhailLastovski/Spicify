﻿using System;
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

            for (int i = 0; i < 5; i++)
            {
                CustomPattern pattern = new CustomPattern
                {
                    NameLabel = i.ToString(),
                    ImageSource = null,
                    ButtonCommand = new Command(() => ButtonClicked(i)),
                };
            }
        }

        private void ButtonClicked(int i)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}