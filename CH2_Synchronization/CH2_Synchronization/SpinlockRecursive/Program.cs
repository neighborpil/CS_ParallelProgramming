using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpinlockRecursive
{
    class Program
    {
        static SpinLock sl = new SpinLock();

        public static void LockRecursive(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch(LockRecursionException e)
            {
                Console.WriteLine($"Exception : {e}");
            }
            finally
            { 
                // spinlock을 사용하면서 recursive에서 어디가 에러났는지 확인이 가능하다
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    LockRecursive(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Fail to take a lock, x = {x}");
                }
            }
        }

        static void Main(string[] args)
        {
            LockRecursive(5);
            Console.ReadKey();
        }
    }
}
