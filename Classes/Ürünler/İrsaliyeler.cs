using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.Ürünler
{
    internal class İrsaliyeler
    {
        public int irsaliyeID { get; set; }
        public string CariAdi { get; set; }

        public int siparisID { get; set; }

        public string tarih { get; set; }
        public string kargofirması { get; set; }

        public bool sil { get; set; }
        public bool iade { get; set; }
    }
}
