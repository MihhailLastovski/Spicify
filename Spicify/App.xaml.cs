﻿using Spicify.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spicify
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new LoginRegisterPage();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
