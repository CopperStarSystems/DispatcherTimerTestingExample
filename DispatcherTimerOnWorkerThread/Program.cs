using System;

namespace DispatcherTimerOnWorkerThread
{  
    /// <summary>
    /// Runs a unit test for the Ticker class.
    /// </summary>
    class Program
    {
        static void Main()
        {
            Console.WriteLine("About to run the TickerTest...");
            Console.WriteLine();

            TickerTest tickerTest = new TickerTest();
            tickerTest.SetUp();
            tickerTest.TestIfTickerOnlyTicksThreeTimes();

            Console.WriteLine();
            Console.WriteLine("TickerTest finished.");

            Console.ReadKey();
        }        
    }   
}