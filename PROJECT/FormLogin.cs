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
    public partial class FormLogin : Form
    {
        private NpgsqlCommand cmd;
        //private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;

        Koneksi konn = new Koneksi();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = konn.Connection;   
            {
                conn.Open();
                cmd = new NpgsqlCommand("select * from Kasir where kode_kasir ='" + textBox1.Text + "' and password_kasir = '" + textBox2.Text + "';", conn);
                NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                npgsqlDataAdapter.Fill(dataTable);
                cmd.Dispose();
                if (dataTable.Rows.Count!=0)
                { 
                    FormMenuUtama.menu.menuLogin.Enabled = false;   
                    FormMenuUtama.menu.menuLogout.Enabled = true;
                    FormMenuUtama.menu.menuMaster.Enabled = true;
                    FormMenuUtama.menu.menuTransaksi.Enabled = true;      
                    FormMenuUtama.menu.menuUtility.Enabled = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Yang anda inputkan salah");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox1.Text = "KSR001";
            textBox2.Text = "ADMIN";
        }
    }
}
