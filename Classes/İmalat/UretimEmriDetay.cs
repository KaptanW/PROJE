using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.İmalat
{
    internal class UretimEmriDetay
    {
        public int uretimemridetayID { get; set; }
        public int uretimemriID { get; set; }
        public int urunID { get; set; }
        public int urunmiktar { get; set; }
        public bool sil { get; set; }
    }
}
