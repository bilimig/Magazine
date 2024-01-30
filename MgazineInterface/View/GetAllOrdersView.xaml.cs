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
    /// Logika interakcji dla klasy GetAllOrdersView.xaml
    /// </summary>
    public partial class GetAllOrdersView : UserControl
    {

        private List<OrdersHelper> orders;

        public GetAllOrdersView()
        {
            InitializeComponent();
            orders = new List<OrdersHelper>();
            UserList.ItemsSource = orders;

            LoadOrdersAsync();
        }


        private async Task LoadOrdersAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7148/api/Orders/GetAllOrders");

                    if (response.IsSuccessStatusCode)
                    {

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var orderList = JsonConvert.DeserializeObject<List<OrdersHelper>>(jsonResponse);


                        orders.Clear();
                        foreach (var orderr in orderList)
                        {
                            orders.Add(orderr);
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh(object sender, RoutedEventArgs e)
        {

        }

        private void SwitchView(object sender, RoutedEventArgs e)
        {

            AddOrderWindow anotherWindow = new AddOrderWindow();


            anotherWindow.Show();

        }

        private void SwitchToOrderItemWindow(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem != null)
            {
                try
                {
                    OrdersHelper selectedOrders = (OrdersHelper)UserList.SelectedItem;

                    ShowOrderItemsWindow anotherWindow = new ShowOrderItemsWindow(selectedOrders.Id);


                    anotherWindow.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select order to show.");
            }
        }

        
    }
}
