﻿using System;
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
        private ICommand buttonCommand;

        public string NameLabel 
        {
            get { return nameLabel; }
            set 
            { 
                nameLabel = value; 
                OnPropertyChanged("Name");
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
        public ICommand ButtonCommand
        {
            get { return buttonCommand; }
            set
            {
                buttonCommand = value;
                OnPropertyChanged("Button");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}