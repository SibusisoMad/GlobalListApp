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

        static void Main(string[] args)
        {
            
            Thread primeThread = new Thread(GeneratePrimeNumbers.Run);
            primeThread.Start();

            while (globalList.Count < 250000)
            {
                Thread.Sleep(100);
            }

            while (totalCount < 1000000)
            {
                Thread.Sleep(100);
            }

            stopThreads = true;

            primeThread.Join();

            List<int> sortedList = globalList.OrderBy(num => num).ToList();
            int oddCount = sortedList.Count(num => num % 2 != 0);
            int evenCount = sortedList.Count(num => num % 2 == 0);

            Console.WriteLine("Odd Count: " + oddCount);
            Console.WriteLine("Even Count: " + evenCount);

            GlobalListUtility.Serialization.Serializer.SerializeToBinary(sortedList, "global_list.bin");
            GlobalListUtility.Serialization.Serializer.SerializeToXml(sortedList, "global_list.xml");

            Console.WriteLine("Serialization completed.");

            Console.ReadLine();
        }

    }
}