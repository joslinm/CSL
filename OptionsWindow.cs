using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace CSL_Test__1
{
    public partial class OptionsWindow : Form
    {
        SettingsHandler settings = new SettingsHandler();
        Thread tw_thread;
        TorrentWatch tw;

        public OptionsWindow()
        {
            InitializeComponent();
        }

        public OptionsWindow(TorrentXMLHandler data)
        {
            InitializeComponent();
            ErrorProvider.DataSource = CustomFolderTextbox;
            tw = new TorrentWatch(data);
            tw_thread = new Thread(new ThreadStart(tw.Watch));
            tw_thread.Start();

            RestoreSelections();   
        }

        public void StopThread()
        {
            tw_thread.Abort();
        }

        public void StartThread()
        {
            tw_thread = new Thread(new ThreadStart(tw.Watch));
            tw_thread.Start();
        }

        private void RestoreSelections()
        {
            if (settings.GetDoubleSpaceRemoval())
                DoubleSpaceRemoverCheckBox.Checked = true;
            else
                DoubleSpaceRemoverCheckBox.Checked = false;

            if (settings.GetArtistFlip())
                ArtistFlipCheck.Checked = true;
            else
                ArtistFlipCheck.Checked = false;

            if (settings.GetAutoHandleBool())
                AutoCheckTimeCheckBox.Checked = true;
            else
                AutoCheckTimeCheckBox.Checked = false;

            if (settings.GetDownloadAlbum())
                DownloadAlbumCheck.Checked = true;
            else
                DownloadAlbumCheck.Checked = false;

            if (settings.GetDownloadBootleg())
                DownloadBootlegCheck.Checked = true;
            else
                DownloadBootlegCheck.Checked = false;

            if (settings.GetDownloadCompilation())
                DownloadCompilationCheck.Checked = true;
            else
                DownloadCompilationCheck.Checked = false;

            if (settings.GetDownloadEP())
                DownloadEPCheck.Checked = true;
            else
                DownloadEPCheck.Checked = false;

            if (settings.GetDownloadInterview())
                DownloadInterviewCheck.Checked = true;
            else
                DownloadInterviewCheck.Checked = false;

            if (settings.GetDownloadLive())
                DownloadLiveCheck.Checked = true;
            else
                DownloadLiveCheck.Checked = false;

            if (settings.GetDownloadMixtape())
                DownloadMixtapeCheck.Checked = true;
            else
                DownloadMixtapeCheck.Checked = false;

            if (settings.GetDownloadRemix())
                DownloadRemixCheck.Checked = true;
            else
                DownloadRemixCheck.Checked = false;

            if (settings.GetDownloadSingle())
                DownloadSingleCheck.Checked = true;
            else
                DownloadSingleCheck.Checked = false;

            if (settings.GetDownloadSingle())
                DownloadSoundtrackCheck.Checked = true;
            else
                DownloadSoundtrackCheck.Checked = false;

            if (settings.GetDownloadSoundtrack())
                DownloadSoundtrackCheck.Checked = true;
            else
                DownloadSoundtrackCheck.Checked = false;

            if (settings.GetDownloadUnknown())
                DownloadUnknownCheck.Checked = true;
            else
                DownloadUnknownCheck.Checked = false;

            if (settings.GetUppercaseAllFolderNames())
                UppercaseFolderNamesCheckBox.Checked = true;
            else
                UppercaseFolderNamesCheckBox.Checked = false;

            if (settings.GetLowercaseAllFolderNames())
                LowercaseAllFolderNamesCheckBox.Checked = true;
            else
                LowercaseAllFolderNamesCheckBox.Checked = false;

            if (settings.GetTrackTorrents())
                TrackTorrentsCheck.Checked = true;
            else
                TrackTorrentsCheck.Checked = false;

            if (settings.GetTrackZips())
                TrackZipsCheck.Checked = true;
            else
                TrackZipsCheck.Checked = false;

            MusicFolderTextbox.Text = settings.GetDownloadFolder();
            CustomFolderTextbox.Text = settings.GetCustomDirectory();
            TorrentFolderTextbox.Text = settings.GetTorrentSaveFolder();
        }

        private void DoubleSpaceRemoverCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DoubleSpaceRemoverCheckBox.Checked)
                settings.SetRemoveDoubleSpaces(true);
            else
                settings.SetRemoveDoubleSpaces(false);
        }

        private void ArtistFlipCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ArtistFlipCheck.Checked)
                settings.SetArtistFlip(true);
            else
                settings.SetArtistFlip(false);
        }

        private void TorrentProgramDirectoryTextbox_TextChanged(object sender, EventArgs e)
        {
            settings.SetTorrentClientFolder(TorrentProgramDirectoryTextbox.Text);
        }

        private void TorrentProgramDirectoryTextbox_MouseDown(object sender, MouseEventArgs e)
        {
            FolderBrowser.ShowDialog();
            settings.SetTorrentClientFolder(FolderBrowser.SelectedPath);
            TorrentProgramDirectoryTextbox.Text = FolderBrowser.SelectedPath;
        }

        private void MusicFolderTextbox_TextChanged(object sender, EventArgs e)
        {
            settings.SetDownloadFolder(MusicFolderTextbox.Text);
        }

        private void MusicFolderTextbox_MouseDown(object sender, MouseEventArgs e)
        {
            FolderBrowser.ShowDialog();
            settings.SetDownloadFolder(FolderBrowser.SelectedPath);
            MusicFolderTextbox.Text = FolderBrowser.SelectedPath;
        }

        private void TorrentFolderTextbox_TextChanged(object sender, EventArgs e)
        {
            settings.SetTorrentSaveFolder(TorrentFolderTextbox.Text);
        }

        private void TorrentFolderTextbox_MouseDown(object sender, MouseEventArgs e)
        {
            FolderBrowser.ShowDialog();
            settings.SetTorrentSaveFolder(FolderBrowser.SelectedPath);
            TorrentFolderTextbox.Text = FolderBrowser.SelectedPath;
        }

        private void CustomFolderTextbox_TextChanged(object sender, EventArgs e)
        {
            settings.SetCustomDirectory(CustomFolderTextbox.Text);
        }

        private void CustomFolderTextbox_Validating(object sender, CancelEventArgs e)
        {
            bool error = false;
            Match m;
            char[] chararray = CustomFolderTextbox.Text.ToCharArray();
            string letter;

            for(int a = 0; a < chararray.Length; a++)
            {
                if (chararray[a] == '%')
                {
                    letter = chararray[++a].ToString();

                    m = Regex.Match(letter, "a|l|s|c|e|r|v|n|x|u|y|t|i|b|p|d|z");
                    if (!m.Success)
                    {
                        ErrorProvider.SetError(this.CustomFolderTextbox, "Invalid letter after switch");
                        error = true;
                    }
                }
            }
            if (!error)
                ErrorProvider.Clear();
        }

        private void AutoCheckTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoCheckTimeCheckBox.Checked)
            {
                settings.SetAutoHandleBool(true);

                if (tw_thread != null)
                {
                    if (!tw_thread.IsAlive)
                    {
                        tw_thread.Abort();
                        Thread.Sleep(100);
                        tw_thread = new Thread(new ThreadStart(tw.Watch));
                        tw_thread.Start();
                    }
                }
                else
                {
                    tw_thread = new Thread(new ThreadStart(tw.Watch));
                    tw_thread.Start();
                }  
            }
            else
            {
                settings.SetAutoHandleBool(false);
                if (tw_thread.IsAlive)
                    tw_thread.Abort();
            }
        }

        public void StopTorrentWatch()
        {
            tw_thread.Abort();
        }

        private void DownloadAlbumCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadAlbumCheck.Checked)
                settings.SetDownloadAlbum(true);
            else
                settings.SetDownloadAlbum(false);
        }

        private void DownloadEPCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadEPCheck.Checked)
                settings.SetDownloadEP(true);
            else
                settings.SetDownloadEP(false);
        }

        private void DownloadSingleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadSingleCheck.Checked)
                settings.SetDownloadSingle(true);
            else
                settings.SetDownloadSingle(false);
        }

        private void DownloadLiveCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadLiveCheck.Checked)
                settings.SetDownloadLive(true);
            else
                settings.SetDownloadLive(false);
        }

        private void DownloadRemixCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadRemixCheck.Checked)
                settings.SetDownloadRemix(true);
            else
                settings.SetDownloadRemix(false);
        }

        private void DownloadCompilationCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadCompilationCheck.Checked)
                settings.SetDownloadCompilation(true);
            else
                settings.SetDownloadCompilation(false);
        }

        private void DownloadSoundtrackCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadSoundtrackCheck.Checked)
                settings.SetDownloadSoundtrack(true);
            else
                settings.SetDownloadSoundtrack(false);
        }

        private void DownloadBootlegCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadBootlegCheck.Checked)
                settings.SetDownloadBootleg(true);
            else
                settings.SetDownloadBootleg(false);
        }

        private void DownloadInterviewCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadInterviewCheck.Checked)
                settings.SetDownloadInterview(true);
            else
                settings.SetDownloadInterview(false);
        }

        private void DownloadMixtapeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadMixtapeCheck.Checked)
                settings.SetDownloadMixtape(true);
            else
                settings.SetDownloadMixtape(false);
        }

        private void DownloadUnknownCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadUnknownCheck.Checked)
                settings.SetDownloadUnknown(true);
            else
                settings.SetDownloadUnknown(false);
        }

        private void TrackTorrentsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackTorrentsCheck.Checked)
                settings.SetTrackTorrents(true);
            else
                settings.SetTrackTorrents(false);
        }

        private void TrackZipsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackZipsCheck.Checked)
                settings.SetTrackZips(true);
            else
                settings.SetTrackZips(false);
        }


    }
}
