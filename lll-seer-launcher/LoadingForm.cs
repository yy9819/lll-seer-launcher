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
using System.Reflection;

namespace lll_seer_launcher
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            string swfPath = Directory.GetCurrentDirectory() + "\\bin\\loading\\bibiloading.swf";
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\bin\\loading\\")) Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\bin\\loading\\");
            if (!File.Exists(swfPath))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string fullResourceName = assembly.GetName().Name + ".bibiloading";
                using (Stream resourceStream = assembly.GetManifestResourceStream(fullResourceName))
                {
                    if (resourceStream != null)
                    {
                        // 创建目标文件
                        using (FileStream fileStream = new FileStream(swfPath, FileMode.Create))
                        {
                            // 将资源流复制到目标文件
                            resourceStream.CopyTo(fileStream);
                        }
                    }
                }
            }
            this.loadingWebBrowser.Navigate(new Uri(swfPath).AbsoluteUri);
        }
    }
}
