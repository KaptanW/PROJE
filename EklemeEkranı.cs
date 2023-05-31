using ERP_PROJESİ.Classes.İmalat;
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
using Dapper;
using ERP_PROJESİ.Classes;
using ERP_PROJESİ.Classes.Satış;
using ERP_PROJESİ.Classes.Ürünler;
using System.Collections;
using System.Reflection;
using ERP_PROJESİ.Classes.Muhasebe;

namespace ERP_PROJESİ
{
    public partial class EklemeEkranı : Form
    {
        //Ekleme ekranı ebatı için değişkenler
        int width, height;

        bool oldubitti = false;
        bool verionayı = true;
        public int selectedid { get; set; }
        ComboBox gizlicombo = new ComboBox();
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-PRMBC7J; initial Catalog = ERP; Integrated Security = True");
        //Bu sayfadaki elementleri dinamik olarak çağırdığım için elementler load ekranında oluşuyor fakat oluşan elementler local bir method'ta oluştuğu için bunları globale taşımam gerekiyordu. onun için her element için bir
        //list collection oluşturdum ve bu listelerde tuttum ve çağırdım
        #region listler classlar için
        public List<TextBox> textBoxes = new List<TextBox>();
        public List<ComboBox> ComboBoxes = new List<ComboBox>(); 
        public List<RadioButton> radioButtons = new List<RadioButton>();
        public List<DateTimePicker> DateTimePicks = new List<DateTimePicker>();   
        public List<CheckBox> CheckBoxes = new List<CheckBox>();
        public List<DataGridView> detaytablosu = new List<DataGridView>();
        public List<Button> buttons = new List<Button>();
        #endregion
        public string baslangictarihi { get; set; }

        public string SatinmiSatismi { get; set; }
        public string selectedPage { get; set; }
        //selected page değerine göre üst taraftaki giriş yazısını değiştirdim. bunun için aşağıdaki string değerini kullandım.
        string giriskelimesi;
        public string timer;
        Ana ana = new Ana();
        int detayselectedid;
        public EklemeEkranı(Ana ana)
        {
            InitializeComponent();
            this.ana = ana;

        }

        public EklemeEkranı(string selectedPage)
        {
            InitializeComponent();
            this.selectedPage = selectedPage;
        }
        
