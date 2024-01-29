using MgazineInterface.Models;
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
    /// Logika interakcji dla klasy AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : UserControl
    {
        public AddOrderView()
        {
            InitializeComponent();
        }
        private async void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            OrdersJSON orders = new OrdersJSON();

            // Tutaj dodaj logikę obsługującą dodawanie danych
            //orders.UserId = textBoxDane1.Text;
            //orders.ClientId = textBoxDane2.Text;
            //orders.Date = textBoxDane3.Text;
            //orders.StatusId = textBoxDane4.Text;
            //orders.TotalValue = textBoxDane5.Text;
            //orders.TypeId = textBoxDane6.Text;


            string jsonContent = JsonConvert.SerializeObject(orders);


            using (HttpClient client = new HttpClient())
            {


                // Przygotuj dane do wysłania jako JSON
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Wysyłanie żądania POST na odpowiedni endpoint (załóżmy, że endpoint to "/api/Clients/AddClient")
                using (var response = await client.PostAsync("https://localhost:7148/api/Orders/AddNewOrder", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Obsługa sukcesu
                    }
                    else
                    {
                        // Obsługa błędu
                    }
                }

            }
        }
    }
}