using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec28_ManualResetEventSlim
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
                Console.WriteLine("Waiting for water..");
                evt.Wait();
                Console.WriteLine("Here is your tea");
                evt.Wait(); // ManualRestEventSlim의 경우에는 true/false와 같다 한번 Set()이 실행되면, 그 뒤 Wait이 얼마나 오든지 패스한다
                evt.Wait();

            });

            makeTea.Wait();
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    } 
}
