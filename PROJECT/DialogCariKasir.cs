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
    public partial class DialogCariKasir : Form
    {
        Koneksi konn = new Koneksi();
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        public string kode_kasir = "";

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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();
            conn.Close();
        }
        void CariKasir()
        {
            NpgsqlConnection conn = konn.Connection;
            conn.Open();
            cmd = new NpgsqlCommand("select *  from Kasir where kode_kasir like '%" + textBox1.Text + "%'", conn);
            ds = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            npgsqlDataAdapter.Fill(ds, "Kasir");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Kasir";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();
            conn.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CariKasir();
        }
        public string ambil_kode_kasir
        {
            get { return kode_kasir; }
        }
        public DialogCariKasir()
        {
            InitializeComponent();
            MunculDataKasir();
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                kode_kasir = row.Cells["kode_kasir"].Value.ToString();
                this.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }
    }
}
