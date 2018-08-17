using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec34_ThreadLocalStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            //int sum = 0;
            //Parallel.For(0, 1000, i =>
            //{
            //    Interlocked.Add(ref sum, i); // interlock이 많이 걸린다
            //});

            int sum = 0;

            Parallel.For(1, 1001,
                () => 0, // 초기값
                (x, state, tls) => // body : value, state, ThreadLocalStorage // 임시 저장소로써 각 스레드의 결과값이 tls에 저장된다
                {
                    tls += x;
                    Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                    return tls;
                },
                partialSum => // finally : 결과값 : 각 스레드의 결과값을 최종적으로 더한다
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                    Interlocked.Add(ref sum, partialSum); // 마지막에 한번 sum에 interlocked로 접근한다
                });
            Console.WriteLine($"Sum of 1 - 1000 = {sum}");
            Console.ReadKey();
        }
    }
}
