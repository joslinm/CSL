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

namespace CSL
{
    public partial class ErrorWindow : Form
    {
        static DirectoryHandler DirectoryHandler = new DirectoryHandler(); //Prevent infinite loop between DirectoryHandler & ErrorWindow

        int s_index;
        int e_index;
        bool okButtonSelected;
        bool discard = false;
        static string applyToAll = "";
        
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

        public string IssueYearError(FileInfo file)
        {
            if (applyToAll.Contains("CSL::YEAR|"))
            {
                s_index = applyToAll.IndexOf("CSL::YEAR|") + 10;
                e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            InstructionalLabel.Text = "Please provide a year below";
            ErrorLabel.Text = "CSL was unable to parse out a year";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::YEAR|" + SelectionTextBox.Text + "***";

                ApplyToAllCheck.Checked = false;
                return SelectionTextBox.Text;
            }
        }
        public string IssueArtistError(FileInfo file)
        {
            if (applyToAll.Contains("CSL::ARTIST|"))
            {
                s_index = applyToAll.IndexOf("CSL::ARTIST|") + 12;
                e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            InstructionalLabel.Text = "Please provide an artist below";
            ErrorLabel.Text = "CSL was unable to parse out an artist";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;
            
            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::ARTIST|" + SelectionTextBox.Text + "***";

                ApplyToAllCheck.Checked = false;
                return SelectionTextBox.Text;
            }
        }
        public string IssueAlbumError(FileInfo file)
        {
            if (applyToAll.Contains("CSL::ALBUM|"))
            {
                  s_index = applyToAll.IndexOf("CSL::ALBUM|") + 11;
                  e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out an album";
            InstructionalLabel.Text = "Please provide an album below";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;
            
            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::ALBUM|" + SelectionTextBox.Text + "***";

                ApplyToAllCheck.Checked = false;
                return SelectionTextBox.Text;
            }
        }
        public string IssueReleaseFormatError(FileInfo file)
        {
            if (applyToAll.Contains("CSL::RELEASEF|"))
            {
                  s_index = applyToAll.IndexOf("CSL::RELEASEF|") + 14;
                  e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out a release format";
            InstructionalLabel.Text = "Please select a release format below";

            ReleaseSelectionComboBox.Show();
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;
            
            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::RELEASEF|" + ReleaseSelectionComboBox.Text + "***";

                ApplyToAllCheck.Checked = false;
                return ReleaseSelectionComboBox.Text;
            }

        }
        public string IssuePhysicalFormatError(FileInfo file)
        {
            if (applyToAll.Contains("CSL::PHYSICALF|"))
            {
                  s_index = applyToAll.IndexOf("CSL::PHYSICALF|") + 15;
                  e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out a physical format";
            InstructionalLabel.Text = "Please select a physical format below";

            PhysicalFormatSelectionComboBox.Show();
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::PHYSICALF|" + PhysicalFormatSelectionComboBox.Text + "***";

                ApplyToAllCheck.Checked = false;
                return PhysicalFormatSelectionComboBox.Text;
            }
        }
        public string IssueBitformatError(FileInfo file)
        {
            if (applyToAll.Contains("CSL::BITF|"))
            {
                  s_index = applyToAll.IndexOf("CSL::BITF|") + 10;
                  e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out a bit format";
            InstructionalLabel.Text = "Please select a bit format below";

            BitformatSelectionComboBox.Show();
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::BITF|" + BitformatSelectionComboBox.Text + "***";

                ApplyToAllCheck.Checked = false;
                return BitformatSelectionComboBox.Text;
            }
        }
        public string IssueBitrateError(FileInfo file)
        {
            if (applyToAll.Contains("CSL::BITR|"))
            {
                  s_index = applyToAll.IndexOf("CSL::BITR|") + 10;
                  e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL was unable to parse out a bitrate";
            InstructionalLabel.Text = "Please select a bitrate below";

            BitrateSelectionComboBox.Show();
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::BITR|" + BitrateSelectionComboBox.Text + "***";

                return BitrateSelectionComboBox.Text;
            }
        }
        public string IssueBirthError(FileInfo file)
        {
            if (applyToAll.Contains("CSL::BIRTH|"))
            {
                  s_index = applyToAll.IndexOf("CSL::BIRTH|") + 11;
                  e_index = applyToAll.IndexOf("***", s_index);
                return applyToAll.Substring(s_index, e_index - s_index);
            }

            ErrorLabel.Text = "CSL doesn't know which site this torrent came from";
            InstructionalLabel.Text = "Please select a torrent site below";
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            BirthSelectionComboBox.Show();
            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
            {
                if (ApplyToAllCheck.Checked)
                    applyToAll += "CSL::BIRTH|" + BirthSelectionComboBox.Text + "***";

                return BirthSelectionComboBox.Text;
            }
        }
        public string IssueIllegalCharactersError(FileInfo file, string destPath)
        {
            ErrorLabel.Text = "There's illegal characters in destination path: \"" + destPath + "\"";
            InstructionalLabel.Text = "Please correct the path below";

            SelectionTextBox.Show();
            SelectionTextBox.Text = destPath;
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }
        public string IssueArtistWarning(FileInfo file, string artist)
        {
            InstructionalLabel.Text = "Validate and change if necessary";
            ErrorLabel.Text = "MusicBrainz doesn't recognize this artist: \"" + artist + "\"";

            SelectionTextBox.Show();
            SelectionTextBox.Text = artist;
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
                return SelectionTextBox.Text;
        }
        public string IssueAlbumWarning(FileInfo file, string album)
        {
            InstructionalLabel.Text = "Validate and change if necessary";
            ErrorLabel.Text = "MusicBrainz doesn't recognize this album: \"" + album + "\"";

            SelectionTextBox.Show();
            SelectionTextBox.Text = album;
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
                return SelectionTextBox.Text;
        }
        public string IssueYearWarning(FileInfo file, string year)
        {
            InstructionalLabel.Text = "Please provide the correct year";
            ErrorLabel.Text = "This year is unrealistic: \"" + year + "\"";

            SelectionTextBox.Show();
            SelectionTextBox.Text = year;
            FileNameLabel.Text = file.Name;
            FilePathRichTextBox.Text = file.FullName;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            if (discard)
            {
                discard = false; //To ensure future Issues aren't discarded
                return null;
            }
            else
                return SelectionTextBox.Text;
            
        }
        public void IssueFileMoveWarning(FileInfo file, bool handled)
        {
            InstructionalLabel.Text = "The file could not be moved";
                ErrorLabel.Text = "Please manually delete or move it";

            FilePathRichTextBox.Text = file.FullName;
            FileNameLabel.Text = file.Name;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();
        }
        public void IssueGeneralWarning(string message, string submessage, string file)
        {
            InstructionalLabel.Text = message;
            ErrorLabel.Text = submessage;
            DiscardButton.Visible = false;
            if (file != null)
            {
                FilePathRichTextBox.Text = file;
            }
            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();
        }

        public bool IssueCreateEmptyDirectoriesWarning(string message, string submessage)
        {
            InstructionalLabel.Text = message;
            ErrorLabel.Text = submessage;
            DiscardButton.Visible = false;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            return this.okButtonSelected;
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            discard = true;
            //Reset all to hidden -- the issues choose which one to show
            SelectionTextBox.Visible = false;
            ReleaseSelectionComboBox.Visible = false;
            BitformatSelectionComboBox.Visible = false;
            BitrateSelectionComboBox.Visible = false;
            BirthSelectionComboBox.Visible = false;
            PhysicalFormatSelectionComboBox.Visible = false;

            this.Hide();
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            DiscardButton.Visible = true;
            //Reset all to hidden -- the issues choose which one to show
            SelectionTextBox.Visible = false;
            ReleaseSelectionComboBox.Visible = false;
            BitformatSelectionComboBox.Visible = false;
            BitrateSelectionComboBox.Visible = false;
            BirthSelectionComboBox.Visible = false;
            PhysicalFormatSelectionComboBox.Visible = false;
            this.okButtonSelected = true;

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
