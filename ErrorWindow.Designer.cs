namespace CSL_Test__1
{
    partial class ErrorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserInputPanel = new System.Windows.Forms.Panel();
            this.BirthSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.DiscardButton = new System.Windows.Forms.Button();
            this.PhysicalFormatSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.BitrateSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.BitformatSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.ReleaseSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.InstructionalLabel = new System.Windows.Forms.Label();
            this.SelectionTextBox = new System.Windows.Forms.TextBox();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.FilePathRichTextBox = new System.Windows.Forms.RichTextBox();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.FileLabelLabel = new System.Windows.Forms.Label();
            this.UserInputPanel.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserInputPanel
            // 
            this.UserInputPanel.BackColor = System.Drawing.SystemColors.Info;
            this.UserInputPanel.Controls.Add(this.BirthSelectionComboBox);
            this.UserInputPanel.Controls.Add(this.DiscardButton);
            this.UserInputPanel.Controls.Add(this.PhysicalFormatSelectionComboBox);
            this.UserInputPanel.Controls.Add(this.OkButton);
            this.UserInputPanel.Controls.Add(this.BitrateSelectionComboBox);
            this.UserInputPanel.Controls.Add(this.BitformatSelectionComboBox);
            this.UserInputPanel.Controls.Add(this.ReleaseSelectionComboBox);
            this.UserInputPanel.Controls.Add(this.InstructionalLabel);
            this.UserInputPanel.Controls.Add(this.SelectionTextBox);
            this.UserInputPanel.Location = new System.Drawing.Point(5, 152);
            this.UserInputPanel.Name = "UserInputPanel";
            this.UserInputPanel.Size = new System.Drawing.Size(582, 108);
            this.UserInputPanel.TabIndex = 0;
            // 
            // BirthSelectionComboBox
            // 
            this.BirthSelectionComboBox.FormattingEnabled = true;
            this.BirthSelectionComboBox.Items.AddRange(new object[] {
            "What",
            "Waffles"});
            this.BirthSelectionComboBox.Location = new System.Drawing.Point(11, 64);
            this.BirthSelectionComboBox.Name = "BirthSelectionComboBox";
            this.BirthSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.BirthSelectionComboBox.TabIndex = 6;
            this.BirthSelectionComboBox.Visible = false;
            // 
            // DiscardButton
            // 
            this.DiscardButton.BackColor = System.Drawing.Color.DarkRed;
            this.DiscardButton.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscardButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.DiscardButton.Location = new System.Drawing.Point(480, 10);
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.Size = new System.Drawing.Size(88, 50);
            this.DiscardButton.TabIndex = 3;
            this.DiscardButton.Text = "Discard Torrent";
            this.DiscardButton.UseVisualStyleBackColor = false;
            this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
            // 
            // PhysicalFormatSelectionComboBox
            // 
            this.PhysicalFormatSelectionComboBox.FormattingEnabled = true;
            this.PhysicalFormatSelectionComboBox.Items.AddRange(new object[] {
            "CD",
            "Vinyl",
            "Cassette",
            "DVD",
            "WEB",
            "SACD",
            "Soundboard",
            "DAT",
            "Other"});
            this.PhysicalFormatSelectionComboBox.Location = new System.Drawing.Point(11, 64);
            this.PhysicalFormatSelectionComboBox.Name = "PhysicalFormatSelectionComboBox";
            this.PhysicalFormatSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.PhysicalFormatSelectionComboBox.TabIndex = 5;
            this.PhysicalFormatSelectionComboBox.Visible = false;
            // 
            // OkButton
            // 
            this.OkButton.BackColor = System.Drawing.SystemColors.Control;
            this.OkButton.Font = new System.Drawing.Font("Courier New", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.Location = new System.Drawing.Point(430, 62);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(138, 37);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.MouseLeave += new System.EventHandler(this.OkButton_MouseLeave);
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            this.OkButton.MouseHover += new System.EventHandler(this.OkButton_MouseHover);
            // 
            // BitrateSelectionComboBox
            // 
            this.BitrateSelectionComboBox.FormattingEnabled = true;
            this.BitrateSelectionComboBox.Items.AddRange(new object[] {
            "V0(VBR)",
            "V2(VBR)",
            "Lossless",
            "192",
            "256",
            "320",
            "V1(VBR)",
            "V4(VBR)",
            "V6(VBR)",
            "V8(VBR)",
            "V10(VBR)",
            "APX(VBR)",
            "APS(VBR)",
            "q8.X(VBR)"});
            this.BitrateSelectionComboBox.Location = new System.Drawing.Point(11, 64);
            this.BitrateSelectionComboBox.Name = "BitrateSelectionComboBox";
            this.BitrateSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.BitrateSelectionComboBox.TabIndex = 4;
            this.BitrateSelectionComboBox.Visible = false;
            // 
            // BitformatSelectionComboBox
            // 
            this.BitformatSelectionComboBox.FormattingEnabled = true;
            this.BitformatSelectionComboBox.Items.AddRange(new object[] {
            "MP3",
            "AAC",
            "FLAC",
            "AC3",
            "DTS",
            "Ogg"});
            this.BitformatSelectionComboBox.Location = new System.Drawing.Point(11, 64);
            this.BitformatSelectionComboBox.Name = "BitformatSelectionComboBox";
            this.BitformatSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.BitformatSelectionComboBox.TabIndex = 3;
            this.BitformatSelectionComboBox.Visible = false;
            // 
            // ReleaseSelectionComboBox
            // 
            this.ReleaseSelectionComboBox.FormattingEnabled = true;
            this.ReleaseSelectionComboBox.Items.AddRange(new object[] {
            "Album",
            "Bootleg",
            "Live Album",
            "Mixtape",
            "EP",
            "Compilation",
            "Interview",
            "Remix",
            "Single",
            "Unknown",
            "Soundtrack"});
            this.ReleaseSelectionComboBox.Location = new System.Drawing.Point(11, 64);
            this.ReleaseSelectionComboBox.Name = "ReleaseSelectionComboBox";
            this.ReleaseSelectionComboBox.Size = new System.Drawing.Size(159, 21);
            this.ReleaseSelectionComboBox.TabIndex = 2;
            this.ReleaseSelectionComboBox.Visible = false;
            // 
            // InstructionalLabel
            // 
            this.InstructionalLabel.AutoSize = true;
            this.InstructionalLabel.Font = new System.Drawing.Font("Courier New", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstructionalLabel.Location = new System.Drawing.Point(7, 10);
            this.InstructionalLabel.Name = "InstructionalLabel";
            this.InstructionalLabel.Size = new System.Drawing.Size(239, 20);
            this.InstructionalLabel.TabIndex = 1;
            this.InstructionalLabel.Text = "Instructional Statement";
            // 
            // SelectionTextBox
            // 
            this.SelectionTextBox.Location = new System.Drawing.Point(11, 65);
            this.SelectionTextBox.Name = "SelectionTextBox";
            this.SelectionTextBox.Size = new System.Drawing.Size(338, 20);
            this.SelectionTextBox.TabIndex = 0;
            this.SelectionTextBox.Visible = false;
            this.SelectionTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectionTextBox_KeyPress);
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.SystemColors.Info;
            this.DescriptionPanel.Controls.Add(this.FilePathRichTextBox);
            this.DescriptionPanel.Controls.Add(this.FileNameLabel);
            this.DescriptionPanel.Controls.Add(this.ErrorLabel);
            this.DescriptionPanel.Controls.Add(this.FileLabelLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(5, 3);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(582, 143);
            this.DescriptionPanel.TabIndex = 1;
            // 
            // FilePathRichTextBox
            // 
            this.FilePathRichTextBox.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePathRichTextBox.Location = new System.Drawing.Point(8, 76);
            this.FilePathRichTextBox.Name = "FilePathRichTextBox";
            this.FilePathRichTextBox.Size = new System.Drawing.Size(560, 45);
            this.FilePathRichTextBox.TabIndex = 5;
            this.FilePathRichTextBox.Text = "";
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileNameLabel.Location = new System.Drawing.Point(64, 56);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(80, 17);
            this.FileNameLabel.TabIndex = 6;
            this.FileNameLabel.Text = "FileName";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Courier New", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ErrorLabel.Location = new System.Drawing.Point(7, 18);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(259, 20);
            this.ErrorLabel.TabIndex = 0;
            this.ErrorLabel.Text = "This is the error message";
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FileLabelLabel
            // 
            this.FileLabelLabel.AutoSize = true;
            this.FileLabelLabel.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileLabelLabel.Location = new System.Drawing.Point(5, 56);
            this.FileLabelLabel.Name = "FileLabelLabel";
            this.FileLabelLabel.Size = new System.Drawing.Size(53, 17);
            this.FileLabelLabel.TabIndex = 4;
            this.FileLabelLabel.Text = "File:";
            // 
            // ErrorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(599, 263);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.UserInputPanel);
            this.Name = "ErrorWindow";
            this.Text = "[CSL] -- Error";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ErrorWindow_KeyPress);
            this.UserInputPanel.ResumeLayout(false);
            this.UserInputPanel.PerformLayout();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel UserInputPanel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.ComboBox ReleaseSelectionComboBox;
        private System.Windows.Forms.Label InstructionalLabel;
        private System.Windows.Forms.TextBox SelectionTextBox;
        private System.Windows.Forms.Button DiscardButton;
        private System.Windows.Forms.Label FileLabelLabel;
        private System.Windows.Forms.RichTextBox FilePathRichTextBox;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.ComboBox BitformatSelectionComboBox;
        private System.Windows.Forms.ComboBox BitrateSelectionComboBox;
        private System.Windows.Forms.ComboBox PhysicalFormatSelectionComboBox;
        private System.Windows.Forms.ComboBox BirthSelectionComboBox;

    }
}