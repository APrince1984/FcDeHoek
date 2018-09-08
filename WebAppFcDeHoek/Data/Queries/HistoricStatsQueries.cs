using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public static class HistoricStatsQueries
    {
        public static IQueryable<HistoricStats> GetByIdPlayer(FcDeHoekContext context, int idPlayer)
        {
            return context.HistoricStatses.Where(hs => hs.IdPerson == idPlayer);
        }
    }
}