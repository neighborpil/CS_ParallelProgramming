using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec11_InterlockedOperation
{
    public class BankAccount
    {
        private int balance;

        public int Balance { get => balance; set => balance = value; }

        public void Deposit(int amount)
        {
            Interlocked.Add(ref balance, amount);

            /*
            # 메모리 배리어

            1
            2
            Interlocked.MemoryBarrier();
            3

            의 순서로 프로세스가 동작한다고 했을 때  
            CPU는 1, 2, 3순서가 아니라, 3, 1, 2 등으로 순서가 일정하지 않을 수 있다
            이 때에 MemoryBarrier 적어주면 배리어 이전 코드는 메모리 배리어 이후를 
            넘어 갈 수 없다는 것이다
             */
            Interlocked.MemoryBarrier();
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref balance, -amount);
        }
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() => {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}.");
            Console.ReadKey();
        }
    }
}
