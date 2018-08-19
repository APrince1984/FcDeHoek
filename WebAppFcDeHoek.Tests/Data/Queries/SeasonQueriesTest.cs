using System;
using System.Linq;
using NUnit.Framework;
using WebAppFcDeHoek.Data;

namespace WebAppFcDeHoek.Tests.Data.Queries
{
    [TestFixture]
    public class SeasonQueriesTest
    {
        [Test]
        public void SeasonContainsData()
        {
            using (var context = new FcDeHoekContext())
            {
                var seasons = context.Seasons.ToList();
                Assert.IsNotNull(seasons);
                Assert.IsNotEmpty(seasons);
                foreach (var season in seasons)
                {
                    Console.WriteLine(
                        $"show me {season.IdSeason} - {season.SeasonStartYear} - {season.SeasonEndYear} - {season.Division}");
                }
            }
        }
    }
}
