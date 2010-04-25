using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlServerCe;

namespace CSL_Test__1
{
    public partial class ErrorWindow : Form
    {
        bool discard = false;
        static string applyToAll = "";
        DirectoryHandler dh = new DirectoryHandler();

        public ErrorWindow()
        {
            InitializeComponent();
        }

        public void ClearApplyToAll()
        {
            applyToAll = "";
        }

        private void OkButton_MouseHover(object sender, EventArgs e)
        {
            OkButton.BackColor = Color.Aquamarine;
        }
        private void OkButton_MouseLeave(object sender, EventArgs e)
        {
            OkButton.BackColor = Control.DefaultBackColor;
        }

        private void ErrorWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Keys.Escape))
            {
                DiscardButton.PerformClick();
            }
        }

        public string IssueYearError(string file)
        {
            if (applyToAll.Contains("CSL::YEAR|"))
            {
                int s_index = applyToAll.IndexOf("CSL::YEAR|") + 10;
                int e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            InstructionalLabel.Text = "Please provide a year below";
            ErrorLabel.Text = "CSL was unable to parse out a year";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::YEAR|" + SelectionTextBox.Text + "***";

                return SelectionTextBox.Text;
            }
        }
        public string IssueArtistError(string file)
        {
            if (applyToAll.Contains("CSL::ARTIST|"))
            {
                int s_index = applyToAll.IndexOf("CSL::ARTIST|") + 12;
                int e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            InstructionalLabel.Text = "Please provide an artist below";
            ErrorLabel.Text = "CSL was unable to parse out an artist";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;
            
            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::ARTIST|" + SelectionTextBox.Text + "***";

                return SelectionTextBox.Text;
            }
        }
        public string IssueAlbumError(string file)
        {
            if (applyToAll.Contains("CSL::ALBUM|"))
            {
                int s_index = applyToAll.IndexOf("CSL::ALBUM|") + 11;
                int e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out an album";
            InstructionalLabel.Text = "Please provide an album below";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;
            
            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::ALBUM|" + SelectionTextBox.Text + "***";

                return SelectionTextBox.Text;
            }
        }
        public string IssueReleaseFormatError(string file)
        {
            if (applyToAll.Contains("CSL::RELEASEF|"))
            {
                int s_index = applyToAll.IndexOf("CSL::RELEASEF|") + 14;
                int e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out a release format";
            InstructionalLabel.Text = "Please select a release format below";

            ReleaseSelectionComboBox.Show();
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;
            
            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::RELEASEF|" + ReleaseSelectionComboBox.Text + "***";

                return ReleaseSelectionComboBox.Text;
            }

        }
        public string IssuePhysicalFormatError(string file)
        {
            if (applyToAll.Contains("CSL::PHYSICALF|"))
            {
                int s_index = applyToAll.IndexOf("CSL::PHYSICALF|") + 15;
                int e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out a physical format";
            InstructionalLabel.Text = "Please select a physical format below";

            PhysicalFormatSelectionComboBox.Show();
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::PHYSICALF|" + PhysicalFormatSelectionComboBox.Text + "***";

                return PhysicalFormatSelectionComboBox.Text;
            }
        }
        public string IssueBitformatError(string file)
        {
            if (applyToAll.Contains("CSL::BITF|"))
            {
                int s_index = applyToAll.IndexOf("CSL::BITF|") + 10;
                int e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out a bit format";
            InstructionalLabel.Text = "Please select a bit format below";

            BitformatSelectionComboBox.Show();
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::BITF|" + BitformatSelectionComboBox.Text + "***";

                return BitformatSelectionComboBox.Text;
            }
        }
        public string IssueBitrateError(string file)
        {
            if (applyToAll.Contains("CSL::BITR|"))
            {
                int s_index = applyToAll.IndexOf("CSL::BITR|") + 10;
                int e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out a bitrate";
            InstructionalLabel.Text = "Please select a bitrate below";

            BitrateSelectionComboBox.Show();
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::BITR|" + BitrateSelectionComboBox.Text + "***";

                return BitrateSelectionComboBox.Text;
            }
        }
        public string IssueBirthError(string file)
        {
            if (applyToAll.Contains("CSL::BIRTH|"))
            {
                int s_index = applyToAll.IndexOf("CSL::BIRTH|") + 11;
                int e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL doesn't know which site this torrent came from";
            InstructionalLabel.Text = "Please select a torrent site below";
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            BirthSelectionComboBox.Show();
            this.Activate();
            this.ShowDialog();

           if (discard)
                return null;
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::BIRTH|" + BirthSelectionComboBox.Text + "***";

                return BirthSelectionComboBox.Text;
            }
        }
        public string IssueIllegalCharactersError(string file, string destPath)
        {
            ErrorLabel.Text = "There's illegal characters in destination path: \"" + destPath + "\"";
            InstructionalLabel.Text = "Please correct the path below";

            SelectionTextBox.Show();
            SelectionTextBox.Text = destPath;
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }
        public string IssueArtistWarning(string file, string artist)
        {
            InstructionalLabel.Text = "Validate and change if necessary";
            ErrorLabel.Text = "MusicBrainz doesn't recognize this artist: \"" + artist + "\"";

            SelectionTextBox.Show();
            SelectionTextBox.Text = artist;
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
                return SelectionTextBox.Text;
        }
        public string IssueAlbumWarning(string file, string album)
        {
            InstructionalLabel.Text = "Validate and change if necessary";
            ErrorLabel.Text = "MusicBrainz doesn't recognize this album: \"" + album + "\"";

            SelectionTextBox.Show();
            SelectionTextBox.Text = album;
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
                return SelectionTextBox.Text;
        }
        public string IssueYearWarning(string file, string year)
        {
            InstructionalLabel.Text = "Please provide the correct year";
            ErrorLabel.Text = "This year is unrealistic: \"" + year + "\"";

            SelectionTextBox.Show();
            SelectionTextBox.Text = year;
            FileNameLabel.Text = dh.GetFileName(file, true);
            FilePathRichTextBox.Text = file;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            if (discard)
                return null;
            else
                return SelectionTextBox.Text;
            
        }
        public void IssueFileMoveWarning(string file, bool handled)
        {
            InstructionalLabel.Text = "The file could not be moved";
                ErrorLabel.Text = "Please manually delete or move it";


            FilePathRichTextBox.Text = file;
            FileNameLabel.Text = dh.GetFileName(file, true);

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();
        }
        public void IssueGeneralWarning(string message, string submessage, string file)
        {
            InstructionalLabel.Text = message;
            ErrorLabel.Text = submessage;
            if (file != null)
            {
                FilePathRichTextBox.Text = file;
                FileNameLabel.Text = dh.GetFileName(file, true);
            }
            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            discard = true;
            this.Hide();
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SelectionTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
                OkButton.PerformClick();
            else
                OkButton.Enabled = true;
        }
    }
}
