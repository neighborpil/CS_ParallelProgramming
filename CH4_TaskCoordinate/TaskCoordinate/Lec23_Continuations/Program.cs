using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec23_Continuations
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
            });

            var tast2 = task.ContinueWith(t => { // t는 위에서 끝이난 스레드를 말한다
                Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
            });

            tast2.Wait(); // task1, 2가 순차적으로 실행이 된다.

            Console.ReadKey();
        } 
    }
}
