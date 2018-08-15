using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEventSlimTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new ManualResetEventSlim();

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                evt.Wait(); //one time trigger
                Console.WriteLine("Enjoy your tea");
                evt.Wait();

            });

            makeTea.Wait();

            Console.ReadKey();
        }
    }
}
