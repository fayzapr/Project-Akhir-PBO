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
    public partial class FormTransaksiKasir : Form
    {
        Koneksi konn = new Koneksi();
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;

        private void bersih()
        {
            textBox1.Text = "";
            DateTimePicker_kwitansi.Value = DateTime.Now;
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "0";
            textBox5.Text = "0";    
            textBox2.Enabled = false;   
            textBox3.Enabled = false;     
            button3.Enabled = false;
            button6.Enabled = false;
        }
        private void awal()
        {
            bersih();
            NpgsqlConnection conn = konn.Connection;
            {
                try
                {
                    conn.Open();
                    cmd = new NpgsqlCommand("select * from Transaksi order by no_kwitansi desc", conn); 
                    ds = new DataSet();
                    da = new NpgsqlDataAdapter(cmd);    
                    da.Fill(ds, "Transaksi");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Transaksi";
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;   
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                finally
                {
                    conn.Close();
                }
                groupBox3.Enabled = false;  
            }
        }
        void NoOtomatis()
        {
            long hitung;
            string urutan;
            NpgsqlDataReader rd;
            NpgsqlConnection conn = konn.Connection;
            conn.Open();
            cmd = new NpgsqlCommand("select no_kwitansi from transaksi where no_kwitansi in(select max(no_kwitansi) from transaksi) order by no_kwitansi desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["no_kwitansi"].ToString().Length -12, 4)) + 1;
                string kodeurutan = "000" + hitung;
                urutan = "TRX-" + kodeurutan.Substring(kodeurutan.Length - 4, 4) + "/" + DateTime.Now.ToString("MM/yyyy");
            }
            else
            {
                urutan = "TRX-0001/" + DateTime.Now.ToString("MM/yyyy");
            }
            rd.Close();
            textBox1.Enabled = false;
            textBox1.Text = urutan;
            conn.Close();
        }
        private void simpan_transaksi()
        {
            NpgsqlConnection conn = konn.Connection;
            {
                NpgsqlCommand cmd = new NpgsqlCommand("insert into transaksi values('" + textBox1.Text + "','" + DateTimePicker_kwitansi.Text + "','" + textBox6.Text + "')", conn);
                //conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                //conn.Close();
            }
        }
        private void simpan_detailtransaksi()
        {
            NpgsqlConnection conn = konn.Connection;
            {
                NpgsqlCommand cmd = new NpgsqlCommand("insert into detail_transaksi values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + textBox4.Text + "')", conn);
                //conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                //conn.Close();
            }
        }
        private void refreshPenjualan()
        {
            NpgsqlConnection conn = konn.Connection;
            {
                try
                {
                    //conn.Open();
                    cmd = new NpgsqlCommand("select * from detail_transaksi where no_kwitansi = '" + textBox1.Text + "'", conn);
                    ds = new DataSet();
                    da = new NpgsqlDataAdapter(cmd);
                    da.Fill(ds, "detail_transaksi");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "detail_transaksi";
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.Refresh();
                    cmd.Dispose ();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                /*finally
                {
                    conn.Close();
                }*/
            }
        }

        private void refreshTransaksi()
        {
            refreshPenjualan(); 
            textBox2.Clear();   
            textBox3.Clear();
            textBox4.Text = "0";
            textBox5.Text = "0";    
            textBox5.Focus();   
        }
        private void totalseluruh()
        {
            NpgsqlConnection conn = konn.Connection;
            {
                //conn.Open();
                cmd = new NpgsqlCommand ("select sum (harga_barang) as TotalBayar from detail_transaksi where no_kwitansi = '" + textBox1.Text + "' ", conn);    
                cmd.Connection = conn;
                string result = cmd.ExecuteScalar().ToString();  
                string TotalSeluruh = result.ToString();
                labelTotalSeluruh.Text = result.ToString(); 
                //conn.Close();
            }
        }
        public FormTransaksiKasir()
        {
            InitializeComponent();
            awal();
            NoOtomatis();   
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormTransaksiKasir_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogCariBarang brg = new DialogCariBarang();  
            brg.ShowDialog();
            textBox2.Text = brg.ambil_kode_barang;
            textBox3.Text = brg.ambil_nama_barang;    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            awal();
            NoOtomatis();
            DateTimePicker_kwitansi.Focus();
            button1.Enabled = true;
            button7.Enabled = true; 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogCariKasir ksr = new DialogCariKasir();
            ksr.ShowDialog();
            textBox6.Text = ksr.ambil_kode_kasir;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NpgsqlDataReader reader = null;
            NpgsqlConnection conn = konn.Connection;
            {
                if (textBox1.Text.Trim() == "" || textBox6.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "")
                {
                    MessageBox.Show("Pastikan data lengkap!");
                }
                else
                {
                    conn.Open();
                    cmd = new NpgsqlCommand("select * from detail_transaksi where no_kwitansi = '" + textBox1.Text + "'", conn);
                    //cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();
                    cmd.Dispose();
                    if (reader.Read())
                    {
                        reader.Close();
                        simpan_detailtransaksi();
                        totalseluruh();
                    }
                    else
                    {
                        reader.Close();
                        simpan_transaksi();
                        simpan_detailtransaksi();
                        totalseluruh();
                    }
                    reader.Close();
                    button7.Enabled = false;    
                    button3.Enabled = true;
                    groupBox3.Enabled = true;
                    refreshTransaksi();
                    conn.Close();
                }
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button5.Enabled = false;
            button6.Enabled = true;
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            textBox2.Text = row.Cells["no_kwitansi"].Value.ToString();
            textBox3.Text = row.Cells["kode_barang"].Value.ToString();
            textBox5.Text = row.Cells["jumlah_barang"].Value.ToString();
            textBox4.Text = row.Cells["harga_barang"].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = konn.Connection;
            {
                cmd = new NpgsqlCommand("delete from detail_transaksi where no_kwitansi = '" + textBox1.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                totalseluruh();
                refreshTransaksi();
                button5.Enabled = true;
                button6.Enabled = false;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Transaksi Selesai!");
            awal();
            labelTotalSeluruh.Text = "0";
            button7.Enabled =true;
            NoOtomatis();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
