using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GlobalListApp.Threads
{
   public class Program
    {
        public static List<int> globalList = new List<int>();
        public static int totalCount = 0;
        public static object lockObject = new object();
        public static bool stopThreads = false;

        private static readonly ILogger Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("GlobalListLog.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


        public static void Main(string[] args)
        {
            try
            {

                Thread oddNumberThread = new Thread(GenerateOddNumbers.Run);
                Thread primeNumberThread = new Thread(GeneratePrimeNumbers.Run);
                Thread evenNumberThread = new Thread(GenerateEvenNumbers.Run);

                oddNumberThread.Start();
                primeNumberThread.Start();

                while (globalList.Count < 250000)
                {
                    Thread.Sleep(100);
                }

                evenNumberThread.Start();

                while (totalCount < 1000000)
                {
                    Thread.Sleep(100);
                }

                stopThreads = true;

                oddNumberThread.Join();
                primeNumberThread.Join();
                evenNumberThread.Join();

                List<int> sortedList = globalList.OrderBy(num => num).ToList();

                int oddCount = sortedList.Count(num => num % 2 != 0);
                int evenCount = sortedList.Count(num => num % 2 == 0);

                Console.WriteLine("Odd Count: " + oddCount);
                Console.WriteLine("Even Count: " + evenCount);

                GlobalListUtility.Serialization.Serializer.SerializeToBinary(sortedList, "global_list.bin");
                GlobalListUtility.Serialization.Serializer.SerializeToXml(sortedList, "global_list.xml");

                Logger.Information("Serialization completed. Sorted List Count: {SortedListCount}, Odd Count: {OddCount}, Even Count: {EvenCount}", sortedList.Count, oddCount, evenCount);

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An unhandled exception occurred: {ErrorMessage}", ex.Message);
            }
            
        }

    }
}