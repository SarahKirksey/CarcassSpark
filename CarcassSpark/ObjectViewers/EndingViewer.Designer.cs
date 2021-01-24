﻿namespace CarcassSpark.ObjectViewers
{
    partial class EndingViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndingViewer));
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.imageLabel = new System.Windows.Forms.Label();
            this.flavourLabel = new System.Windows.Forms.Label();
            this.animLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.achievementTextBox = new System.Windows.Forms.TextBox();
            this.achievementLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.deletedCheckBox = new System.Windows.Forms.CheckBox();
            this.endindFlavourComboBox = new System.Windows.Forms.ComboBox();
            this.animComboBox = new System.Windows.Forms.ComboBox();
            this.extendsLabel = new System.Windows.Forms.Label();
            this.extendsTextBox = new System.Windows.Forms.TextBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.idTextBox.Location = new System.Drawing.Point(462, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(183, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.idTextBox, "Internal name for the ending");
            this.idTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextBox.Location = new System.Drawing.Point(271, 25);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(185, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.labelTextBox, "Name of the ending as displayed on the ending screen");
            this.labelTextBox.TextChanged += new System.EventHandler(this.LabelTextBox_TextChanged);
            // 
            // imageTextBox
            // 
            this.imageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.imageTextBox.Location = new System.Drawing.Point(271, 282);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(185, 20);
            this.imageTextBox.TabIndex = 3;
            this.imageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.imageTextBox, "Image\'s filename, minus extension, from \"images/endings\" to be displayed on the e" +
        "nding screen. When not present the game uses the ID to find this image.");
            this.imageTextBox.TextChanged += new System.EventHandler(this.ImageTextBox_TextChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AcceptsReturn = true;
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(271, 147);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(374, 109);
            this.descriptionTextBox.TabIndex = 6;
            this.ToolTip.SetToolTip(this.descriptionTextBox, "Description to be displayed on the ending screen. Supports Cultist Simulator\'s te" +
        "xt parsing.");
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
            // 
            // idLabel
            // 
            this.idLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(589, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(54, 13);
            this.idLabel.TabIndex = 7;
            this.idLabel.Text = "Ending ID";
            // 
            // labelLabel
            // 
            this.labelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(270, 9);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(69, 13);
            this.labelLabel.TabIndex = 8;
            this.labelLabel.Text = "Ending Label";
            // 
            // imageLabel
            // 
            this.imageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.imageLabel.AutoSize = true;
            this.imageLabel.Location = new System.Drawing.Point(273, 266);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(72, 13);
            this.imageLabel.TabIndex = 9;
            this.imageLabel.Text = "Ending Image";
            // 
            // flavourLabel
            // 
            this.flavourLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flavourLabel.AutoSize = true;
            this.flavourLabel.Location = new System.Drawing.Point(270, 48);
            this.flavourLabel.Name = "flavourLabel";
            this.flavourLabel.Size = new System.Drawing.Size(78, 13);
            this.flavourLabel.TabIndex = 10;
            this.flavourLabel.Text = "Ending Flavour";
            // 
            // animLabel
            // 
            this.animLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.animLabel.AutoSize = true;
            this.animLabel.Location = new System.Drawing.Point(592, 48);
            this.animLabel.Name = "animLabel";
            this.animLabel.Size = new System.Drawing.Size(53, 13);
            this.animLabel.TabIndex = 13;
            this.animLabel.Text = "Animation";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(270, 131);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 14;
            this.descriptionLabel.Text = "Description";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 385);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(570, 385);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // achievementTextBox
            // 
            this.achievementTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.achievementTextBox.Location = new System.Drawing.Point(462, 282);
            this.achievementTextBox.Name = "achievementTextBox";
            this.achievementTextBox.Size = new System.Drawing.Size(183, 20);
            this.achievementTextBox.TabIndex = 17;
            this.ToolTip.SetToolTip(this.achievementTextBox, resources.GetString("achievementTextBox.ToolTip"));
            this.achievementTextBox.TextChanged += new System.EventHandler(this.AchievementTextBox_TextChanged);
            // 
            // achievementLabel
            // 
            this.achievementLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.achievementLabel.AutoSize = true;
            this.achievementLabel.Location = new System.Drawing.Point(576, 266);
            this.achievementLabel.Name = "achievementLabel";
            this.achievementLabel.Size = new System.Drawing.Size(69, 13);
            this.achievementLabel.TabIndex = 18;
            this.achievementLabel.Text = "Achievement";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(253, 367);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.AcceptsReturn = true;
            this.commentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commentsTextBox.Location = new System.Drawing.Point(271, 321);
            this.commentsTextBox.Multiline = true;
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentsTextBox.Size = new System.Drawing.Size(372, 58);
            this.commentsTextBox.TabIndex = 19;
            this.ToolTip.SetToolTip(this.commentsTextBox, "Comments are not displayed anywhere ingame.");
            this.commentsTextBox.TextChanged += new System.EventHandler(this.CommentsTextBox_TextChanged);
            // 
            // commentsLabel
            // 
            this.commentsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(273, 305);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(56, 13);
            this.commentsLabel.TabIndex = 20;
            this.commentsLabel.Text = "Comments";
            // 
            // deletedCheckBox
            // 
            this.deletedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deletedCheckBox.AutoSize = true;
            this.deletedCheckBox.Location = new System.Drawing.Point(582, 91);
            this.deletedCheckBox.Name = "deletedCheckBox";
            this.deletedCheckBox.Size = new System.Drawing.Size(63, 17);
            this.deletedCheckBox.TabIndex = 21;
            this.deletedCheckBox.Text = "Deleted";
            this.deletedCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.deletedCheckBox, "When true, any ending with the ID specified above will be completely removed from" +
        " the game");
            this.deletedCheckBox.UseVisualStyleBackColor = true;
            this.deletedCheckBox.CheckStateChanged += new System.EventHandler(this.DeletedCheckBox_CheckStateChanged);
            // 
            // endindFlavourComboBox
            // 
            this.endindFlavourComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endindFlavourComboBox.FormattingEnabled = true;
            this.endindFlavourComboBox.Items.AddRange(new object[] {
            "None",
            "Grand",
            "Melancholy",
            "Pale",
            "Vile"});
            this.endindFlavourComboBox.Location = new System.Drawing.Point(271, 64);
            this.endindFlavourComboBox.Name = "endindFlavourComboBox";
            this.endindFlavourComboBox.Size = new System.Drawing.Size(185, 21);
            this.endindFlavourComboBox.TabIndex = 22;
            this.ToolTip.SetToolTip(this.endindFlavourComboBox, "Determines the music and effects played on the ending screen");
            this.endindFlavourComboBox.SelectedIndexChanged += new System.EventHandler(this.EndindFlavourComboBox_SelectedIndexChanged);
            // 
            // animComboBox
            // 
            this.animComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.animComboBox.FormattingEnabled = true;
            this.animComboBox.Items.AddRange(new object[] {
            "DramaticLight",
            "DramaticLightCool",
            "DramaticLightEvil"});
            this.animComboBox.Location = new System.Drawing.Point(462, 64);
            this.animComboBox.Name = "animComboBox";
            this.animComboBox.Size = new System.Drawing.Size(181, 21);
            this.animComboBox.TabIndex = 23;
            this.ToolTip.SetToolTip(this.animComboBox, "Determines the animation played during the ending sequence");
            this.animComboBox.SelectedIndexChanged += new System.EventHandler(this.AnimComboBox_SelectedIndexChanged);
            // 
            // extendsLabel
            // 
            this.extendsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extendsLabel.AutoSize = true;
            this.extendsLabel.Location = new System.Drawing.Point(270, 92);
            this.extendsLabel.Name = "extendsLabel";
            this.extendsLabel.Size = new System.Drawing.Size(45, 13);
            this.extendsLabel.TabIndex = 24;
            this.extendsLabel.Text = "Extends";
            // 
            // extendsTextBox
            // 
            this.extendsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extendsTextBox.Location = new System.Drawing.Point(271, 108);
            this.extendsTextBox.Name = "extendsTextBox";
            this.extendsTextBox.Size = new System.Drawing.Size(372, 20);
            this.extendsTextBox.TabIndex = 25;
            this.ToolTip.SetToolTip(this.extendsTextBox, "Comma separated list, \"example1,example2,example3\", for use in ending templates.");
            this.extendsTextBox.TextChanged += new System.EventHandler(this.ExtendsTextBox_TextChanged);
            // 
            // EndingViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(657, 420);
            this.Controls.Add(this.extendsTextBox);
            this.Controls.Add(this.extendsLabel);
            this.Controls.Add(this.animComboBox);
            this.Controls.Add(this.endindFlavourComboBox);
            this.Controls.Add(this.deletedCheckBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.achievementLabel);
            this.Controls.Add(this.achievementTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.animLabel);
            this.Controls.Add(this.flavourLabel);
            this.Controls.Add(this.imageLabel);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.imageTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(673, 459);
            this.Name = "EndingViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EndingViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox imageTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.Label flavourLabel;
        private System.Windows.Forms.Label animLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox achievementTextBox;
        private System.Windows.Forms.Label achievementLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.CheckBox deletedCheckBox;
        private System.Windows.Forms.ComboBox endindFlavourComboBox;
        private System.Windows.Forms.ComboBox animComboBox;
        private System.Windows.Forms.Label extendsLabel;
        private System.Windows.Forms.TextBox extendsTextBox;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}