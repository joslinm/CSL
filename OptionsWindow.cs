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
            ReleaseAlbumTextBox.Text = settings.GetReleaseFormat("Album");
            ReleaseBootlegTextbox.Text = settings.GetReleaseFormat("Bootleg");
            ReleaseCompilationTextBox.Text = settings.GetReleaseFormat("Compilation");
            ReleaseEPTextBox.Text = settings.GetReleaseFormat("EP");
            ReleaseInterviewTextBox.Text = settings.GetReleaseFormat("Interview");
            ReleaseLiveTextBox.Text = settings.GetReleaseFormat("Live");
            ReleaseMixtapeTextBox.Text = settings.GetReleaseFormat("Mixtape");
            ReleaseRemixTextBox.Text = settings.GetReleaseFormat("Remix");
            ReleaseSingleTextBox.Text = settings.GetReleaseFormat("Single");
            ReleaseSoundboardTextBox.Text = settings.GetReleaseFormat("Soundboard");
            ReleaseUnknownTextBox.Text = settings.GetReleaseFormat("Unknown");
            Bitrate24bitTextBox.Text = settings.GetBitrate("24bitLossless");
            BitrateAPSTextBox.Text = settings.GetBitrate("APS");
            BitrateAPXTextBox.Text = settings.GetBitrate("APX");
            BitrateCBRTextBox.Text = settings.GetBitrate("CBR");
            BitrateLosslessTextBox.Text = settings.GetBitrate("Lossless");
            BitrateQ8xTextBox.Text = settings.GetBitrate("q8x.");
            BitrateVBRTextBox.Text = settings.GetBitrate("VBR");
            MusicFolderTextbox.Text = settings.GetDownloadFolder();
            CustomFolderTextbox.Text = settings.GetCustomDirectory();
            TorrentFolderTextbox.Text = settings.GetTorrentSaveFolder();
            TorrentProgramDirectoryTextbox.Text = settings.GetTorrentClientFolder();
            AutoCheckTime.Value = settings.GetAutoHandleTime();

            if (settings.GetMinimizeToTray())
                MinimizeToTrayCheckbox.Checked = true;
            else
                MinimizeToTrayCheckbox.Checked = false;
            if (settings.GetDoubleSpaceRemoval())
                DoubleSpaceRemoverCheckBox.Checked = true;
            else
                DoubleSpaceRemoverCheckBox.Checked = false;

            if (settings.GetArtistFlip())
                ArtistFlipCheck.Checked = true;
            else
                ArtistFlipCheck.Checked = false;

            if (settings.GetAutoHandleBool())
                AutoProcessCheckbox.Checked = true;
            else
            {
                AutoProcessCheckbox.Checked = false;
                AutoCheckTime.Enabled = false;
            }

            if (settings.GetDownloadFormatExists("Album"))
                DownloadAlbumCheck.Checked = true;
            else
                DownloadAlbumCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Bootleg"))
                DownloadBootlegCheck.Checked = true;
            else
                DownloadBootlegCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Compilation"))
                DownloadCompilationCheck.Checked = true;
            else
                DownloadCompilationCheck.Checked = false;

            if (settings.GetDownloadFormatExists("EP"))
                DownloadEPCheck.Checked = true;
            else
                DownloadEPCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Interview"))
                DownloadInterviewCheck.Checked = true;
            else
                DownloadInterviewCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Live"))
                DownloadLiveCheck.Checked = true;
            else
                DownloadLiveCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Mixtape"))
                DownloadMixtapeCheck.Checked = true;
            else
                DownloadMixtapeCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Remix"))
                DownloadRemixCheck.Checked = true;
            else
                DownloadRemixCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Single"))
                DownloadSingleCheck.Checked = true;
            else
                DownloadSingleCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Single"))
                DownloadSoundtrackCheck.Checked = true;
            else
                DownloadSoundtrackCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Soundtrack"))
                DownloadSoundtrackCheck.Checked = true;
            else
                DownloadSoundtrackCheck.Checked = false;

            if (settings.GetDownloadFormatExists("Unknown"))
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

                for (int a = 0; a < chararray.Length; a++)
                {
                    if (chararray[a] == '%')
                    {
                        try
                        {
                            letter = chararray[++a].ToString();

                            m = Regex.Match(letter, "a|l|s|c|e|r|v|n|x|u|y|t|i|b|p|d|z");
                            if (!m.Success)
                            {
                                ErrorProvider.SetError(this.CustomFolderTextbox, "Invalid letter after switch");
                                error = true;
                            }
                        }
                        catch { }
                    }
                }
         
            if (!error)
                ErrorProvider.Clear();
        }

        private void AutoProcessCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoProcessCheckbox.Checked)
            {
                AutoCheckTime.Enabled = true;
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
                AutoCheckTime.Enabled = false;
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
                settings.AddDownloadFormat("Album");
            else
                settings.RemoveDownloadFormat("Album");
        }

        private void DownloadEPCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadEPCheck.Checked)
                settings.AddDownloadFormat("EP");
            else
                settings.RemoveDownloadFormat("EP");
        }

        private void DownloadSingleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadSingleCheck.Checked)
                settings.AddDownloadFormat("Single");
            else
                settings.RemoveDownloadFormat("Single");
        }

        private void DownloadLiveCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadLiveCheck.Checked)
                settings.AddDownloadFormat("Live");
            else
                settings.RemoveDownloadFormat("Live");
        }

        private void DownloadRemixCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadRemixCheck.Checked)
                settings.AddDownloadFormat("Remix");
            else
                settings.RemoveDownloadFormat("Remix");
        }

        private void DownloadCompilationCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadCompilationCheck.Checked)
                settings.AddDownloadFormat("Compilation");
            else
                settings.RemoveDownloadFormat("Compilation");
        }

        private void DownloadSoundtrackCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadSoundtrackCheck.Checked)
                settings.AddDownloadFormat("Soundtrack");
            else
                settings.RemoveDownloadFormat("Soundtrack");
        }

        private void DownloadBootlegCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadBootlegCheck.Checked)
                settings.AddDownloadFormat("Bootleg");
            else
                settings.RemoveDownloadFormat("Bootleg");
        }

        private void DownloadInterviewCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadInterviewCheck.Checked)
                settings.AddDownloadFormat("Interview");
            else
                settings.RemoveDownloadFormat("Interview");
        }

        private void DownloadMixtapeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadMixtapeCheck.Checked)
                settings.AddDownloadFormat("Mixtape");
            else
                settings.RemoveDownloadFormat("Mixtape");
        }

        private void DownloadUnknownCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadUnknownCheck.Checked)
                settings.AddDownloadFormat("Unknown");
            else
                settings.RemoveDownloadFormat("Unknown");
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SwitchesWindow sw = new SwitchesWindow();
            sw.Show();
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void OptionsWindow_Load(object sender, EventArgs e)
        {

        }

        private void LowercaseAllFolderNamesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LowercaseAllFolderNamesCheckBox.Checked)
                settings.SetLowercaseAllFolderNames(true);
            else
                settings.SetLowercaseAllFolderNames(false);
        }

        private void UppercaseFolderNamesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UppercaseFolderNamesCheckBox.Checked)
                settings.SetUppercaseAllFolderNames(true);
            else
                settings.SetUppercaseAllFolderNames(false);
        }

        private void AutoCheckTime_ValueChanged(object sender, EventArgs e)
        {
            settings.SetAutoHandleTime(AutoCheckTime.Value);
        }

        private void ReleaseAlbumTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Album", ReleaseAlbumTextBox.Text);
        }

        private void ReleaseEPTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("EP", ReleaseEPTextBox.Text);
        }

        private void ReleaseLiveTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Live", ReleaseLiveTextBox.Text);
        }

        private void ReleaseSingleTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Single", ReleaseSingleTextBox.Text);
        }

        private void ReleaseMixtapeTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Mixtape", ReleaseMixtapeTextBox.Text);
        }

        private void ReleaseRemixTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Remix", ReleaseRemixTextBox.Text);
        }

        private void ReleaseSoundboardTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Soundboard", ReleaseSoundboardTextBox.Text);
        }

        private void ReleaseCompilationTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Compilation", ReleaseCompilationTextBox.Text);
        }

        private void ReleaseBootlegTextbox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Bootleg", ReleaseBootlegTextbox.Text);
        }

        private void ReleaseInterviewTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Interview", ReleaseInterviewTextBox.Text);
        }

        private void ReleaseUnknownTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetReleaseFormatName("Unknown", ReleaseUnknownTextBox.Text);
        }

        private void BitrateVBRTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetBitrateName("VBR", BitrateVBRTextBox.Text);
        }

        private void BitrateCBRTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetBitrateName("CBR", BitrateCBRTextBox.Text);
        }

        private void BitrateAPXTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetBitrateName("APX", BitrateAPXTextBox.Text);
        }

        private void BitrateAPSTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetBitrateName("APS", BitrateAPSTextBox.Text);
        }

        private void BitrateQ8xTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetBitrateName("q8.x", BitrateQ8xTextBox.Text);
        }

        private void BitrateLosslessTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetBitrateName("Lossless", BitrateLosslessTextBox.Text);
        }

        private void Bitrate24bitTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.SetBitrateName("24bitLossless", Bitrate24bitTextBox.Text);
        }

        private void MinimizeToTrayCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (MinimizeToTrayCheckbox.Checked)
                settings.SetMinimizeToTray(true);
            else
                settings.SetMinimizeToTray(false);
        }



    }
}
