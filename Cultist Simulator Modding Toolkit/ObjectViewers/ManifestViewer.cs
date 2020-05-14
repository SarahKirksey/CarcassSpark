﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CultistSimulatorModdingToolkit.ObjectTypes;

namespace CultistSimulatorModdingToolkit.ObjectViewers
{
    public partial class ManifestViewer : Form
    {
        public Manifest displayedManifest;

        public ManifestViewer(Manifest manifest)
        {
            InitializeComponent();
            this.displayedManifest = manifest;
            modNameTextBox.Text = manifest.name;
            modAuthorTextBox.Text = manifest.author;
            modVersionTextBox.Text = manifest.version;
            modDescriptionTextBox.Text = manifest.description;
            longDescriptionTextBox.Text = manifest.description_long;
            if (manifest.dependencies != null)
            {
                foreach (Manifest.Dependency dep in manifest.dependencies)
                {
                    dependeniesDataGridView.Rows.Add(dep.modId, dep.VersionOperator, dep.version);
                }
            }
        }

        private void modNameTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.name = modNameTextBox.Text;
        }

        private void modAuthorTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.author = modAuthorTextBox.Text;
        }

        private void modVersionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.version = modVersionTextBox.Text;
        }

        private void modDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.description = modDescriptionTextBox.Text;
        }

        private void longDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.description_long = longDescriptionTextBox.Text;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if(dependeniesDataGridView.RowCount > 1)
            {
                displayedManifest.dependencies = new List<Manifest.Dependency>();
                foreach (DataGridViewRow row in dependeniesDataGridView.Rows)
                {
                    Manifest.Dependency dep = new Manifest.Dependency();
                    if (row.Cells[0].Value != null) dep.modId = row.Cells[0].Value.ToString();
                    if (row.Cells[1].Value != null) dep.VersionOperator = row.Cells[1].Value.ToString();
                    if (row.Cells[2].Value != null) dep.version = row.Cells[2].Value.ToString();
                    if (dep.modId != null)
                    {
                        displayedManifest.dependencies.Add(dep);
                    }
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
