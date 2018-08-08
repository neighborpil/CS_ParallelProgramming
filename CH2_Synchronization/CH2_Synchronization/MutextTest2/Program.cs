using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutextTest2
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
            catch(WaitHandleCannotBeOpenedException e)
            {
                mutex = new Mutex(false, appName);
                Console.WriteLine("We can run the program just fine");
            }
            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}
