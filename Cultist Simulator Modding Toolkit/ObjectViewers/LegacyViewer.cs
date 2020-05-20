﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;

namespace CarcassSpark.ObjectViewers
{
    public partial class LegacyViewer : Form
    {
        public Legacy displayedLegacy;
        bool editing;

        public LegacyViewer(Legacy legacy, bool? editing)
        {
            InitializeComponent();
            displayedLegacy = legacy;
            fillValues(legacy);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void fillValues(Legacy legacy)
        {
            if (legacy.id != null) idTextBox.Text = legacy.id;
            if (legacy.label != null) labelTextBox.Text = legacy.label;
            if (legacy.description != null) descriptionTextBox.Text = legacy.description;
            if (legacy.startdescription != null) startdescriptionTextBox.Text = legacy.startdescription;
            if (legacy.image != null) imageTextBox.Text = legacy.image;
            if (Utilities.getLegacyImage(legacy.image) != null)
            {
                pictureBox1.Image = Utilities.getLegacyImage(legacy.image);
            }
            if (legacy.fromEnding != null) fromEndingTextBox.Text = legacy.fromEnding;
            if (legacy.availableWithoutEndingMatch.HasValue) availableWithoutEndingMatchCheckBox.Checked = legacy.availableWithoutEndingMatch.Value;
            if (legacy.startingVerbId != null) startingVerbIdTextBox.Text = legacy.startingVerbId;
            if (legacy.effects != null)
            {
                foreach (KeyValuePair<string, int> kvp in legacy.effects)
                {
                    effectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (legacy.effects_extend != null)
            {
                foreach (KeyValuePair<string, int> kvp in legacy.effects_extend)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    row.CreateCells(effectsDataGridView, kvp.Key, kvp.Value);
                    effectsDataGridView.Rows.Add(row);
                }
            }
            if (legacy.effects_remove != null)
            {
                foreach (string removeId in legacy.effects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    row.CreateCells(effectsDataGridView, removeId);
                    effectsDataGridView.Rows.Add(row);
                }
            }
            if (legacy.excludesOnEnding != null)
            {
                foreach (string ending in legacy.excludesOnEnding)
                {
                    excludesOnEndingListView.Items.Add(ending);
                }
            }
            if (legacy.excludesOnEnding_prepend != null)
            {
                foreach (string ending in legacy.excludesOnEnding_prepend)
                {
                    ListViewItem item = new ListViewItem(ending);
                    item.BackColor = Utilities.ListPrependColor;
                    excludesOnEndingListView.Items.Add(item);
                }
            }
            if (legacy.excludesOnEnding_append != null)
            {
                foreach (string ending in legacy.excludesOnEnding_append)
                {
                    ListViewItem item = new ListViewItem(ending);
                    item.BackColor = Utilities.ListAppendColor;
                    excludesOnEndingListView.Items.Add(item);
                }
            }
            if (legacy.excludesOnEnding_remove != null)
            {
                foreach (string ending in legacy.excludesOnEnding_remove)
                {
                    ListViewItem item = new ListViewItem(ending);
                    item.BackColor = Utilities.ListRemoveColor;
                    excludesOnEndingListView.Items.Add(item);
                }
            }
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly =!editing;
            descriptionTextBox.ReadOnly = !editing;
            startdescriptionTextBox.ReadOnly = !editing;
            imageTextBox.ReadOnly = !editing;
            fromEndingTextBox.ReadOnly = !editing;
            availableWithoutEndingMatchCheckBox.Enabled = editing;
            startingVerbIdTextBox.ReadOnly = !editing;
            effectsDataGridView.ReadOnly = !editing;
            effectsDataGridView.AllowUserToAddRows = editing;
            effectsDataGridView.AllowUserToDeleteRows = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            addExcludesButton.Visible = editing;
            addExcludesTextBox.Visible = editing;
            removeButton.Visible = editing;
            excludeAddLabel.Visible = editing;
        }

        private void effectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = effectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            ElementViewer ev = new ElementViewer(Utilities.getElement(id), editing);
            ev.ShowDialog();
        }

        private void excludesOnEndingListView_DoubleClick(object sender, EventArgs e)
        {
            string id = excludesOnEndingListView.SelectedItems[0].ToString();
            LegacyViewer lv = new LegacyViewer(Utilities.getLegacy(id), editing);
            lv.ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Legacies must have an ID");
                return;
            }
            if (effectsDataGridView.RowCount > 1)
            {
                
                foreach (DataGridViewRow row in effectsDataGridView.Rows)
                {
                    string key = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : null;
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (key != null)
                    {
                        if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                        {
                            if (displayedLegacy.effects_extend == null) displayedLegacy.effects_extend = new Dictionary<string, int>();
                            displayedLegacy.effects_extend[key] = value.Value;
                        }
                        else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                        {
                            if (displayedLegacy.effects_remove == null) displayedLegacy.effects_remove = new List<string>();
                            if (!displayedLegacy.effects_remove.Contains(key)) displayedLegacy.effects_remove.Add(key);
                        }
                        else
                        {
                            if (displayedLegacy.effects == null) displayedLegacy.effects = new Dictionary<string, int>();
                            displayedLegacy.effects[key] = value.Value;
                        }
                    }
                    //if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedLegacy.effects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
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

        private void addExcludesButton_Click(object sender, EventArgs e)
        {
            if (addExcludesTextBox.Text != "" && addExcludesTextBox.Text != null)
            {
                excludesOnEndingListView.Items.Add(addExcludesTextBox.Text);
                displayedLegacy.excludesOnEnding.Add(addExcludesTextBox.Text);
                addExcludesTextBox.Text = "";
                addExcludesTextBox.Focus();
            }
        }

        private void addExcludesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (addExcludesTextBox.Text != "" && addExcludesTextBox.Text != null)
                {
                    excludesOnEndingListView.Items.Add(addExcludesTextBox.Text);
                    displayedLegacy.excludesOnEnding.Add(addExcludesTextBox.Text);
                    addExcludesTextBox.Text = "";
                    addExcludesTextBox.Focus();
                }
            }
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.label = labelTextBox.Text;
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.description = descriptionTextBox.Text;
        }

        private void startdescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.startdescription = startdescriptionTextBox.Text;
        }

