using FluentAssertions;
using System.IO;
using Xunit;

namespace MobilePayHomework
{
    public class TransactionProcessorTests
    {
        [Fact]
        public void Main_should_not_throw()
        {
            Program.Main(null);
        }

        [Fact]
        public void Transaction_fee_should_be_charged()
        {
            var sut = TransactionProcessor.Default(invoiceFixedFee: 0m, teliaDiscount: 0m, circleKDiscount: 0m);
            var lines = @"
2018-09-02 CIRCLE_K 120
2018-09-04 TELIA    200
2018-10-22 CIRCLE_K 300
2018-10-29 CIRCLE_K 150
";
            var actual = sut.ProcessLines(lines);
            actual.Should().BeEquivalentTo(new[]
            {
                new Fee("2018-09-02","CIRCLE_K", 1.20m),
                new Fee("2018-09-04","TELIA"   , 2.00m),
                new Fee("2018-10-22","CIRCLE_K", 3.00m),
                new Fee("2018-10-29","CIRCLE_K", 1.50m),
            });

        }

        [Fact]
        public void Telia_should_get_discount()
        {
            var sut = TransactionProcessor.Default(invoiceFixedFee: 0m);
            var lines = @"
2018-09-02 TELIA 120
2018-09-04 TELIA 200
2018-10-22 TELIA 300
2018-10-29 TELIA 150
";
            var actual = sut.ProcessLines(lines);
            actual.Should().BeEquivalentTo(new[]
            {
               new Fee("2018-09-02","TELIA", 1.08m),
               new Fee("2018-09-04","TELIA", 1.80m),
               new Fee("2018-10-22","TELIA", 2.70m),
               new Fee("2018-10-29","TELIA", 1.35m),
           });
        }

        [Fact]
        public void CircleK_should_get_discount()
        {
            var sut = TransactionProcessor.Default(invoiceFixedFee: 0m);
            var lines = @"
2018-09-02 CIRCLE_K 120
2018-09-04 CIRCLE_K 200
2018-10-22 CIRCLE_K 300
2018-10-29 CIRCLE_K 150
";
            var actual = sut.ProcessLines(lines);
            actual.Should().BeEquivalentTo(new[]
            {
               new Fee("2018-09-02","CIRCLE_K", 0.96m),
               new Fee("2018-09-04","CIRCLE_K", 1.60m),
               new Fee("2018-10-22","CIRCLE_K", 2.40m),
               new Fee("2018-10-29","CIRCLE_K", 1.20m),
           });
        }

        [Fact]
        public void Invoice_fixed_fee_should_be_charged()
        {
            var sut = TransactionProcessor.Default();
            var lines = @"
2018-09-02 7-ELEVEN 120
2018-09-04 NETTO    200
2018-10-22 7-ELEVEN 300
2018-10-29 7-ELEVEN 150
";
            var actual = sut.ProcessLines(lines);
            actual.Should().BeEquivalentTo(new[]
            {
               new Fee("2018-09-02","7-ELEVEN", 30.20m),
               new Fee("2018-09-04","NETTO"   , 31.00m),
               new Fee("2018-10-22","7-ELEVEN", 32.00m),
               new Fee("2018-10-29","7-ELEVEN", 1.50m),
           });
        }

