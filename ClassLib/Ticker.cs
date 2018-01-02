using System;
using System.Windows.Threading;

namespace ClassLib
{
    /// <summary>
    /// Prints 'Tick!' to the console a certain number of times
    /// over a specified period of time.  This is the class which
    /// is tested in this demo app.
    /// </summary>
    public class Ticker
    {
        #region Data

        int _ticks;
        readonly DispatcherTimer _timer;
        readonly TickerSettings _settings;

        #endregion // Data

        #region Constructor

        public Ticker(TickerSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            _settings = settings;

            _timer = new DispatcherTimer(_settings.Priority);
            _timer.Interval = _settings.Interval;
            _timer.Tick += this.OnTimerTick;
        }

        #endregion // Constructor

        #region OnTimerTick

        /// <summary>
        /// Handles the DispatcherTimer's Tick event.
        /// </summary>
        void OnTimerTick(object sender, EventArgs e)
        {
            Console.WriteLine("Tick!");
            ++this.Ticks;
        }

        #endregion // OnTimerTick

        #region Start

        /// <summary>
        /// Tells the Ticker to begin ticking.
        /// </summary>
        public void Start()
        {
            this.Ticks = 0;
            _timer.Start();
        }

        #endregion // Start

        #region Ticks

        /// <summary>
        /// Returns the number of times the Ticker has ticked.
        /// </summary>
        public int Ticks
        {
            get { return _ticks; }
            private set
            {
                _ticks = value;
                if (_ticks == _settings.NumberOfTicks)
                {
                    _timer.Stop();
                }
            }
        }

        #endregion // Ticks
    }
}