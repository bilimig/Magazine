using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Magazine.Models;
using MgazineInterface.Models;

namespace MgazineInterface.ViewModel
{
    public class GetAllClientsVM
    {
        public ObservableCollection<ClientsJSON> Clients { get; set; }

        public ICommand ShowWindowCommand { get; set; }

        
    }
}
