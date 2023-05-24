using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.Satış
{
    internal class Satışsipariş
    {
        public int SiparisID { get; set; }
        public string CariAdi { get; set; }
        public string siparistarihi { get; set; }
        public float totaltutar { get; set; }
        public string gönderilenkargofirması { get; set; }
        public bool sil { get; set; }
    }
}
