using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HearthstoneCurveSimulator
{
    public partial class SimulationComponent : Component
    {
        [Description("The number of iterations to run.")]
        public int Iterations { get; set; }

        [Description("The deck to use for simulation")]
        public IEnumerable<int> Deck { get; set; }

        [Description("Set card usage order")]
        public bool UseBigCardsFirst { get; set; }

        /// <summary>
        /// Creates a new <see cref="SimulationComponent"/>
        /// </summary>
        public SimulationComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new <see cref="SimulationComponent"/>
        /// </summary>
        /// <param name="container"></param>
        public SimulationComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Run the simulation in the background
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void simulationWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var tmpSimResults = new Dictionary<int, Dictionary<int, int>>();

            for (var i = 0; i < Iterations; i++)
            {
                tmpSimResults.Add(i, Simulate(e.Argument as List<int>));

                OnProgressChanged(GetPercentComplete(i));
            }

            e.Result = AggregateResults(tmpSimResults);
        }

        /// <summary>
        /// Collects the results of all iterations and averages all of the curves.
        /// </summary>
        /// <param name="resultSet">The collection of result sets</param>
        /// <returns>The final results; notice probabilities move to double precision.</returns>
        private static Dictionary<int, double> AggregateResults(Dictionary<int, Dictionary<int, int>> resultSet)
        {
            var tmpReturn = new Dictionary<int, int>();

            foreach (var i in resultSet.Keys.Select(r => resultSet[r]).SelectMany(runInfo => runInfo))
            {
                if (tmpReturn.ContainsKey(i.Key))
                {
                    tmpReturn[i.Key] += i.Value;
                }
                else
                {
                    tmpReturn.Add(i.Key, i.Value);
                }
            }

            return tmpReturn.ToDictionary(i => i.Key, i =>
                1.0 * i.Value / resultSet.Count);
        }

        /// <summary>
        /// Just does the percentage completed math
        /// </summary>
        /// <param name="current">The current iteration</param>
        /// <returns>a progress changed event arg</returns>
        private ProgressChangedEventArgs GetPercentComplete(int current)
        {
            return new ProgressChangedEventArgs((int)((1.0 * current / Iterations * 1.0) * 100.0), null);
        }

        /// <summary>
        /// Fires the progress changed event delegate
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void simulationWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnProgressChanged(e);
        }

        /// <summary>
        /// ProgressChanged
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        /// <summary>
        /// OnProgressChanged invocation
        /// </summary>
        /// <param name="e">event args</param>
        private void OnProgressChanged(ProgressChangedEventArgs e)
        {
            var handler = ProgressChanged;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// The RNG (used for shuffle)
        /// </summary>
        private readonly Random _random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// The backing store for shuffle veracity
        /// </summary>
        private int _shuffleVeracity = 300;

        /// <summary>
        /// The amount of shuffles to perform.
        /// </summary>
        public int ShuffleVeracity
        {
            get { return _shuffleVeracity; }
            set { _shuffleVeracity = value; }
        }

        /// <summary>
        /// Run the simulation with the passed deck
        /// </summary>
        /// <param name="deck">the deck to run the simulation on</param>
        public void Run(IEnumerable<int> deck)
        {
            if (simulationWorker.IsBusy)
            {
                return;
            }

            simulationWorker.RunWorkerAsync(deck);
        }

        /// <summary>
        /// Simulates card draw and returns results as a map
        /// </summary>
        /// <param name="aDeck">The deck to perform the simulation over</param>
        /// <returns>the result map</returns>
        private Dictionary<int, int> Simulate(IEnumerable<int> aDeck)
        {
            var tmpResult = new Dictionary<int, int>();

            var deck = new List<int>(aDeck);
            var hand = new List<int>();

            if (deck.Count <= 5)
            {
                return tmpResult;
            }

            for (var i1 = 0; i1 < ShuffleVeracity; i1++)
            {
                var sourceCardIndex = _random.Next(((IList<int>)deck).Count);
                var targetCardIndex = _random.Next(((IList<int>)deck).Count);

                var sourceCardValue = ((IList<int>)deck)[sourceCardIndex];
                var targetCardValue = ((IList<int>)deck)[targetCardIndex];

                ((IList<int>)deck)[targetCardIndex] = sourceCardValue;
                ((IList<int>)deck)[sourceCardIndex] = targetCardValue;
            }

            var turns = 0;

            for (var i = 0; i < 5; i++)
            {
                hand.Add(deck[0]);
                deck.RemoveAt(0);
            }

            while (hand.Any())
            {
                var dmg = 0;

                var mana = ++turns > 10 ? 10 : turns;

                while (hand.Any(s => s <= mana))
                {
                    var playableCards = hand.Where(s => s <= mana).ToArray();
                    var tmpValue = UseBigCardsFirst ? playableCards.Max() : playableCards.Min();

                    hand.Remove(tmpValue);

                    mana -= tmpValue;
                    dmg += tmpValue;
                }

                tmpResult.Add(turns, dmg);

                if (deck.Count <= 0)
                {
                    continue;
                }

                if (hand.Count >= 10)
                {
                    deck.RemoveAt(0);
                }
                else
                {
                    hand.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }

            return tmpResult;
        }

        /// <summary>
        /// Fires the run worker completed event 
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the argument</param>
        private void simulationWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSimulationCompleted(new SimulationDoneEvent
                {
                    Results = e.Result as Dictionary<int, double>
                });
        }

        /// <summary>
        /// The simulation completed event
        /// </summary>
        public event EventHandler<SimulationDoneEvent> SimulationCompleted;

        /// <summary>
        /// Event invocation for when simulation completion event
        /// </summary>
        /// <param name="e">The done event args</param>
        protected virtual void OnSimulationCompleted(SimulationDoneEvent e)
        {
            var handler = SimulationCompleted;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// The done event argument event args class
        /// </summary>
        public class SimulationDoneEvent : EventArgs
        {
            public Dictionary<int, double> Results { get; set; }
        }
    }
}
