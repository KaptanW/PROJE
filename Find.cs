using ERP_PROJESİ.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_PROJESİ
{

    public partial class Find : Form
    {
        public string urunturu;
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-THFGP40; initial Catalog = ERP; Integrated Security = True");

        Ana Ana;
        public string arama { get; set; }
        public Find()
        {
            InitializeComponent();
        }

        //aşağıdaki komut find ekranını kapatır
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();

            }
        }

        private void aramatxt_TextChanged(object sender, EventArgs e)
        {
            Ana.arama = aramatxt.Text;
            switch (Ana.selectedPage)
            {
                case "makinalar":
                    Ana.MakinaListesi();
                    break;
                case "imalatçı":
                    Ana.İmalatciListesi();
                    break;
                case "personeller":
                    Ana.personelListele();
                    break;
                case "cariler":
                    Ana.CarileriListele();
                    break;
                case "ürünler":
                    Ana.urunlistele(urunturu);
                    break;
                case "operasyonekle":
                    Ana.OperasyonListesi();
                    break;
                case "rotalar":
                    Ana.rotaListele();
                    break;
                case "üretimemri":
                    Ana.UretimEmriListesi();
                    break;
                case "satissipariş":
                    Ana.satissiparisleriListesi();
                    break;
                case "kategori":
                    Ana.kategoriListele();
                    break;
                case "hakedis":
                    Ana.HakedislerListesi();
                    break;
                case "satinalimfaturası":
                    Ana.SatınAlımFaturalarıListele();
                    break;
                case "satisalımiade":
                    Ana.SatınAlımİadeListele();
                    break;
                case "satisfaturalari":
                    Ana.SatışFaturalarıListele();
                    break;
                case "satisiade":
                    Ana.SatışİadeListele();
                    break;
            }
            }
        public Find(Ana ana)
        {
            this.Ana = ana;
            InitializeComponent();
        }
        private void Find_FormClosing(object sender, FormClosingEventArgs e)
        {
            


        }

        private void Find_Load(object sender, EventArgs e)
        {

        }

        
    }
}
