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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-EKQK6BH\SQLEXPRESS;Initial Catalog=DBMesaj;Integrated Security=True");


        private void button1_Click(object sender, EventArgs e)
        {
            bgl.Open();

            SqlCommand komut = new SqlCommand("Select * from tbl_kisiler where NUMARA = @p1 and SIFRE = @p2", bgl);
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            
            if(dr.Read())
            {
                Panel prm = new Panel();
                prm.numara = maskedTextBox1.Text;

                prm.Show();
                this.Hide();
                MessageBox.Show("Giriş Başarılı");
            }
            else
            {
                MessageBox.Show("Hatalı Değer Kullanıcı veya Şifre");
            }
            
            bgl.Close();

        }
    }
}
