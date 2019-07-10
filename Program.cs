using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MobilePayHomework
{
    public class Program
    {
        public static void Main(string[] args = null)
        {
            var lines = args ?? File.ReadAllLines("./transactions.txt");
            var output = new Dictionary<Transaction, Fee>();
            foreach (var line in lines)
            {
                var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (!items.Any())
                    continue;

                var date = DateTime.Parse(items[0]);
                var merchant = items[1];
                var amount = decimal.Parse(items[2]);
                var transaction = new Transaction(date, merchant, amount);
                Console.WriteLine(transaction);
                output.Add(transaction, Fee.FromTransaction(transaction));
            }
            Console.WriteLine("Hello World!");
        }
    }

    public class Transaction
    {
        public DateTime Date { get; }
        public string Merchant { get; }
        public decimal Amount { get; }

        public Transaction(in DateTime date, string merchant, decimal amount)
        {
            Date = date;
            Merchant = merchant;
            Amount = amount;
        }
        public override string ToString()
        {
            return $"{nameof(Date)}: {Date:yyyy-MM-dd}, {nameof(Merchant)}: {Merchant,-15}, {nameof(Amount)}: {Amount:F2}";
        }

    }

    public class Fee
    {
        public DateTime Date { get; }
        public string Merchant { get; }
        public decimal Amount { get; }
        public Fee(string date, string merchant, decimal amount) : this(DateTime.Parse(date), merchant, amount)
        {
        }
        public Fee(DateTime date, string merchant, decimal amount)
        {
            Date = date;
            Merchant = merchant;
            Amount = amount;
        }

        public static Fee FromTransaction(Transaction input, decimal invoiceFee = 0)
        {
            var charge = 0.01m;
            var discount = 0m;
            if (input.Merchant == "TELIA")
            {
                discount = 0.1m;
            }
            else if (input.Merchant == "CIRCLE_K")
            {
                discount = 0.2m;
            }

            var amount = input.Amount * charge;
            return new Fee(input.Date, input.Merchant, amount * (1m - discount) + invoiceFee);
        }

        public override string ToString()
        {
            return $"{nameof(Date)}: {Date:yyyy-MM-dd}, {nameof(Merchant)}: {Merchant,-15}, {nameof(Amount)}: {Amount:F2}";
        }
    }

    public class InvoicingPipeline
    {
        public readonly Dictionary<Transaction, Fee> Output = new Dictionary<Transaction, Fee>();

        public InvoicingPipeline(string lines, bool invoiceFee = false) 
            : this(lines.Split("\r\n"), invoiceFee)
        {
        }

        public InvoicingPipeline(IEnumerable<string> lines, bool invoiceFee = false)
        {
            foreach (var line in lines)
            {
                var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (!items.Any())
                    continue;

                var date = DateTime.Parse(items[0]);
                var merchant = items[1];
                var amount = decimal.Parse(items[2]);
                var transaction = new Transaction(date, merchant, amount);
                var invoiceFeeAmount = 0m;
                if (invoiceFee && !Output.Any(x => x.Key.Merchant == merchant && x.Key.Date.Year == date.Year && x.Key.Date.Month == date.Month))
                {
                    invoiceFeeAmount = 29m;
                }
                Output.Add(transaction, Fee.FromTransaction(transaction, invoiceFeeAmount));
            }

        }
    }
}
