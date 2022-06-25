using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace PROJECT
{
    public partial class FormMaterKasir : Form
    {
        Koneksi konn = new Koneksi();
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;

        void munculLevel()
        {
            comboBox1.Items.Add("ADMIN");
        }

        void KondisiAwal()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            munculLevel();
            MunculDataKasir();
        }

        public FormMaterKasir()
        {
            InitializeComponent();
        }

        private void FormMaterKasir_Load(object sender, EventArgs e)
        {
            KondisiAwal();  
            //NoOtomatis();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void MunculDataKasir()
        {
            NpgsqlConnection conn = konn.Connection;
            conn.Open();
            cmd = new NpgsqlCommand("select *  from Kasir", conn);
            ds = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            npgsqlDataAdapter.Fill(ds, "Kasir");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Kasir";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Refresh();
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e) 
        {
            if(textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan semua form terisi");
            }
            else
            {
                NpgsqlConnection conn = konn.Connection;
                conn.Open();
                cmd = new NpgsqlCommand("insert into Kasir values('"+ textBox1.Text +"','"+ textBox2.Text +"','"+ textBox3.Text +"','"+ comboBox1.Text +"')", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil di Input");
                conn.Close();
                KondisiAwal();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                NpgsqlConnection conn = konn.Connection;
                conn.Open();
                cmd = new NpgsqlCommand("select * from Kasir where kode_kasir ='" + textBox1.Text + "'", conn);
                cmd.ExecuteNonQuery();
                rd = cmd.ExecuteReader();
                cmd.Dispose();
                if (rd.Read())
                {
                    textBox1.Text = rd[0].ToString();
                    textBox2.Text = rd[1].ToString();
                    textBox3.Text = rd[2].ToString();
                    comboBox1.Text = rd[3].ToString();
                }
                else
                {
                    MessageBox.Show("Data tidak ada");
                }
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan semua form terisi");
            }
            else
            {
                NpgsqlConnection conn = konn.Connection;
                conn.Open();
                cmd = new NpgsqlCommand("update Kasir set nama_kasir='" + textBox2.Text + "' ,password_kasir='" + textBox3.Text + "' ,level_kasir= '"+ comboBox1.Text + "' where kode_kasir='" + textBox1.Text + "'", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil di Edit");
                conn.Close();
                KondisiAwal();
            }
        }
        private void button3_Click(object sender, EventArgs e) 
        {
            if(textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan semua form terisi");
            }
            else
            {
                NpgsqlConnection conn = konn.Connection;
                cmd = new NpgsqlCommand("delete from Kasir where kode_kasir='" + textBox1.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil di Hapus");
                conn.Close();
                KondisiAwal();
            }
        }
    }
}
