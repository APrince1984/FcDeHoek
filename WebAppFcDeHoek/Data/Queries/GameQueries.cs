using System;
using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public static class GameQueries
    {
        public static Game GetById(FcDeHoekContext context, int idGame)
        {
            return context.Games.FirstOrDefault(g => g.IdGame == idGame);
        }

        public static IQueryable<Game> GetAllGamesBetweenDates(FcDeHoekContext context, DateTime startDate,
            DateTime endDate)
        {
            return context.Games.Where(g => g.MatchDate >= startDate && g.MatchDate <= endDate);
        }

        public static IQueryable<Game> GetAllGamesByIdTeam(FcDeHoekContext context, int idTeam)
        {
            return context.Games.Where(g => g.IdHomeTeam == idTeam || g.IdAwayTeam == idTeam);
        }

        public static IQueryable<Game> GetAllGamesByIdSeasonAndIdTeam(FcDeHoekContext context, int idSeason, int idTeam)
        {
            return GetAllGamesByIdTeam(context, 1).Where(g => g.IdSeason == idSeason);
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

        public static Game GetNextGame(FcDeHoekContext context)
        {
            return GetAllGamesByIdTeam(context, 1).OrderBy(g => g.MatchDate).FirstOrDefault(g => g.MatchDate > DateTime.Now);
        }

        public static IQueryable<Game> GetNextGames(FcDeHoekContext context)
        {
            var gameFcDeHoek = GetNextGame(context);
            if (gameFcDeHoek == null)
                return null;

            return context.Games.Where(g => g.MatchDate == gameFcDeHoek.MatchDate);
        }

        public static Game GetPreviousGame(FcDeHoekContext context)
        {
            return GetAllGamesByIdTeam(context, 1).OrderByDescending(g => g.MatchDate).FirstOrDefault(g => g.MatchDate <= DateTime.Now);
        }
    }
}