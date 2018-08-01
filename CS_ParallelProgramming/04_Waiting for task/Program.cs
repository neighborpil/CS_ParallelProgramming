using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _04_Waiting_for_task
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");

                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I'm done!");
            }, token);
            t.Start();

            //t.Wait(token); // Task.WaitAll(); // 모든 task를 기다린다

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            // Task.WaitAll(t, t2); // 둘다 기다림
            // Task.WaitAny(t, t2); // 둘중 하나가 끝나면 메인스레드 진행
            // Task.WaitAny(new[] { t, t2 }, 4000, token); // 둘중 하나가 종료될때까지 4초 기다린다

            //Console.ReadKey();
            //cts.Cancel();

            Task.WaitAll(new[] { t, t2 }, 4000, token); // 둘 다 끝날때까지 4초 기다린다

            Console.WriteLine($"Task t status is {t.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            Console.WriteLine("Main program done." );
            Console.ReadKey();
        }
    }
}
