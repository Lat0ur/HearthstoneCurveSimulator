using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HearthstoneCurveSimulator
{
    public partial class ResultGraphControl : UserControl
    {
        public ResultGraphControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simultionResults"></param>
        public void GraphResults(Dictionary<int, double> simultionResults)
        {
            chartResults.Series[0].Points.Clear();
            chartResults.Series[1].Points.Clear();

            var turn = 1;

            foreach (var kvp in simultionResults)
            {
                var mana = turn >= 10 ? 10 : turn;

                chartResults.Series[0].Points.AddXY(kvp.Key, kvp.Value);
                chartResults.Series[1].Points.AddXY(kvp.Key, mana - kvp.Value);

                turn++;
            }
        }
    }
}
