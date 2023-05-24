using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.Muhasebe
{
    internal class Faturadetay
    {
        public int faturadetayid { get; set; }
        public int faturaID { get; set; }
        public int urunID { get; set; }
        public int urunadet { get; set; }
        public float birimfiyat { get; set; }
        public bool sil { get; set; }
    }
}
