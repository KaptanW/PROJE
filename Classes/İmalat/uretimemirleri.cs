using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.İmalat
{
    public class uretimemirleri
    {
        public int uretimemriID { get; set; }
        public string calisanadi { get; set; }

        public string verilistarihi { get; set; }
        public string baslangıctarihi { get; set; }
        public string bitistarihi { get; set; }
        public string planlananbaslangıctarihi { get; set; }
        public int siparisID { get; set; }
        public string cıkanurunID { get; set; }
        public int rotaID { get; set; }
        public string uretimindurumu { get; set; }
        public bool sil { get; set; }


    }
}
