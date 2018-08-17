using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLock
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

        public void Transfer(BankAccount where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }
    }

    class Program
    {
        static ReaderWriterLockSlim padLock = new ReaderWriterLockSlim();
        static Random random = new Random();

        static void Main(string[] args)
        {
            int x = 0;

            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    padLock.EnterReadLock();
                    Console.WriteLine($"Entered read lock, x = {x}");
                    Thread.Sleep(3000);
                    padLock.ExitReadLock();
                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padLock.EnterWriteLock();
                Console.WriteLine("Write lock acquired");
                int newValue = random.Next(10);
                x = newValue;
                Console.WriteLine($"Set x = {x}");
                padLock.ExitWriteLock();
                Console.WriteLine("Write lock released");

            }
        }
    }
}
