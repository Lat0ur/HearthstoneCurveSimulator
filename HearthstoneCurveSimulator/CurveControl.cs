using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HearthstoneCurveSimulator
{
    /// <summary>
    /// CurveControl
    /// </summary>
    public partial class CurveControl : UserControl
    {
        /// <summary>
        /// The backing store for the deck.
        /// </summary>
        private List<int> _deck = new List<int>();

        /// <summary>
        /// Creates a new <see cref="CurveControl"/>
        /// </summary>
        public CurveControl()
        {
            InitializeComponent();
        }

        public bool EventsDisabled { get; set; }

        /// <summary>
        /// LoadDeck
        /// </summary>
        /// <param name="deck"></param>
        public void LoadDeck(int[] deck)
        {
            var tmpCurveData = DeckToCurve(deck);

            lock (this)
            {
                EventsDisabled = true;

                var index = 1;

                numericUpDown1.Value = tmpCurveData[index++];
                numericUpDown2.Value = tmpCurveData[index++];
                numericUpDown3.Value = tmpCurveData[index++];
                numericUpDown4.Value = tmpCurveData[index++];
                numericUpDown5.Value = tmpCurveData[index++];
                numericUpDown6.Value = tmpCurveData[index++];
                numericUpDown7.Value = tmpCurveData[index++];
                numericUpDown8.Value = tmpCurveData[index++];
                numericUpDown9.Value = tmpCurveData[index++];
                numericUpDown10.Value = tmpCurveData[index];

                EventsDisabled = false;

                numericUpDown_ValueChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// DeckToCurve will convert a deck format into a distribution format
        /// </summary>
        /// <param name="deck">the deck to convert</param>
        /// <returns></returns>
        private static Dictionary<int, int> DeckToCurve(int[] deck)
        {
            var tmpReturn = new Dictionary<int, int>();

            for (var i = 1; i <= 10; i++)
            {
                tmpReturn.Add(i, deck.Count(s => s == i));
            }

            return tmpReturn;
        }

        /// <summary>
        /// ManaCurve
        /// </summary>
        public List<int> Deck
        {
            get { return _deck; }
            set
            {
                _deck = value;

                OnDeckChanged(new DeckChangedEvent
                    {
                        Deck = value
                    });
            }
        }

        /// <summary>
        /// Occurs when any of the numeric up downs have their value changed
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EventsDisabled)
            {
                return;
            }

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


            Deck = tmpDeck;
        }

        /// <summary>
        /// DeckChanged
        /// </summary>
        public event EventHandler<DeckChangedEvent> DeckChanged;

        /// <summary>
        /// OnDeckChanged
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnDeckChanged(DeckChangedEvent e)
        {
            var handler = DeckChanged;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// DeckChangedEvent
        /// </summary>
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
