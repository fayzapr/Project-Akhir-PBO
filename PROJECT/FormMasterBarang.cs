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
    public partial class FormMasterBarang : Form
    {
        Koneksi konn = new Koneksi();
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;

        void munculSatuan()
        {
            comboBox1.Items.Add("PCS");
            comboBox1.Items.Add("BOX");
            comboBox1.Items.Add("KG");
            comboBox1.Items.Add("PACK");
        }

        void KondisiAwal()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            comboBox1.Text = "";
            munculSatuan();
            MunculDataBarang();
        }

        void MunculDataBarang()
        {
            NpgsqlConnection conn = konn.Connection;
            conn.Open();
            cmd = new NpgsqlCommand("select *  from Barang", conn);
            ds = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            npgsqlDataAdapter.Fill(ds, "Barang");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Barang";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();
            conn.Close();

        }
        void CariBarang()
        {
            NpgsqlConnection conn = konn.Connection;
            conn.Open();
            cmd = new NpgsqlCommand("select *  from Barang where kode_barang like '%" + textBox6.Text + "%' or nama_barang like '%" + textBox6.Text + "%' ", conn);
            ds = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            npgsqlDataAdapter.Fill(ds, "Barang");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Barang";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();
            conn.Close();
        }
        public FormMasterBarang()
        {
            InitializeComponent();
        }

        private void FormMasterBarang_Load(object sender, EventArgs e)
        {
            KondisiAwal();
            //NoOtomatis();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan semua form terisi");
            }
            else
            {
                NpgsqlConnection conn = konn.Connection;
                conn.Open();
                cmd = new NpgsqlCommand("insert into Barang values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "')", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil di Input");
                conn.Close();
                KondisiAwal();
                //NoOtomatis();
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                NpgsqlConnection conn = konn.Connection;
                conn.Open();
                cmd = new NpgsqlCommand("select * from barang where kode_barang ='" + textBox1.Text + "'", conn);
                cmd.ExecuteNonQuery();
                rd = cmd.ExecuteReader();
                cmd.Dispose();
                if (rd.Read())
                {
                    textBox1.Text = rd[0].ToString();
                    textBox2.Text = rd[1].ToString();
                    textBox3.Text = rd[2].ToString();
                    textBox4.Text = rd[3].ToString();
                    textBox5.Text = rd[4].ToString();
                    comboBox1.Text = rd[5].ToString();
                }
                else
                {
                    MessageBox.Show("Data tidak ada");
                }
                conn.Close();
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan semua form terisi");
            }
            else
            {
                NpgsqlConnection conn = konn.Connection;
                conn.Open();
                cmd = new NpgsqlCommand("update Barang set nama_barang='" + textBox2.Text + "' ,harga_beli='" + textBox3.Text + "' ,harga_jual='" + textBox4.Text + "' ,jumlah_barang='" + textBox5.Text + "' ,satuan_barang= '" + comboBox1.Text + "' where kode_barang='" + textBox1.Text + "'", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil di Edit");
                conn.Close();
                KondisiAwal();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Pastikan semua form terisi");
            }
            else
            {
                NpgsqlConnection conn = konn.Connection;
                cmd = new NpgsqlCommand("delete from barang where kode_barang='" + textBox1.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil di Hapus");
                conn.Close();
                KondisiAwal();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            CariBarang();
        }
    }
}
