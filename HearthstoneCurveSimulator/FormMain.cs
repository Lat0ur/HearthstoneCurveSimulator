using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void curveControl1_DeckChanged(object sender, CurveControl.DeckChangedEvent e)
        {
            toolStripStatusLabel1.Text = string.Format("Cards: {0}", e.Deck.Count);

            simulationComponent.Run(e.Deck);
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

        private void simulationComponent_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
    }
}
