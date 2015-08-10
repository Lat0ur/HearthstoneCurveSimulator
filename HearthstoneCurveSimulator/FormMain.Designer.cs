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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.resultGraphControl1 = new HearthstoneCurveSimulator.ResultGraphControl();
            this.curveControl1 = new HearthstoneCurveSimulator.CurveControl();
            this.simulationComponent = new HearthstoneCurveSimulator.SimulationComponent(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStripMain.Size = new System.Drawing.Size(938, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 395);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(938, 43);
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
            this.resultGraphControl1.Location = new System.Drawing.Point(545, 29);
            this.resultGraphControl1.Name = "resultGraphControl1";
            this.resultGraphControl1.Size = new System.Drawing.Size(381, 361);
            this.resultGraphControl1.TabIndex = 4;
            // 
            // curveControl1
            // 
            this.curveControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.curveControl1.Deck = ((System.Collections.Generic.List<int>)(resources.GetObject("curveControl1.Deck")));
            this.curveControl1.Location = new System.Drawing.Point(13, 29);
            this.curveControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.curveControl1.Name = "curveControl1";
            this.curveControl1.Size = new System.Drawing.Size(525, 361);
            this.curveControl1.TabIndex = 3;
            this.curveControl1.DeckChanged += new System.EventHandler<HearthstoneCurveSimulator.CurveControl.DeckChangedEvent>(this.curveControl1_DeckChanged);
            // 
            // simulationComponent
            // 
            this.simulationComponent.Deck = null;
            this.simulationComponent.Iterations = 500;
            this.simulationComponent.ShuffleVeracity = 300;
            this.simulationComponent.UseBigCardsFirst = false;
            this.simulationComponent.SimulationCompleted += new System.EventHandler<HearthstoneCurveSimulator.SimulationComponent.SimulationDoneEvent>(this.simulationComponent_SimulationCompleted);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 438);
            this.Controls.Add(this.resultGraphControl1);
            this.Controls.Add(this.curveControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.Text = "Hearthstone Mana Curve Simulator";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private CurveControl curveControl1;
        private SimulationComponent simulationComponent;
        private ResultGraphControl resultGraphControl1;
    }
}

