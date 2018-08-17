using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionHandlingTest
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
            var t1 = new Task(() =>
            {
                throw new InvalidOperationException("Can't do this!") { Source = "t1" };
            });
            t1.Start();

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't do this!") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {

                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid Op!"); // exception으로 처리 X
                        return true;
                    }
                    else
                        return false;
                });
            }
        }
    }
}
