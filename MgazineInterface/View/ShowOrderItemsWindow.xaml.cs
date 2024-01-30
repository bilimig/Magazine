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
        private List<OrderStatusHelper> orderStatuses;


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
            UpdateTotalValue();

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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7148/api/OrderStatuses/GetAllStatuses/");

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response content to a list of UomHelper
                        var uomsFromApi = JsonConvert.DeserializeObject<List<OrderStatusHelper>>(await response.Content.ReadAsStringAsync());

                        // Populate the ComboBox with the retrieved UOMs
                        comboBoxStatus.ItemsSource = uomsFromApi;
                    }
                    else
                    {
                        // Handle error
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void UpdateTotalValue()
        {
            decimal totalValue = 0;

            foreach (OrderItemsHelper orderItem in orderItems)
            {

                if (orderItem.Amount.HasValue && orderItem.Price.HasValue)
                {
                    totalValue += orderItem.Amount.Value * orderItem.Price.Value;
                }
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:7148/api/Orders/ChangeOrderTotalValue/{_orderid}/{totalValue}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                       

                        
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



        private async void ChangeStatus(object sender, RoutedEventArgs e)
        {
            int currentStatus = (int) comboBoxStatus.SelectedValue;
            using (HttpClient client = new HttpClient())
            {

                // Fetch all UOMs from the API
                var response = await client.PutAsync($"https://localhost:7148/api/Orders/UpdateOrderStatus/{_orderid}/{currentStatus}", null) ;

               
                
            }

        }

        private void comboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    
    
}
