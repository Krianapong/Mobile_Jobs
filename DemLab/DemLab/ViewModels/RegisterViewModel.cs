using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DemLab.Models;
using Xamarin.Forms;
using DemLab.APIs;

namespace DemLab.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Register Register { get; set; }
        ApiService apiService;
        public Command BackCommand { get; }
        public Command RegisterCommand { get; }

        public RegisterViewModel()
        {
            apiService = new ApiService();
            Register = new Register();

            BackCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });

            RegisterCommand = new Command(async () =>
            {
               var responsr = await apiService.Register(Register);
                if(responsr)
                {
                    await Application.Current.MainPage.DisplayAlert("Register", "Register successfully", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Register", "Faill!!!", "OK");
                }
            });

            BackCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Login());
            });
        }
    }
}
