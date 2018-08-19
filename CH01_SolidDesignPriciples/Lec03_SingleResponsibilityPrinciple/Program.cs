using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec03_SingleResponsibilityPrinciple
{
    public class Journal
    {
        /*
        Single Resonsibility Principle
         - 하나의 클래스는 하나의 하나의 일만 해야한다
        */
        private readonly List<string> entries = new List<string>();
        private static int count = 0;
        public int AddEntry(string text)
        {
            entries.Add($"{++count} : {text}");
            return count; //memento pattern
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        /* 현재 Journal class는 single responsibility principle을 위반하고 있다
        entries를 저장할 뿐만 아니라 managing하고 있기 때문이다 */

        //public void Save(string fileName)
        //{
        //    File.WriteAllText(fileName, ToString());
        //}

        //public static Journal Load(string fileName)
        //{

        //}

        //public void Load(Uri uri)
        //{

        //}
    }

    // Persistence(하드에 오래 보관하는 기능)을 따로 분리하였다
    public class Persistence
    {
        public void SaveToFile(Journal j, string fileName, bool overwrite = false)
        {
            if(overwrite || !File.Exists(fileName))
             {
                File.WriteAllText(fileName, j.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");
            Console.WriteLine(j);

            var p = new Persistence();
            var fileName = $@"C:\temp\persistence.txt";
            p.SaveToFile(j, fileName, true);
            Process.Start(fileName);
        }
    }
}
