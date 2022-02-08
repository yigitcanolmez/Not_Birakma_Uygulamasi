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

namespace KarsilikMesajlasma
{
    public partial class Panel : Form
    {
        public Panel()
        {
            InitializeComponent();
        }
        public string numara;
        
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-EKQK6BH\SQLEXPRESS;Initial Catalog=DBMesaj;Integrated Security=True");

        void gelenkutusu()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select (AD+ ' ' +SOYAD) AS GONDEREN,BASLIK,ICERIK From tbl_mesajlar inner join tbl_kisiler on tbl_mesajlar.GONDEREN = tbl_kisiler.NUMARA where ALICI =" + numara, bgl);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
            void gidenkutusu()
            {
                SqlDataAdapter da1 = new SqlDataAdapter("Select  (AD + ' ' + SOYAD) AS 'Alıcı', BASLIK, ICERIK From tbl_mesajlar inner join tbl_kisiler on tbl_mesajlar.ALICI = tbl_kisiler.NUMARA where GONDEREN =" + numara, bgl);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                dataGridView3.DataSource = dt1;
            }


            private void Panel_Load(object sender, EventArgs e)
        {
            lblnumara.Text = numara;
            gelenkutusu();
            gidenkutusu();

            bgl.Open();
            SqlCommand sql = new SqlCommand("Select AD+SOYAD as 'AD SOYAD' from tbl_kisiler where NUMARA=" + numara,bgl);
            SqlDataReader dr = sql.ExecuteReader();
            if(dr.Read())
            {
                lbladsoyad.Text = dr[0].ToString();
            }
            bgl.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bgl.Open();
          
            SqlCommand komut = new SqlCommand("Insert into tbl_mesajlar (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)",bgl);
           
            komut.Parameters.AddWithValue("@p1",numara);
            komut.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p3", textBox2.Text);
            komut.Parameters.AddWithValue("@p4",richTextBox1.Text);

            komut.ExecuteNonQuery();

            MessageBox.Show("Mesaj başarıyla gönderilmiştir.");

            bgl.Close();
            gelenkutusu();
            gidenkutusu();
        }
    }
}
