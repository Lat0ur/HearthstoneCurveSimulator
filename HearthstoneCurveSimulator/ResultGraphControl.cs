using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HearthstoneCurveSimulator
{
    /// <summary>
    /// ResultGraphControl
    /// </summary>
    public partial class ResultGraphControl : UserControl
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ResultGraphControl"/>
        /// </summary>
        public ResultGraphControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Graph the simulation results
        /// </summary>
        /// <param name="simultionResults">The simulation results to graph</param>
        public void GraphResults(Dictionary<int, double> simultionResults)
        {
            if (simultionResults == null)
            {
                return;
            }

            chartResults.Annotations.Clear();
            chartResults.Series[0].Points.Clear();
            chartResults.Series[1].Points.Clear();

            var turn = 1;

            foreach (var kvp in simultionResults)
            {
                var mana = turn >= 10 ? 10 : turn;

                chartResults.Series[0].Points.AddXY(kvp.Key, kvp.Value);

                var manaMiss = mana - kvp.Value;
                var manaMissPct = (manaMiss / mana) * 100.0;

                var manaMissDataPoint = new DataPoint(kvp.Key, manaMiss);

                chartResults.Series[1].Points.Add(manaMissDataPoint);

                if (manaMissPct >= 50)
                {
                    chartResults.Annotations.Add(new CalloutAnnotation
                    {
                        AnchorDataPoint = manaMissDataPoint,
                        Name = Guid.NewGuid().ToString(),
                        Text = "Off Curve: " + manaMissPct + "%"
                    });
                }

                turn++;
            }
        }
    }
}
