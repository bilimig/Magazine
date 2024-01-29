using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MgazineInterface.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace MgazineInterface.View
{
    /// <summary>
    /// Logika interakcji dla klasy GetAllProductsView.xaml
    /// </summary>
    public partial class GetAllProductsView : UserControl
    {
        private List<ProductHelper> products;

        public GetAllProductsView()
        {
            InitializeComponent();
            products = new List<ProductHelper>();
            UserList.ItemsSource = products;

            // Call the method to retrieve and display products
            LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7148/api/Products/GetAllProducts");

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response content to a collection of Product objects using Newtonsoft.Json
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var productList = JsonConvert.DeserializeObject<List<ProductHelper>>(jsonResponse);

                        // Clear the existing collection and add the new products
                        products.Clear();
                        foreach (var product in productList)
                        {
                            products.Add(product);
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
                    ProductHelper selectedProduct = (ProductHelper)UserList.SelectedItem;

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7148/api/Products/DeleteProduct/{selectedProduct.Id}");

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Product removed successfully.");
                            
                            await LoadProductsAsync();
                            products.Remove(selectedProduct);

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
