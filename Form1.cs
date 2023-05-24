using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dapper;

namespace ERP_PROJESİ
{
    public partial class Form1 : Form
    {
        //kullanıcı için kullanacağım classım
        class _kullanıcı
        {
            public int id { get; set; }
            public string kullaniciadi { get; set; }
            public string parola { get; set; }
            public int ünvan { get; set; }
        }
        //ana forma göndermek için yazdığım calisanid
        public string calisanid;
        //SQL bağlantısı
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-PRMBC7J; initial Catalog = ERP; Integrated Security = True");

        public Form1()
        {
            InitializeComponent();
        }

        //Login sayfasının dizaynının bir kısmını burada yapıyorum.
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        //giriş buttonu - burada textbox1 ve 2 deki değerleri username ve password olarak çekiyorum ve validateuser methodunda çağırıyorum.
        private void button1_Click(object sender, EventArgs e)
        {


            string username = textBox1.Text;
            string password = textBox2.Text;

            int userId = ValidateUser(username, password);
            if (userId != default(int))
            {
                
                Ana anaa = new Ana(userId);

                this.Hide();
                anaa.Form1 = this;
                anaa.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya parola yanlış.");
            }


        }
        //Kapandıktan sonra yaşanabilcek problemler için bir applicationexit komutu çağırıyorum
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //kod ekranına gelebilmek için ana sayfada çift tıkladığımda gelen komut. hızlı bir şekilde kod sayfasına gelmemi sağladı (üşendim)
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
        //İlk login methodum. Yeni olan daha işlevsel olduğundan onu kullanıyorum.
            public void Login(string username, string password)
        {
            bool kullanıcı = false;
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@kullaniciadi", username);

            var reader = SqlCon.ExecuteReader("kullanici", parameters, commandType:CommandType.StoredProcedure);
            if (reader.Read())
            {
                kullanıcı=true;
            }
            reader.Dispose();
            reader.Close();
            DynamicParameters param = new DynamicParameters();
            param.Add("@kullanici", username);
            param.Add("@parola", password);
            if (kullanıcı)
            {
                reader = SqlCon.ExecuteReader("Parola", param, commandType: CommandType.StoredProcedure);
                if (reader.Read())
                {


                           //Ana anaa = new Ana();

                           this.Hide();
                    MessageBox.Show(calisanid); 
                            //anaa.Show();
                  


                }else { MessageBox.Show("Şifre hatalı"); }
            }
            
            else { MessageBox.Show("Kullanıcı bulunamadı"); }
            reader.Dispose();
            reader.Close();

        }
        //güncel giriş methodu. Sqldeki Kullanıcılar tablosundan kullaniciadi ve parola değerlerini eşleştiriyor.
        private int ValidateUser(string username, string password)
        {

                string sql = "SELECT UserId FROM kullanicilar_ WHERE kullaniciadi = @kullanici AND parola = @parola";
                int userId = SqlCon.QuerySingleOrDefault<int>(sql, new { kullanici = username, parola = password });

                return userId;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }
        #region classlar


        #endregion
    }
}
