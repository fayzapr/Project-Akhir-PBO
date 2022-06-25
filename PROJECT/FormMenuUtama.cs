using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT
{
    public partial class FormMenuUtama : Form
    {
        public static FormMenuUtama menu;
        MenuStrip mnstrip;
        FormLogin frmLogin;
        FormTransaksiKasir frmTransaksi;
        FormUtility frmUtility;    
        void frmLogin_fromClosed(object sender, FormClosedEventArgs e) //agar tidak terjadi double saat di klik
        {
            frmLogin = null;
        }

        FormMaterKasir frmKasir;
        void frmKasir_fromClosed(object sender, FormClosedEventArgs e)
        {
            frmKasir = null;
        }

        FormMasterBarang frmBarang;
        void frmBarang_fromClosed(object sender, FormClosedEventArgs e)
        {
            frmBarang = null;
        }
        void frmTransaksi_fromClosed(object sender, FormClosedEventArgs e)
        {
            frmTransaksi = null;
        }
        void frmUtility_fromClosed(object sender, FormClosedEventArgs e)
        {
            frmUtility = null;
        }


        void MenuLock()
        {
            menuLogin.Enabled = true;
            menuLogout.Enabled = false;
            menuMaster.Enabled = false;
            menuTransaksi.Enabled = false;   
            menuUtility.Enabled = false;    
            menu = this;
        }
        public FormMenuUtama()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FormMenuUtama_Load(object sender, EventArgs e)
        {
            MenuLock(); 
        }

        private void menuLogin_Click(object sender, EventArgs e)
        {
            if (frmLogin == null)
            {
                frmLogin = new FormLogin();
                frmLogin.FormClosed += new FormClosedEventHandler(frmLogin_fromClosed);
                frmLogin.ShowDialog();  
            }
            else
            {
                frmLogin.Activate();
            }
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            MenuLock();
        }

        private void kasirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmKasir == null)
            {
                frmKasir = new FormMaterKasir();
                frmKasir.FormClosed += new FormClosedEventHandler(frmKasir_fromClosed);
                frmKasir.ShowDialog();
            }
            else
            {
                frmKasir.Activate();
            }
        }

        private void barangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmBarang == null)
            {
                frmBarang = new FormMasterBarang();
                frmBarang.FormClosed += new FormClosedEventHandler(frmBarang_fromClosed);
                frmBarang.ShowDialog();
            }
            else
            {
                frmBarang.Activate();
            }
        }

        private void penjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmTransaksi == null)
            {
                frmTransaksi = new FormTransaksiKasir();
                frmTransaksi.FormClosed += new FormClosedEventHandler(frmTransaksi_fromClosed);
                frmTransaksi.ShowDialog();
            }
            else
            {
                frmTransaksi.Activate();
            }
        }

        private void menuUtility_Click(object sender, EventArgs e)
        {

        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmUtility == null)
            {
                frmUtility = new FormUtility();
                frmUtility.FormClosed += new FormClosedEventHandler(frmUtility_fromClosed);
                frmUtility.ShowDialog();
            }
            else
            {
                frmUtility.Activate();
            }
        }
    }
}
