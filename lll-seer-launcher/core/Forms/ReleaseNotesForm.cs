using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lll_seer_launcher.core.Forms
{
    public partial class ReleaseNotesForm : Form
    {
        public ReleaseNotesForm()
        {
            InitializeComponent();
        }

        private void releaseNotesForm_Load(object sender, EventArgs e)
        {

            this.webBrowser1.Url = new System.Uri("http://52.68.134.105/lllSeerLauncher/releaseNotes/", System.UriKind.Absolute);
        }
        private void releaseNotesForm_Closing(object sender,FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
