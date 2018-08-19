using System;
using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public static class SeasonQueries
    {
        private const int _seasonStartMonth = 9;
        
        public static Season GetCurrentSeason(FcDeHoekContext context)
        {
            return GetSeasonByStartYear(context, GetStartYear()) ?? context.Seasons.OrderBy(s => s.SeasonStartYear).FirstOrDefault();
        }

        public static Season GetSeasonByStartYear(FcDeHoekContext context, int startYear)
        {
            return context.Seasons.FirstOrDefault(s => s.SeasonStartYear == startYear);
        }

        public static Season GetSeasonById(FcDeHoekContext context, int idSeason)
        {
            return context.Seasons.FirstOrDefault(s => s.IdSeason == idSeason);
        }

        private static int GetStartYear()
        {
            if (DateTime.Now.Month >=_seasonStartMonth && DateTime.Now.Month <= 12)
                return DateTime.Now.Year;

            return DateTime.Now.Year - 1;
        }
    }
}