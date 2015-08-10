using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HearthstoneCurveSimulator
{
    public partial class CurveControl : UserControl
    {
        private List<int> _deck = new List<int>();

        public CurveControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ManaCurve
        /// </summary>
        public List<int> Deck
        {
            get { return _deck; }
            set { _deck = value; }
        }

        /// <summary>
        /// Occurs when any of the numeric up downs have their value changed
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var tmpDeck = new List<int>();

            tmpDeck.AddRange(GetCards(1, numericUpDown1));
            tmpDeck.AddRange(GetCards(2, numericUpDown2));
            tmpDeck.AddRange(GetCards(3, numericUpDown3));
            tmpDeck.AddRange(GetCards(4, numericUpDown4));
            tmpDeck.AddRange(GetCards(5, numericUpDown5));
            tmpDeck.AddRange(GetCards(6, numericUpDown6));
            tmpDeck.AddRange(GetCards(7, numericUpDown7));
            tmpDeck.AddRange(GetCards(8, numericUpDown8));
            tmpDeck.AddRange(GetCards(9, numericUpDown9));
            tmpDeck.AddRange(GetCards(10, numericUpDown10));

            var tmpCurveData = tmpDeck.GroupBy(s => s).ToDictionary(s => s.Key, s => tmpDeck.Count(v => v == s.Key));
        
            chartManaCurve.Series["Mana"].Points.Clear();

            for (var i = 0; i <= 10; i++)
            {
                chartManaCurve.Series["Mana"].Points.AddXY(i,
                    tmpCurveData.ContainsKey(i) ? tmpCurveData[i] : 0);
            }

            OnDeckChanged(new DeckChangedEvent
                {
                    Deck = (Deck = tmpDeck),
                });
        }

        public event EventHandler<DeckChangedEvent> DeckChanged;

        protected virtual void OnDeckChanged(DeckChangedEvent e)
        {
            EventHandler<DeckChangedEvent> handler = DeckChanged;
            if (handler != null) handler(this, e);
        }

        public class DeckChangedEvent : EventArgs
        {
            public List<int> Deck { get; set; }
        }

        /// <summary>
        /// Given card cost and a numeric up down; returns an enumeration of cards.
        /// </summary>
        /// <param name="cardCost">The card cost</param>
        /// <param name="numericUpDown">The numeric up down</param>
        /// <returns>An enumeration of cards</returns>
        private static IEnumerable<int> GetCards(int cardCost, NumericUpDown numericUpDown)
        {
            for (var i = 0; i < numericUpDown.Value; i++)
            {
                yield return cardCost;
            }
        }
    }
}
