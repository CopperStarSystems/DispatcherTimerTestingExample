using System;

namespace ConsoleAppTest
{  
    /// <summary>
    /// Runs a unit test for the Ticker class.
    /// </summary>
    class Program
    {
        static void Main()
        {
            Console.WriteLine("About to run TickerTest...");
            Console.WriteLine();
			
			// Run the unit test.
            TickerTest tickerTest = new TickerTest();
            tickerTest.SetUp();
			tickerTest.TickerHonorsIntervalAndNumberOfTicks();

            Console.WriteLine();
            Console.WriteLine("TickerTest finished.");

            Console.ReadKey();
        }        
    }   
}