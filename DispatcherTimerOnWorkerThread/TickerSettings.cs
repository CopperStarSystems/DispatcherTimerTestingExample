using System;
using System.Windows.Threading;

namespace DispatcherTimerOnWorkerThread
{
    /// <summary>
    /// Provides values used to configure a Ticker object.
    /// </summary>
    class TickerSettings
    {
        #region Data

        readonly TimeSpan _interval;
        readonly int _numberOfTicks;
        readonly DispatcherPriority _priority;

        #endregion // Data

        #region Constructor

        public TickerSettings(TimeSpan interval, int numberOfTicks, DispatcherPriority priority)
        {
            if (interval.Ticks < 1)
                throw new ArgumentOutOfRangeException("interval");

            if (numberOfTicks < 1)
                throw new ArgumentOutOfRangeException("numberOfTicks");

            if (priority == DispatcherPriority.Invalid)
                throw new ArgumentException("priority cannot be 'Invalid'");

            _interval = interval;
            _numberOfTicks = numberOfTicks;
            _priority = priority;
        }

        #endregion // Constructor

        #region Properties

        /// <summary>
        /// The amount of time between ticks.
        /// </summary>
        public TimeSpan Interval
        {
            get { return _interval; }
        }

        /// <summary>
        /// The number of times a Ticker should 
        /// print 'Tick!' to the console.
        /// </summary>
        public int NumberOfTicks
        {
            get { return _numberOfTicks; }
        }

        /// <summary>
        /// The priority given to a Ticker's DispatcherTimer.
        /// </summary>
        public DispatcherPriority Priority
        {
            get { return _priority; }
        }

        #endregion // Properties
    }     
}