namespace HearthstoneCurveSimulator
{
    partial class SimulationComponent
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.simulationWorker = new System.ComponentModel.BackgroundWorker();
            // 
            // simulationWorker
            // 
            this.simulationWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.simulationWorker_DoWork);
            this.simulationWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.simulationWorker_ProgressChanged);
            this.simulationWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.simulationWorker_RunWorkerCompleted);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker simulationWorker;
    }
}