        [Fact]
        public void Transactions_should_be_charged()
        {
            var lines = File.ReadAllText("./transactions.txt");
            var processor = TransactionProcessor.Default();
            var actual = processor.ProcessLines(lines);

            actual.Should().BeEquivalentTo(new[]
            {
               new Fee("2018-09-01","7-ELEVEN", 30.00m),
               new Fee("2018-09-04","CIRCLE_K", 29.80m),
               new Fee("2018-09-07","TELIA", 29.90m),
               new Fee("2018-09-09","NETTO", 30.00m),
               new Fee("2018-09-13","CIRCLE_K", 0.80m ),
               new Fee("2018-09-16","TELIA", 0.90m ),
               new Fee("2018-09-19","7-ELEVEN", 1.00m ),
               new Fee("2018-09-22","CIRCLE_K", 0.80m ),
               new Fee("2018-09-25","TELIA", 0.90m ),
               new Fee("2018-09-28","7-ELEVEN", 1.00m ),
               new Fee("2018-09-30","CIRCLE_K", 0.80m ),
               new Fee("2018-10-01","7-ELEVEN", 30.00m),
               new Fee("2018-10-04","CIRCLE_K", 29.80m),
               new Fee("2018-10-07","TELIA", 29.90m),
               new Fee("2018-10-10","NETTO", 30.00m),
               new Fee("2018-10-13","CIRCLE_K", 0.80m ),
               new Fee("2018-10-16","TELIA", 0.90m ),
               new Fee("2018-10-19","7-ELEVEN", 1.00m ),
               new Fee("2018-10-22","CIRCLE_K", 0.80m ),
               new Fee("2018-10-25","TELIA", 0.90m ),
               new Fee("2018-10-28","7-ELEVEN", 1.00m ),
               new Fee("2018-10-30","CIRCLE_K", 0.80m ),
           });
        }

        [Fact]
        public void Invoice_fee_not_charged_for_0_fee()
        {
            var sut = TransactionProcessor.Default();
            var lines = @"
2018-09-01 7-ELEVEN 0
2018-09-01 7-ELEVEN 100
2018-09-04 CIRCLE_K 100
2018-09-07 TELIA    100
2018-09-09 NETTO    100
2018-09-13 CIRCLE_K 100
2018-09-16 TELIA    100
2018-09-19 7-ELEVEN 100
2018-09-22 CIRCLE_K 100
2018-09-25 TELIA    100
2018-09-28 7-ELEVEN 100
2018-09-30 CIRCLE_K 100
                       
2018-10-01 7-ELEVEN 100
2018-10-04 CIRCLE_K 100
2018-10-07 TELIA    100
2018-10-10 NETTO    100
2018-10-13 CIRCLE_K 100
2018-10-16 TELIA    100
2018-10-19 7-ELEVEN 100
2018-10-22 CIRCLE_K 100
2018-10-25 TELIA    100
2018-10-28 7-ELEVEN 100
2018-10-30 CIRCLE_K 100
";
            var actual = sut.ProcessLines(lines);
            actual.Should().BeEquivalentTo(new[]
            {
                new Fee("2018-09-01","7-ELEVEN", 0m),
                new Fee("2018-09-01","7-ELEVEN", 30.00m),
                new Fee("2018-09-04","CIRCLE_K", 29.80m),
                new Fee("2018-09-07","TELIA", 29.90m),
                new Fee("2018-09-09","NETTO", 30.00m),
                new Fee("2018-09-13","CIRCLE_K", 0.80m ),
                new Fee("2018-09-16","TELIA", 0.90m ),
                new Fee("2018-09-19","7-ELEVEN", 1.00m ),
                new Fee("2018-09-22","CIRCLE_K", 0.80m ),
                new Fee("2018-09-25","TELIA", 0.90m ),
                new Fee("2018-09-28","7-ELEVEN", 1.00m ),
                new Fee("2018-09-30","CIRCLE_K", 0.80m ),
                new Fee("2018-10-01","7-ELEVEN", 30.00m),
                new Fee("2018-10-04","CIRCLE_K", 29.80m),
                new Fee("2018-10-07","TELIA", 29.90m),
                new Fee("2018-10-10","NETTO", 30.00m),
                new Fee("2018-10-13","CIRCLE_K", 0.80m ),
                new Fee("2018-10-16","TELIA", 0.90m ),
                new Fee("2018-10-19","7-ELEVEN", 1.00m ),
                new Fee("2018-10-22","CIRCLE_K", 0.80m ),
                new Fee("2018-10-25","TELIA", 0.90m ),
                new Fee("2018-10-28","7-ELEVEN", 1.00m ),
                new Fee("2018-10-30","CIRCLE_K", 0.80m ),
            });
        }

    }
}