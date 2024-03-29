﻿using MgazineInterface.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MgazineInterface.View
{
    /// <summary>
    /// Logika interakcji dla klasy GetAllClients.xaml
    /// </summary>
    public partial class GetAllClients : UserControl
    {
        private List<ClientsHelper> clients;
        public GetAllClients()
        {
            InitializeComponent();
            clients = new List<ClientsHelper>();
            UserList.ItemsSource = clients;


            LoadClients();
        }
        private async Task LoadClients()
        {
            await LoadClientsAsync();
            UserList.Items.Refresh();

        }

        private async Task LoadClientsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7148/api/Clients/GetAllClientsWithDetails/");

                    if (response.IsSuccessStatusCode)
                    {

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var clientList = JsonConvert.DeserializeObject<List<ClientsHelper>>(jsonResponse);


                        clients.Clear();
                        foreach (var clientt in clientList)
                        {
                            clients.Add(clientt);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem != null)
            {
                try
                {
                    ClientsHelper selectedClients = (ClientsHelper)UserList.SelectedItem;


                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7148/api/Clients/DeleteClient/{selectedClients.Id}/");

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Product removed successfully.");

                            await LoadClientsAsync();
                            clients.Remove(selectedClients);

                            UserList.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show($"Error: {response.ReasonPhrase}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a product to remove.");
            }

        }





        private void SwitchView(object sender, RoutedEventArgs e)
        {
            AddClientWindow anotherWindow = new AddClientWindow();


            anotherWindow.Show();

        }

        

        private void Refresh(object sender, RoutedEventArgs e)
        {
            LoadClients();

        }

        
    }
}
