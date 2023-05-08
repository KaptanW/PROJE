using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes
{
    internal class Satinalmasiparisleri
    {
        public int satinalmaSiparisID { get; set; }
        public string CariAdi { get; set; }
        public int Calısanid { get; set; }
        public string siparistarihi { get; set; }
        public int totaltutar { get; set; }
        public string gönderilenkargofirması { get; set; }
        public bool sil { get; set; }
    }
}
