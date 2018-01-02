using ClassLib;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace NUnitTests
{   
    /// <summary>
    /// Tests if a Ticker ticks the number of times it 
    /// is supposed to over a certain period of time.
    /// </summary>
	[TestFixture]
    public class TickerTest : IntegrationTestBase
    {
        #region Data

        Ticker systemUnderTest;
        TickerSettings _settings;

        #endregion // Data

		#region ConfigureTickerSettings

		/// <summary>
		/// Initializes data used in the test.
		/// </summary>
		[SetUp]
		public void ConfigureTickerSettings()
		{
			TimeSpan interval = TimeSpan.FromSeconds(1);
			int numberOfTicks = 3;
			DispatcherPriority priority = DispatcherPriority.Normal;

			// VERY IMPORTANT!!!
			// For the DispatcherTimer to tick when it is not running
			// in a normal WPF application, you must give it a priority
			// higher than 'Background' (which is the default priority). 
			// In this demo we give it a priority of 'Normal'.
			_settings = new TickerSettings(interval, numberOfTicks, priority);
		}

		#endregion // ConfigureTickerSettings

		#region TickerHonorsIntervalAndNumberOfTicks

		[Test]
		public void TickerHonorsIntervalAndNumberOfTicks()
		{
			systemUnderTest = null;  
		    
			base.BeginExecuteTest();

			Assert.IsNotNull(systemUnderTest, "systemUnderTest should have been assigned a value.");
			Assert.AreEqual(3, systemUnderTest.Ticks);
		}

		#endregion // TickerHonorsIntervalAndNumberOfTicks

		#region Asynchronous Test Logic

		protected override void ExecuteTestAsync()
		{
			Debug.Assert(base.IsRunningOnWorkerThread);

			// Note: The object which creates a DispatcherTimer
			// must create it with the Dispatcher for the worker
			// thread.  Creating the Ticker on the worker thread
			// ensures that its DispatcherTimer uses the worker
			// thread's Dispatcher.
			systemUnderTest = new Ticker(_settings);

			// Tell the Ticker to start ticking.
			systemUnderTest.Start();

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