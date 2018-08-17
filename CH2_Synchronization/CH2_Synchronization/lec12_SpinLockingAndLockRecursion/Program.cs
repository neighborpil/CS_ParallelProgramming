using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lec12_SpinLockingAndLockRecursion
{
    /*b
    Spinlock 사용법
    lock하지 않아도 도니다
    deadlock방지 가능
    */
    public class BankAccount
    {
        private int balance;

        public int Balance { get => balance; set => balance = value; }

        public void Deposit(int amount)
        {
            balance += amount;
        }

        public void Withdraw(int amount)
        {
            balance -= amount;
        }
    }

    class Program
    {
        static SpinLock sl = new SpinLock(true);

        public static void LockRecursion(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch(LockRecursionException e)
            {
                Console.WriteLine($"Exception: {e}");
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a loc, x = {x}");
                    LockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Faile to take a lock, x = {x}");
                }
            }
        }

        static void Main(string[] args)
        {
            #region Example1

            //var tasks = new List<Task>();
            //var ba = new BankAccount();

            //SpinLock sl = new SpinLock();

            //for (int i = 0; i < 10; i++)
            //{
            //    tasks.Add(Task.Factory.StartNew(() =>
            //    {
            //        for (int j = 0; j < 1000; j++)
            //        {
            //            var lockTaken = false;
            //            try
            //            {
            //                sl.Enter(ref lockTake
            //                ba.Deposit(100);
            //            }
            //            finally
            //            {
            //                if (lockTaken)
            //                    sl.Exit();
            //            }
            //        }
            //    }));

            //    tasks.Add(Task.Factory.StartNew(() => {
            //        for (int j = 0; j < 1000; j++)
            //        {
            //            bool lockTaken = false;
            //            try
            //            {
            //                sl.Enter(ref lockTaken);
            //                ba.Withdraw(100);
            //            }
            //            finally
            //            {
            //                if (lockTaken)
            //                    sl.Exit();
            //            }
            //        }
            //    }));
            //}

            //Task.WaitAll(tasks.ToArray());
            //Console.WriteLine($"Balance is {ba.Balance}");
            #endregion

            LockRecursion(5);

            Console.ReadKey();
        }
    }
}
