﻿namespace Cultist_Simulator_Modding_Toolkit
{
    partial class MainForm
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
            this.loadVanillaButton = new System.Windows.Forms.Button();
            this.openModButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openSettingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loadVanillaButton
            // 
            this.loadVanillaButton.Location = new System.Drawing.Point(12, 12);
            this.loadVanillaButton.Name = "loadVanillaButton";
            this.loadVanillaButton.Size = new System.Drawing.Size(130, 50);
            this.loadVanillaButton.TabIndex = 0;
            this.loadVanillaButton.Text = "Open Vanilla Assets";
            this.loadVanillaButton.UseVisualStyleBackColor = true;
            this.loadVanillaButton.Click += new System.EventHandler(this.loadVanillaButton_Click);
            // 
            // openModButton
            // 
            this.openModButton.Location = new System.Drawing.Point(12, 68);
            this.openModButton.Name = "openModButton";
            this.openModButton.Size = new System.Drawing.Size(130, 50);
            this.openModButton.TabIndex = 1;
            this.openModButton.Text = "Open Mod";
            this.openModButton.UseVisualStyleBackColor = true;
            this.openModButton.Click += new System.EventHandler(this.openModButton_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // openSettingsButton
            // 
            this.openSettingsButton.Location = new System.Drawing.Point(12, 124);
            this.openSettingsButton.Name = "openSettingsButton";
            this.openSettingsButton.Size = new System.Drawing.Size(130, 50);
            this.openSettingsButton.TabIndex = 2;
            this.openSettingsButton.Text = "Settings";
            this.openSettingsButton.UseVisualStyleBackColor = true;
            this.openSettingsButton.Click += new System.EventHandler(this.openSettingsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 261);
            this.Controls.Add(this.openSettingsButton);
            this.Controls.Add(this.openModButton);
            this.Controls.Add(this.loadVanillaButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadVanillaButton;
        private System.Windows.Forms.Button openModButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button openSettingsButton;
    }
}