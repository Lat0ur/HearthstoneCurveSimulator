using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace HearthstoneCurveSimulator
{
    /// <summary>
    /// SimulationComponent
    /// <remarks>
    /// Responsible for performing the simulation</remarks>
    /// </summary>
    public partial class SimulationComponent : Component
    {
        [Description("The number of iterations to run.")]
        public int Iterations
        {
            get { return _iterations; }
            set
            {
                if (_iterations == value) return;
                OnSimulationParametersChanged();
                _iterations = value;
            }
        }

        [Description("The deck to use for simulation")]
        public int[] Deck
        {
            get { return _deck; }
            set
            {
                _deck = value;
                OnSimulationParametersChanged();
            }
        }

        [Description("The name of the deck")]
        public string DeckName { get; set; }

        [Description("The amount of shuffling to do")]
        public int ShuffleVeracity
        {
            get { return _shuffleVeracity; }
            set
            {
                if (_shuffleVeracity == value) return;
                _shuffleVeracity = value;
                OnSimulationParametersChanged();
            }
        }

        [Description("Use hero power")]
        public bool UseHeroPower
        {
            get { return _useHeroPower; }
            set
            {
                if (_useHeroPower == value) return;
                _useHeroPower = value;
                OnSimulationParametersChanged();
            }
        }

        /// <summary>
        /// CardUsage
        /// </summary>
        [Description("Card usage behavior")]
        public CardUsageBehavior CardUsage
        {
            get { return _cardUsage; }
            set
            {
                if (_cardUsage == value) return;
                _cardUsage = value;
                OnSimulationParametersChanged();
            }
        }

        /// <summary>
        /// The deck limit
        /// </summary>
        private const int DeckLimit = 30;

        /// <summary>
        /// The mana limit
        /// </summary>
        private const int ManaLimit = 10;

        /// <summary>
        /// The RNG (used for shuffle)
        /// </summary>
        private readonly Random _random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// the deck backing store
        /// </summary>
        private int[] _deck = new int[0];

        /// <summary>
        /// The backing store for shuffle veracity
        /// </summary>
        private int _shuffleVeracity = 300;

        /// <summary>
        /// parameter for use hero power
        /// </summary>
        private bool _useHeroPower;

        /// <summary>
        /// parameter for card usage
        /// </summary>
        private CardUsageBehavior _cardUsage;

        /// <summary>
        /// The number of iterations to use for the simulation.
        /// </summary>
        private int _iterations;

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
                Deck = e.Argument as int[];

                tmpSimResults.Add(i, Simulate() ?? EmptyResultSet);

                simulationWorker.ReportProgress(GetPercentComplete(i));
            }

            e.Result = AggregateResults(tmpSimResults);
        }

        /// <summary>
        /// The empty result set;
        /// <remarks>prevents new() from being called for each iteration</remarks>
        /// </summary>
        private readonly static Dictionary<int, int> EmptyResultSet =
            new Dictionary<int, int>();

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
        private int GetPercentComplete(int current)
        {
            return (int)((1.0 * current / Iterations * 1.0) * 100.0);
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
        /// SimulationParametersChanged
        /// </summary>
        public event EventHandler<EventArgs> SimulationParametersChanged;

        /// <summary>
        /// OnSimulationParametersChanged
        /// </summary>
        protected virtual void OnSimulationParametersChanged()
        {
            var handler = SimulationParametersChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Run the simulation with the passed deck
        /// </summary>
        public void Run()
        {
            if (simulationWorker.IsBusy)
            {
                return;
            }

            simulationWorker.RunWorkerAsync(Deck);
        }

        /// <summary>
        /// Simulates card draw and returns results as a map
        /// </summary>
        /// <returns>the result map</returns>
        private Dictionary<int, int> Simulate()
        {
            if (Deck == null)
            {
                return null;
            }

            var tmpResult = new Dictionary<int, int>();

            var deck = new List<int>(Deck);
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

                if (UseHeroPower)
                {
                    hand.Add(2);
                }

                while (hand.Any(s => s <= mana))
                {
                    var playableCards = hand.Where(s => s <= mana).ToArray();

                    int tmpValue;

                    switch (CardUsage)
                    {
                        case CardUsageBehavior.UseBigCardsFirst:
                            tmpValue = playableCards.Max();
                            break;
                        case CardUsageBehavior.UseSmallCardsFirst:
                            tmpValue = playableCards.Min();
                            break;
                        case CardUsageBehavior.UseRandomCard:
                            tmpValue = playableCards[_random.Next(playableCards.Length)];
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    hand.Remove(tmpValue);

                    mana -= tmpValue;
                    dmg += tmpValue;
                }

                if (UseHeroPower)
                {
                    hand.Remove(2);
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

        /// <summary>
        /// CardUsageBehavior
        /// </summary>
        public enum CardUsageBehavior
        {
            UseBigCardsFirst,
            UseSmallCardsFirst,
            UseRandomCard
        }

        /// <summary>
        /// Creates a new deck
        /// <remarks>The deck is randomly weighted</remarks>
        /// </summary>
        public int[] New()
        {
            var tmpDeck = new List<int>();

            while (tmpDeck.Count < DeckLimit)
            {
                tmpDeck.Add(_random.Next(1, ManaLimit));
            }

            return Deck = tmpDeck.ToArray();
        }


        /// <summary>
        /// Save the deck and simulation parameters
        /// </summary>
        /// <param name="strFullPath">the file path</param>
        public bool Save(string strFullPath)
        {
            try
            {
                var aObj = new CurveSimulation
                {
                    Deck = Deck,
                    Name = DeckName,
                    Iterations = Iterations,
                    CardUsageBehavior = CardUsage,
                    ShuffleVeracity = ShuffleVeracity,
                    UseHeroPower = UseHeroPower,
                };

                using (var tmpRawStream = File.Open(strFullPath, File.Exists(strFullPath) ? FileMode.Truncate : FileMode.CreateNew))
                using (var tmpXmlWriter = new XmlTextWriter(tmpRawStream, new System.Text.UTF8Encoding()))
                {
                    new XmlSerializer(aObj.GetType()).Serialize(tmpXmlWriter, aObj);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region saving and serialization

        [Serializable]
        public class CurveSimulation
        {
            [XmlElement]
            public int[] Deck { get; set; }

            [XmlAttribute]
            public string Name { get; set; }

            [XmlAttribute]
            public int Iterations { get; set; }

            [XmlAttribute]
            public CardUsageBehavior CardUsageBehavior { get; set; }

            [XmlAttribute]
            public int ShuffleVeracity { get; set; }

            [XmlAttribute]
            public bool UseHeroPower { get; set; }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFullPath"></param>
        /// <returns></returns>
        public bool Load(string strFullPath)
        {
            if (string.IsNullOrEmpty(strFullPath) || !File.Exists(strFullPath))
            {
                return false;
            }

            using (Stream fs = new FileStream(strFullPath, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(CurveSimulation));
                var tmp = serializer.Deserialize(fs) as CurveSimulation;

                if (tmp == null) return false;

                lock (this)
                {
                    // unwire parameters changed events
                    var tmpEvent = SimulationParametersChanged;
                    SimulationParametersChanged = null;

                    Deck = tmp.Deck;
                    UseHeroPower = tmp.UseHeroPower;
                    CardUsage = tmp.CardUsageBehavior;
                    Iterations = tmp.Iterations;
                    ShuffleVeracity = tmp.ShuffleVeracity;
                    DeckName = tmp.Name;

                    // rewire parameters changed events
                    SimulationParametersChanged = tmpEvent;
                }

                OnSimulationParametersChanged();
            }

            return true;
        }
    }
}
