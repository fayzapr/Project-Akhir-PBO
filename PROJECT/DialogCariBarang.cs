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
    public partial class DialogCariBarang : Form
    {
        Koneksi konn = new Koneksi();
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        public string kode_barang, nama_barang= "";

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
            cmd = new NpgsqlCommand("select *  from Barang where kode_barang like '%" + textBox1.Text + "%' or nama_barang like '%" + textBox1.Text + "%' ", conn);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CariBarang();   
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //cmd.Parameters.Add(new NpgsqlParameter("harga_jual", Convert.ToString(e.RowIndex)));
                kode_barang = row.Cells["kode_barang"].Value.ToString();
                nama_barang = row.Cells["nama_barang"].Value.ToString();
                //harga_jual = row.Cells["harga_jual"].Value.ToString();
                this.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString()); 
            }
        }
        public string ambil_kode_barang
        {
            get { return kode_barang; }
        }
        public string ambil_nama_barang
        {
            get { return nama_barang; }   
        }
        /*public int ambil_harga_jual
        {
            get
            {
                return harga_jual;
            }
        }*/

        public DialogCariBarang()
        {
            InitializeComponent();
            MunculDataBarang();

        }
    }
}
