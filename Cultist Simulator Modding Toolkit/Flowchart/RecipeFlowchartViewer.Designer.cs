﻿namespace CultistSimulatorModdingToolkit.Flowchart
{
    partial class RecipeFlowchartViewer
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
            this.diagram1 = new MindFusion.Diagramming.Diagram();
            this.diagramView1 = new MindFusion.Diagramming.WinForms.DiagramView();
            this.zoomControl1 = new MindFusion.Common.WinForms.ZoomControl();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.loadLinkedRecipesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // diagram1
            // 
            this.diagram1.AutoAlignNodes = true;
            this.diagram1.LinkCrossings = MindFusion.Diagramming.LinkCrossings.Arcs;
            this.diagram1.LinkHeadShape = MindFusion.Diagramming.Shape.FromId("PointerArrow");
            this.diagram1.LinkSegments = 2;
            this.diagram1.LinkShape = MindFusion.Diagramming.LinkShape.Cascading;
            this.diagram1.ShapeText = "Node";
            this.diagram1.TouchThreshold = 0F;
            this.diagram1.TreeViewNodeStyle.Brush = new MindFusion.Drawing.SolidBrush("#FF9ACD32");
            // 
            // diagramView1
            // 
            this.diagramView1.AllowInplaceEdit = true;
            this.diagramView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diagramView1.Behavior = MindFusion.Diagramming.Behavior.PanAndModify;
            this.diagramView1.Diagram = this.diagram1;
            this.diagramView1.LicenseKey = "asdf";
            this.diagramView1.Location = new System.Drawing.Point(0, 0);
            this.diagramView1.Name = "diagramView1";
            this.diagramView1.Size = new System.Drawing.Size(676, 409);
            this.diagramView1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.diagramView1.TabIndex = 0;
            this.diagramView1.Text = "diagramView1";
            // 
            // zoomControl1
            // 
            this.zoomControl1.BackColor = System.Drawing.Color.Transparent;
            this.zoomControl1.Location = new System.Drawing.Point(12, 12);
            this.zoomControl1.Name = "zoomControl1";
            this.zoomControl1.Padding = new System.Windows.Forms.Padding(5);
            this.zoomControl1.Size = new System.Drawing.Size(59, 388);
            this.zoomControl1.TabIndex = 1;
            this.zoomControl1.Target = this.diagramView1;
            this.zoomControl1.TickPosition = MindFusion.Common.WinForms.TickPosition.Left;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 182);
            this.progressBar1.Maximum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(652, 54);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // loadLinkedRecipesButton
            // 
            this.loadLinkedRecipesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadLinkedRecipesButton.Location = new System.Drawing.Point(548, 12);
            this.loadLinkedRecipesButton.Name = "loadLinkedRecipesButton";
            this.loadLinkedRecipesButton.Size = new System.Drawing.Size(116, 23);
            this.loadLinkedRecipesButton.TabIndex = 3;
            this.loadLinkedRecipesButton.Text = "Load Linked Recipes";
            this.loadLinkedRecipesButton.UseVisualStyleBackColor = true;
            this.loadLinkedRecipesButton.Click += new System.EventHandler(this.loadLinkedRecipesButton_Click);
            // 
            // RecipeFlowchartViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 411);
            this.Controls.Add(this.loadLinkedRecipesButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.zoomControl1);
            this.Controls.Add(this.diagramView1);
            this.Name = "RecipeFlowchartViewer";
            this.Text = "RecipeFlowchartViewer";
            this.Load += new System.EventHandler(this.RecipeFlowchartViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MindFusion.Diagramming.Diagram diagram1;
        private MindFusion.Diagramming.WinForms.DiagramView diagramView1;
        private MindFusion.Common.WinForms.ZoomControl zoomControl1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button loadLinkedRecipesButton;
    }
}