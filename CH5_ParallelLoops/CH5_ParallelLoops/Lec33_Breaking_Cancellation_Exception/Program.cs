using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec33_Breaking_Cancellation_Exception
{
    class Program
    {
        public static void Demo()
        {
            Parallel.For(0, 20, (int x, ParallelLoopState state) =>
            {
                Console.WriteLine($"{x}[{Task.CurrentId}]\t");

                if(x == 10)
                {

                }
            });
        }
        static void Main(string[] args)
        {
            Demo();

            Console.ReadKey();
        }
    }
}
