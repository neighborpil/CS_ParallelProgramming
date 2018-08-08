using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec17_ConcurrentDictionary
{
    class Program
    {
        private static ConcurrentDictionary<string, string> capitals = 
            new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? $"Task {Task.CurrentId}" :
                "Main Thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not added")} the element.");
        }

        static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            //capitals["Russia"] = "Leningrad";
            //capitals["Russia"] = "Moscow"; // 키가 있는데 등록된다, value 교체
            //capitals.AddOrUpdate("Russia", "Moscow", 
            //    (k, old) => old + " --> Moscow"); 
            //    //  key, value, 키가 존재할 경우 부여할 value

            //Console.WriteLine($"Capital of Russia is {capitals["Russia"]}");

            capitals["Sweden"] = "uppsala";
            var capOfSweeden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The capital of sweden is {capOfSweeden}");

            const string toRemove = "Russia";
            var didRemove = capitals.TryRemove(toRemove, out string removed);
            if (didRemove)
                Console.WriteLine($"We just removed {removed}");
            else
                Console.WriteLine($"Failed to remove the capital of {toRemove}");

            foreach (var capital in capitals)
            {
                Console.WriteLine($" - {capital.Value} is the capital of {capital.Key}");
            }

            Console.ReadKey();
        }
    }  
}
