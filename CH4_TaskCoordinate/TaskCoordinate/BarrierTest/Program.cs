using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierTest
{
    class Program
    {
        static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on (taks a bit longer)");
            Thread.Sleep(2000);
            barrier.SignalAndWait();
            Console.WriteLine("Pouring water into cup.");
            barrier.SignalAndWait();
            Console.WriteLine("Putting teh kettle away");
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea(fast)");
            barrier.SignalAndWait();
            Console.WriteLine("Adding tea");
            barrier.SignalAndWait();
            Console.WriteLine("Adding suger");
        }

        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
             {
                 Console.WriteLine("Enjoy your cup of tea.");
             });
            Task.WaitAll(tea);
            Console.ReadKey();
        }
    }
}
