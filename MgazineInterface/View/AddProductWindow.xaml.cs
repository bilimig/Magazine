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
    /// Logika interakcji dla klasy AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {

        
        
            private List<UomHelper> uoms = new List<UomHelper>();

            public AddProductWindow()
            {
                InitializeComponent();
                LoadUomsAsync();
            }
            private async void LoadUomsAsync()
            {
                using (HttpClient client = new HttpClient())
                {
                    // Fetch all UOMs from the API
                    var response = await client.GetAsync("https://localhost:7148/api/Uoms/GetAllUoms");

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response content to a list of UomHelper
                        var uomsFromApi = JsonConvert.DeserializeObject<List<UomHelper>>(await response.Content.ReadAsStringAsync());

                        // Populate the ComboBox with the retrieved UOMs
                        comboBoxUoms.ItemsSource = uomsFromApi;
                    }
                    else
                    {
                        // Handle error
                    }
                }
            
            }

            private async void Dodaj_Click(object sender, RoutedEventArgs e)
            {
                ProductsJSON product = new ProductsJSON();
                

                int uom = (int)comboBoxUoms.SelectedValue;
                int.TryParse(textBoxDane4.Text, out int amount);
                
                // Tutaj dodaj logikę obsługującą dodawanie danych
                product.Name = textBoxDane1.Text;
                product.UomId = uom;
                product.BaseUnit = textBoxDane3.Text;
                product.Amount = amount;


                string jsonContent = JsonConvert.SerializeObject(product);



                using (HttpClient client = new HttpClient())
                {


                    
                    StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    
                    using (var response = await client.PostAsync("https://localhost:7148/api/Products/AddNewProduct", content))
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

        private void comboBoxUoms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
