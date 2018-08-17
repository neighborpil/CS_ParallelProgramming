using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec28_2_AutoResetEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new AutoResetEvent(false); // false 설정

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set(); // true로 변한다
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                evt.WaitOne(); // false로 변한다
                Console.WriteLine("Here is your tea");
                //evt.WaitOne(); //true가 되지 않는 이상 freezing 한다
                var ok = evt.WaitOne(1000); // false
                if (ok)
                {
                    Console.WriteLine("Enjoy your tea");
                }
                else
                    Console.WriteLine("No tea for you");
            });

            makeTea.Wait();
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
