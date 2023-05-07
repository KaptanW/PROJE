using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.Muhasebe
{
    internal class satışiade
    {
        public int satisiadeirsaliyesiID { get; set; } 

        public int faturaid { get; set; }
        public int tarih { get; set; }
        public int kargofirmasi { get; set; }
        public bool sil { get; set; }
    }
}
