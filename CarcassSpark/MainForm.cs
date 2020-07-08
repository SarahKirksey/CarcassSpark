﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;
using CarcassSpark.Flowchart;

namespace CarcassSpark
{
    public partial class MainForm : Form
    {
        static string currentDirectory = Program.currentDirectory;

        private string directoryToVanillaContent = currentDirectory + "cultistsimulator_Data/StreamingAssets/content/core/";

        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(currentDirectory + "csmt.settings.json"))
            {
                Settings.loadSettings(currentDirectory + "csmt.settings.json");
            }
            if (Settings.settings["openWithVanilla"] != null && Settings.settings["openWithVanilla"].ToObject<bool>())
            {
                ModViewer mv = new ModViewer(directoryToVanillaContent, true);
                // Utilities.currentMods.Add(mv);
                mv.Show();
            }
            if (Settings.settings["rememberPreviousMod"] != null && Settings.settings["rememberPreviousMod"].ToObject<bool>())
            {
                ModViewer mv = new ModViewer(Settings.settings["previousMod"].ToString(), false);
                // Utilities.currentMods.Add(mv);
                mv.Show();
            }
        }

        private void loadVanillaButton_Click(object sender, EventArgs e)
        {
            ModViewer mv = new ModViewer(directoryToVanillaContent, true);
            // Utilities.currentMods.Add(mv);
            mv.Show();
        }

        private void openModButton_Click(object sender, EventArgs e)
        {
            modFolderBrowserDialog.SelectedPath = (Settings.settings["previousMod"] != null) ? Settings.settings["previousMod"].ToString() : currentDirectory;
            DialogResult dr = modFolderBrowserDialog.ShowDialog();
            if(dr == DialogResult.OK)
            {
                string location = modFolderBrowserDialog.SelectedPath;
                ModViewer mv = new ModViewer(location, false);
                Settings.settings["previousMod"] = location;
                mv.Show();
                Settings.saveSettings();
            }
        }

        private void openSettingsButton_Click(object sender, EventArgs e)
        {
            new Settings().Show();
        }

        private void newModButton_Click(object sender, EventArgs e)
        {
            modFolderBrowserDialog.SelectedPath = currentDirectory;
            DialogResult dr = modFolderBrowserDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string location = modFolderBrowserDialog.SelectedPath;
                ModViewer mv = new ModViewer(true, location);
                Settings.settings["previousMod"] = location;
                mv.Show();
                Settings.saveSettings();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TabbedModViewer tmv = new TabbedModViewer();
            tmv.Show();
        }
    }
}
