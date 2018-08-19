using System;
using System.Collections.Generic;
using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public static class CalendarQueries
    {
        public static Calendar GetCalendarById(FcDeHoekContext context, int id)
        {
            return context.Calendars.FirstOrDefault(c => c.IdCalendar == id);
        }

        public static IQueryable<Calendar> GetCalendarsByIdSeason(FcDeHoekContext context, int idSeason)
        {
            return context.Calendars.Where(c => c.IdSeason == idSeason);
        }

        public static IQueryable<Calendar> GetCalendersByIdSeasonAndIdCompetition(FcDeHoekContext context, int idSeason, int idCompetition)
        {
            return GetCalendarsByIdSeason(context, idSeason).Where(c => c.IdCompetition == idCompetition);
        }

        public static Calendar GetNextCalendarGame(FcDeHoekContext context)
        {
            return context.Calendars.OrderBy(c => c.MatchDay).FirstOrDefault(c => c.MatchDay >= DateTime.Now);
        }
    }
}