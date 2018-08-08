using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lec13_2_Mutex2
{
    class Program
    {
        static void Main(string[] args)
        {
            const string appName = "MyApp";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch (WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine("we can run the program just fine");
                mutex = new Mutex(false, appName);
            }

            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}
