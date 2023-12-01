using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DemLab.Models;
using Xamarin.Forms;
using DemLab.APIs;

namespace DemLab.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Login Login { get; set; }
        ApiService apiService;
        string result;
        public string Result
        {
            get => result;
            set
            {
                result = value;
                var args = new PropertyChangedEventArgs(nameof(Result));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }

        public LoginViewModel()
        {
            apiService = new ApiService();
            Login = new Login();
            LoginCommand = new Command(async () =>
            {
                if(Login.Email == "admin" && Login.Password == "123")
                {
                    Application.Current.MainPage = new View.admin();
                }
                var response = await apiService.Login(Login);
                if (response)
                {
                    Result = "Success";
                    Application.Current.MainPage = new View.MainTabbedPage();
                }
                else
                {
                    Result = "Fail !!!";
                }
            });

            RegisterCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Register());
            });
        }
    }
}
