using System;
using System.Collections.Generic;
using System.Linq;

namespace MobilePayHomework
{
    public class Transaction
    {
        public DateTime Date { get; }
        public string Merchant { get; }
        public decimal Amount { get; }

        public Transaction(in DateTime date, in string merchant, in decimal amount)
        {
            Date = date;
            Merchant = merchant;
            Amount = amount;
        }
        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} {Merchant,-8} {Amount:F2}";
        }
    }

    public class Fee
    {
        public DateTime Date { get; }
        public string Merchant { get; }
        public decimal Amount { get; set; }
        public Fee(in string date, in string merchant, in decimal amount)
            : this(DateTime.Parse(date), merchant, amount)
        {
        }
        public Fee(in DateTime date, in string merchant, in decimal amount)
        {
            Date = date;
            Merchant = merchant;
            Amount = amount;
        }

        public static Fee FromTransaction(in Transaction transaction)
        {
            return new Fee(transaction.Date, transaction.Merchant, 0m);
        }

        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} {Merchant,-8} {Amount:F2}";
        }
    }

    public class TransactionProcessor
    {
        private const decimal TransactionPercentageFee = 0.01m;
        private const decimal TeliaDiscount = 0.10m;
        private const decimal CircleKDiscount = 0.20m;
        private const decimal InvoiceFixedFee = 29m;

        private readonly List<IPipeline> _pipelines = new List<IPipeline>();

        public TransactionProcessor(params IPipeline[] pipelines)
        {
            _pipelines.AddRange(pipelines);
        }

        public static TransactionProcessor Default(
            in decimal transactionPercentageFee = TransactionPercentageFee,
            in decimal teliaDiscount = TeliaDiscount,
            in decimal circleKDiscount = CircleKDiscount,
            in decimal invoiceFixedFee = InvoiceFixedFee)
        {
            return new TransactionProcessor(
                new TransactionFeeProcessor(transactionPercentageFee)
                , new CircleKPipeline(circleKDiscount)
                , new TeliaPipeline(teliaDiscount)
                , new InvoiceFeePipeline(invoiceFixedFee));
        }

        public IEnumerable<Fee> ProcessLines(string lines)
        {
            return lines
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(TransactionParser.Do)
                .Select(Process);
        }

        public Fee Process(Transaction transaction)
        {
            var fee = Fee.FromTransaction(transaction);
            _pipelines.ForEach(pipeline => pipeline.Calculate(transaction, fee));
            return fee;
        }
    }

    public interface IPipeline
    {
        Fee Calculate(in Transaction transaction, in Fee fee);
    }

    public class TransactionFeeProcessor : IPipeline
    {
        private readonly decimal _transactionPercentageFee;

        public TransactionFeeProcessor(in decimal transactionPercentageFee)
        {
            _transactionPercentageFee = transactionPercentageFee;
        }

        public Fee Calculate(in Transaction transaction, in Fee fee)
        {
            fee.Amount = transaction.Amount * _transactionPercentageFee;
            return fee;
        }
    }

    public class CircleKPipeline : IPipeline
    {
        private const string CircleK = "CIRCLE_K";
        private readonly decimal _transactionFeePercentageDiscount;

        public CircleKPipeline(decimal transactionFeePercentageDiscount)
        {
            _transactionFeePercentageDiscount = transactionFeePercentageDiscount;
        }

        public Fee Calculate(in Transaction transaction, in Fee fee)
        {
            if (transaction.Merchant != CircleK)
            {
                return fee;
            }
            fee.Amount *= 1 - _transactionFeePercentageDiscount;
            return fee;

        }
    }

    public class TeliaPipeline : IPipeline
    {
        private const string Telia = "TELIA";
        private readonly decimal _transactionFeePercentageDiscount;

        public TeliaPipeline(in decimal transactionFeePercentageDiscount)
        {
            _transactionFeePercentageDiscount = transactionFeePercentageDiscount;
        }

        public Fee Calculate(in Transaction transaction, in Fee fee)
        {
            if (transaction.Merchant != Telia)
            {
                return fee;
            }
            fee.Amount *= 1 - _transactionFeePercentageDiscount;
            return fee;

        }
    }

    public class InvoiceFeePipeline : IPipeline
    {
        private readonly List<Transaction> _memeroy = new List<Transaction>();
        private readonly decimal _invoiceFixedFee;

        public InvoiceFeePipeline(in decimal invoiceFixedFee)
        {
            _invoiceFixedFee = invoiceFixedFee;
        }

        public Fee Calculate(in Transaction t, in Fee fee)
        {
            var transaction = t;

            bool ThisMonth(Transaction x) =>
                x.Merchant == transaction.Merchant
                && x.Date.Year == transaction.Date.Year
                && x.Date.Month == transaction.Date.Month;

            if (_memeroy.Any(ThisMonth))
            {
                return fee;
            }
            
            _memeroy.Add(t);

            if (fee.Amount > 0)
            {
                fee.Amount += _invoiceFixedFee;
            }

            return fee;
        }
    }

    public static class TransactionParser
    {
        public static Transaction Do(string line)
        {
            var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!items.Any())
            {
                return null;
            }

            var date = DateTime.Parse(items[0]);
            var merchant = items[1];
            var amount = decimal.Parse(items[2]);
            var transaction = new Transaction(date, merchant, amount);
            return transaction;
        }
    }
}