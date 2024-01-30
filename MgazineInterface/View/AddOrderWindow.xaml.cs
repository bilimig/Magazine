using MgazineInterface.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MgazineInterface.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window

    {
        private List<ClientsHelper> clientsHelperList = new List<ClientsHelper>();
        private List<UserHelper> usersHelperList = new List<UserHelper>();
        private List<OrderTypeHelper> ordertypeHelperList = new List<OrderTypeHelper>();







        public AddOrderWindow()
        {
            InitializeComponent();
            LoadOrderList();
        }
       

        public async void LoadOrderList()
        {
            using (HttpClient client = new HttpClient())
            {
               
                var response = await client.GetAsync("https://localhost:7148/api/Clients/GetAllClients");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to a list of UomHelper
                    var clientsFromApi = JsonConvert.DeserializeObject<List<ClientsHelper>>(await response.Content.ReadAsStringAsync());

                    // Populate the ComboBox with the retrieved UOMs
                    clientid.ItemsSource = clientsFromApi;
                }
                else
                {
                    // Handle error
                }
            }

            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync("https://localhost:7148/api/OrderTypes/GetAllTypes");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to a list of UomHelper
                    var typesFromApi = JsonConvert.DeserializeObject<List<OrderTypeHelper>>(await response.Content.ReadAsStringAsync());

                    // Populate the ComboBox with the retrieved UOMs
                    typeid.ItemsSource = typesFromApi;
                }
                else
                {
                    // Handle error
                }
            }


            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync("https://localhost:7148/GetAllUsers");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to a list of UomHelper
                    var usersFromApi = JsonConvert.DeserializeObject<List<UserHelper>>(await response.Content.ReadAsStringAsync());

                    // Populate the ComboBox with the retrieved UOMs
                    userid.ItemsSource = usersFromApi;
                }
                else
                {
                    // Handle error
                }
            }



        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            OrdersJSON orders = new OrdersJSON();


            int useridd = (int)userid.SelectedValue;
            int clientidd = (int)clientid.SelectedValue;
            int typeidd = (int)typeid.SelectedValue;





            // Tutaj dodaj logikę obsługującą dodawanie danych
            orders.UserId = useridd;
            orders.ClientId = clientidd;
            orders.Date = DateTime.Now;
            orders.StatusId = 1;
            orders.TotalValue = 0;
            orders.TypeId = typeidd;



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

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
