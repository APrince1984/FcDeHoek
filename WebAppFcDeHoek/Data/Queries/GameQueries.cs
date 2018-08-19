using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public static class GameQueries
    {
        public static IQueryable<Game> GetAllGamesByIdTeam(FcDeHoekContext context, int idTeam)
        {
            return context.Games.Where(g => g.IdHomeTeam == idTeam || g.IdAwayTeam == idTeam);
        }

        public static IQueryable<Game> GetAllGamesByIdSeason(FcDeHoekContext context, int idSeason)
        {
            return context.Games.Where(g => g.IdSeason == idSeason);
        }

        public static IQueryable<Game> GetAllGamesByIdSeasonAndIdCompetition(FcDeHoekContext context, int idSeason,
            int idCompetition)
        {
            return GetAllGamesByIdSeason(context, idSeason).Where(g => g.IdCompetition == idCompetition);
        }
    }
}