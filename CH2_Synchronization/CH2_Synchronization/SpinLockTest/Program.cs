using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpinLockTest
{
    public class BankAccount
    {
        private int balance;

        public int Balance { get => balance; set => balance = value; }

        public void Deposit(int amount)
        {
            balance += amount;
        }

        public void WithDraw(int amount)
        {
            balance -= amount;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            SpinLock sl = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.WithDraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Balance is {ba.Balance}");
            Console.ReadKey();
        }
    }
}
