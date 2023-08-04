using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalListApp.Threads
{
    public class GenerateOddNumbers
    {
        public static void Run()

        {
            int number = 1;

            while(!Program.stopThreads)
            {
                lock(Program.lockObject)
                {
                    if(number % 2 != 0)
                    {
                        Program.globalList.Add(number);
                        Program.totalCount++;
                    }
                    number++;
                }
            }
        }
    }
}
