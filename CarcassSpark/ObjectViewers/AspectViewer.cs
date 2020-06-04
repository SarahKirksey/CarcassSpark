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
    public partial class AspectViewer : Form
    {
        public Aspect displayedAspect;
        Dictionary<string, Induces> inducesDictionary;
        bool editing;
        event EventHandler<Aspect> SuccessCallback;

        public AspectViewer(Aspect aspect, EventHandler<Aspect> SuccessCallback)
        {
            InitializeComponent();
            this.displayedAspect = aspect;
            if(aspect.extends != null)
            {
                Aspect extendedAspect = Utilities.getAspect(aspect.extends[0]);
                extendsTextBox.Text = aspect.extends[0];
                fillValues(extendedAspect);
            }
            fillValues(aspect);
            if (SuccessCallback != null)
            {
                this.SuccessCallback += SuccessCallback;
                setEditingMode(true);
                editing = true;
            }
            else
            {
                setEditingMode(false);
                editing = false;
            }
        }
        
        void setEditingMode(bool editing)
        {
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            iconTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            extendsTextBox.ReadOnly = !editing;
            isHiddenCheckBox.Enabled = editing;
            noartworkneededCheckBox.Enabled = editing;
            inducesDataGridView.AllowUserToAddRows = editing;
            inducesDataGridView.AllowUserToDeleteRows = editing;
            inducesDataGridView.ReadOnly = !editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            okButton.Visible = editing;
        }

        public void fillValues(Aspect aspect)
        {
            if (aspect.id != null) idTextBox.Text = aspect.id;
            if (aspect.label != null) labelTextBox.Text = aspect.label;
            if (!aspect.noartneeded.HasValue || aspect.noartneeded.Value == false)
            {
                if (aspect.icon != null)
                {
                    iconTextBox.Text = aspect.icon;
                    pictureBox1.Image = Utilities.getAspectImage(aspect.icon);
                }
                else
                {
                    pictureBox1.Image = Utilities.getAspectImage(aspect.id);
                }
            }
            if (aspect.description != null) descriptionTextBox.Text = aspect.description;
            if (aspect.isHidden.HasValue) isHiddenCheckBox.Checked = aspect.isHidden.Value;
            if (aspect.induces != null)
            {
                inducesDictionary = new Dictionary<string, Induces>();
                foreach (Induces induces in aspect.induces)
                {
                    inducesDictionary.Add(induces.id, induces);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, induces.id, induces.chance, induces.additional.HasValue ? induces.additional.Value : false);
                    inducesDataGridView.Rows.Add(newRow);
                }
            }
            if (aspect.induces_prepend != null)
            {
                Dictionary<string, Induces> tmpDictionary = new Dictionary<string, Induces>();
                foreach (Induces induces in aspect.induces_prepend)
                {
                    inducesDictionary.Add(induces.id, induces);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, induces.id, induces.chance, induces.additional.HasValue ? induces.additional.Value : false);
                    newRow.DefaultCellStyle.BackColor = Utilities.ListPrependColor;
                    inducesDataGridView.Rows.Insert(0, newRow);
                }
            }
            if (aspect.induces_append != null)
            {
                Dictionary<string, Induces> tmpDictionary = new Dictionary<string, Induces>();
                foreach (Induces induces in aspect.induces_append)
                {
                    inducesDictionary.Add(induces.id, induces);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, induces.id, induces.chance, induces.additional.HasValue ? induces.additional.Value : false);
                    newRow.DefaultCellStyle.BackColor = Utilities.ListAppendColor;
                    inducesDataGridView.Rows.Add(newRow);
                }
            }
            if (aspect.induces_remove != null)
            {
                foreach (string removeId in aspect.induces_remove)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, removeId);
                    newRow.DefaultCellStyle.BackColor = Utilities.ListRemoveColor;
                    inducesDataGridView.Rows.Add(newRow);
                }
            }
        }

        private void inducesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = inducesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            RecipeViewer rv = new RecipeViewer(Utilities.getRecipe(id), null);
            rv.ShowDialog();
        }
        
        private void okButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Aspects must have an ID.");
                return;
            }
            if (inducesDataGridView.Rows.Count > 1) {
                displayedAspect.induces = new List<Induces>();
                foreach (DataGridViewRow row in inducesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedAspect.induces.Add(new Induces(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value), Convert.ToBoolean(row.Cells[2].Value)));
                }
            }
            DialogResult = DialogResult.OK;
            Close();
            SuccessCallback?.Invoke(this, displayedAspect);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.label = labelTextBox.Text;
        }

        private void iconTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.icon = iconTextBox.Text;
            if(Utilities.getAspectImage(iconTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.getAspectImage(iconTextBox.Text);
            }
        }

        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.extends = new List<string> { extendsTextBox.Text };
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.description = descriptionTextBox.Text;
        }

        private void isHiddenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedAspect.isHidden = isHiddenCheckBox.Checked;
        }

        private void noartworkneededCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedAspect.noartneeded = noartworkneededCheckBox.Checked;
        }

        private void inducesDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            Induces induces = key != null ? inducesDictionary[key] : null;
            if (e.Row.DefaultCellStyle.BackColor == Utilities.ListAppendColor)
            {
                if (displayedAspect.induces_append == null) return;
                if (displayedAspect.induces_append.Contains(induces)) displayedAspect.induces_append.Remove(induces);
                if (displayedAspect.induces_append.Count == 0) displayedAspect.induces_append = null;
            }
            else if (e.Row.DefaultCellStyle.BackColor == Utilities.ListPrependColor)
            {
                if (displayedAspect.induces_prepend == null) return;
                if (displayedAspect.induces_prepend.Contains(induces)) displayedAspect.induces_prepend.Remove(induces);
                if (displayedAspect.induces_prepend.Count == 0) displayedAspect.induces_prepend = null;
            }
            else if (e.Row.DefaultCellStyle.BackColor == Utilities.ListRemoveColor)
            {
                if (displayedAspect.induces_remove == null) return;
                if (displayedAspect.induces_remove.Contains(key)) displayedAspect.induces_remove.Remove(key);
                if (displayedAspect.induces_remove.Count == 0) displayedAspect.induces_remove = null;
            }
            else
            {
                if (displayedAspect.induces == null) return;
                if (displayedAspect.induces.Contains(induces)) displayedAspect.induces.Remove(induces);
                if (displayedAspect.induces.Count == 0) displayedAspect.induces = null;
            }
        }
    }
}
