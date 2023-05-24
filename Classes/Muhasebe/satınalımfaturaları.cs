using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_PROJESİ.Classes.Muhasebe
{
    internal class fatura
    {
        public int faturaID { get; set; }
        public int CariID { get; set; }

        public string faturatarihi { get; set; }
        public float Tutar { get; set; }
        public string odemebilgisi { get; set; }
        public bool iade { get; set; }
        public bool sil { get; set;}
    }
}
