using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DemLab.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace DemLab.APIs
{
    class ApiService
    {
        HttpClient client;

        public ApiService()
        {
            client = new HttpClient();
        }

        public async Task<ObservableCollection<Product>> GetProducts()
        {
            ObservableCollection<Product> Items = null;

            try
            {
                var response = await client.GetAsync("http://10.0.2.2:52796/api/JobsAPI");
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<Product>> (content);
                    return Items;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return null;
        }
        public async Task<ObservableCollection<Carts>> GetCarts()
        {
            ObservableCollection<Carts> Items = null;

            try
            {
                var response = await client.GetAsync("http://10.0.2.2:52796/api/AspNetUsers");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<Carts>>(content);
                    return Items;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return null;
        }
        public async Task<bool> Register(Register Item)
        {
            try
            {
                string json = JsonConvert.SerializeObject(Item);
                StringContent sContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://10.0.2.2:55544/api/Account/Register", sContent);

                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }
        public async Task<bool> Login(Login login)
        {
            User Item = null;
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("Content-Type", "application/x-www-from-urlencode");
                dict.Add("grant_type", "password");
                dict.Add("username", login.Email);
                dict.Add("password", login.Password);

                var response = await client.PostAsync("http://10.0.2.2:55544/token", new FormUrlEncodedContent(dict));
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Item = JsonConvert.DeserializeObject<User>(content);
                    Preferences.Set("username", Item.userName);
                    Preferences.Set("token_type", Item.token_type);
                    Preferences.Set("access_token", Item.access_token);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return false;
        }
        public async Task<bool> AddOrder(Order Item)
        {
            try
            {
                string json = JsonConvert.SerializeObject(Item);
                StringContent sContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://10.0.2.2:52796/api/AspNetUsers2", sContent);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
        public async Task<ObservableCollection<Order>> GetUserOrders()
        {
            ObservableCollection<Order> Items = null;
            var username = Preferences.Get("username", "");
            try
            {
                var response = await client.GetAsync("http://10.0.2.2:52796/api/AspNetUsers?username=" + username);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<Order>>(content);
                    return Items;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return null;
        }
        public async Task<bool> Logout()
        {
            try
            {
                var access_token = Preferences.Get("access_token", "");
                var token_type = Preferences.Get("token_type", "");
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token_type + " " + access_token);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer" + access_token);

                var response = await client.PostAsync("http://10.0.2.2:64080/api/Account/logout", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
