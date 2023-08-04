using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalListApp.Threads
{
    public static class GeneratePrimeNumbers
    {
        public static void Run()
        {
            int number = 2;

            while(!Program.stopThreads)
            {
                lock (Program.lockObject)
                {
                    if(IsPrime(number))
                    {
                        Program.globalList.Add(number);
                        Program.totalCount++;
                    }

                    number++;

                }

            }
        }

        private static bool IsPrime(int number)
        {
            if(number < 2)
                return false;

            for(int i = 2; i <= Math.Sqrt(number); i++)
            {
                if(number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
