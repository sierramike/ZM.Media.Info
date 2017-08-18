using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZM.Media.Info.WinTest
{
    public partial class frmMaster : Form
    {
        public frmMaster()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                txtFileName.Text = ofd.FileName;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            var MI = new Media.Info.MediaInfoWrapper();

            var mfi = MI.Read(txtFileName.Text);

            txtDetails.Text = mfi.Display().Replace("\n", "\r\n");
        }
    }
}
