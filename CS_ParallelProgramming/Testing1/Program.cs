using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing1
{
    class Program
    {
        static void Main(string[] args)
        {
            // test1 -> using lamda
            //Task task1 = new Task(() => write('c'));
            //task1.Start();
            //Task.Factory.StartNew(() => write(1));
            //Task.Factory.StartNew(() => write('f', 2));

            // test2 
            //Task task1 = new Task(write, 'c');
            //task1.Start();
            //Task.Factory.StartNew(write, '2');
            ////Task.Factory.StartNew(write, new object[] { '3', 1 }); 안됨, 안에서 object  나눠줘야

            // test3 - reulst
            Task<int> task1 = new Task<int>(() => length("Testing"));
            task1.Start();
            Task<int> task2 =Task.Factory.StartNew(() => length("test"));

            Console.WriteLine($"Task1 : {task1.Result}");
            Console.WriteLine($"Task2 : {task2.Result}");

            Console.ReadKey();

        }

        public static int length(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}");
            return o.ToString().Length;
        }


        public static void write(char c, int i32)
        {
            int i = 1000;
            while(i-- > 0)
            {
                Console.Write($"{c}{i32}");
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

        public static void write(object o)
        {
            int i = 1000;
            while(i-- > 0)
            {
                Console.Write(o);
            }
        }
    }
}