        //ana formdaki her liste için farklı bir sayfa oluşturuyor ve oluşan elementleri yukarda belirttiğim listelere atıyorum ayrıca sayfalarda oluşan elementlerin methodlarını da burda atıyorum.
        public void EklemeEkranı_Load(object sender, EventArgs e)
        {
            this.Font = new System.Drawing.Font("Montserrat SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Text = selectedPage;
            BackColor = ColorTranslator.FromHtml("#eeeeee");
            if(oldubitti == false)
            switch (selectedPage)
            {
                case "üretimemri":
                    #region üretim emri ekleme

                    giriskelimesi = "Üretim emri";
                    Label urunID = new Label();
                    urunID.Text = "Ürün";
                    urunID.Location = new Point(50, 50);
                    urunID.Size = new Size(150, 25);
                    Controls.Add(urunID);
                    ComboBox IDtxt = new ComboBox();
                    IDtxt.Location = new Point(250, 50);
                    IDtxt.Size = new Size(250, 25);
                    Controls.Add(IDtxt);
                    ComboBoxes.Add(IDtxt);
                    Label verilenTarih = new Label();
                    verilenTarih.Text = "Planlanan bitiş tarihi";
                    verilenTarih.Location = new Point(50, 100);
                    verilenTarih.Size = new Size(150, 50);
                    Controls.Add(verilenTarih);
                    DateTimePicker date = new DateTimePicker();
                    date.Location = new Point(250, 100);
                    date.Size = new Size(250, 50);
                    Controls.Add(date);
                    DateTimePicks.Add(date);
                    Label siparisID = new Label();
                    siparisID.Text = "Sipariş kodu";
                    siparisID.Location = new Point(50, 150);
                    siparisID.Size = new Size(150, 25);
                    Controls.Add(siparisID);
                    TextBox siparisidtxt = new TextBox();
                    siparisidtxt.Location = new Point(250, 150);
                    siparisidtxt.Size = new Size(250, 50);
                    Controls.Add((siparisidtxt));
                    textBoxes.Add(siparisidtxt);
                    textBoxes.Add((siparisidtxt));
                    Label rotaID = new Label();
                    rotaID.Text = "Rota";
                    rotaID.Location = new Point(50, 200);
                    rotaID.Size = new Size(150, 25);
                    Controls.Add((rotaID));
                    ComboBox rota = new ComboBox();
                    rota.Location = new Point(250, 200);
                    rota.Size = new Size(250, 50);
                    Controls.Add(rota);
                    ComboBoxes.Add(rota);
                    Label hammaddelbl = new Label();
                    hammaddelbl.Text = "Hammadde";
                    hammaddelbl.Location = new Point(50, 250);
                    hammaddelbl.Size = new Size(150, 25);
                    Controls.Add(hammaddelbl);
                    ComboBox hammaddecombo = new ComboBox();
                    hammaddecombo.Location = new Point(250, 250);
                    hammaddecombo.Size = new Size(250, 50);
                    Controls.Add(hammaddecombo);
                    ComboBoxes.Add(hammaddecombo);
                    Label adet =new Label();
                    adet.Text = "Miktarı";
                    adet.Location = new Point(50,300);
                    adet.Size = new Size(150,25);
                    Controls.Add (adet);
                    TextBox adettxt = new TextBox();
                    adettxt.Location = new Point(250, 300);
                    adettxt.Size = new Size(250, 50);
                    Controls.Add(adettxt);
                    textBoxes.Add(adettxt);
                    DataGridView hammadelistesi = new DataGridView();
                    hammadelistesi.Location = new Point(520, 50);
                    hammadelistesi.Size = new Size(350, 280);
                    detaytablosu.Add(hammadelistesi);
                    detaytablosu[0].Click += new EventHandler(hammaddeid);
                    Controls.Add(hammadelistesi);
                    RadioButton basladibutton = new RadioButton();
                    basladibutton.Text = "Başladı";
                    basladibutton.Location = new Point(50, 350);
                    basladibutton.Size = new Size(100, 25);
                    Controls.Add(basladibutton);
                    radioButtons.Add(basladibutton);
                    RadioButton bittibutton = new RadioButton();
                    bittibutton.Text = "Bitti";
                    bittibutton.Location = new Point(150, 350);
                    bittibutton.Size = new Size(100, 25);
                    Controls.Add(bittibutton);
                    radioButtons.Add(bittibutton);
                    Button ekle = new Button();
                    ekle.Location = new Point(520, 350);
                    ekle.Size = new Size(120, 75);
                    buttons.Add(ekle);
                    ekle.Text = "Hammade ekle";
                    Controls.Add(ekle);
                    ekle.Click += new EventHandler(hammaddeekleduzenle);
                    Button sil = new Button();
                    sil.Location = new Point(650, 350);
                    sil.Size = new Size(120, 75);
                    buttons.Add(sil);
                    sil.Text = "Hammade sil";
                    Controls.Add(sil);
                    sil.Click += new EventHandler(emirdetaysil);
                    üretimemriuruncombo();
                    width = 900;
                    height = 500;
                    #endregion
                    break;
                case "makinalar":
                    #region Makinalar

                    giriskelimesi = "Makina";
                    Label makinaadı = new Label();
                    makinaadı.Text = "Makina Adı";
                    makinaadı.Location = new Point(50, 50);
                    makinaadı.Size = new Size(150, 25);
                    Controls.Add(makinaadı);
                    TextBox makinaaditxt = new TextBox();
                    makinaaditxt.Location = new Point(250, 50);
                    makinaaditxt.Size = new Size(250, 50);
                    Controls.Add((makinaaditxt));
                    textBoxes.Add(makinaaditxt);
                    Label makinastok = new Label();
                    makinastok.Text = "Makina Adeti";
                    makinastok.Location = new Point(50, 100);
                    makinastok.Size = new Size(150, 25);
                    Controls.Add(makinastok);
                    TextBox makinastoktxt = new TextBox();
                    makinastoktxt.Location = new Point(250, 100);
                    makinastoktxt.Size = new Size(250, 50);
                    Controls.Add((makinastoktxt));
                    textBoxes.Add((makinastoktxt));
                    Label bakim = new Label();
                    bakim.Text = "Bakım Tarihi";
                    bakim.Location = new Point(50, 150);
                    bakim.Size = new Size(150, 25);
                    Controls.Add(bakim);
                    DateTimePicker bakimdate = new DateTimePicker();
                    bakimdate.Location = new Point(250, 150);
                    bakimdate.Size = new Size(250, 50);
                    Controls.Add(bakimdate);
                    DateTimePicks.Add(bakimdate);
                    Label makinaacıklama = new Label();
                    makinaacıklama.Text = "Makina Açıklaması";
                    makinaacıklama.Location = new Point(50, 200);
                    makinaacıklama.Size = new Size(150, 25);
                    Controls.Add(makinaacıklama);
                    TextBox makinaacıklamatxt = new TextBox();
                    makinaacıklamatxt.Location = new Point(250, 200);
                    makinaacıklamatxt.Size = new Size(250, 80);
                    makinaacıklamatxt.Multiline = true;
                    Controls.Add((makinaacıklamatxt));
                    textBoxes.Add(makinaacıklamatxt);
                    width = 550;
                    height = 450;
                    #endregion
                    break;

                case "rotalar":
                    #region Rota
                    giriskelimesi = "Rota";
                    Label OperasyonID = new Label();
                    OperasyonID.Text = "Operasyon";
                    OperasyonID.Location = new Point(50, 50);
                    OperasyonID.Size = new Size(150, 25);
                    Controls.Add(OperasyonID);
                    ComboBox RotaIDcombo = new ComboBox();
                    RotaIDcombo.Location = new Point(250, 50);
                    RotaIDcombo.Size = new Size(250, 25);
                    Controls.Add(RotaIDcombo);
                    ComboBoxes.Add(RotaIDcombo);
                    Button opekle = new Button();
                    opekle.Location = new Point(520, 250);
                    opekle.Size = new Size(120, 75);
                    opekle.Text = "Operasyon ekle";
                    Controls.Add(opekle);
                    opekle.Click += new EventHandler(rotadetayaekle);
                    DataGridView oplistesi = new DataGridView();
                    oplistesi.Location = new Point(520, 50);
                    oplistesi.Size = new Size(300, 180);
                    Controls.Add((oplistesi));
                    detaytablosu.Add(oplistesi);
                    oplistesi.Click += new EventHandler(opid);
                    Label urunid = new Label();
                    urunid.Text = "Rota Açıklama";
                    urunid.Location = new Point(50, 100);
                    urunid.Size = new Size(150, 25);
                    Controls.Add(urunid);
                    TextBox rotaaçıklama = new TextBox();
                    rotaaçıklama.Location = new Point(250, 100);
                    rotaaçıklama.Size = new Size(250, 25);
                    rotaaçıklama.Multiline = true;
                    Controls.Add(rotaaçıklama);
                    textBoxes.Add(rotaaçıklama);
                    rotadetaytablosu();

                    Button opsilme = new Button();
                    opsilme.Location = new Point(650, 250);
                    opsilme.Size = new Size(120, 75);
                    opsilme.Text = "Operasyon sil";
                    Controls.Add(opsilme);
                    opsilme.Click += new EventHandler(opsil);
                    width = 900;
                    height = 400;
                    #endregion

                    break;
                case "günlükraporekle":
                    #region günlükraporekle
                    giriskelimesi = "Günlük Rapor";
                    Label operasyonIDlbl = new Label();
                    operasyonIDlbl.Text = "Operasyon";
                    operasyonIDlbl.Location = new Point(50, 50);
                    operasyonIDlbl.Size = new Size(150, 25);
                    Controls.Add(operasyonIDlbl);
                    ComboBox operasyonID = new ComboBox();
                    operasyonID.Location = new Point(250, 50);
                    operasyonID.Size = new Size(250, 25);
                    Controls.Add(operasyonID);
                    Label kullanılanmakinaIDlbl = new Label();
                    kullanılanmakinaIDlbl.Text = "Kullanılan Makina";
                    kullanılanmakinaIDlbl.Location = new Point(50, 100);
                    kullanılanmakinaIDlbl.Size = new Size(200, 50);
                    Controls.Add(kullanılanmakinaIDlbl);
                    ComboBox kullanılanmakinaID = new ComboBox();
                    kullanılanmakinaID.Location = new Point(250, 100);
                    kullanılanmakinaID.Size = new Size(250, 25);
                    Controls.Add(kullanılanmakinaID);
                    Label kullanılanmalzemeIDlbl = new Label();
                    kullanılanmalzemeIDlbl.Text = "Kullanılan Malzeme";
                    kullanılanmalzemeIDlbl.Location = new Point(50, 150);
                    kullanılanmalzemeIDlbl.Size = new Size(150, 50);
                    Controls.Add(kullanılanmalzemeIDlbl);
                    ComboBox kullanılanmalzemeID = new ComboBox();
                    kullanılanmalzemeID.Location = new Point(250, 150);
                    kullanılanmalzemeID.Size = new Size(250, 25);
                    Controls.Add(kullanılanmalzemeID);
                    Label operasyondatelbl = new Label();
                    operasyondatelbl.Text = "Operasyon Tarihi";
                    operasyondatelbl.Location = new Point(50, 200);
                    operasyondatelbl.Size = new Size(150, 50);
                    Controls.Add(operasyondatelbl);
                    DateTimePicker operasyondate = new DateTimePicker();
                    operasyondate.Location = new Point(250, 200);
                    operasyondate.Size = new Size(250, 25);
                    Controls.Add(operasyondate);
                    Label imalatcıIDlbl = new Label();
                    imalatcıIDlbl.Text = "İmalatcı";
                    imalatcıIDlbl.Location = new Point(50, 250);
                    imalatcıIDlbl.Size = new Size(150, 50);
                    Controls.Add(imalatcıIDlbl);
                    ComboBox imalatcıID = new ComboBox();
                    imalatcıID.Location = new Point(250, 250);
                    imalatcıID.Size = new Size(250, 25);
                    Controls.Add(imalatcıID);


                    break;
                #endregion
                case "operasyonekle":
                    #region operasyonekle
                    giriskelimesi = "Operasyon";
                    Label operasyonadilbl = new Label();
                    operasyonadilbl.Text = "Operasyon Adi";
                    operasyonadilbl.Location = new Point(50, 60);
                    operasyonadilbl.Size = new Size(200, 50);
                    Controls.Add(operasyonadilbl);
                    TextBox operasyonadi = new TextBox();
                    operasyonadi.Location = new Point(250, 60);
                    operasyonadi.Size = new Size(250, 25);
                    Controls.Add(operasyonadi);
                    textBoxes.Add( operasyonadi );
                    width = 550;
                    height = 275;
                    break;
                #endregion
                case "personeller":
                    #region personel
                    giriskelimesi = "Personel";
                    Label calısanAdiLBL = new Label();
                    calısanAdiLBL.Text = "Adı";
                    calısanAdiLBL.Location = new Point(50, 50);
                    calısanAdiLBL.Size = new Size(150, 25);
                    Controls.Add(calısanAdiLBL);
                    TextBox calısanadiTXT = new TextBox();
                    calısanadiTXT.Location = new Point(250, 50);
                    calısanadiTXT.Size = new Size(250, 25);
                    Controls.Add(calısanadiTXT);
                    textBoxes.Add(calısanadiTXT);
                    Label calısansoyadiLBL = new Label();
                    calısansoyadiLBL.Text = "Soyadı";
                    calısansoyadiLBL.Location = new Point(50, 100);
                    calısansoyadiLBL.Size = new Size(200, 50);
                    Controls.Add(calısansoyadiLBL);
                    TextBox calısansoyadiTXT = new TextBox();
                    calısansoyadiTXT.Location = new Point(250, 100);
                    calısansoyadiTXT.Size = new Size(250, 25);
                    Controls.Add(calısansoyadiTXT);
                    textBoxes.Add(calısansoyadiTXT);
                    Label isegirisDateLBL = new Label();
                    isegirisDateLBL.Text = "İşe Giriş Tarihi";
                    isegirisDateLBL.Location = new Point(50, 150);
                    isegirisDateLBL.Size = new Size(200, 50);
                    Controls.Add(isegirisDateLBL);
                    DateTimePicker isegirisDate = new DateTimePicker();
                    isegirisDate.Location = new Point(250, 150);
                    isegirisDate.Size = new Size(250, 50);
                    DateTimePicks.Add(isegirisDate);
                    Controls.Add(isegirisDate);
                    Label telefonLBL = new Label();
                    telefonLBL.Text = "Telefon";
                    telefonLBL.Location = new Point(50, 200);
                    telefonLBL.Size = new Size(200, 50);
                    Controls.Add(telefonLBL);
                    TextBox telefonTXT = new TextBox();
                    telefonTXT.Location = new Point(250, 200);
                    telefonTXT.Size = new Size(250, 25);
                    Controls.Add(telefonTXT);
                    textBoxes.Add(telefonTXT);
                    Label ünvanLBL = new Label();
                    ünvanLBL.Text = "Ünvanı";
                    ünvanLBL.Location = new Point(50, 250);
                    ünvanLBL.Size = new Size(200, 50);
                    Controls.Add(ünvanLBL);
                    ComboBox ünvanID = new ComboBox();
                    ünvanID.Location = new Point(250, 250);
                    ünvanID.Size = new Size(250, 25);
                    Controls.Add(ünvanID);
                    ComboBoxes.Add(ünvanID);
                    personelkombobox();

                    width = 550;
                    height = 450;
                    break;
                #endregion
                case "hakedis":
                    #region hakedis

                    giriskelimesi = "Hakedis";
                    Label hakedistarih = new Label();
                    hakedistarih.Text = "Tarih";
                    hakedistarih.Location = new Point(50, 50);
                    hakedistarih.Size = new Size(150, 25);
                    Controls.Add(hakedistarih);
                    ComboBox hakedistarihcombo = new ComboBox();
                    hakedistarihcombo.Location = new Point(250, 50);
                    hakedistarihcombo.Size = new Size(250, 25);
                    Controls.Add(hakedistarihcombo);
                    Button ayekle = new Button();
                    ayekle.Location = new Point(510, 50);
                    ayekle.Size = new Size(200, 25);
                    ayekle.Text = "Yeni ay ekle";
                    Controls.Add(ayekle);
                    Label hakedisad = new Label();
                    hakedisad.Text = "Çalışan";
                    hakedisad.Location = new Point(50, 100);
                    hakedisad.Size = new Size(150, 25);
                    Controls.Add(hakedisad);
                    ComboBox calısanhakediscombo = new ComboBox();
                    calısanhakediscombo.Location = new Point(250, 100);
                    calısanhakediscombo.Size = new Size(250, 25);
                    Controls.Add(calısanhakediscombo);
                    Label maas = new Label();
                    maas.Text = "Maaşı";
                    maas.Location = new Point(50, 150);
                    maas.Size = new Size(150, 25);
                    Controls.Add(maas);
                    TextBox maastxt = new TextBox();
                    maastxt.Location = new Point(250, 150);
                    maastxt.Size = new Size(250, 25);
                    Controls.Add(maastxt);
                    Label calisilansaat = new Label();
                    calisilansaat.Text = "Çalışılan Saat";
                    calisilansaat.Location = new Point(50, 200);
                    calisilansaat.Size = new Size(150, 25);
                    Controls.Add(calisilansaat);
                    TextBox calisilansaattxt = new TextBox();
                    calisilansaattxt.Location = new Point(250, 200);
                    calisilansaattxt.Size = new Size(250, 25);
                    Controls.Add(calisilansaattxt);
                    Label prim = new Label();
                    prim.Text = "Prim";
                    prim.Location = new Point(50, 250);
                    prim.Size = new Size(150, 25);
                    Controls.Add(prim);
                    TextBox primtxt = new TextBox();
                    primtxt.Location = new Point(250, 250);
                    primtxt.Size = new Size(250, 25);
                    Controls.Add(primtxt);
                    Label hakedisnot = new Label();
                    hakedisnot.Text = "Not";
                    hakedisnot.Location = new Point(50, 300);
                    hakedisnot.Size = new Size(150, 25);
                    Controls.Add(hakedisnot);
                    TextBox hakedisnottxt = new TextBox();
                    hakedisnottxt.Location = new Point(250, 300);
                    hakedisnottxt.Size = new Size(250, 50);
                    hakedisnottxt.Multiline = true;
                    Controls.Add(hakedisnottxt);
                    #endregion
                    break;
                case "cariler":
                    #region cariler
                    giriskelimesi = "Cariler";
                    Label cariadiLBL = new Label();
                    cariadiLBL.Text = "Adı";
                    cariadiLBL.Location = new Point(50, 50);
                    cariadiLBL.Size = new Size(150, 25);
                    Controls.Add(cariadiLBL);
                    TextBox cariadiTXT = new TextBox();
                    cariadiTXT.Location = new Point(250, 50);
                    cariadiTXT.Size = new Size(250, 25);
                    textBoxes.Add(cariadiTXT);
                    Controls.Add(cariadiTXT);
                    Label caritelefonTXT = new Label();
                    caritelefonTXT.Text = "Telefon";
                    caritelefonTXT.Location = new Point(50, 75);
                    caritelefonTXT.Size = new Size(150, 25);
                    Controls.Add(caritelefonTXT);
                    TextBox caritelefonLBL = new TextBox();
                    caritelefonLBL.Location = new Point(250, 75);
                    caritelefonLBL.Size = new Size(250, 25);
                    textBoxes.Add(caritelefonLBL);
                    Controls.Add(caritelefonLBL);
                    Label cariadresLBL = new Label();
                    cariadresLBL.Text = "Adres";
                    cariadresLBL.Location = new Point(50, 100);
                    cariadresLBL.Size = new Size(150, 25);
                    Controls.Add(cariadresLBL);
                    TextBox cariadresTXT = new TextBox();
                    cariadresTXT.Location = new Point(250, 100);
                    cariadresTXT.Size = new Size(250, 100);
                    cariadresTXT.Multiline = true;
                    textBoxes.Add(cariadresTXT);
                    Controls.Add(cariadresTXT);
                    Label carimailLBL = new Label();
                    carimailLBL.Text = "Mail";
                    carimailLBL.Location = new Point(50, 210);
                    carimailLBL.Size = new Size(150, 25);
                    Controls.Add(carimailLBL);
                    TextBox carimailTXT = new TextBox();
                    carimailTXT.Location = new Point(250, 210);
                    carimailTXT.Size = new Size(250, 25);
                    textBoxes.Add(carimailTXT);
                    Controls.Add(carimailTXT);
                    Label hesapnumarasıLBL = new Label();
                    hesapnumarasıLBL.Text = "Hesap Numarası";
                    hesapnumarasıLBL.Location = new Point(50, 235);
                    hesapnumarasıLBL.Size = new Size(150, 25);
                    Controls.Add(hesapnumarasıLBL);
                    TextBox hesapnumarasıTXT = new TextBox();
                    hesapnumarasıTXT.Location = new Point(250, 235);
                    hesapnumarasıTXT.Size = new Size(250, 25);
                    textBoxes.Add(hesapnumarasıTXT);
                    Controls.Add(hesapnumarasıTXT);
                    Label cariülkeLBL = new Label();
                    cariülkeLBL.Text = "Ülke";
                    cariülkeLBL.Location = new Point(50, 260);
                    cariülkeLBL.Size = new Size(150, 25);
                    Controls.Add(cariülkeLBL);
                    TextBox cariülkeTXT = new TextBox();
                    cariülkeTXT.Location = new Point(250, 260);
                    cariülkeTXT.Size = new Size(250, 25);
                    Controls.Add(cariülkeTXT);
                    textBoxes.Add(cariülkeTXT);
                    Label carisehirLBL = new Label();
                    carisehirLBL.Text = "Sehir";
                    carisehirLBL.Location = new Point(50, 285);
                    carisehirLBL.Size = new Size(150, 25);
                    Controls.Add(carisehirLBL);
                    TextBox carisehirTXT = new TextBox();
                    carisehirTXT.Location = new Point(250, 285);
                    carisehirTXT.Size = new Size(250, 25);
                    Controls.Add(carisehirTXT);
                    textBoxes.Add(carisehirTXT);
                    Label caripostakoduLBL = new Label();
                    caripostakoduLBL.Text = "Posta Kodu";
                    caripostakoduLBL.Location = new Point(50, 310);
                    caripostakoduLBL.Size = new Size(150, 25);
                    Controls.Add(caripostakoduLBL);
                    TextBox caripostakoduTXT = new TextBox();
                    caripostakoduTXT.Location = new Point(250, 310);
                    caripostakoduTXT.Size = new Size(250, 25);
                    Controls.Add(caripostakoduTXT);
                    textBoxes.Add(caripostakoduTXT);
                    Label caritürüLBL = new Label();
                    caritürüLBL.Text = "Tür Seçiniz";
                    caritürüLBL.Location = new Point(550, 50);
                    caritürüLBL.Size = new Size(150, 25);
                    Controls.Add(caritürüLBL);
                    RadioButton caritürüRD3 = new RadioButton();
                    caritürüRD3.Text = "Tedarikçi";
                    caritürüRD3.Location = new Point(550, 75);
                    caritürüRD3.Size = new Size(200, 25);
                    radioButtons.Add(caritürüRD3 );
                    Controls.Add(caritürüRD3);
                    RadioButton caritürüRB = new RadioButton();
                    caritürüRB.Text = "Müşteri";
                    caritürüRB.Location = new Point(550, 100);
                    caritürüRB.Size = new Size(200, 25);
                    radioButtons.Add(caritürüRB);
                    Controls.Add(caritürüRB);
                    RadioButton caritürüRB1 = new RadioButton();
                    caritürüRB1.Text = "Her İkisi";
                    caritürüRB1.Location = new Point(550, 125);
                    caritürüRB1.Size = new Size(200, 25);
                    Controls.Add(caritürüRB1);
                    radioButtons.Add(caritürüRB1);

                    width = 550;
                    height = 505;
                    break;
                #endregion
                    //ürünler ekleme düzenleme tamam - combobox çift olarak veriyor
                case "ürünler":
                    #region ürünler
                    giriskelimesi = "ürünler";
                    Label ürünadıLBL = new Label();
                    ürünadıLBL.Text = "Adı";
                    ürünadıLBL.Location = new Point(50, 50);
                    ürünadıLBL.Size = new Size(150, 25);
                    Controls.Add(ürünadıLBL);
                    TextBox ürünadıTXT = new TextBox();
                    ürünadıTXT.Location = new Point(250, 50);
                    ürünadıTXT.Size = new Size(250, 25);
                    textBoxes.Add(ürünadıTXT); //1
                    Controls.Add(ürünadıTXT);
                    Label ürünacıklamaLBL = new Label();
                    ürünacıklamaLBL.Text = "Acıklama";
                    ürünacıklamaLBL.Location = new Point(50, 75);
                    ürünacıklamaLBL.Size = new Size(150, 25);
                    Controls.Add(ürünacıklamaLBL);
                    TextBox ürünacıklamaTXT = new TextBox();
                    ürünacıklamaTXT.Location = new Point(250, 75);
                    ürünacıklamaTXT.Size = new Size(250, 100);
                    ürünacıklamaTXT.Multiline = true;
                    textBoxes.Add(ürünacıklamaTXT); //2
                    Controls.Add(ürünacıklamaTXT);
                    Label ürünfiyatLBL = new Label();
                    ürünfiyatLBL.Text = "Raf Kodu";
                    ürünfiyatLBL.Location = new Point(50, 185);
                    ürünfiyatLBL.Size = new Size(150, 25);
                    Controls.Add(ürünfiyatLBL);
                    TextBox ürünfiyatTXT = new TextBox();
                    ürünfiyatTXT.Location = new Point(250, 185);
                    ürünfiyatTXT.Size = new Size(250, 25);
                    textBoxes.Add(ürünfiyatTXT); //3
                    Controls.Add(ürünfiyatTXT);
                    Label ürünkategorisiLBL = new Label();
                    ürünkategorisiLBL.Text = "Kategori";
                    ürünkategorisiLBL.Location = new Point(50, 210);
                    ürünkategorisiLBL.Size = new Size(150, 25);
                    Controls.Add(ürünkategorisiLBL);
                    ComboBox ürünkategorisiTXT = new ComboBox();
                    ürünkategorisiTXT.Location = new Point(250, 210);
                    ürünkategorisiTXT.Size = new Size(250, 25);
                    ComboBoxes.Add(ürünkategorisiTXT);
                    Controls.Add(ürünkategorisiTXT);
                    Label ürünmiktariLBL = new Label();
                    ürünmiktariLBL.Text = "Miktarı";
                    ürünmiktariLBL.Location = new Point(50, 235);
                    ürünmiktariLBL.Size = new Size(150, 25);
                    Controls.Add(ürünmiktariLBL);
                    TextBox ürünmiktar = new TextBox();
                    ürünmiktar.Location = new Point(250, 235);
                    ürünmiktar.Size = new Size(250, 25);
                    textBoxes.Add(ürünmiktar); //4
                    Controls.Add(ürünmiktar);
                    RadioButton ürüntürüRD = new RadioButton();
                    ürüntürüRD.Text = "Ticari Ürünler";
                    ürüntürüRD.Location = new Point(250, 295);
                    ürüntürüRD.Size = new Size(120, 25);
                    radioButtons.Add(ürüntürüRD);
                    Controls.Add(ürüntürüRD);
                    RadioButton ürüntürüRD1 = new RadioButton();
                    ürüntürüRD1.Text = "Mamüller";
                    ürüntürüRD1.Location = new Point(250, 320);
                    ürüntürüRD1.Size = new Size(120, 25);
                    radioButtons.Add(ürüntürüRD1);
                    Controls.Add(ürüntürüRD1);
                    RadioButton ürüntürüRD2 = new RadioButton();
                    ürüntürüRD2.Text = "Yari Mamüller";
                    ürüntürüRD2.Location = new Point(380, 295);
                    ürüntürüRD2.Size = new Size(120, 25);
                    radioButtons.Add(ürüntürüRD2);
                    Controls.Add(ürüntürüRD2);
                    RadioButton ürüntürüRD3 = new RadioButton();
                    ürüntürüRD3.Text = "Hammaddeler";
                    ürüntürüRD3.Location = new Point(380, 320);
                    ürüntürüRD3.Size = new Size(120, 25);
                    radioButtons.Add(ürüntürüRD3);
                    Controls.Add(ürüntürüRD3);
                    urunlercombobox();
                    ürünkategorisiTXT.Text = "";

                    width = 550;
                    height = 510;
                    break;
                #endregion 
                    //kategori ekleme tamam değiştirirken hata veriyor
                case "kategori":
                    #region kategoriler
                    giriskelimesi = "Kategori";
                    Label kategoriadıLBL = new Label();
                    kategoriadıLBL.Text = "Adı";
                    kategoriadıLBL.Location = new Point(50, 50);
                    kategoriadıLBL.Size = new Size(150, 25);
                    Controls.Add(kategoriadıLBL);
                    TextBox kategoriadıTXT = new TextBox();
                    kategoriadıTXT.Location = new Point(250, 50);
                    kategoriadıTXT.Size = new Size(250, 25);
                    textBoxes.Add(kategoriadıTXT); //1
                    Controls.Add(kategoriadıTXT);
                    Label kategoriacıklamaLBL = new Label();
                    kategoriacıklamaLBL.Text = "Acıklama";
                    kategoriacıklamaLBL.Location = new Point(50, 75);
                    kategoriacıklamaLBL.Size = new Size(150, 25);
                    Controls.Add(kategoriacıklamaLBL);
                    TextBox kategoriacıklamaTXT = new TextBox();
                    kategoriacıklamaTXT.Location = new Point(250, 75);
                    kategoriacıklamaTXT.Size = new Size(250, 100);
                    kategoriacıklamaTXT.Multiline = true;
                    textBoxes.Add(kategoriacıklamaTXT); //2
                    Controls.Add(kategoriacıklamaTXT);

                    width = 550;
                    height = 350;
                    #endregion
                    break;
                case "siparişler":
                    #region Satın Alma Siparişleri
                    giriskelimesi = "Satış Siparişleri";
                    Label cariadiLBL1 = new Label();
                    cariadiLBL1.Text = "Cari Adı";
                    cariadiLBL1.Location = new Point(50, 50);
                    cariadiLBL1.Size = new Size(150, 25);
                    Controls.Add(cariadiLBL1);
                    TextBox cariaditxt = new TextBox();
                    cariaditxt.Location = new Point(250, 50);
                    cariaditxt.Size = new Size(200, 25);
                    Controls.Add(cariaditxt);
                    Label siparisTH = new Label();
                    siparisTH.Text = "Siparis Tarihi";
                    siparisTH.Location = new Point(50, 75);
                    siparisTH.Size = new Size(150, 25);
                    Controls.Add(siparisTH);
                    DateTimePicker urunadtxt = new DateTimePicker();
                    urunadtxt.Location = new Point(250, 75);
                    urunadtxt.Size = new Size(200, 25);
                    Controls.Add(urunadtxt);
                    Label teslimtarihiTH = new Label();
                    teslimtarihiTH.Text = "Teslim Tarihi";
                    teslimtarihiTH.Location = new Point(50, 100);
                    teslimtarihiTH.Size = new Size(150, 25);
                    Controls.Add(teslimtarihiTH);
                    DateTimePicker birimfiyattxt = new DateTimePicker();
                    birimfiyattxt.Location = new Point(250, 100);
                    birimfiyattxt.Size = new Size(200, 25);
                    Controls.Add(birimfiyattxt);
                    Label kargofirmaLBL = new Label();
                    kargofirmaLBL.Text = "Gönderilen Kargo Firması";
                    kargofirmaLBL.Location = new Point(50, 125);
                    kargofirmaLBL.Size = new Size(150, 25);
                    Controls.Add(kargofirmaLBL);
                    TextBox kargofirmatxt = new TextBox();
                    kargofirmatxt.Location = new Point(250, 125);
                    kargofirmatxt.Size = new Size(200, 25);
                    Controls.Add(kargofirmatxt);
                    DataGridView Satinalmadetaydatagrid = new DataGridView();
                    Satinalmadetaydatagrid.Location = new Point(515, 50);
                    Satinalmadetaydatagrid.Size = new Size(350, 300);
                    Controls.Add(Satinalmadetaydatagrid);
                    Button ekleBTN2 = new Button();
                    ekleBTN2.Text = "EKLE";
                    ekleBTN2.Location = new Point(575, 375);
                    ekleBTN2.Size = new Size(75, 50);
                    Controls.Add(ekleBTN2);
                    Button ekleBTN3 = new Button();
                    ekleBTN3.Text = "ÇIKAR";
                    ekleBTN3.Location = new Point(750, 375);
                    ekleBTN3.Size = new Size(75, 50);
                    Controls.Add(ekleBTN3);

                    width = 900;
                    height = 525;

                    #endregion
                    break;
                case "satınalmairsaliyeleri":
                    #region satın alma irsaliyesi

                    giriskelimesi = "İrsaliye";
                    Label cariadilbl = new Label();
                    cariadilbl.Text = "Cari Adı";
                    cariadilbl.Location = new Point(50, 50);
                    cariadilbl.Size = new Size(150, 25);
                    Controls.Add(cariadilbl);
                    ComboBox cariadicombo = new ComboBox();
                    cariadicombo.Location = new Point(250, 50);
                    cariadicombo.Size = new Size(200, 25);
                    Controls.Add(cariadicombo);
                    ComboBoxes.Add(cariadicombo);
                    Label siparisIDlbl = new Label();
                    siparisIDlbl.Text = "SiparişID";
                    siparisIDlbl.Location = new Point(50, 100);
                    siparisIDlbl.Size = new Size(150, 25);
                    Controls.Add(siparisIDlbl);
                    ComboBox siparisIDcombo = new ComboBox();
                    siparisIDcombo.Location = new Point(250, 100);
                    siparisIDcombo.Size = new Size(200, 25);
                    Controls.Add(siparisIDcombo);
                    ComboBoxes.Add(siparisIDcombo);
                    Label urunıdlbl = new Label();
                    urunıdlbl.Text = "UrunID";
                    urunıdlbl.Location = new Point(50, 150);
                    urunıdlbl.Size = new Size(150, 25);
                    Controls.Add(urunıdlbl);
                    ComboBox urunıdtxt = new ComboBox();
                    urunıdtxt.Location = new Point(250, 150);
                    urunıdtxt.Size = new Size(200, 25);
                    Controls.Add(urunıdtxt);
                    ComboBoxes.Add(urunıdtxt);
                    Label urunmiktarlbl = new Label();
                    urunmiktarlbl.Text = "Miktar";
                    urunmiktarlbl.Location = new Point(50, 200);
                    urunmiktarlbl.Size = new Size(150, 25);
                    Controls.Add(urunmiktarlbl);
                    TextBox urunmiktartxt = new TextBox();
                    urunmiktartxt.Location = new Point(250, 200);
                    urunmiktartxt.Size = new Size(200, 25);
                    Controls.Add(urunmiktartxt);
                    textBoxes.Add(urunmiktartxt);
                    Label kargofirmasilbl = new Label();
                    kargofirmasilbl.Text = "Kargo Firması";
                    kargofirmasilbl.Location = new Point(50, 250);
                    kargofirmasilbl.Size = new Size(150, 25);
                    Controls.Add(kargofirmasilbl);
                    TextBox kargofirmasitxt = new TextBox();
                    kargofirmasitxt.Location = new Point(250, 250);
                    kargofirmasitxt.Size = new Size(200, 25);
                    Controls.Add(kargofirmasitxt);
                    textBoxes.Add(kargofirmasitxt);
                    Label iadelbl = new Label();
                    iadelbl.Text = "İade";
                    iadelbl.Location = new Point(50, 300);
                    iadelbl.Size = new Size(50, 25);
                    Controls.Add(iadelbl);
                    CheckBox iadeck = new CheckBox();
                    iadeck.Location = new Point(250, 300);
                    iadeck.Size = new Size(50, 25);
                    Controls.Add(iadeck);
                    CheckBoxes.Add(iadeck);
                    DataGridView irsaliyedetaysdatagrid = new DataGridView();
                    irsaliyedetaysdatagrid.Location = new Point(470, 50);
                    irsaliyedetaysdatagrid.Size = new Size(400, 300);
                    Controls.Add(irsaliyedetaysdatagrid);
                    detaytablosu.Add(irsaliyedetaysdatagrid);
                    Button uruneklebtn = new Button();
                    uruneklebtn.Text = "Ürün Ekle";
                    uruneklebtn.Location = new Point(570, 360);
                    uruneklebtn.Size = new Size(100, 25);
                    Controls.Add(uruneklebtn);
                    buttons.Add(uruneklebtn);
                    buttons[0].Click += new EventHandler(irsaliyekle);
                    Button uruncikarbtn = new Button();
                    uruncikarbtn.Text = "Ürün Çıkar";
                    uruncikarbtn.Location = new Point(680, 360);
                    uruncikarbtn.Size = new Size(100, 25);
                    Controls.Add(uruncikarbtn);
                    buttons.Add(uruncikarbtn);
                    buttons[1].Click += new EventHandler(irsaliyedetaysil);
                    irsaliyedetayılistele();
                    detaytablosu[0].Click += new EventHandler(irsaliyeid);

                    width = 900;
                    height = 510;
                    break;
                #endregion
                case "fatura":
                    #region Satın Alım Faturası
                    giriskelimesi = "Fatura";
                    Label cariidLBL = new Label();
                    cariidLBL.Text = "Cari";
                    cariidLBL.Location = new Point(50, 75);
                    cariidLBL.Size = new Size(50, 25);
                    Controls.Add(cariidLBL);
                    TextBox cariidTXT = new TextBox();
                    cariidTXT.Location = new Point(250, 75);
                    cariidTXT.Size = new Size(250, 25);
                    Controls.Add(cariidTXT);
                    textBoxes.Add(cariidTXT);
                    Label tarihLBL = new Label();
                    tarihLBL.Text = "Tarih";
                    tarihLBL.Location = new Point(50, 100);
                    tarihLBL.Size = new Size(50, 25);
                    Controls.Add(tarihLBL);
                    DateTimePicker tarihDT = new DateTimePicker();
                    tarihDT.Location = new Point(250, 100);
                    tarihDT.Size = new Size(250, 25);
                    Controls.Add(tarihDT);
                    DateTimePicks.Add(tarihDT);
                    Label tutarLBL = new Label();
                    tutarLBL.Text = "Tutar";
                    tutarLBL.Location = new Point(50, 125);
                    tutarLBL.Size = new Size(120, 25);
                    Controls.Add(tutarLBL);
                    TextBox tutarTXT = new TextBox();
                    tutarTXT.Location = new Point(250, 125);
                    tutarTXT.Size = new Size(250, 25);
                    Controls.Add(tutarTXT);
                    textBoxes.Add(tutarTXT);
                    Label odemeLBL = new Label();
                    odemeLBL.Text = "Ödeme Bilgisi";
                    odemeLBL.Location = new Point(50, 150);
                    odemeLBL.Size = new Size(120, 25);
                    Controls.Add(odemeLBL);
                    TextBox odemeTXT = new TextBox();
                    odemeTXT.Location = new Point(250, 150);
                    odemeTXT.Size = new Size(250, 25);
                    Controls.Add(odemeTXT);
                    textBoxes.Add(odemeTXT);
                    Label ürünıdLBL = new Label();
                    ürünıdLBL.Text = "Ürün ID'si";
                    ürünıdLBL.Location = new Point(50, 175);
                    ürünıdLBL.Size = new Size(175, 25);
                    Controls.Add(ürünıdLBL);
                    ComboBox ürünıdTXT = new ComboBox();
                    ürünıdTXT.Location = new Point(250, 175);
                    ürünıdTXT.Size = new Size(250, 25);
                    Controls.Add(ürünıdTXT);
                    ComboBoxes.Add(ürünıdTXT );
                    Label ürünadetLBL = new Label();
                    ürünadetLBL.Text = "Ürün Adet";
                    ürünadetLBL.Location = new Point(50, 200);
                    ürünadetLBL.Size = new Size(175, 25);
                    Controls.Add(ürünadetLBL);
                    TextBox ürünadetTXT = new TextBox();
                    ürünadetTXT.Location = new Point(250, 200);
                    ürünadetTXT.Size = new Size(250, 25);
                    Controls.Add(ürünadetTXT);
                    textBoxes.Add(ürünadetTXT);
                    Label birimfiyatLBL = new Label();
                    birimfiyatLBL.Text = "Birim Fiyat";
                    birimfiyatLBL.Location = new Point(50, 225);
                    birimfiyatLBL.Size = new Size(175, 25);
                    Controls.Add(birimfiyatLBL);
                    TextBox birimfiyatTXT = new TextBox();
                    birimfiyatTXT.Location = new Point(250, 225);
                    birimfiyatTXT.Size = new Size(250, 25);
                    Controls.Add(birimfiyatTXT);
                    textBoxes.Add(birimfiyatTXT);
                    DataGridView dataDGW = new DataGridView();
                    dataDGW.Location = new Point(515, 50);
                    dataDGW.Size = new Size(350, 300);
                    detaytablosu.Add(dataDGW);
                    Controls.Add(dataDGW);
                    detaytablosu.Add(dataDGW);
                    detaytablosu[0].Click += new EventHandler(faturadetayid);
                    Button ekleBTN = new Button();
                    ekleBTN.Text = "EKLE";
                    ekleBTN.Location = new Point(575, 375);
                    ekleBTN.Size = new Size(75, 50);
                    Controls.Add(ekleBTN);
                    buttons.Add(ekleBTN);
                    buttons[0].Click += faturadetayekleme;
                    Button ekleBTN1 = new Button();
                    ekleBTN1.Text = "ÇIKAR";
                    ekleBTN1.Location = new Point(750, 375);
                    ekleBTN1.Size = new Size(75, 50);
                    Controls.Add(ekleBTN1);
                    buttons.Add(ekleBTN1);
                    buttons[1].Click += new EventHandler(faturadetaysil);
                    CheckBox iadeCH = new CheckBox();
                    iadeCH.Location = new Point(250, 250);
                    iadeCH.Size = new Size(100, 50);
                    iadeCH.Text = "İade";
                    Controls.Add(iadeCH);
                    CheckBoxes.Add(iadeCH);
                    faturadetaylistele();
                    width = 900;
                    height = 525;
                    break;
                    #endregion
                    case "kayıtekranı":
                        #region kayıtekranı
                        giriskelimesi = "Kullanıcı";
                        Label kullanıcılbl = new Label();
                        kullanıcılbl.Text = "Kullanıcı Adı";
                        kullanıcılbl.Location = new Point(50, 50);
                        kullanıcılbl.Size = new Size(150, 25);
                        Controls.Add(kullanıcılbl);
                        TextBox kullanıcıTXT = new TextBox();
                        kullanıcıTXT.Location = new Point(250, 50);
                        kullanıcıTXT.Size = new Size(250, 25);
                        textBoxes.Add(kullanıcıTXT); //1
                        Controls.Add(kullanıcıTXT);
                        Label ParolaLBL = new Label();
                        ParolaLBL.Text = "Parola";
                        ParolaLBL.Location = new Point(50, 75);
                        ParolaLBL.Size = new Size(150, 25);
                        Controls.Add(ParolaLBL);
                        TextBox ParolaTXT = new TextBox();
                        ParolaTXT.Location = new Point(250, 75);
                        ParolaTXT.Size = new Size(250, 25);
                        textBoxes.Add(ParolaTXT); //2
                        Controls.Add(ParolaTXT);
                        width = 550;
                        height = 300;
                        break;
                    #endregion
                    default:
                    break;
            }
            try
            {
                //detay tablosu gerektiren bazı sayfalar var ve yeni oluşan datagridviewe gerekli olan bir method fakat normal sayfaları açtığımda hata veriyordu o yüzden try catch fonksiyonu içine aldım
                detaytablosu[0].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception)
            {

            }
            this.Size = new Size(width, height);
            Label giriş = new Label();
            giriş.Text = " " + (giriskelimesi) + " bilgilerini giriniz";
            giriş.Location = new Point(250, 25);
            giriş.Size = new Size(250, 100);
            giriş.Size = new Size(500, 100);
            Controls.Add(giriş);
            Button ad = new Button();
            ad.Location = new Point(250, (height-150));
            ad.Size = new Size(120, 75);
            ad.Text = "Ekle";
            Controls.Add(ad);
            ad.Click += new EventHandler(ekle);
            oldubitti = true;
        }
        //aşağıda yazdığımekleme ve güncelleme methodlarını çağırdığım ekleme buttonu
        void ekle(object sender, EventArgs e)
        {
            switch (selectedPage)
            {
                case "ürünler":
                    Urunekleduzenle();
                    break;
                case "kategori":
                    kategoriguncelleme();
                    break;
                case "personeller":
                    personelekleduzenle();
                    break;
                case "cariler":
                cariekleduzenle();
                    break;
                case "operasyonekle":
                    operasyoneklegüncelle();
                    break;
                case "üretimemri":
                    uretimemriduzenle();
                break;
                case "makinalar":
                    makinagüncelleekle();
                    break;
                case "rotalar":
                    rotagüncelle();
                    break;
                case "satınalmairsaliyeleri":
                    irsaliyegenelekleme();
                    break;
                case "fatura":
                    faturaekleme();
                    break;
                default:
                    break;
            }
            if(verionayı == true)
            MessageBox.Show("Veri Girildi");
            verionayı = true;
            ana.refresh_Click(this,null);
        }

        //ekran kapanırken sayfada yenileme yapar.
        public void EklemeEkranı_FormClosing(object sender, FormClosingEventArgs e)
        {
            ana.refresh_Click(this,null);
        }

        //güncelleme ve ekleme için oluşturduğum methodlar
        #region EklemeDüzenleme Methodları
        #region imalat
        #region üretim emirleri sayfası
        #region üretim emirleri ürünler combobox
        public void üretimemriuruncombo()
        {

            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            List<ürünler> list = SqlCon.Query<ürünler>("select * from Urun_Tablosu where sil = 'True' and urunturu = 'Ticari'", SqlCon).ToList<ürünler>();
            ComboBoxes[0].DataSource = list;
            ComboBoxes[0].DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxes[0].DisplayMember = "urunadi";
            ComboBoxes[0].ValueMember = "urunID";
            ComboBoxes[0].SelectionStart = ComboBoxes[0].Text.Length;


            List<ürünler> list1 = SqlCon.Query<ürünler>("select * from Urun_Tablosu where sil = 'True' and urunturu = 'Hammadde'", SqlCon).ToList<ürünler>();
            ComboBoxes[2].DataSource = list1;
            ComboBoxes[2].DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxes[2].DisplayMember = "urunadi";
            ComboBoxes[2].ValueMember = "urunID";
            ComboBoxes[2].SelectionStart = ComboBoxes[2].Text.Length;

            List<Rota> list2 = SqlCon.Query<Rota>("select * from Rota where sil = 'True'", SqlCon).ToList<Rota>();
            ComboBoxes[1].DataSource = list2;
            ComboBoxes[1].DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxes[1].DisplayMember = "rotaID";
            ComboBoxes[1].ValueMember = "rotaID";
            ComboBoxes[1].SelectionStart = ComboBoxes[2].Text.Length;

            List<UretimEmriDetay> list3 = SqlCon.Query<UretimEmriDetay>("select * from Uretim_Emri_Hammadde_Detay urd inner join Urun_Tablosu u on u.urunID = urd.urunID  where urd.sil = 'True' and uretimemriID = "+selectedid+"", SqlCon).ToList<UretimEmriDetay>();
            detaytablosu[0].DataSource = list3;
            detaytablosu[0].Columns[0].Visible = false;
            detaytablosu[0].Columns[1].Visible = false;
            detaytablosu[0].Columns[4].Visible = false;
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #region hammadde detay cell click
        public void hammaddeid(object sender, EventArgs e)
        {
            detayselectedid = int.Parse(detaytablosu[0].CurrentRow.Cells[0].Value.ToString());
            ComboBoxes[2].SelectedValue = int.Parse(detaytablosu[0].CurrentRow.Cells[2].Value.ToString());
            textBoxes[2].Text = detaytablosu[0].CurrentRow.Cells[3].Value.ToString();
        }

        public void hammaddeekleduzenle(object sender, EventArgs e)
        {
            SqlCon.Open();

                DynamicParameters param = new DynamicParameters();
                param.Add("@detayid", detayselectedid);
                param.Add("@id", selectedid);
                param.Add("@urunid", int.Parse(ComboBoxes[2].SelectedValue.ToString()));
            try
            {

                param.Add("@urunmiktar", int.Parse(textBoxes[2].Text));
            }
            catch (Exception)
            {

                MessageBox.Show("Hammadde Miktarı Girilmedi.");
            }
                SqlCon.Execute("UretimiemridetayDuzenleekle", param, commandType: CommandType.StoredProcedure);
                üretimemriuruncombo();
                detayselectedid = 0;
                SqlCon.Close();
        }

        #endregion
        #region üretim emri ekleme button
        public void uretimemriduzenle()
        {
            verionayı = false;

            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@id",selectedid);

            if (baslangictarihi == null && radioButtons[0].Checked == true)
            {
                param.Add("@bitistarihi", null);
                param.Add("@baslangictarihi", timer);
            }
            
             else if (radioButtons[1].Checked == true && baslangictarihi != null)
            {

                param.Add("@bitistarihi", timer);

                param.Add("@baslangictarihi", baslangictarihi);
            }

            else if (radioButtons[1].Checked == true)
            {

                param.Add("@bitistarihi", timer);

                param.Add("@baslangictarihi", baslangictarihi);
            }
            else
            {
                param.Add("@baslangictarihi", null);
                param.Add("@bitistarihi", null);
            }
            try
            {

                param.Add("@siparisid", int.Parse(textBoxes[0].Text));
            }
            catch (Exception)
            {

                MessageBox.Show("Sipariş ID Girilmedi");
                return;
            }
            param.Add("@cikanurunid", ComboBoxes[0].SelectedValue);
            param.Add("@rotaid", ComboBoxes[1].SelectedValue);
            param.Add("@planlanantarih", DateTimePicks[0].Value.ToString());
          
            if (radioButtons[0].Checked == true) param.Add("@uretimindurumu", "Başladı");
            else if (radioButtons[1].Checked == true) param.Add("@uretimindurumu", "Bitti");
            else param.Add("@uretimindurumu", "Başlamadı");
            try
            {
                verionayı = true; 
                SqlCon.Execute("UretimEmriDuzenleme", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                MessageBox.Show("Girilen Veriler Hatalı.");
                return;
            }

            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #region Üretim emri detay silme
        public void emirdetaysil(object sender, EventArgs e)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@detayid", detayselectedid);
            SqlCon.Execute("Uretimiemridetaysil", param, commandType: CommandType.StoredProcedure);
            üretimemriuruncombo();
            detayselectedid = 0;
        }
        #endregion
        #endregion
        #region makinalar
        public void makinagüncelleekle()
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@id",selectedid);
            param.Add("@MakinaAdi", textBoxes[0].Text);
            param.Add("@makinastogu", textBoxes[1].Text);
            param.Add("@bakımtarihi", DateTimePicks[0].Value.ToString());
            param.Add("@makinaaciklaması", textBoxes[2].Text);
            SqlCon.Execute("MakinaEkleVeDuzenle", param, commandType: CommandType.StoredProcedure);



            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #region rota detay tablosu



        //sil false olduğu için çoka çokta yeni ekleyemiyor.
        public void rotadetaytablosu()
        {
            List<Operasyonlar> list = SqlCon.Query<Operasyonlar>("select * from Operasyon where sil = 'True'", SqlCon).ToList<Operasyonlar>();
            ComboBoxes[0].DataSource = list;
            ComboBoxes[0].DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxes[0].DisplayMember = "OperasyonAdi";
            ComboBoxes[0].ValueMember = "OperasyonID";

            List<Rotaoperasyon> list1 = SqlCon.Query<Rotaoperasyon>("select * from Rota_ve_Operasyon where sil = 'True' and rotaID = "+ selectedid + "", SqlCon).ToList<Rotaoperasyon>();
            detaytablosu[0].DataSource = list1;


        }

        public void rotadetayaekle(object sender, EventArgs e)
        {

            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", selectedid);
            param.Add("@operasyonid", int.Parse(ComboBoxes[0].SelectedValue.ToString()));
            try
            {

                SqlCon.Execute("RotayaOperasyonEkleDuzenle", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                MessageBox.Show("Bu rotada bu operasyon var");
            }
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }

            rotadetaytablosu();
        }

        public void opsil(object sender, EventArgs e)
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", selectedid);
            param.Add("@operasyonid", detayselectedid);
            SqlCon.Execute("Rotaoperasyonusil", param, commandType: CommandType.StoredProcedure);

            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
            rotadetaytablosu();
        }
        public void rotagüncelle()
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", selectedid);
            param.Add("@aciklama", textBoxes[0].Text);
            SqlCon.Execute("rotaduzenle", param, commandType: CommandType.StoredProcedure);
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        public void opid(object sender, EventArgs e)
        {
            detayselectedid = int.Parse(detaytablosu[0].CurrentRow.Cells[1].Value.ToString());
            ComboBoxes[0].SelectedValue = int.Parse(detaytablosu[0].CurrentRow.Cells[1].Value.ToString());
        }
        #endregion
        #region operasyonlar

        public void operasyoneklegüncelle()
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@OperasyonID", selectedid);
            param.Add("@OperasyonAdi", textBoxes[0].Text.Trim());
            param.Add("@sil", "True");
            SqlCon.Execute("OperasyonEkleveDuzenle", param, commandType: CommandType.StoredProcedure);
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #endregion
        #region Muhasebe
        #region Faturalar
        #region Faturaverileri
        public void faturadetaylistele()
        {
            if (SatinmiSatismi == "Satış")
            {
                
                List<Faturadetay> list = SqlCon.Query<Faturadetay>("select * from Satis_Faturasi_Detay where faturaID = " + selectedid + " and sil = 'True'", SqlCon).ToList<Faturadetay>();
                detaytablosu[0].DataSource = list;
                detaytablosu[0].Columns[0].Visible = false;
                detaytablosu[0].Columns[1].Visible = false;
                detaytablosu[0].Columns[5].Visible = false;
            }
            else if (SatinmiSatismi == "Satın")
            {
                List<Faturadetay> list = SqlCon.Query<Faturadetay>("select * from Satin_Alma_Faturalari_Detay where faturaID = " + selectedid + " and sil = 'True'", SqlCon).ToList<Faturadetay>();
                detaytablosu[0].DataSource = list;
                detaytablosu[0].Columns[0].Visible = false;
                detaytablosu[0].Columns[1].Visible = false;
                detaytablosu[0].Columns[5].Visible = false;
            }

            List<ürünler> list3 = SqlCon.Query<ürünler>("select * from Urun_Tablosu where sil = 'True' and urunturu = 'Ticari'", SqlCon).ToList<ürünler>();
            ComboBoxes[0].DataSource = list3;
            ComboBoxes[0].DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxes[0].DisplayMember = "urunadi";
            ComboBoxes[0].ValueMember = "urunID";

        }
        #endregion
        #region faturadetayidcekme
        public void faturadetayid(object sender, EventArgs e)
        {
            detayselectedid = int.Parse(detaytablosu[0].CurrentRow.Cells[0].Value.ToString());

            ComboBoxes[0].SelectedValue = int.Parse(detaytablosu[0].CurrentRow.Cells[2].Value.ToString());
            textBoxes[4].Text = detaytablosu[0].CurrentRow.Cells[3].Value.ToString();
            textBoxes[5].Text = detaytablosu[0].CurrentRow.Cells[4].Value.ToString();
        }
        #endregion
        #region faturadetayekleme
        public void faturadetayekleme(object sender, EventArgs e)
        {

            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", detayselectedid);
            param.Add("@faturaID", selectedid);
            param.Add("@urunid", int.Parse(ComboBoxes[0].SelectedValue.ToString()));
            param.Add("@urunadet", int.Parse(textBoxes[3].Text));
            param.Add("@birimfiyat", float.Parse(textBoxes[4].Text));
            if (SatinmiSatismi == "Satın")
            {
                try
                {

                    SqlCon.Execute("SatinAlmaFaturaEkleDüzenle", param, commandType: CommandType.StoredProcedure);
                   
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (SatinmiSatismi == "Satış")
            {
                try
                {

                    SqlCon.Execute("SatişFaturaEkleDüzenle", param, commandType: CommandType.StoredProcedure);
                    
                }
                catch (Exception)
                {

                    throw;
                }
            }

            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }

            faturadetaylistele();
        }
        #endregion
        #region faturadetaysilme
        public void faturadetaysil(object sender, EventArgs e)
        {


                DynamicParameters param = new DynamicParameters();
                param.Add("@id", detayselectedid);
                if (SatinmiSatismi == "Satın")
                {
                    SqlCon.Execute("SatinAlmaFaturaDetaySilme", param, commandType: CommandType.StoredProcedure);
                }
                else if (SatinmiSatismi == "Satış")
                {

                    SqlCon.Execute("SatişAlmaFaturaDetaySilme", param, commandType: CommandType.StoredProcedure);
                }

                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }

                faturadetaylistele();
            
        }

        #endregion
        #region Faturaekleme
        public void faturaekleme()
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", selectedid);
            param.Add("@cariid", int.Parse(textBoxes[0].Text));
            param.Add("@faturatarihi", DateTimePicks[0].Value.ToString());
            param.Add("@tutar", float.Parse(textBoxes[1].Text));
            param.Add("@ödemebilgisi", textBoxes[2].Text); 
            if (CheckBoxes[0].Checked == true)
                param.Add("@iade", true);
            else param.Add("@iade", false);
            if (SatinmiSatismi == "Satın")
            {
                try
                {

                    SqlCon.Execute("SatinAlmaFaturaGenelEkleDüzenle", param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {

                }
            }
            else if (SatinmiSatismi == "Satış")
            {

                try
                {
                    SqlCon.Execute("SatişAlmaFaturaGenelEkleDüzenle", param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {

                }

            }
        }

        #endregion
        #endregion
        #endregion
        #region urunler tab control
        #region urunler

        #region ürünler kategori kombobox
        //ürünlerdeki kategori için combo box

        public void urunlercombobox()
        {

            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            List<kategori> list = SqlCon.Query<kategori>("select * from Urun_Kategorileri where kategoriadi Like '%%' and sil = 'True'", SqlCon).ToList<kategori>();
            ComboBoxes[0].DataSource = list;
            ComboBoxes[0].AutoCompleteMode = AutoCompleteMode.SuggestAppend; //AutoCompleteMode'ı None olarak ayarlayarak, yazarken seçeneklerin açılmayacağı ve yazma işlemine engel olmayacağı sağlanır.
            ComboBoxes[0].AutoCompleteSource = AutoCompleteSource.CustomSource;
            ComboBoxes[0].DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxes[0].DisplayMember = "kategoriadi";
            ComboBoxes[0].ValueMember = "urunkategoriID";
            ComboBoxes[0].SelectionStart = ComboBoxes[0].Text.Length;
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        public void Urunekleduzenle()
        {
            string secilenurun = "";
            if (radioButtons[0].Checked) secilenurun = "Ticari";
            if (radioButtons[1].Checked) secilenurun = "Mamul";
            if (radioButtons[2].Checked) secilenurun = "YarıMamul";
            if (radioButtons[3].Checked) secilenurun = "Hammadde";
            if(SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            MessageBox.Show(ComboBoxes[0].SelectedValue.ToString());

            

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", selectedid);
            param.Add("@Urunadi", textBoxes[0].Text.Trim());
            param.Add("@Urunaciklaması", textBoxes[1].Text.Trim());
            param.Add("@UrunkategoriID", ComboBoxes[0].SelectedValue);
            param.Add("@Urunturu", secilenurun);
            param.Add("@rafkodu", int.Parse(textBoxes[2].Text.Trim()));
            param.Add("@stok_miktarı", int.Parse(textBoxes[3].Text.Trim()));
            param.Add("@sil", "True");
            SqlCon.Execute("UrunEkleveDuzenle",param,commandType:CommandType.StoredProcedure);




            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #region kategori
        public void kategoriguncelleme()
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@kategoriid", selectedid);
            param.Add("@kategoriadi", textBoxes[0].Text.Trim());
            param.Add("@kategoriaciklamasi", textBoxes[1].Text.Trim());
            param.Add("@sil", "True");
            SqlCon.Execute("kategoriekleveduzenle", param, commandType: CommandType.StoredProcedure);


            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #region irsaliyeler
        #region irsaliyedetaylisteleme

        public void irsaliyedetayılistele()
        {
            if(SatinmiSatismi == "Satış")
            {

                List<irsaliyedetay> list = SqlCon.Query<irsaliyedetay>("select * from Satin_Alma_İrsaliye_Detay where irsaliyeID = " + selectedid + " and sil = 'True'", SqlCon).ToList<irsaliyedetay>();
                detaytablosu[0].DataSource = list;
            }
            else if (SatinmiSatismi == "Satın")
            {
                List<irsaliyedetay> list = SqlCon.Query<irsaliyedetay>("select * from Satin_Alma_İrsaliye_Detay where irsaliyeID = " + selectedid + " and sil = 'True'", SqlCon).ToList<irsaliyedetay>();
                detaytablosu[0].DataSource = list;
            }

            List<Cariler> list1 = SqlCon.Query<Cariler>("select * from Cari_Hesaplar where sil = 'True'", SqlCon).ToList<Cariler>();
            ComboBoxes[0].DataSource = list1;
            ComboBoxes[0].DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxes[0].DisplayMember = "CariAdi";
            ComboBoxes[0].ValueMember = "CariID";

            if(SatinmiSatismi == "Satış")
            {
                List<Satışsipariş> list2 = SqlCon.Query<Satışsipariş>("select * from Satis_Siparisleri where sil = 'True'", SqlCon).ToList<Satışsipariş>();
                ComboBoxes[1].DataSource = list2;
                ComboBoxes[1].DropDownStyle = ComboBoxStyle.DropDown;
                ComboBoxes[1].DisplayMember = "SiparisID";
                ComboBoxes[1].ValueMember = "SiparisID";
            }
            else if (SatinmiSatismi == "Satın")
            {
                List<Satinalmasiparisleri> list2 = SqlCon.Query<Satinalmasiparisleri>("select * from Satin_Alma_Siparişleri where sil = 'True'", SqlCon).ToList<Satinalmasiparisleri>();
                ComboBoxes[1].DataSource = list2;
                ComboBoxes[1].DropDownStyle = ComboBoxStyle.DropDown;
                ComboBoxes[1].DisplayMember = "SiparisID";
                ComboBoxes[1].ValueMember = "SiparisID";
            }
            

            List<ürünler> list3 = SqlCon.Query<ürünler>("select * from Urun_Tablosu where sil = 'True' and urunturu = 'Ticari'", SqlCon).ToList<ürünler>();
            ComboBoxes[2].DataSource = list3;
            ComboBoxes[2].DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxes[2].DisplayMember = "urunadi";
            ComboBoxes[2].ValueMember = "urunID";
        }
        #endregion
        #region irsaliyedetayidçekme

        public void irsaliyeid(object sender, EventArgs e)
        {
            detayselectedid = int.Parse(detaytablosu[0].CurrentRow.Cells[0].Value.ToString());
            ComboBoxes[2].SelectedValue =int.Parse(detaytablosu[0].CurrentRow.Cells[2].Value.ToString());
            textBoxes[0].Text = detaytablosu[0].CurrentRow.Cells[3].Value.ToString();
        }
        #endregion
        #region irsaliyedetayekleme

        public void irsaliyekle(object sender, EventArgs e)
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", detayselectedid);
            param.Add("@irsaliyeid", selectedid);
            param.Add("@urunid", int.Parse(ComboBoxes[2].SelectedValue.ToString()));
            param.Add("@urunadet", int.Parse(textBoxes[0].Text));
            if(SatinmiSatismi == "Satın")
            {
                SqlCon.Execute("SatinAlmaİrsaliyeEkleDüzenle", param, commandType: CommandType.StoredProcedure);
            }
            else if (SatinmiSatismi == "Satış")
            {

                SqlCon.Execute("SatisAlmaİrsaliyeEkleDüzenle", param, commandType: CommandType.StoredProcedure);
            }

            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }

            irsaliyedetayılistele();
        }
        #endregion
        #region irsaliyedetaysilme

        public void irsaliyedetaysil(object sender, EventArgs e)
        {

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", detayselectedid);
            if (SatinmiSatismi == "Satın")
            {
                SqlCon.Execute("Satinirsaliyedetaysilme",param,  commandType: CommandType.StoredProcedure);
            }
            else if (SatinmiSatismi == "Satış")
            {

                SqlCon.Execute("Satisirsaliyedetaysilme",param,  commandType: CommandType.StoredProcedure);
            }

            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }

            irsaliyedetayılistele();
        }
        #endregion
        #region irsaliyeekleme
        public void irsaliyegenelekleme()
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", selectedid);
            param.Add("@siparisid", int.Parse(ComboBoxes[1].SelectedValue.ToString()));
            param.Add("@cariid", int.Parse(ComboBoxes[0].SelectedValue.ToString()));
            param.Add("@kargofirması", textBoxes[1].Text);
            if (CheckBoxes[0].Checked == true)
            param.Add("@iade", true);
            else param.Add("@iade", false);
            if (SatinmiSatismi == "Satın")
            {
                try
                {

                    SqlCon.Execute("SatinAlmaİrsaliyeGenelEkleDüzenle", param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    
                }
            }
            else if (SatinmiSatismi == "Satış")
            {

                try
                {
                    SqlCon.Execute("SatisAlmaİrsaliyeGenelEkleDüzenle", param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {

                }
                
            }

            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #endregion
        #endregion
        #region Personeller
        public void personelekleduzenle()
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            int indexofsecretcombo = ComboBoxes[0].SelectedIndex;
            int indexofunvan = int.Parse(gizlicombo.Items[indexofsecretcombo].ToString());


            DynamicParameters param = new DynamicParameters();
            param.Add("@calisanid", selectedid);
            param.Add("@calisanadi", textBoxes[0].Text.Trim());
            param.Add("@calisansoyadi", textBoxes[1].Text.Trim());
            param.Add("@isegiris", DateTimePicks[0].Value.ToString("yyyy-MM-dd"));
            param.Add("@telefon", int.Parse(textBoxes[2].Text.Trim()));
            param.Add("@unvanID", indexofunvan);
            param.Add("@sil", "True");
            SqlCon.Execute("personeleklevedüzenle", param, commandType: CommandType.StoredProcedure);



            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #region Cariler
        public void cariekleduzenle()
        {

            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            DynamicParameters param = new DynamicParameters();

            param.Add("@CariID",selectedid);
            param.Add("@CariAdi", textBoxes[0].Text.Trim());
            param.Add("@CariTelefon", int.Parse(textBoxes[1].Text.Trim()));
            param.Add("@CariAdres", textBoxes[2].Text.Trim());
            param.Add("@Carimail", textBoxes[3].Text.Trim());
            param.Add("@carihesapnumarası", textBoxes[4].Text.Trim());
            param.Add("@cariülke", textBoxes[5].Text.Trim());
            param.Add("@carisehir", textBoxes[6].Text.Trim());
            param.Add("@caripostakodu", textBoxes[7].Text.Trim());
            if (radioButtons[0].Checked == true) param.Add("@carituru", "Tedarikçi");
            if (radioButtons[1].Checked == true) param.Add("@carituru", "Müşteri");
            if (radioButtons[2].Checked == true) param.Add("@carituru", "Her ikisi");
            param.Add("@sil", "True");

            SqlCon.Execute("Carieklevedüzenle", param, commandType: CommandType.StoredProcedure);

            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        #endregion

        #region çalışan ünvan
        //personel ekleme ekranındaki kombo box

        public void personelkombobox()
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }

            SqlCommand cmd = new SqlCommand("select * from Unvan where sil = 'True'", SqlCon);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ComboBoxes[0].Items.Add(dr[1]);
                gizlicombo.Items.Add(dr[0].ToString());
            }

            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        #endregion
        //esc ile kapanmasını sağlar
        private void EklemeEkranı_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void EklemeEkranı_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void EklemeEkranı_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
