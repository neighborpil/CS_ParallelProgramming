using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {
                // 한번더 trycatch로 묶음으로써, handleing 할 수 있는것(true반환)하는것은 처리하고
                // 나머지 exception은 메세지 띄운다
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handled elsewhere : {e.GetType()}");
                }
            }
            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        private static void Test()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this!") { Source = "t" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            //try
            //{
            //    Task.WaitAll(t, t2);
            //}
            //catch (AggregateException ae)
            //{
            //    foreach (var e in ae.InnerExceptions)
            //    {
            //        Console.WriteLine($"Exception {e.GetType()} from {e.Source}");
            //    }
            //}

            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    if (e is InvalidOperationException) // 핸들링 할 수 있는 것
                    {
                        Console.WriteLine("Invalid op!");
                        return true;
                    }
                    else
                        return false; // 아닌 것은 넘겨버리고 위에서 Exception 처리를 한다
                });
            }
        }
    }
}
