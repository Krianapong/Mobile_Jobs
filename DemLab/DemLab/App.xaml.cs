﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemLab
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new View.MainTabbedPage();
            MainPage = new NavigationPage(new View.Login());
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
