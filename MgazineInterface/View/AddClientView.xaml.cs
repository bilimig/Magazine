using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
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
using Newtonsoft.Json;


namespace MgazineInterface.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddClientView.xaml
    /// </summary>
    public partial class AddClientView : UserControl
    {
        public AddClientView()
        {
            InitializeComponent();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            ContactDetailsJSON contactDetails = new ContactDetailsJSON();

            // Tutaj dodaj logikę obsługującą dodawanie danych
            contactDetails.Name= textBoxDane1.Text;
            contactDetails.SecondName = textBoxDane2.Text;
            contactDetails.Address = textBoxDane3.Text;
            contactDetails.Phone = textBoxDane4.Text;


            string jsonContent = JsonConvert.SerializeObject(contactDetails);


            using (HttpClient client = new HttpClient())
            {


                // Przygotuj dane do wysłania jako JSON
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Wysyłanie żądania POST na odpowiedni endpoint (załóżmy, że endpoint to "/api/Clients/AddClient")
                var response =  client.PostAsync("https://localhost:7148/api/Clients/AddClient", content);

                
            }

            // Możesz tutaj użyć wprowadzonych danych według potrzeb
            // Na przykład, można utworzyć obiekt i dodać go do kolekcji, baz danych, itp.
        }
    }
}