        private void imageTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.image = imageTextBox.Text;
            if (Utilities.getLegacyImage(imageTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.getLegacyImage(imageTextBox.Text);
            }
        }

        private void fromEndingTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.fromEnding = fromEndingTextBox.Text;
        }

        private void startingVerbIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.startingVerbId = startingVerbIdTextBox.Text;
        }

        private void availableWithoutEndingMatch_CheckedChanged(object sender, EventArgs e)
        {
            displayedLegacy.availableWithoutEndingMatch = availableWithoutEndingMatchCheckBox.Checked;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (displayedLegacy.excludesOnEnding.Contains(excludesOnEndingListView.SelectedItems[0].Text))
            {
                displayedLegacy.excludesOnEnding.Remove(excludesOnEndingListView.SelectedItems[0].Text);
                excludesOnEndingListView.Items.Remove(excludesOnEndingListView.SelectedItems[0]);
            }
        }

        private void addStatusBarElementButton_Click(object sender, EventArgs e)
        {
            if (statusBarElementTextBox.Text != "" && statusBarElementTextBox.Text != null)
            {
                statusBarElementsListView.Items.Add(statusBarElementTextBox.Text);
                displayedLegacy.statusbarelements.Add(statusBarElementTextBox.Text);
                statusBarElementTextBox.Text = "";
                statusBarElementTextBox.Focus();
            }
        }

        private void removeStatusBarElementButton_Click(object sender, EventArgs e)
        {
            if (displayedLegacy.statusbarelements.Contains(statusBarElementsListView.SelectedItems[0].Text))
            {
                displayedLegacy.statusbarelements.Remove(statusBarElementsListView.SelectedItems[0].Text);
                statusBarElementsListView.Items.Remove(statusBarElementsListView.SelectedItems[0]);
            }
        }

        private void statusBarElementTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (statusBarElementTextBox.Text != "" && statusBarElementTextBox.Text != null)
                {
                    statusBarElementsListView.Items.Add(statusBarElementTextBox.Text);
                    displayedLegacy.statusbarelements.Add(statusBarElementTextBox.Text);
                    statusBarElementTextBox.Text = "";
                    statusBarElementTextBox.Focus();
                }
            }
        }

        private void setAsExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
            }
        }

        private void setAsRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
            }
        }

        private void effectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[0].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            int? value = e.Row.Cells[1].Value != null ? Convert.ToInt32(e.Row.Cells[1].Value) : (int?)null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {
                if (displayedLegacy.effects_extend.ContainsKey(key)) displayedLegacy.effects_extend.Remove(key);
                if (displayedLegacy.effects_extend.Count == 0) displayedLegacy.effects_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {
                if (displayedLegacy.effects_remove.Contains(key)) displayedLegacy.effects_remove.Remove(key);
                if (displayedLegacy.effects_remove.Count == 0) displayedLegacy.effects_remove = null;
            }
            else
            {
                if (displayedLegacy.effects.ContainsKey(key)) displayedLegacy.effects.Remove(key);
                if (displayedLegacy.effects.Count == 0) displayedLegacy.effects = null;
            }
        }

        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text != null && extendsTextBox.Text != "")
            {
                displayedLegacy.extends = new List<string> { extendsTextBox.Text };
            }
            else
            {
                displayedLegacy.extends = null;
            }
        }
    }
}
