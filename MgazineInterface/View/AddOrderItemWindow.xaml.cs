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
using System.Windows.Shapes;

namespace MgazineInterface.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddOrderItemWindow.xaml
    /// </summary>
    public partial class AddOrderItemWindow : Window
    {
        private List<ProductHelper> products;
        private int _orderid;
        public AddOrderItemWindow(int orderid)
        {
            InitializeComponent();
            _orderid = orderid;

            LoadProductsAsync();
        }

        private async void LoadProductsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                // Fetch all UOMs from the API
                var response = await client.GetAsync("https://localhost:7148/api/Products/GetAllProducts");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to a list of UomHelper
                    var productsFromApi = JsonConvert.DeserializeObject<List<ProductHelper>>(await response.Content.ReadAsStringAsync());

                    // Populate the ComboBox with the retrieved UOMs
                    textBoxDane1.ItemsSource = productsFromApi;
                }
                else
                {
                    // Handle error
                }
            }
        }

        private async void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            OrderItemsHelper orderItem = new OrderItemsHelper();


            int product = (int)textBoxDane1.SelectedValue;
            decimal.TryParse(textBoxDanePrice.Text, out decimal decimalValue);
            int amount = int.Parse(textBoxAmount.Text);


            
            orderItem.ProductId = product;
            orderItem.Price = decimalValue;
            orderItem.Amount = amount;
            orderItem.OrderId = _orderid;


            string jsonContent = JsonConvert.SerializeObject(orderItem);



            using (HttpClient client = new HttpClient())
            {



                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                using (var response = await client.PostAsync("https://localhost:7148/api/OrderItems/AddNewOrderItem", content))
                {
                    if (response.IsSuccessStatusCode)
                    {

                    }
                    else
                    {

                    }
                }

            }

            Close();

        }
    }
    
}
