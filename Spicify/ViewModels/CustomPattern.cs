﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Spicify.Models;
using System.Xml;
using System.Collections.ObjectModel;

namespace Spicify.ViewModels
{
    public class CustomPattern : INotifyPropertyChanged
    {
        PatternViewModel patternViewModel;
        public Data Data { get; private set; }
        public CustomPattern() 
        {
            Data = new Data();
        }

        public PatternViewModel PatternViewModel 
        {
            get { return patternViewModel; }
            set 
            {
                patternViewModel = value;
                OnPropertyChanged("PatternViewModel");
            }
        }

        public string NameLabel
        {
            get { return Data.NameLabel; }
            set
            {
                Data.NameLabel = value;
                OnPropertyChanged("NameLabel");
            }
        }

        public ImageSource ImageSource
        {
            get { return Data.ImageSource; }
            set
            {
                Data.ImageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public ImageSource ImageButton
        {
            get { return Data.ImageButton; }
            set
            {
                Data.ImageButton = value;
                OnPropertyChanged("ImageButton");
            }
        }

        public bool IsFavorite
        {
            get { return Data.IsFavorite; }
            set
            {
                Data.IsFavorite = value;
                OnPropertyChanged("IsFavorite");
            }
        }

        public string Description
        {
            get { return Data.Description; }
            set
            {
                Data.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public string CookingInstructions
        {
            get { return Data.CookingInstructions; }
            set
            {
                Data.CookingInstructions = value;
                OnPropertyChanged("CookingInstructions");
            }
        }

        public List<string> Ingredients
        {
            get { return Data.Ingredients; }
            set
            {
                Data.Ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }

        public int RecipeID 
        {
            get { return Data.RecipeID; }
            set 
            { Data.RecipeID = value;
              OnPropertyChanged("RecipeID");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}