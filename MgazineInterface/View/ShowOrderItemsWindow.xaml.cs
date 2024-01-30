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
    /// Logika interakcji dla klasy ShowOrderItemsWindow.xaml
    /// </summary>
    /// 

    public partial class ShowOrderItemsWindow : Window
    {
        private List<OrderItemsHelper> orderItems;


        private int _orderid;
        public ShowOrderItemsWindow(int orderid)
        {
            InitializeComponent();
            orderItems = new List<OrderItemsHelper>();
            UserList.ItemsSource = orderItems;
            _orderid = orderid;

            LoadOrderItems();
        }

        private async Task LoadOrderItems()
        {
            await LoadOrderItemsAsync();
            UserList.Items.Refresh();

        }

        private async Task LoadOrderItemsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7148/api/Orders/GetAllOrderandItemsByOrder/{_orderid}");

                    if (response.IsSuccessStatusCode)
                    {

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var orderItemsList = JsonConvert.DeserializeObject<List<OrderItemsHelper>>(jsonResponse);


                        orderItems.Clear();
                        foreach (var orderItemss in orderItemsList)
                        {
                            orderItems.Add(orderItemss);
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

        private void Refresh(object sender, RoutedEventArgs e)
        {
            LoadOrderItems();
        }

        private void SwitchView(object sender, RoutedEventArgs e)
        {
            AddOrderItemWindow anotherWindow = new AddOrderItemWindow(_orderid);


            anotherWindow.Show();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem != null)
            {
                try
                {
                    OrderItemsHelper selectedOrderItems = (OrderItemsHelper)UserList.SelectedItem;

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7148/api/OrderItems/DeleteOrderItems/{selectedOrderItems.Id}/");

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Product removed successfully.");
                            
                            await LoadOrderItemsAsync();
                            orderItems.Remove(selectedOrderItems);

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

    }
    
}
