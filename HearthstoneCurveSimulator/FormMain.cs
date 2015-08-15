using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace HearthstoneCurveSimulator
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// curveControl1_DeckChanged
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void curveControl1_DeckChanged(object sender, CurveControl.DeckChangedEvent e)
        {
            statusLabel.Text = string.Format("Cards: {0}", e.Deck.Count);

            simulationComponent.Deck = e.Deck.ToArray();
            simulationComponent.Run();
        }

        /// <summary>
        /// simulationComponent_SimulationCompleted
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void simulationComponent_SimulationCompleted(object sender, SimulationComponent.SimulationDoneEvent e)
        {
            resultGraphControl1.GraphResults(e.Results);
        }

        /// <summary>
        /// simulationComponent_ProgressChanged_1
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void simulationComponent_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// FormMain_Load
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            propertyGridSimuation.SelectedObject = simulationComponent;
        }

        /// <summary>
        /// Closes <see cref="FormMain"/>
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// newToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curveControl1.LoadDeck(simulationComponent.New());
        }

        /// <summary>
        /// saveToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogMain.ShowDialog(this);
        }

        /// <summary>
        /// saveAsToolStripMenuItem_Clicks
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// simulationComponent_SimulationParametersChanged
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void simulationComponent_SimulationParametersChanged(object sender, EventArgs e)
        {
            simulationComponent.Run();
        }

        /// <summary>
        /// saveFileDialogMain_FileOk
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void saveFileDialogMain_FileOk(object sender, CancelEventArgs e)
        {
            // if saving fails cancel is set to true
            e.Cancel = !simulationComponent.Save(saveFileDialogMain.FileName);
        }

        /// <summary>
        /// openToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogMain.ShowDialog(this);
        }

        /// <summary>
        /// openFileDialogMain_FileOk
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void openFileDialogMain_FileOk(object sender, CancelEventArgs e)
        {
            if (!(e.Cancel = !simulationComponent.Load(openFileDialogMain.FileName)))
            {
                curveControl1.LoadDeck(simulationComponent.Deck);
            }

            propertyGridSimuation.SelectedObject = simulationComponent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simulationComponent.Deck = new int[0];
            curveControl1.LoadDeck(new int[0]);
        }
    }
}
