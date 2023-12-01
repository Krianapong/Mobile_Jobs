using DemLab.APIs;
using DemLab.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DemLab.ViewModels
{
    class CartViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ApiService apiService;
        public Command BackCommand { get; }

        public Command LogoutCommand { get; }
        public ObservableCollection<Order> Orders
        {
            get
            {
                return myorders;
            }
            set
            {
                myorders = value;
                var args = new PropertyChangedEventArgs(nameof(Orders));
                PropertyChanged?.Invoke(this, args);
            }
        }
        ObservableCollection<Order> myorders;

        public CartViewModels()
        {
            apiService = new ApiService();
            Orders = new ObservableCollection<Order>();
            BackCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            GetCart();

           LogoutCommand = new Command(async () =>
            {
               var responsr = await apiService.Logout();
                if(responsr)
                {
                    Application.Current.MainPage = new NavigationPage(new View.Login());
                }
            }); 

            LogoutCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Login());
            });
            BackCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Login());
            });
        }
        async void GetCart()
        {
            Orders = await apiService.GetUserOrders();
        }
    }
}
