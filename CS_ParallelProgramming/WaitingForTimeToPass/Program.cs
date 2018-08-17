using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTimeToPass
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm; you have 5 seconds");
                bool cancelled = token.WaitHandle.WaitOne(5000); // 일정시간 기다리기
                Console.WriteLine(cancelled ? "Bomb disarmed." : "Boom!!!");

                // SpinWait.SpinUntil(); // 지정된 조건을 마칠때까지 반복
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
