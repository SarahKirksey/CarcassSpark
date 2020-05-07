﻿namespace Cultist_Simulator_Modding_Toolkit
{
    partial class AspectViewer
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.iconTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.inducesLabel = new System.Windows.Forms.Label();
            this.inducesDataGridView = new System.Windows.Forms.DataGridView();
            this.recipeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extendsTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.isHiddenCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inducesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(118, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(154, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(118, 38);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(154, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.labelTextBox.TextChanged += new System.EventHandler(this.labelTextBox_TextChanged);
            // 
            // iconTextBox
            // 
            this.iconTextBox.Location = new System.Drawing.Point(118, 64);
            this.iconTextBox.Name = "iconTextBox";
            this.iconTextBox.Size = new System.Drawing.Size(154, 20);
            this.iconTextBox.TabIndex = 2;
            this.iconTextBox.Text = "Icon";
            this.iconTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.iconTextBox.TextChanged += new System.EventHandler(this.iconTextBox_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 118);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(260, 131);
            this.descriptionTextBox.TabIndex = 4;
            this.descriptionTextBox.Text = "Description";
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // inducesLabel
            // 
            this.inducesLabel.AutoSize = true;
            this.inducesLabel.Location = new System.Drawing.Point(364, 15);
            this.inducesLabel.Name = "inducesLabel";
            this.inducesLabel.Size = new System.Drawing.Size(45, 13);
            this.inducesLabel.TabIndex = 5;
            this.inducesLabel.Text = "Induces";
            // 
            // inducesDataGridView
            // 
            this.inducesDataGridView.AllowUserToResizeColumns = false;
            this.inducesDataGridView.AllowUserToResizeRows = false;
            this.inducesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inducesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recipeId,
            this.chance});
            this.inducesDataGridView.Location = new System.Drawing.Point(278, 31);
            this.inducesDataGridView.Name = "inducesDataGridView";
            this.inducesDataGridView.Size = new System.Drawing.Size(217, 218);
            this.inducesDataGridView.TabIndex = 6;
            this.inducesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.inducesDataGridView_CellDoubleClick);
            // 
            // recipeId
            // 
            this.recipeId.HeaderText = "Recipe ID";
            this.recipeId.Name = "recipeId";
            this.recipeId.Width = 87;
            // 
            // chance
            // 
            this.chance.HeaderText = "Chance";
            this.chance.Name = "chance";
            this.chance.Width = 87;
            // 
            // extendsTextBox
            // 
            this.extendsTextBox.Location = new System.Drawing.Point(118, 90);
            this.extendsTextBox.Name = "extendsTextBox";
            this.extendsTextBox.Size = new System.Drawing.Size(154, 20);
            this.extendsTextBox.TabIndex = 7;
            this.extendsTextBox.Text = "Extends";
            this.extendsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.extendsTextBox.TextChanged += new System.EventHandler(this.extendsTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 255);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 38);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(395, 255);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 38);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // isHiddenCheckBox
            // 
            this.isHiddenCheckBox.AutoSize = true;
            this.isHiddenCheckBox.Location = new System.Drawing.Point(206, 255);
            this.isHiddenCheckBox.Name = "isHiddenCheckBox";
            this.isHiddenCheckBox.Size = new System.Drawing.Size(66, 17);
            this.isHiddenCheckBox.TabIndex = 10;
            this.isHiddenCheckBox.Text = "Hidden?";
            this.isHiddenCheckBox.UseVisualStyleBackColor = true;
            this.isHiddenCheckBox.CheckedChanged += new System.EventHandler(this.isHiddenCheckBox_CheckedChanged);
            // 
            // AspectViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 305);
            this.Controls.Add(this.isHiddenCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.extendsTextBox);
            this.Controls.Add(this.inducesDataGridView);
            this.Controls.Add(this.inducesLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.iconTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Name = "AspectViewer";
            this.Text = "Aspect Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inducesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox iconTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label inducesLabel;
        private System.Windows.Forms.DataGridView inducesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn chance;
        private System.Windows.Forms.TextBox extendsTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox isHiddenCheckBox;
    }
}