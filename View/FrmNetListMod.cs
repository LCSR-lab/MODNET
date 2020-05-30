using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Dto;
using Business;

namespace View
{
    public partial class FrmNetListMod : Form
    {
        private string msg;

        NetListModMain netListModMain = new NetListModMain();


        public FrmNetListMod()
        {
            InitializeComponent();
            txtBoxNetList.Text = "ready";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void btnExec_Click(object sender, EventArgs e)
        {
            this.msg = netListModMain.NetListMain();
            textBox1.Text = this.msg;
            txtBoxNetList.Text = "closed processing";
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            FrmConfig frmConfig = new FrmConfig();
            frmConfig.pathObjetct(netListModMain);
            frmConfig.Show();
        }
    }
}
