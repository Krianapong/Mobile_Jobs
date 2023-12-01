using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DemLab.Models;
using Xamarin.Forms;
using DemLab.APIs;
using Xamarin.Essentials;

namespace DemLab.ViewModels
{
    class ProductListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Product> Products
        {
            get
            {
                return myproducts;
            }
            set
            {
                myproducts = value;
                var args = new PropertyChangedEventArgs(nameof(Products));
                PropertyChanged?.Invoke(this, args);
            }
        }
        ObservableCollection<Product> myproducts;
        public Command SelectCommand { get; }
        public Command CloseCommand{ get; }
        public Command AddCommand { get; }
        public Product selectedProduct { get; set; }

        ApiService apiService;
        public ProductListViewModel()
        {
            Products = new ObservableCollection<Product>();
            apiService = new ApiService();

            GetProduct();
            //Products.Add(new Product { Title = "AAA" });
            //Products.Add(new Product { Title = "AAA" });

            SelectCommand = new Command(async () =>
            {
                var sendVar = new { selectedProduct = selectedProduct, CloseCommand = CloseCommand, AddCommand = AddCommand };
                var ProdDetail = new View.ProductDetail
                {
                    BindingContext = sendVar
                };
                await Application.Current.MainPage.Navigation.PushModalAsync(ProdDetail);
            });

            AddCommand = new Command(async () =>
            {
                Order Order = new Order();
                Order.Username = selectedProduct.Title;
                //Order.ProdID = selectedProduct.ID;
                //Order.Price = selectedProduct.Price;
                Order.Username = Preferences.Get("username", "");
                var responsr = await apiService.AddOrder(Order);
                if(responsr)
                {
                    await Application.Current.MainPage.DisplayAlert("Order", "Product added", "OK");
                }
            });

            CloseCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });

        }

        async void GetProduct()
        {
            Products = await apiService.GetProducts();
        }
    }
}
