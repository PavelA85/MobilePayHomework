using System.Linq;
using FluentAssertions;
using Xunit;

namespace MobilePayHomework
{
    public class Tests
    {
        [Fact]
        public void MainTest()
        {
            Program.Main(null);
        }

        [Fact]
        public void UserStory2_discount_for_telia()
        {
            var sut = new InvoicingPipeline(@"
2018-09-02 CIRCLE_K1 120
2018-09-04 TELIA1    200
2018-10-22 CIRCLE_K1 300
2018-10-29 CIRCLE_K1 150
");
            sut.Output.Select(x => x.Value).Should().BeEquivalentTo(new[]
            {
                new Fee("2018-09-02","CIRCLE_K1", 1.20m),
                new Fee("2018-09-04","TELIA1"   , 2.00m),
                new Fee("2018-10-22","CIRCLE_K1", 3.00m),
                new Fee("2018-10-29","CIRCLE_K1", 1.50m),
            });

        }
        [Fact]
        public void UserStory3_discount_for_telia()
        {
           var sut =  new InvoicingPipeline(@"
2018-09-02 TELIA 120
2018-09-04 TELIA 200
2018-10-22 TELIA 300
2018-10-29 TELIA 150
");
           sut.Output.Select(x => x.Value).Should().BeEquivalentTo(new[]
           {
               new Fee("2018-09-02","TELIA", 1.08m),
               new Fee("2018-09-04","TELIA", 1.80m),
               new Fee("2018-10-22","TELIA", 2.70m),
               new Fee("2018-10-29","TELIA", 1.35m),
           });
        }
        [Fact]
        public void UserStory4_discount_for_telia()
        {
           var sut =  new InvoicingPipeline(@"
2018-09-02 CIRCLE_K 120
2018-09-04 CIRCLE_K 200
2018-10-22 CIRCLE_K 300
2018-10-29 CIRCLE_K 150
");
           sut.Output.Select(x => x.Value).Should().BeEquivalentTo(new[]
           {
               new Fee("2018-09-02","CIRCLE_K", 0.96m),
               new Fee("2018-09-04","CIRCLE_K", 1.60m),
               new Fee("2018-10-22","CIRCLE_K", 2.40m),
               new Fee("2018-10-29","CIRCLE_K", 1.20m),
           });
        }
        [Fact]
        public void UserStory5_discount_for_telia()
        {
           var sut =  new InvoicingPipeline(@"
2018-09-02 7-ELEVEN 120
2018-09-04 NETTO    200
2018-10-22 7-ELEVEN 300
2018-10-29 7-ELEVEN 150
");
           sut.Output.Select(x => x.Value).Should().BeEquivalentTo(new[]
           {
               new Fee("2018-09-02","7-ELEVEN", 30.20m),
               new Fee("2018-09-04","NETTO"   , 31.00m),
               new Fee("2018-10-22","7-ELEVEN", 32.00m),
               new Fee("2018-10-29","7-ELEVEN", 1.50m),
           });
        }
    }
}