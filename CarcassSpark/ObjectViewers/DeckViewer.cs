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
    public partial class DeckViewer : Form
    {
        public Deck displayedDeck;
        bool editing;
        event EventHandler<Deck> SuccessCallback;

        public DeckViewer(Deck deck)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            fillValues(deck);
            setEditingMode(false);
            setInternal(false);
        }

        public DeckViewer(Deck deck, EventHandler<Deck> SuccessCallback)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            fillValues(deck);
            if (SuccessCallback != null)
            {
                setEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else setEditingMode(false);
            setInternal(false);
        }

        public DeckViewer(Deck deck, EventHandler<Deck> SuccessCallback, bool? internalDeck)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            fillValues(deck);
            if (SuccessCallback != null) setEditingMode(true);
            else setEditingMode(false);
            if (internalDeck.HasValue) setInternal(internalDeck.Value);
            else setInternal(false);
        }

        void setInternal(bool internalDeck)
        {
            idTextBox.Visible = !internalDeck;
            idLabel.Visible = !internalDeck;
            drawsNumericUpDown.Visible = internalDeck;
            drawsLabel.Visible = internalDeck;
            drawmessagesDataGridView.Visible = !internalDeck;
            drawmessagesLlabel.Visible = !internalDeck;
        }

        void fillValues(Deck deck)
        {
            if (deck.id != null) idTextBox.Text = deck.id;
            if (deck.label != null) labelTextBox.Text = deck.label;
            if (deck.comments != null) commentsTextBox.Text = deck.comments;
            if (deck.description != null) descriptionTextBox.Text = deck.description;
            if (deck.resetonexhaustion.HasValue) resetOnExhaustionCheckBox.Checked = deck.resetonexhaustion.Value;
            defaultCardTextBox.Text = deck.defaultcard;
            if (deck.draws.HasValue) drawsNumericUpDown.Value = deck.draws.Value;
            if (deck.spec != null)
            {
                foreach (string id in deck.spec)
                {
                    ListViewItem lvi = new ListViewItem(id);
                    specListView.Items.Add(lvi);
                }
            }
            if (deck.spec_append != null)
            {
                foreach (string id in deck.spec_append)
                {
                    ListViewItem lvi = new ListViewItem(id);
                    lvi.BackColor = Utilities.ListAppendColor;
                    specListView.Items.Add(lvi);
                }
            }
            if (deck.spec_prepend != null)
            {
                foreach (string id in deck.spec_prepend)
                {
                    ListViewItem lvi = new ListViewItem(id);
                    lvi.BackColor = Utilities.ListPrependColor;
                    specListView.Items.Add(lvi);
                }
            }
            if (deck.spec_remove != null)
            {
                foreach (string id in deck.spec_remove)
                {
                    ListViewItem lvi = new ListViewItem(id);
                    lvi.BackColor = Utilities.ListRemoveColor;
                    specListView.Items.Add(lvi);
                }
            }
            if (deck.drawmessages != null)
            {
                foreach (KeyValuePair<string, string> kvp in deck.drawmessages)
                {
                    drawmessagesDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (deck.drawmessages_extend != null)
            {
                foreach (KeyValuePair<string, string> kvp in deck.drawmessages_extend)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(drawmessagesDataGridView, kvp.Key, kvp.Value);
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    drawmessagesDataGridView.Rows.Add(row);
                }
            }
            if (deck.drawmessages_remove != null)
            {
                foreach (string removeId in deck.drawmessages_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(drawmessagesDataGridView, removeId);
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    drawmessagesDataGridView.Rows.Add(row);
                }
            }
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            resetOnExhaustionCheckBox.Enabled = editing;
            defaultCardTextBox.ReadOnly = !editing;
            drawsNumericUpDown.Enabled = editing;
            drawmessagesDataGridView.ReadOnly = !editing;
            drawmessagesDataGridView.AllowUserToAddRows = editing;
            drawmessagesDataGridView.AllowUserToDeleteRows = editing;
            newCardTextBox.Visible = editing;
            newCardButton.Visible = editing;
            removeCardButton.Visible = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }
        
        private void newCardButton_Click(object sender, EventArgs e)
        {
            if(newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                specListView.Items.Add(newCardTextBox.Text);
                if (displayedDeck.spec == null) displayedDeck.spec = new List<string>();
                displayedDeck.spec.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Standalone Decks must have an ID");
                return;
            }
            if (drawmessagesDataGridView.Rows.Count > 1)
            {
                displayedDeck.drawmessages = new Dictionary<string, string>();
                foreach (DataGridViewRow row in drawmessagesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                        {
                            displayedDeck.drawmessages.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        }
                        else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                        {
                            displayedDeck.drawmessages.Remove(row.Cells[0].Value.ToString());
                        }
                        else
                        {
                            displayedDeck.drawmessages.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        }
                    }
                }
            }
            DialogResult = DialogResult.OK;
            Close();
            SuccessCallback?.Invoke(this, displayedDeck);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.label = labelTextBox.Text;
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.description = descriptionTextBox.Text;
        }

        private void commentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.comments = commentsTextBox.Text;
        }

        private void newCardTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
                {
                    specListView.Items.Add(newCardTextBox.Text);
                    if (displayedDeck.spec == null) displayedDeck.spec = new List<string>();
                    displayedDeck.spec.Add(newCardTextBox.Text);
                    newCardTextBox.Text = "";
                    newCardTextBox.Focus();
                }
            }
        }

        private void defaultCardTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.defaultcard = defaultCardTextBox.Text;
        }

        private void resetOnExhaustionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedDeck.resetonexhaustion = resetOnExhaustionCheckBox.Checked;
        }

        private void removeCardButton_Click(object sender, EventArgs e)
        {
            if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                if (displayedDeck.spec.Contains(newCardTextBox.Text))
                {
                    displayedDeck.spec.Remove(newCardTextBox.Text);
                    specListView.Items.RemoveByKey(newCardTextBox.Text);
                    if (displayedDeck.spec.Count == 0) displayedDeck.spec = null;
                    newCardTextBox.Text = "";
                    newCardTextBox.Focus();
                }
            }
        }

        private void specListView_DoubleClick(object sender, EventArgs e)
        {
            if (specListView.SelectedItems == null) return;
            string id = specListView.SelectedItems[0].Text.ToString();
            if (id.Contains("deck:") && Utilities.deckExists(id.Substring(id.IndexOf(":"))))
            {
                DeckViewer dv = new DeckViewer(Utilities.getDeck(id.Substring(id.IndexOf(":"))), null);
                dv.Show();
            }
            else if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), null);
                ev.Show();
            }
        }

        private void drawmessagesDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedDeck.drawmessages_extend == null) return;
                if (displayedDeck.drawmessages_extend.ContainsKey(key)) displayedDeck.drawmessages_extend.Remove(key);
                if (displayedDeck.drawmessages_extend.Count == 0) displayedDeck.drawmessages_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedDeck.drawmessages_remove == null) return;
                if (displayedDeck.drawmessages_remove.Contains(key)) displayedDeck.drawmessages_remove.Remove(key);
                if (displayedDeck.drawmessages_remove.Count == 0) displayedDeck.drawmessages_remove = null;
            }
            else
            {
                if (displayedDeck.drawmessages == null) return;
                if (displayedDeck.drawmessages.ContainsKey(key)) displayedDeck.drawmessages.Remove(key);
                if (displayedDeck.drawmessages.Count == 0) displayedDeck.drawmessages = null;
            }
        }

        private void specPrependButton_Click(object sender, EventArgs e)
        {
            if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                ListViewItem item = new ListViewItem(newCardTextBox.Text);
                item.BackColor = Utilities.ListPrependColor;
                specListView.Items.Add(item);
                if (displayedDeck.spec_prepend == null) displayedDeck.spec_prepend = new List<string>();
                displayedDeck.spec_prepend.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
        }

        private void specAppendButton_Click(object sender, EventArgs e)
        {
            if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                ListViewItem item = new ListViewItem(newCardTextBox.Text);
                item.BackColor = Utilities.ListAppendColor;
                specListView.Items.Add(item);
                if (displayedDeck.spec_append == null) displayedDeck.spec_append = new List<string>();
                displayedDeck.spec_append.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
        }

        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.extends = extendsTextBox.Text;
        }
    }
}
