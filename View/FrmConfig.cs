using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Business;
using Dto;

namespace View
{
    public partial class FrmConfig : Form
    {
        private string path = "C:\\NetListmod\\analysis\\";
        private string output = "C:\\NetListmod\\analysis\\output\\";
        private string src = "C:\\NetListmod\\analysis\\src\\";
        private string date = "C:\\NetListmod\\analysis\\date\\";

        //FrmNetListMod frmNetList;
        NetListModMain netList = new NetListModMain();

        public FrmConfig()
        {
            InitializeComponent();

            textBoxPath.Text = this.path;
            textBoxOutput.Text = this.output;
            textBoxSrc.Text = this.src;
            textBoxDate.Text = this.date;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialogPath.ShowDialog();
            string folderPath = folderBrowserDialogPath.SelectedPath;
            this.path = folderPath + "\\";
            this.output = folderPath + "\\output\\";
            this.src = folderPath + "\\src\\";
            this.date = folderPath + "\\date\\";

            textBoxPath.Text = this.path;
            textBoxOutput.Text = this.output;
            textBoxSrc.Text = this.src;
            textBoxDate.Text = this.date;

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            path = "C:\\NetListmod\\analysis\\";
            output = "C:\\NetListmod\\analysis\\output\\";
            src = "C:\\NetListmod\\analysis\\src\\";
            date = "C:\\NetListmod\\analysis\\date\\";

            textBoxPath.Text = path;
            textBoxOutput.Text = output;
            textBoxSrc.Text = src;
            textBoxDate.Text = date;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            PathTransfer pathTransfer = new PathTransfer();

            pathTransfer.myPath = this.path;
            pathTransfer.myOutput = this.output;
            pathTransfer.mySrc = this.src;
            pathTransfer.myDate = this.date;

            netList.ConfigPath(pathTransfer);

            Close();
        }

        public void pathObjetct(NetListModMain netListModMain)
        {
            netList = netListModMain;
        }
    }
}
