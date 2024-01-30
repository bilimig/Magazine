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
    
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private async void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            ContactDetailsJSON contactDetails = new ContactDetailsJSON();

           
            contactDetails.Name = textBoxDane1.Text;
            contactDetails.SecondName = textBoxDane2.Text;
            contactDetails.Phone = textBoxDane3.Text;
            contactDetails.Address = textBoxDane4.Text;


            string jsonContent = JsonConvert.SerializeObject(contactDetails);


            using (HttpClient client = new HttpClient())
            {


               
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var response = await client.PostAsync("https://localhost:7148/api/ContactDetails/AddNewUserContactDetails", content);
                
                    

            }

            

            Close();
        }

    }
}

