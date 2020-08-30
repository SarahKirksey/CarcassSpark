﻿namespace CarcassSpark.ObjectViewers
{
    partial class LegacyViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegacyViewer));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.fromEndingTextBox = new System.Windows.Forms.TextBox();
            this.startdescriptionTextBox = new System.Windows.Forms.TextBox();
            this.availableWithoutEndingMatchCheckBox = new System.Windows.Forms.CheckBox();
            this.effectsDataGridView = new System.Windows.Forms.DataGridView();
            this.effectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.effectsAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyOperationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsRemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startingVerbIdTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.startDescriptionLabel = new System.Windows.Forms.Label();
            this.imageLabel = new System.Windows.Forms.Label();
            this.fromEndingLabel = new System.Windows.Forms.Label();
            this.effectsLabel = new System.Windows.Forms.Label();
            this.excludesOnEndingLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.startingVerbLabel = new System.Windows.Forms.Label();
            this.addExcludesTextBox = new System.Windows.Forms.TextBox();
            this.addExcludesButton = new System.Windows.Forms.Button();
            this.excludeAddLabel = new System.Windows.Forms.Label();
            this.removeButton = new System.Windows.Forms.Button();
            this.excludesOnEndingListView = new System.Windows.Forms.ListView();
            this.statusBarElementsListView = new System.Windows.Forms.ListView();
            this.statusBarElementsLabel = new System.Windows.Forms.Label();
            this.statusBarElementTextBox = new System.Windows.Forms.TextBox();
            this.statusBarElementsLabel2 = new System.Windows.Forms.Label();
            this.addStatusBarElementButton = new System.Windows.Forms.Button();
            this.removeStatusBarElementButton = new System.Windows.Forms.Button();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.deletedCheckBox = new System.Windows.Forms.CheckBox();
            this.newStartCheckBox = new System.Windows.Forms.CheckBox();
            this.tableCoverImageTextBox = new System.Windows.Forms.TextBox();
            this.tableCoverImageLabel = new System.Windows.Forms.Label();
            this.tableSurfaceImageLabel = new System.Windows.Forms.Label();
            this.tableSurfaceImageTextBox = new System.Windows.Forms.TextBox();
            this.tableEdgeImageLabel = new System.Windows.Forms.Label();
            this.tableEdgeImageTextBox = new System.Windows.Forms.TextBox();
            this.extendsTextBox = new System.Windows.Forms.TextBox();
            this.extendsLabel = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).BeginInit();
            this.propertyOperationContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(148, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.idTextBox, "Internal name of legacy");
            this.idTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(254, 25);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(100, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.labelTextBox, "Name of legacy as displayed during legacy selection, under the player\'s name, and" +
        " in verbs.");
            this.labelTextBox.TextChanged += new System.EventHandler(this.LabelTextBox_TextChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AcceptsReturn = true;
            this.descriptionTextBox.Location = new System.Drawing.Point(148, 64);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(235, 86);
            this.descriptionTextBox.TabIndex = 3;
            this.ToolTip.SetToolTip(this.descriptionTextBox, "Description of legacy to be displayed during legacy selection");
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
            // 
            // imageTextBox
            // 
            this.imageTextBox.Location = new System.Drawing.Point(360, 25);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(100, 20);
            this.imageTextBox.TabIndex = 4;
            this.imageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.imageTextBox, "Image\'s filename, minus extension, from \"images/legacies\" to be displayed during " +
        "legacy selection and, when New Start is true, in the Mods & DLC menu.");
            this.imageTextBox.TextChanged += new System.EventHandler(this.ImageTextBox_TextChanged);
            // 
            // fromEndingTextBox
            // 
            this.fromEndingTextBox.Location = new System.Drawing.Point(572, 25);
            this.fromEndingTextBox.Name = "fromEndingTextBox";
            this.fromEndingTextBox.Size = new System.Drawing.Size(100, 20);
            this.fromEndingTextBox.TabIndex = 5;
            this.fromEndingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.fromEndingTextBox, "ID of ending to either require to be available or be more frequent after getting," +
        " depending on other properties.");
            this.fromEndingTextBox.TextChanged += new System.EventHandler(this.FromEndingTextBox_TextChanged);
            // 
            // startdescriptionTextBox
            // 
            this.startdescriptionTextBox.AcceptsReturn = true;
            this.startdescriptionTextBox.Location = new System.Drawing.Point(389, 64);
            this.startdescriptionTextBox.Multiline = true;
            this.startdescriptionTextBox.Name = "startdescriptionTextBox";
            this.startdescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.startdescriptionTextBox.Size = new System.Drawing.Size(235, 86);
            this.startdescriptionTextBox.TabIndex = 6;
            this.ToolTip.SetToolTip(this.startdescriptionTextBox, "Description of legacy to be displayed upon beginning the descent");
            this.startdescriptionTextBox.TextChanged += new System.EventHandler(this.StartdescriptionTextBox_TextChanged);
            // 
            // availableWithoutEndingMatchCheckBox
            // 
            this.availableWithoutEndingMatchCheckBox.AutoSize = true;
            this.availableWithoutEndingMatchCheckBox.Location = new System.Drawing.Point(12, 194);
            this.availableWithoutEndingMatchCheckBox.Name = "availableWithoutEndingMatchCheckBox";
            this.availableWithoutEndingMatchCheckBox.Size = new System.Drawing.Size(178, 17);
            this.availableWithoutEndingMatchCheckBox.TabIndex = 7;
            this.availableWithoutEndingMatchCheckBox.Text = "Available Without Ending Match";
            this.availableWithoutEndingMatchCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.availableWithoutEndingMatchCheckBox, "When true, the legacy does not strictly require the specified ending to be availa" +
        "ble, but is produced as an option at a reduced rate when you get a different end" +
        "ing.");
            this.availableWithoutEndingMatchCheckBox.UseVisualStyleBackColor = true;
            this.availableWithoutEndingMatchCheckBox.CheckStateChanged += new System.EventHandler(this.AvailableWithoutEndingMatchCheckBox_CheckStateChanged);
            // 
            // effectsDataGridView
            // 
            this.effectsDataGridView.AllowUserToResizeColumns = false;
            this.effectsDataGridView.AllowUserToResizeRows = false;
            this.effectsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.effectsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.effectId,
            this.effectsAmount});
            this.effectsDataGridView.ContextMenuStrip = this.propertyOperationContextMenuStrip;
            this.effectsDataGridView.Location = new System.Drawing.Point(12, 262);
            this.effectsDataGridView.Name = "effectsDataGridView";
            this.effectsDataGridView.Size = new System.Drawing.Size(333, 194);
            this.effectsDataGridView.TabIndex = 8;
            this.ToolTip.SetToolTip(this.effectsDataGridView, "Elements to give the player at the beginning of the game.");
            this.effectsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EffectsDataGridView_CellDoubleClick);
            this.effectsDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.EffectsDataGridView_UserDeletedRow);
            // 
            // effectId
            // 
            this.effectId.HeaderText = "Element ID";
            this.effectId.Name = "effectId";
            this.effectId.Width = 145;
            // 
            // effectsAmount
            // 
            this.effectsAmount.HeaderText = "Amount";
            this.effectsAmount.Name = "effectsAmount";
            this.effectsAmount.Width = 145;
            // 
            // propertyOperationContextMenuStrip
            // 
            this.propertyOperationContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsExtendToolStripMenuItem,
            this.setAsRemoveToolStripMenuItem});
            this.propertyOperationContextMenuStrip.Name = "propertyOperationContextMenuStrip";
            this.propertyOperationContextMenuStrip.Size = new System.Drawing.Size(151, 48);
            // 
            // setAsExtendToolStripMenuItem
            // 
            this.setAsExtendToolStripMenuItem.Name = "setAsExtendToolStripMenuItem";
            this.setAsExtendToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setAsExtendToolStripMenuItem.Text = "Set as Extend";
            this.setAsExtendToolStripMenuItem.Click += new System.EventHandler(this.SetAsExtendToolStripMenuItem_Click);
            // 
            // setAsRemoveToolStripMenuItem
            // 
            this.setAsRemoveToolStripMenuItem.Name = "setAsRemoveToolStripMenuItem";
            this.setAsRemoveToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setAsRemoveToolStripMenuItem.Text = "Set as Remove";
            this.setAsRemoveToolStripMenuItem.Click += new System.EventHandler(this.SetAsRemoveToolStripMenuItem_Click);
            // 
            // startingVerbIdTextBox
            // 
            this.startingVerbIdTextBox.Location = new System.Drawing.Point(466, 25);
            this.startingVerbIdTextBox.Name = "startingVerbIdTextBox";
            this.startingVerbIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.startingVerbIdTextBox.TabIndex = 10;
            this.startingVerbIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.startingVerbIdTextBox, "ID of the verb you want the legacy to start the game with.\r\nIf not present, \"work" +
        "\" is used by default.");
            this.startingVerbIdTextBox.TextChanged += new System.EventHandler(this.StartingVerbIdTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 462);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 25);
            this.okButton.TabIndex = 11;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(145, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(56, 13);
            this.idLabel.TabIndex = 12;
            this.idLabel.Text = "Legacy ID";
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(254, 9);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(71, 13);
            this.labelLabel.TabIndex = 13;
            this.labelLabel.Text = "Legacy Label";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(145, 48);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 14;
            this.descriptionLabel.Text = "Description";
            // 
            // startDescriptionLabel
            // 
            this.startDescriptionLabel.AutoSize = true;
            this.startDescriptionLabel.Location = new System.Drawing.Point(386, 48);
            this.startDescriptionLabel.Name = "startDescriptionLabel";
            this.startDescriptionLabel.Size = new System.Drawing.Size(85, 13);
            this.startDescriptionLabel.TabIndex = 15;
            this.startDescriptionLabel.Text = "Start Description";
            // 
            // imageLabel
            // 
            this.imageLabel.AutoSize = true;
            this.imageLabel.Location = new System.Drawing.Point(357, 9);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(36, 13);
            this.imageLabel.TabIndex = 16;
            this.imageLabel.Text = "Image";
            // 
            // fromEndingLabel
            // 
            this.fromEndingLabel.AutoSize = true;
            this.fromEndingLabel.Location = new System.Drawing.Point(569, 9);
            this.fromEndingLabel.Name = "fromEndingLabel";
            this.fromEndingLabel.Size = new System.Drawing.Size(66, 13);
            this.fromEndingLabel.TabIndex = 17;
            this.fromEndingLabel.Text = "From Ending";
            // 
            // effectsLabel
            // 
            this.effectsLabel.AutoSize = true;
            this.effectsLabel.Location = new System.Drawing.Point(9, 246);
            this.effectsLabel.Name = "effectsLabel";
            this.effectsLabel.Size = new System.Drawing.Size(73, 13);
            this.effectsLabel.TabIndex = 18;
            this.effectsLabel.Text = "Starting Cards";
            // 
            // excludesOnEndingLabel
            // 
            this.excludesOnEndingLabel.AutoSize = true;
            this.excludesOnEndingLabel.Location = new System.Drawing.Point(587, 153);
            this.excludesOnEndingLabel.Name = "excludesOnEndingLabel";
            this.excludesOnEndingLabel.Size = new System.Drawing.Size(159, 13);
            this.excludesOnEndingLabel.TabIndex = 19;
            this.excludesOnEndingLabel.Text = "Exclude this after these legacies";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(765, 462);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 25);
            this.cancelButton.TabIndex = 20;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // startingVerbLabel
            // 
            this.startingVerbLabel.AutoSize = true;
            this.startingVerbLabel.Location = new System.Drawing.Point(463, 9);
            this.startingVerbLabel.Name = "startingVerbLabel";
            this.startingVerbLabel.Size = new System.Drawing.Size(68, 13);
            this.startingVerbLabel.TabIndex = 21;
            this.startingVerbLabel.Text = "Starting Verb";
            // 
            // addExcludesTextBox
            // 
            this.addExcludesTextBox.Location = new System.Drawing.Point(590, 265);
            this.addExcludesTextBox.Name = "addExcludesTextBox";
            this.addExcludesTextBox.Size = new System.Drawing.Size(115, 20);
            this.addExcludesTextBox.TabIndex = 22;
            this.addExcludesTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddExcludesTextBox_KeyDown);
            // 
            // addExcludesButton
            // 
            this.addExcludesButton.Location = new System.Drawing.Point(711, 265);
            this.addExcludesButton.Name = "addExcludesButton";
            this.addExcludesButton.Size = new System.Drawing.Size(75, 20);
            this.addExcludesButton.TabIndex = 23;
            this.addExcludesButton.Text = "Add";
            this.addExcludesButton.UseVisualStyleBackColor = true;
            this.addExcludesButton.Click += new System.EventHandler(this.AddExcludesButton_Click);
            // 
            // excludeAddLabel
            // 
            this.excludeAddLabel.AutoSize = true;
            this.excludeAddLabel.Location = new System.Drawing.Point(587, 249);
            this.excludeAddLabel.Name = "excludeAddLabel";
            this.excludeAddLabel.Size = new System.Drawing.Size(109, 13);
            this.excludeAddLabel.TabIndex = 24;
            this.excludeAddLabel.Text = "Legacy ID to Exclude";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(790, 265);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 20);
            this.removeButton.TabIndex = 25;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // excludesOnEndingListView
            // 
            this.excludesOnEndingListView.HideSelection = false;
            this.excludesOnEndingListView.Location = new System.Drawing.Point(590, 169);
            this.excludesOnEndingListView.MultiSelect = false;
            this.excludesOnEndingListView.Name = "excludesOnEndingListView";
            this.excludesOnEndingListView.Size = new System.Drawing.Size(275, 77);
            this.excludesOnEndingListView.TabIndex = 26;
            this.ToolTip.SetToolTip(this.excludesOnEndingListView, "Endings to disallow this legacy from being available after");
            this.excludesOnEndingListView.UseCompatibleStateImageBehavior = false;
            this.excludesOnEndingListView.View = System.Windows.Forms.View.List;
            this.excludesOnEndingListView.DoubleClick += new System.EventHandler(this.ExcludesOnEndingListView_DoubleClick);
            // 
            // statusBarElementsListView
            // 
            this.statusBarElementsListView.HideSelection = false;
            this.statusBarElementsListView.Location = new System.Drawing.Point(257, 169);
            this.statusBarElementsListView.Name = "statusBarElementsListView";
            this.statusBarElementsListView.Size = new System.Drawing.Size(324, 77);
            this.statusBarElementsListView.TabIndex = 27;
            this.ToolTip.SetToolTip(this.statusBarElementsListView, "Used to specify which 4 aspects or elements will be tracked in the status bar. Wh" +
        "en not present, the default 4 (health, passion, reason, funds) will be used.");
            this.statusBarElementsListView.UseCompatibleStateImageBehavior = false;
            // 
            // statusBarElementsLabel
            // 
            this.statusBarElementsLabel.AutoSize = true;
            this.statusBarElementsLabel.Location = new System.Drawing.Point(351, 153);
            this.statusBarElementsLabel.Name = "statusBarElementsLabel";
            this.statusBarElementsLabel.Size = new System.Drawing.Size(102, 13);
            this.statusBarElementsLabel.TabIndex = 28;
            this.statusBarElementsLabel.Text = "Status Bar Elements";
            // 
            // statusBarElementTextBox
            // 
            this.statusBarElementTextBox.Location = new System.Drawing.Point(354, 268);
            this.statusBarElementTextBox.Name = "statusBarElementTextBox";
            this.statusBarElementTextBox.Size = new System.Drawing.Size(115, 20);
            this.statusBarElementTextBox.TabIndex = 29;
            this.statusBarElementTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StatusBarElementTextBox_KeyDown);
            // 
            // statusBarElementsLabel2
            // 
            this.statusBarElementsLabel2.AutoSize = true;
            this.statusBarElementsLabel2.Location = new System.Drawing.Point(351, 251);
            this.statusBarElementsLabel2.Name = "statusBarElementsLabel2";
            this.statusBarElementsLabel2.Size = new System.Drawing.Size(157, 13);
            this.statusBarElementsLabel2.TabIndex = 30;
            this.statusBarElementsLabel2.Text = "Element ID to Add to Status Bar";
            // 
            // addStatusBarElementButton
            // 
            this.addStatusBarElementButton.Location = new System.Drawing.Point(475, 267);
            this.addStatusBarElementButton.Name = "addStatusBarElementButton";
            this.addStatusBarElementButton.Size = new System.Drawing.Size(38, 20);
            this.addStatusBarElementButton.TabIndex = 31;
            this.addStatusBarElementButton.Text = "Add";
            this.addStatusBarElementButton.UseVisualStyleBackColor = true;
            this.addStatusBarElementButton.Click += new System.EventHandler(this.AddStatusBarElementButton_Click);
            // 
            // removeStatusBarElementButton
            // 
            this.removeStatusBarElementButton.Location = new System.Drawing.Point(519, 266);
            this.removeStatusBarElementButton.Name = "removeStatusBarElementButton";
            this.removeStatusBarElementButton.Size = new System.Drawing.Size(62, 20);
            this.removeStatusBarElementButton.TabIndex = 32;
            this.removeStatusBarElementButton.Text = "Remove";
            this.removeStatusBarElementButton.UseVisualStyleBackColor = true;
            this.removeStatusBarElementButton.Click += new System.EventHandler(this.RemoveStatusBarElementButton_Click);
            // 
            // commentsLabel
            // 
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(627, 48);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(56, 13);
            this.commentsLabel.TabIndex = 36;
            this.commentsLabel.Text = "Comments";
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.AcceptsReturn = true;
            this.commentsTextBox.Location = new System.Drawing.Point(630, 64);
            this.commentsTextBox.Multiline = true;
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentsTextBox.Size = new System.Drawing.Size(235, 86);
            this.commentsTextBox.TabIndex = 37;
            this.commentsTextBox.TextChanged += new System.EventHandler(this.CommentsTextBox_TextChanged);
            // 
            // deletedCheckBox
            // 
            this.deletedCheckBox.AutoSize = true;
            this.deletedCheckBox.Location = new System.Drawing.Point(12, 148);
            this.deletedCheckBox.Name = "deletedCheckBox";
            this.deletedCheckBox.Size = new System.Drawing.Size(63, 17);
            this.deletedCheckBox.TabIndex = 38;
            this.deletedCheckBox.Text = "Deleted";
            this.deletedCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.deletedCheckBox, "When true, any legacy with the same ID as this ending will be completely removed " +
        "from the game.");
            this.deletedCheckBox.UseVisualStyleBackColor = true;
            this.deletedCheckBox.CheckStateChanged += new System.EventHandler(this.DeletedCheckBox_CheckStateChanged);
            // 
            // newStartCheckBox
            // 
            this.newStartCheckBox.AutoSize = true;
            this.newStartCheckBox.Location = new System.Drawing.Point(12, 171);
            this.newStartCheckBox.Name = "newStartCheckBox";
            this.newStartCheckBox.Size = new System.Drawing.Size(73, 17);
            this.newStartCheckBox.TabIndex = 39;
            this.newStartCheckBox.Text = "New Start";
            this.newStartCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.newStartCheckBox, "When true, the legacy will be displayed in the mods and DLC menu, allowing you to" +
        " start a new descent with this legacy.");
            this.newStartCheckBox.UseVisualStyleBackColor = true;
            this.newStartCheckBox.CheckStateChanged += new System.EventHandler(this.NewStartCheckBox_CheckStateChanged);
            // 
            // tableCoverImageTextBox
            // 
            this.tableCoverImageTextBox.Location = new System.Drawing.Point(354, 307);
            this.tableCoverImageTextBox.Name = "tableCoverImageTextBox";
            this.tableCoverImageTextBox.Size = new System.Drawing.Size(233, 20);
            this.tableCoverImageTextBox.TabIndex = 40;
            this.ToolTip.SetToolTip(this.tableCoverImageTextBox, "Image\'s filename, minus extension, from \"images/ui\" to be used as the centerpiece" +
        " texture for the table.");
            this.tableCoverImageTextBox.TextChanged += new System.EventHandler(this.TableCoverImageTextBox_TextChanged);
            // 
            // tableCoverImageLabel
            // 
            this.tableCoverImageLabel.AutoSize = true;
            this.tableCoverImageLabel.Location = new System.Drawing.Point(351, 291);
            this.tableCoverImageLabel.Name = "tableCoverImageLabel";
            this.tableCoverImageLabel.Size = new System.Drawing.Size(97, 13);
            this.tableCoverImageLabel.TabIndex = 41;
            this.tableCoverImageLabel.Text = "Table Cover Image";
            // 
            // tableSurfaceImageLabel
            // 
            this.tableSurfaceImageLabel.AutoSize = true;
            this.tableSurfaceImageLabel.Location = new System.Drawing.Point(351, 330);
            this.tableSurfaceImageLabel.Name = "tableSurfaceImageLabel";
            this.tableSurfaceImageLabel.Size = new System.Drawing.Size(106, 13);
            this.tableSurfaceImageLabel.TabIndex = 43;
            this.tableSurfaceImageLabel.Text = "Table Surface Image";
            // 
            // tableSurfaceImageTextBox
            // 
            this.tableSurfaceImageTextBox.Location = new System.Drawing.Point(354, 346);
            this.tableSurfaceImageTextBox.Name = "tableSurfaceImageTextBox";
            this.tableSurfaceImageTextBox.Size = new System.Drawing.Size(233, 20);
            this.tableSurfaceImageTextBox.TabIndex = 42;
            this.ToolTip.SetToolTip(this.tableSurfaceImageTextBox, "Image\'s filename, minus extension, from \"images/ui\" to be used as the wood surfac" +
        "e texture for the table.");
            this.tableSurfaceImageTextBox.TextChanged += new System.EventHandler(this.TableSurfaceImageTextBox_TextChanged);
            // 
            // tableEdgeImageLabel
            // 
            this.tableEdgeImageLabel.AutoSize = true;
            this.tableEdgeImageLabel.Location = new System.Drawing.Point(351, 369);
            this.tableEdgeImageLabel.Name = "tableEdgeImageLabel";
            this.tableEdgeImageLabel.Size = new System.Drawing.Size(94, 13);
            this.tableEdgeImageLabel.TabIndex = 45;
            this.tableEdgeImageLabel.Text = "Table Edge Image";
            // 
            // tableEdgeImageTextBox
            // 
            this.tableEdgeImageTextBox.Location = new System.Drawing.Point(354, 385);
            this.tableEdgeImageTextBox.Name = "tableEdgeImageTextBox";
            this.tableEdgeImageTextBox.Size = new System.Drawing.Size(233, 20);
            this.tableEdgeImageTextBox.TabIndex = 44;
            this.ToolTip.SetToolTip(this.tableEdgeImageTextBox, "Image\'s filename, minus extension, from \"images/ui\" to be used as the edge textur" +
        "e for the table.");
            this.tableEdgeImageTextBox.TextChanged += new System.EventHandler(this.TableEdgeImageTextBox_TextChanged);
            // 
            // extendsTextBox
            // 
            this.extendsTextBox.Location = new System.Drawing.Point(678, 25);
            this.extendsTextBox.Name = "extendsTextBox";
            this.extendsTextBox.Size = new System.Drawing.Size(187, 20);
            this.extendsTextBox.TabIndex = 46;
            this.extendsTextBox.TextChanged += new System.EventHandler(this.ExtendsTextBox_TextChanged);
            // 
            // extendsLabel
            // 
            this.extendsLabel.AutoSize = true;
            this.extendsLabel.Location = new System.Drawing.Point(675, 9);
            this.extendsLabel.Name = "extendsLabel";
            this.extendsLabel.Size = new System.Drawing.Size(45, 13);
            this.extendsLabel.TabIndex = 47;
            this.extendsLabel.Text = "Extends";
            // 
            // LegacyViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(877, 499);
            this.Controls.Add(this.extendsLabel);
            this.Controls.Add(this.extendsTextBox);
            this.Controls.Add(this.tableEdgeImageLabel);
            this.Controls.Add(this.tableEdgeImageTextBox);
            this.Controls.Add(this.tableSurfaceImageLabel);
            this.Controls.Add(this.tableSurfaceImageTextBox);
            this.Controls.Add(this.tableCoverImageLabel);
            this.Controls.Add(this.tableCoverImageTextBox);
            this.Controls.Add(this.newStartCheckBox);
            this.Controls.Add(this.deletedCheckBox);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.removeStatusBarElementButton);
            this.Controls.Add(this.addStatusBarElementButton);
            this.Controls.Add(this.statusBarElementsLabel2);
            this.Controls.Add(this.statusBarElementTextBox);
            this.Controls.Add(this.statusBarElementsLabel);
            this.Controls.Add(this.statusBarElementsListView);
            this.Controls.Add(this.excludesOnEndingListView);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.excludeAddLabel);
            this.Controls.Add(this.addExcludesButton);
            this.Controls.Add(this.addExcludesTextBox);
            this.Controls.Add(this.startingVerbLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.excludesOnEndingLabel);
            this.Controls.Add(this.effectsLabel);
            this.Controls.Add(this.fromEndingLabel);
            this.Controls.Add(this.imageLabel);
            this.Controls.Add(this.startDescriptionLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.startingVerbIdTextBox);
            this.Controls.Add(this.effectsDataGridView);
            this.Controls.Add(this.availableWithoutEndingMatchCheckBox);
            this.Controls.Add(this.startdescriptionTextBox);
            this.Controls.Add(this.fromEndingTextBox);
            this.Controls.Add(this.imageTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LegacyViewer";
            this.Text = "LegacyViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).EndInit();
            this.propertyOperationContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox imageTextBox;
        private System.Windows.Forms.TextBox fromEndingTextBox;
        private System.Windows.Forms.TextBox startdescriptionTextBox;
        private System.Windows.Forms.CheckBox availableWithoutEndingMatchCheckBox;
        private System.Windows.Forms.DataGridView effectsDataGridView;
        private System.Windows.Forms.TextBox startingVerbIdTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label startDescriptionLabel;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.Label fromEndingLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectsAmount;
        private System.Windows.Forms.Label effectsLabel;
        private System.Windows.Forms.Label excludesOnEndingLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label startingVerbLabel;
        private System.Windows.Forms.TextBox addExcludesTextBox;
        private System.Windows.Forms.Button addExcludesButton;
        private System.Windows.Forms.Label excludeAddLabel;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ListView excludesOnEndingListView;
        private System.Windows.Forms.ListView statusBarElementsListView;
        private System.Windows.Forms.Label statusBarElementsLabel;
        private System.Windows.Forms.TextBox statusBarElementTextBox;
        private System.Windows.Forms.Label statusBarElementsLabel2;
        private System.Windows.Forms.Button addStatusBarElementButton;
        private System.Windows.Forms.Button removeStatusBarElementButton;
        private System.Windows.Forms.ContextMenuStrip propertyOperationContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setAsExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsRemoveToolStripMenuItem;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.CheckBox deletedCheckBox;
        private System.Windows.Forms.CheckBox newStartCheckBox;
        private System.Windows.Forms.TextBox tableCoverImageTextBox;
        private System.Windows.Forms.Label tableCoverImageLabel;
        private System.Windows.Forms.Label tableSurfaceImageLabel;
        private System.Windows.Forms.TextBox tableSurfaceImageTextBox;
        private System.Windows.Forms.Label tableEdgeImageLabel;
        private System.Windows.Forms.TextBox tableEdgeImageTextBox;
        private System.Windows.Forms.TextBox extendsTextBox;
        private System.Windows.Forms.Label extendsLabel;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}