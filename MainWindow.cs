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
using System.Threading;

namespace CSL_Test__1
{
    public partial class MainWindow : Form
    {
        TorrentXMLHandler data = null;
        uTorrentHandler utorrent = new uTorrentHandler();
        TorrentBuilder builder = new TorrentBuilder();
        Torrent[] torrents;

        public MainWindow()
        {
            data = new TorrentXMLHandler();
            InitializeComponent();
            dataGridView.DataSource = data.table;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.table.AcceptChanges();
            data.SaveAndClose();
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            // transfer the filenames to a string array
            // (yes, everything to the left of the "=" can be put in the 
            // foreach loop in place of "files", but this is easier to understand.)

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Torrent[] torrents = builder.Build(files);
            data.AddTorrents(torrents);
        }

        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files (not text or anything else)
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol
                e.Effect = DragDropEffects.All;
        }

        private void uTorrentSendAllButton_Click(object sender, EventArgs e)
        {
            utorrent.SendTorrents();
            dataGridView.Update();
        }
    }
}
