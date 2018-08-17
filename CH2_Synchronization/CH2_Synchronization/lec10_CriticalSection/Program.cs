using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lec10_CriticalSection
{
    public class BackAccount
    {
        // 기본적인 locking 사용 방법

        public object padlock = new object();
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            /*
            += 연산자의 경우
            op1 : temp <= get_Balance() + amount
            op2 : set_Balance(temp)
            의 두가지 연산으로 나누어져 있다
            따라서 두 연산 사이에 다른 연산이 끼어 들 수 있다

            */
            lock (padlock)
            {
                Balance += amount;

            }
        }

        public void Withdraw(int amount)
        {
            lock (padlock)
            {
                Balance -= amount;

            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BackAccount();

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
