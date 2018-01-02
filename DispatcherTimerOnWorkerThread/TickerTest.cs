using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace DispatcherTimerOnWorkerThread
{   
    /// <summary>
    /// Tests if a Ticker ticks the number of times it 
    /// is supposed to over a certain period of time.
    /// </summary>
    class TickerTest : TestWithActiveDispatcher
    {
        #region Data

        Ticker _ticker;
        TickerSettings _settings;

        #endregion // Data

        #region Public Methods

        /// <summary>
        /// Initializes data used in the test.
        /// </summary>
        public void SetUp()
        {
            TimeSpan interval = TimeSpan.FromSeconds(2);
            int numberOfTicks = 3;
            DispatcherPriority priority = DispatcherPriority.Normal;

            // VERY IMPORTANT!!!
            // For the DispatcherTimer to tick when it is not running
            // in a normal WPF application, you must give it a priority
            // higher than 'Background' (which is the default priority). 
            // In this demo we give it a priority of 'Normal'.
            _settings = new TickerSettings(interval, numberOfTicks, priority);
        }

        /// <summary>
        /// Performs a test on the Ticker class.
        /// </summary>
        public void TestIfTickerOnlyTicksThreeTimes()
        {
            _ticker = null;  
                      
            base.BeginExecuteTest();

            Debug.Assert(_ticker != null, "_ticker should have been assigned a value.");

            // Print the results of the test.  In a real unit test
            // this would be the place to perform asserts.
            Console.WriteLine();
            Console.WriteLine("***************************");
            Console.WriteLine("RESULT:");

            bool testPassed = _ticker.Ticks == _settings.NumberOfTicks;
            Console.WriteLine("Test Passed: " + testPassed);

            Console.WriteLine("Expected ticks: " + _settings.NumberOfTicks);
            Console.WriteLine("Actual ticks: " + _ticker.Ticks);

            Console.WriteLine("***************************");
        }

        #endregion // Public Methods

        #region Asynchronous Test Logic

        protected override void ExecuteTestAsync()
        {
            Debug.Assert(base.IsRunningOnWorkerThread);

            // Note: The object which creates a DispatcherTimer
            // must create it with the Dispatcher for the worker
            // thread.  Creating the Ticker on the worker thread
            // ensures that its DispatcherTimer uses the worker
            // thread's Dispatcher.
            _ticker = new Ticker(_settings);

            // Tell the Ticker to start ticking.
            _ticker.Start();

            // Give the Ticker some time to do its work.
            TimeSpan waitTime = this.CalculateWaitTime();
            base.WaitWithoutBlockingDispatcher(waitTime);

            // Let the base class know that the test is over
            // so that it can turn off the worker thread's
            // message pump.
            base.EndExecuteTest();
        }

        TimeSpan CalculateWaitTime()
        {
            Debug.Assert(base.IsRunningOnWorkerThread);

            // Calculate how much time the Ticker needs to perform 
            // all of it's ticks.  Add some extra time to make sure 
            // it does not tick more than it should.       
            int numTicks = _settings.NumberOfTicks + 1;
            int tickInterval = (int)_settings.Interval.TotalSeconds;
            int secondsToWait = numTicks * tickInterval;
            TimeSpan waitTime = TimeSpan.FromSeconds(secondsToWait);

            return waitTime;
        }

        #endregion // Asynchronous Test Logic
    }
}