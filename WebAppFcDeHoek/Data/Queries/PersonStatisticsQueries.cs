﻿using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public static class PersonStatisticsQueries
    {
        public static IQueryable<PersonStats> GetByIdPerson(FcDeHoekContext context, int idPerson)
        {
            return context.PersonStatistics.Where(ps => ps.IdPerson == idPerson);
        }
    }
}