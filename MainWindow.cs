using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;

namespace CSL_Test__1
{
    public partial class MainWindow : Form
    {
        TorrentXMLHandler data = null;

        public MainWindow()
        {
            data = new TorrentXMLHandler();
            InitializeComponent();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.SaveAndClose();
        }
    }
}
