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
    partial class OptionsWindow : Form
    {
        Thread tw_thread;

        public OptionsWindow()
        {
            InitializeComponent();
            ErrorProvider.DataSource = CustomFolderTextbox;
            RestoreSelections();   
        }
        private void RestoreSelections()
        {
            ReleaseAlbumTextBox.Text = SettingsHandler.GetReleaseFormat("Album");
            ReleaseBootlegTextbox.Text = SettingsHandler.GetReleaseFormat("Bootleg");
            ReleaseCompilationTextBox.Text = SettingsHandler.GetReleaseFormat("Compilation");
            ReleaseEPTextBox.Text = SettingsHandler.GetReleaseFormat("EP");
            ReleaseInterviewTextBox.Text = SettingsHandler.GetReleaseFormat("Interview");
            ReleaseLiveTextBox.Text = SettingsHandler.GetReleaseFormat("Live");
            ReleaseMixtapeTextBox.Text = SettingsHandler.GetReleaseFormat("Mixtape");
            ReleaseRemixTextBox.Text = SettingsHandler.GetReleaseFormat("Remix");
            ReleaseSingleTextBox.Text = SettingsHandler.GetReleaseFormat("Single");
            ReleaseSoundboardTextBox.Text = SettingsHandler.GetReleaseFormat("Soundboard");
            ReleaseUnknownTextBox.Text = SettingsHandler.GetReleaseFormat("Unknown");
            Bitrate24bitTextBox.Text = SettingsHandler.GetBitrate("24bitLossless");
            BitrateAPSTextBox.Text = SettingsHandler.GetBitrate("APS");
            BitrateAPXTextBox.Text = SettingsHandler.GetBitrate("APX");
            BitrateCBRTextBox.Text = SettingsHandler.GetBitrate("CBR");
            BitrateLosslessTextBox.Text = SettingsHandler.GetBitrate("Lossless");
            BitrateQ8xTextBox.Text = SettingsHandler.GetBitrate("q8x.");
            BitrateVBRTextBox.Text = SettingsHandler.GetBitrate("VBR");
            MusicFolderTextbox.Text = SettingsHandler.GetDownloadFolder();
            CustomFolderTextbox.Text = SettingsHandler.GetCustomDirectory();
            TorrentFolderTextbox.Text = SettingsHandler.GetTorrentSaveFolder();
            TorrentProgramDirectoryTextbox.Text = SettingsHandler.GetTorrentClientFolder();
            AutoCheckTime.Value = SettingsHandler.GetRawHandleTime();


            if (SettingsHandler.GetDeleteThe())
                TheArtistOptions.Text = "Flip (Artist, The)";
            else
                TheArtistOptions.Text = "Ignore";

            if (SettingsHandler.GetHandleLoneTAsAlbum())
                TheArtistOptions.Text = "Remove (Artist)";
            else
                TheArtistOptions.Text = "Ignore";

            if (SettingsHandler.GetMinimizeToTray())
                MinimizeToTrayCheckbox.Checked = true;
            else
                MinimizeToTrayCheckbox.Checked = false;
            if (SettingsHandler.GetDoubleSpaceRemoval())
                DoubleSpaceRemoverCheckBox.Checked = true;
            else
                DoubleSpaceRemoverCheckBox.Checked = false;

            if (SettingsHandler.GetAutoHandleBool())
                AutoProcessCheckbox.Checked = true;
            else
            {
                AutoProcessCheckbox.Checked = false;
                AutoCheckTime.Enabled = false;
            }

            if (SettingsHandler.GetDownloadFormatExists("Album"))
                DownloadAlbumCheck.Checked = true;
            else
                DownloadAlbumCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Bootleg"))
                DownloadBootlegCheck.Checked = true;
            else
                DownloadBootlegCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Compilation"))
                DownloadCompilationCheck.Checked = true;
            else
                DownloadCompilationCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("EP"))
                DownloadEPCheck.Checked = true;
            else
                DownloadEPCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Interview"))
                DownloadInterviewCheck.Checked = true;
            else
                DownloadInterviewCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Live"))
                DownloadLiveCheck.Checked = true;
            else
                DownloadLiveCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Mixtape"))
                DownloadMixtapeCheck.Checked = true;
            else
                DownloadMixtapeCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Remix"))
                DownloadRemixCheck.Checked = true;
            else
                DownloadRemixCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Single"))
                DownloadSingleCheck.Checked = true;
            else
                DownloadSingleCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Single"))
                DownloadSoundtrackCheck.Checked = true;
            else
                DownloadSoundtrackCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Soundtrack"))
                DownloadSoundtrackCheck.Checked = true;
            else
                DownloadSoundtrackCheck.Checked = false;

            if (SettingsHandler.GetDownloadFormatExists("Unknown"))
                DownloadUnknownCheck.Checked = true;
            else
                DownloadUnknownCheck.Checked = false;

            if (SettingsHandler.GetUppercaseAllFolderNames())
                TextCaseOptions.Text = "Uppercase (CASE)";
            else
                TextCaseOptions.Text = "Ignore";

            if (SettingsHandler.GetLowercaseAllFolderNames())
                TextCaseOptions.Text = "Lowercase (case)";
            else
                TextCaseOptions.Text = "Ignore";

            if (SettingsHandler.GetTrackTorrents())
                TrackTorrentsCheck.Checked = true;
            else
                TrackTorrentsCheck.Checked = false;

            if (SettingsHandler.GetTrackZips())
                TrackZipsCheck.Checked = true;
            else
                TrackZipsCheck.Checked = false;
        }

        private void DoubleSpaceRemoverCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DoubleSpaceRemoverCheckBox.Checked)
                SettingsHandler.SetRemoveDoubleSpaces(true);
            else
                SettingsHandler.SetRemoveDoubleSpaces(false);
        }

        private void TorrentProgramDirectoryTextbox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetTorrentClientFolder(TorrentProgramDirectoryTextbox.Text);
        }

        private void TorrentProgramDirectoryTextbox_MouseDown(object sender, MouseEventArgs e)
        {
            FolderBrowser.ShowDialog();
            SettingsHandler.SetTorrentClientFolder(FolderBrowser.SelectedPath);
            TorrentProgramDirectoryTextbox.Text = FolderBrowser.SelectedPath;
        }

        private void MusicFolderTextbox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetDownloadFolder(MusicFolderTextbox.Text);
        }

        private void MusicFolderTextbox_MouseDown(object sender, MouseEventArgs e)
        {
            FolderBrowser.ShowDialog();
            SettingsHandler.SetDownloadFolder(FolderBrowser.SelectedPath);
            MusicFolderTextbox.Text = FolderBrowser.SelectedPath;
        }

        private void TorrentFolderTextbox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetTorrentSaveFolder(TorrentFolderTextbox.Text);
        }

        private void TorrentFolderTextbox_MouseDown(object sender, MouseEventArgs e)
        {
            FolderBrowser.ShowDialog();
            SettingsHandler.SetTorrentSaveFolder(FolderBrowser.SelectedPath);
            TorrentFolderTextbox.Text = FolderBrowser.SelectedPath;
        }

        private void CustomFolderTextbox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetCustomDirectory(CustomFolderTextbox.Text);
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
                SettingsHandler.SetAutoHandleBool(true);
            }
            else
            {
                AutoCheckTime.Enabled = false;
                SettingsHandler.SetAutoHandleBool(false);
            }

            MainWindow.UpdateTimer(SettingsHandler.GetAutoHandleBool(), SettingsHandler.GetAutoHandleTime());
        }

        public void StopTorrentWatch()
        {
            tw_thread.Abort();
        }

        private void DownloadAlbumCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadAlbumCheck.Checked)
                SettingsHandler.AddDownloadFormat("Album");
            else
                SettingsHandler.RemoveDownloadFormat("Album");
        }

        private void DownloadEPCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadEPCheck.Checked)
                SettingsHandler.AddDownloadFormat("EP");
            else
                SettingsHandler.RemoveDownloadFormat("EP");
        }

        private void DownloadSingleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadSingleCheck.Checked)
                SettingsHandler.AddDownloadFormat("Single");
            else
                SettingsHandler.RemoveDownloadFormat("Single");
        }

        private void DownloadLiveCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadLiveCheck.Checked)
                SettingsHandler.AddDownloadFormat("Live");
            else
                SettingsHandler.RemoveDownloadFormat("Live");
        }

        private void DownloadRemixCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadRemixCheck.Checked)
                SettingsHandler.AddDownloadFormat("Remix");
            else
                SettingsHandler.RemoveDownloadFormat("Remix");
        }

        private void DownloadCompilationCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadCompilationCheck.Checked)
                SettingsHandler.AddDownloadFormat("Compilation");
            else
                SettingsHandler.RemoveDownloadFormat("Compilation");
        }

        private void DownloadSoundtrackCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadSoundtrackCheck.Checked)
                SettingsHandler.AddDownloadFormat("Soundtrack");
            else
                SettingsHandler.RemoveDownloadFormat("Soundtrack");
        }

        private void DownloadBootlegCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadBootlegCheck.Checked)
                SettingsHandler.AddDownloadFormat("Bootleg");
            else
                SettingsHandler.RemoveDownloadFormat("Bootleg");
        }

        private void DownloadInterviewCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadInterviewCheck.Checked)
                SettingsHandler.AddDownloadFormat("Interview");
            else
                SettingsHandler.RemoveDownloadFormat("Interview");
        }

        private void DownloadMixtapeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadMixtapeCheck.Checked)
                SettingsHandler.AddDownloadFormat("Mixtape");
            else
                SettingsHandler.RemoveDownloadFormat("Mixtape");
        }

        private void DownloadUnknownCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownloadUnknownCheck.Checked)
                SettingsHandler.AddDownloadFormat("Unknown");
            else
                SettingsHandler.RemoveDownloadFormat("Unknown");
        }

        private void TrackTorrentsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackTorrentsCheck.Checked)
                SettingsHandler.SetTrackTorrents(true);
            else
                SettingsHandler.SetTrackTorrents(false);
        }

        private void TrackZipsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackZipsCheck.Checked)
                SettingsHandler.SetTrackZips(true);
            else
                SettingsHandler.SetTrackZips(false);
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

        private void AutoCheckTime_ValueChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetRawHandleTime(AutoCheckTime.Value);

            switch (TimeFormatComboBox.Text)
            {
                case "Seconds":
                    SettingsHandler.SetAutoHandleTime(AutoCheckTime.Value * 1000);
                    break;
                case "Minutes":
                    SettingsHandler.SetAutoHandleTime(AutoCheckTime.Value * 1000 * 60);
                    break;
                case "Hours":
                    SettingsHandler.SetAutoHandleTime(AutoCheckTime.Value * 1000 * 60 * 60);
                    break;
                default:
                    SettingsHandler.SetAutoHandleTime(AutoCheckTime.Value * 1000);
                    break;
            }

            MainWindow.UpdateTimer(SettingsHandler.GetAutoHandleBool(), SettingsHandler.GetAutoHandleTime());
        }

        private void ReleaseAlbumTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Album", ReleaseAlbumTextBox.Text);
        }

        private void ReleaseEPTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("EP", ReleaseEPTextBox.Text);
        }

        private void ReleaseLiveTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Live", ReleaseLiveTextBox.Text);
        }

        private void ReleaseSingleTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Single", ReleaseSingleTextBox.Text);
        }

        private void ReleaseMixtapeTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Mixtape", ReleaseMixtapeTextBox.Text);
        }

        private void ReleaseRemixTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Remix", ReleaseRemixTextBox.Text);
        }

        private void ReleaseSoundboardTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Soundboard", ReleaseSoundboardTextBox.Text);
        }

        private void ReleaseCompilationTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Compilation", ReleaseCompilationTextBox.Text);
        }

        private void ReleaseBootlegTextbox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Bootleg", ReleaseBootlegTextbox.Text);
        }

        private void ReleaseInterviewTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Interview", ReleaseInterviewTextBox.Text);
        }

        private void ReleaseUnknownTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetReleaseFormatName("Unknown", ReleaseUnknownTextBox.Text);
        }

        private void BitrateVBRTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetBitrateName("VBR", BitrateVBRTextBox.Text);
        }

        private void BitrateCBRTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetBitrateName("CBR", BitrateCBRTextBox.Text);
        }

        private void BitrateAPXTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetBitrateName("APX", BitrateAPXTextBox.Text);
        }

        private void BitrateAPSTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetBitrateName("APS", BitrateAPSTextBox.Text);
        }

        private void BitrateQ8xTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetBitrateName("q8.x", BitrateQ8xTextBox.Text);
        }

        private void BitrateLosslessTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetBitrateName("Lossless", BitrateLosslessTextBox.Text);
        }

        private void Bitrate24bitTextBox_TextChanged(object sender, EventArgs e)
        {
            SettingsHandler.SetBitrateName("24bitLossless", Bitrate24bitTextBox.Text);
        }

        private void MinimizeToTrayCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (MinimizeToTrayCheckbox.Checked)
                SettingsHandler.SetMinimizeToTray(true);
            else
                SettingsHandler.SetMinimizeToTray(false);
        }

        private void LoneTorrentAsAlbumCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (LoneTorrentAsAlbumCheck.Checked)
                SettingsHandler.SetHandleLoneTsAsAlbums(true);
            else
                SettingsHandler.SetHandleLoneTsAsAlbums(false);
        }

        private void TimeFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TimeFormatComboBox.Text)
            {
                case "Seconds":
                    if (AutoCheckTime.Value > 10)
                        SettingsHandler.SetAutoHandleTime(AutoCheckTime.Value * 1000);
                    else
                        SettingsHandler.SetAutoHandleTime(10 * 1000);
                    break;
                case "Minutes":
                    SettingsHandler.SetAutoHandleTime(AutoCheckTime.Value * 1000 * 60);
                    break;
                case "Hours":
                    SettingsHandler.SetAutoHandleTime(AutoCheckTime.Value * 1000 * 60 * 60);
                    break;
                default:
                    SettingsHandler.SetAutoHandleTime(AutoCheckTime.Value * 1000);
                    break;
            }

            MainWindow.UpdateTimer(SettingsHandler.GetAutoHandleBool(), SettingsHandler.GetAutoHandleTime());
        }

        private void TheArtistOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TheArtistOptions.Text)
            {

                case "Flip (Artist, The)":
                    SettingsHandler.SetArtistFlip(true);
                    SettingsHandler.SetDeleteTheFolderNames(false);
                    break;
                case "Remove (Artist)":
                    SettingsHandler.SetArtistFlip(false);
                    SettingsHandler.SetDeleteTheFolderNames(true);
                    break;
                default:
                    SettingsHandler.SetArtistFlip(false);
                    SettingsHandler.SetDeleteTheFolderNames(false);
                    break;
            }
        }

        private void TextCaseOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TextCaseOptions.Text)
            {
                case "Uppercase (CASE)":
                    SettingsHandler.SetLowercaseAllFolderNames(false);
                    SettingsHandler.SetUppercaseAllFolderNames(true);
                    break;
                case "Lowercase (case)":
                    SettingsHandler.SetLowercaseAllFolderNames(true);
                    SettingsHandler.SetUppercaseAllFolderNames(false);
                    break;
                default:
                    SettingsHandler.SetLowercaseAllFolderNames(false);
                    SettingsHandler.SetUppercaseAllFolderNames(false);
                    break;
            }
        }
    }
}
