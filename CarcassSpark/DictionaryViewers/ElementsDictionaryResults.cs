﻿using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarcassSpark.DictionaryViewers
{
    public partial class ElementsDictionaryResults : Form
    {
        private readonly Dictionary<Element, Guid> results = new Dictionary<Element, Guid>();
        private readonly Dictionary<string, Element> resultsWithId = new Dictionary<string, Element>();

        public ElementsDictionaryResults(Dictionary<Guid, Element> results)
        {
            InitializeComponent();

            // this.results = results;
            foreach (KeyValuePair<Guid, Element> kvp in results)
            {
                resultsListBox.Items.Add(kvp.Value.ID);
                resultsWithId.Add(kvp.Value.ID, kvp.Value);
                this.results.Add(kvp.Value, kvp.Key);
            }
        }

        private void ResultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null)
            {
                return;
            }

            Element selectedElement = resultsWithId[resultsListBox.SelectedItem.ToString()];
            ElementViewer ev = new ElementViewer(selectedElement.Copy(), null, null);
            ev.Show();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
