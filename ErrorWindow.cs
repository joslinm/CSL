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

        public ErrorWindow()
        {
            InitializeComponent();
        }

        private void OkButton_MouseHover(object sender, EventArgs e)
        {
            OkButton.BackColor = Color.Aquamarine;
        }
        private void OkButton_MouseLeave(object sender, EventArgs e)
        {
            OkButton.BackColor = Control.DefaultBackColor;
        }
        private void SelectionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Keys.Return))
            {
                OkButton.PerformClick();
            }
            else
            {
                OkButton.Enabled = true;
            }
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
            InstructionalLabel.Text = "Please provide a year below";
            ErrorLabel.Text = "CSL was unable to parse out a year";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }
        public string IssueArtistError(string file)
        {
            InstructionalLabel.Text = "Please provide an artist below";
            ErrorLabel.Text = "CSL was unable to parse out an artist";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;
            
            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }
        public string IssueAlbumError(string file)
        {
            ErrorLabel.Text = "CSL was unable to parse out an album";
            InstructionalLabel.Text = "Please provide an album below";

            SelectionTextBox.Show();
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;
            
            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }
        public string IssueReleaseFormatError(string file)
        {
            ErrorLabel.Text = "CSL was unable to parse out a release format";
            InstructionalLabel.Text = "Please select a release format below";

            ReleaseSelectionComboBox.Show();
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;
            
            this.Activate();
            this.ShowDialog();

            return ReleaseSelectionComboBox.Text;
        }
        public string IssuePhysicalFormatError(string file)
        {
            ErrorLabel.Text = "CSL was unable to parse out a physical format";
            InstructionalLabel.Text = "Please select a physical format below";

            PhysicalFormatSelectionComboBox.Show();
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            if (!discard)
                return PhysicalFormatSelectionComboBox.Text;
            else
                return "discard torrent";
        }
        public string IssueBitformatError(string file)
        {
            ErrorLabel.Text = "CSL was unable to parse out a bit format";
            InstructionalLabel.Text = "Please select a bit format below";

            BitformatSelectionComboBox.Show();
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            if (!discard)
                return BitformatSelectionComboBox.Text;
            else
                return "discard torrent";
        }
        public string IssueBitrateError(string file)
        {
            ErrorLabel.Text = "CSL was unable to parse out a bitrate";
            InstructionalLabel.Text = "Please select a bitrate below";

            BitrateSelectionComboBox.Show();
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            if (!discard)
                return BitrateSelectionComboBox.Text;
            else
                return "discard torrent";
        }
        public string IssueBirthError(string file)
        {
            ErrorLabel.Text = "CSL doesn't know which site this torrent came from";
            InstructionalLabel.Text = "Please select a torrent site below";
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            BirthSelectionComboBox.Show();
            this.Activate();
            this.ShowDialog();

            if (!discard)
                return BitrateSelectionComboBox.Text;
            else
                return "discard torrent";
        }
        public string IssueIllegalCharactersError(string file, string destPath)
        {
            ErrorLabel.Text = "There's illegal characters in destination path";
            InstructionalLabel.Text = "Please correct the path below";

            SelectionTextBox.Show();
            SelectionTextBox.Text = destPath;
            OkButton.Enabled = false; //Will enable when key is pressed
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }
        public string IssueArtistWarning(string file, string artist)
        {
            InstructionalLabel.Text = "Validate and change if necessary";
            ErrorLabel.Text = "MusicBrainz doesn't recognize this artist";

            SelectionTextBox.Show();
            SelectionTextBox.Text = artist;
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }
        public string IssueAlbumWarning(string file, string album)
        {
            InstructionalLabel.Text = "Validate and change if necessary";
            ErrorLabel.Text = "MusicBrainz doesn't recognize this album";

            SelectionTextBox.Show();
            SelectionTextBox.Text = album;
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }
        public string IssueYearWarning(string file, string year)
        {
            InstructionalLabel.Text = "Please provide the correct year";
            ErrorLabel.Text = "This year is unrealistic";

            SelectionTextBox.Show();
            SelectionTextBox.Text = year;
            FileNameLabel.Text = Path.GetFileName(file);
            FilePathRichTextBox.Text = file;

            this.Text = "[CSL] -- Warning";
            this.Activate();
            this.ShowDialog();

            return SelectionTextBox.Text;
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            discard = true;
            this.Close();
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
