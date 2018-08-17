using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec32_Parallel_InvokeForForeach
{
    class Program
    {
        static CancellationTokenSource cts = new CancellationTokenSource();

        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i+=step)
            {
                yield return i;
            }
        }

        static void Main(string[] args)
        {
            var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));

            Parallel.Invoke(a, b, c); // 3개가 다끝날때까지 기다린다

            ////////////////

            var po = new ParallelOptions();
            po.CancellationToken = cts.Token;
            Parallel.For(1, 11, po, i =>
            {
                Console.Write($"{i * i}\t");
            }); // 시작 index로부터 끝 인덱스까지 각각 끝이 나기를 기다린다.
            Console.WriteLine();

            ///////////////////////////

            // Parallel.For의 경우 step이 1밖에 안된다
            // 따라서 더 큰 step을 사용하고 싶다면 yield 를 사용해 메소드를 만들고 Foreach를 돌려야 한다
            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);


            //////////////////



            string[] words = { "oh", "what", "a", "night" };
            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} has length {word.Length} (Task {Task.CurrentId})");
            });

            Console.ReadKey();
            
        }
    }
}
