using System;
using System.IO;

namespace MobilePayHomework
{
    public class Program
    {
        public static void Main(string[] args = null)
        {
            var lines = File.ReadAllText("./transactions.txt");
            var result = TransactionProcessor.Default().ProcessLines(lines);
            Console.WriteLine(string.Join("\r\n", result));
        }
    }
}
