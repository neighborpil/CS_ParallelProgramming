using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_ParallelProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            // 람다 쓰는 방법
            //Task.Factory.StartNew(() => write('.'));

            //var t = new Task(() => write('?'));
            //t.Start();
            //write('-');

            // 람다 + 변수 전달 방법
            //Task t = new Task(write, "hello");
            //t.Start();
            //Task.Factory.StartNew(write, 123);

            //아이디 확인
            string text1 = "Testing", text2 = "this";
            var task1 = new Task<int>(TextLength, text1);
            task1.Start();
            Task<int> task2 = Task.Factory.StartNew(TextLength, text2);

            Console.WriteLine($"Length of '{text1}' is {task1.Result}");
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id{Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }



        public static void write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static void write(char c)
        {
            int i = 1000;
            while(i-- > 0)
            {
                Console.Write(c);
            }
        }
    }
}
