using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEventTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });

            Task makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                evt.WaitOne();
                Console.WriteLine("Here is your tea");
                var ok = evt.WaitOne(1000);
                if (ok)
                    Console.WriteLine("Enjoy your tea");
                else
                    Console.WriteLine("No tea for you");
            });

            makeTea.Wait();
            Console.ReadKey();
        }
    }
}
