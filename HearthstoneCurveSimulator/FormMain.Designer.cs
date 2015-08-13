namespace HearthstoneCurveSimulator
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.resultGraphControl1 = new HearthstoneCurveSimulator.ResultGraphControl();
            this.curveControl1 = new HearthstoneCurveSimulator.CurveControl();
            this.simulationComponent = new HearthstoneCurveSimulator.SimulationComponent(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonHero = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 522);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1123, 43);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(300, 37);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(100, 38);
            this.toolStripStatusLabel1.Text = "statusLabel";
            // 
            // resultGraphControl1
            // 
            this.resultGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultGraphControl1.Location = new System.Drawing.Point(545, 87);
            this.resultGraphControl1.Name = "resultGraphControl1";
            this.resultGraphControl1.Size = new System.Drawing.Size(566, 430);
            this.resultGraphControl1.TabIndex = 4;
            // 
            // curveControl1
            // 
            this.curveControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.curveControl1.Deck = ((System.Collections.Generic.List<int>)(resources.GetObject("curveControl1.Deck")));
            this.curveControl1.Location = new System.Drawing.Point(13, 87);
            this.curveControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.curveControl1.Name = "curveControl1";
            this.curveControl1.Size = new System.Drawing.Size(525, 430);
            this.curveControl1.TabIndex = 3;
            this.curveControl1.DeckChanged += new System.EventHandler<HearthstoneCurveSimulator.CurveControl.DeckChangedEvent>(this.curveControl1_DeckChanged);
            // 
            // simulationComponent
            // 
            this.simulationComponent.Deck = null;
            this.simulationComponent.Iterations = 500;
            this.simulationComponent.ShuffleVeracity = 300;
            this.simulationComponent.UseBigCardsFirst = true;
            this.simulationComponent.ProgressChanged += new System.EventHandler<System.ComponentModel.ProgressChangedEventArgs>(this.simulationComponent_ProgressChanged_1);
            this.simulationComponent.SimulationCompleted += new System.EventHandler<HearthstoneCurveSimulator.SimulationComponent.SimulationDoneEvent>(this.simulationComponent_SimulationCompleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonHero});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1123, 32);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonHero
            // 
            this.toolStripButtonHero.Checked = true;
            this.toolStripButtonHero.CheckOnClick = true;
            this.toolStripButtonHero.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonHero.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonHero.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHero.Image")));
            this.toolStripButtonHero.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHero.Name = "toolStripButtonHero";
            this.toolStripButtonHero.Size = new System.Drawing.Size(109, 29);
            this.toolStripButtonHero.Text = "Hero Power";
            this.toolStripButtonHero.CheckedChanged += new System.EventHandler(this.toolStripButtonHero_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 565);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.resultGraphControl1);
            this.Controls.Add(this.curveControl1);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.Text = "Hearthstone Mana Curve Simulator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private CurveControl curveControl1;
        private SimulationComponent simulationComponent;
        private ResultGraphControl resultGraphControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonHero;
    }
}

