using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Models;
using WebAppFcDeHoek.Structs;

namespace WebAppFcDeHoek.Controllers
{
    public class RankingController : Controller
    {
        // GET: Ranking
        public ActionResult Index()
        {
            var ranking = new RankingModel
            {
                Season = SetSeason(),
                Ranking = BuildLeagueRanking()
            };
            
            return View(ranking);
        }

        private static string SetSeason()
        {
            using (var context = new FcDeHoekContext())
            {
                var season = SeasonQueries.GetCurrentSeason(context);
                return $"{season.SeasonStartYear} - {season.SeasonEndYear}";
            }
        }

        private List<RankModel> BuildLeagueRanking()
        {
            using (var context = new FcDeHoekContext())
            {
                var model = new List<RankModel>();
                var allGames = GameQueries.GetAllGamesByIdSeasonAndIdCompetition(context,
                    SeasonQueries.GetCurrentSeason(context).IdSeason, eCompetitionType.League).ToList();

                

                var allPlayedGames = allGames.Where(g => g.GoalsHomeTeam != null && g.GoalsAwayTeam != null && g.MatchDate <= DateTime.Today.Date).ToList();

                var teams = new List<string>();
                teams.AddRange(allGames.Select(g => g.GameHomeTeam.Name).Distinct().ToList());
                teams.AddRange(allGames.Select(g => g.GameAwayTeam.Name).Distinct().ToList());
                teams = teams.Distinct().ToList();

                if (teams.Count < allPlayedGames.Count && allPlayedGames.Count == 0) 
                    return CreateZeroRanking(teams);

                foreach (var team in teams)
                {
                    var ranking = new RankModel{Team = team};

                    var homeGames = allPlayedGames.Where(g => g.GameHomeTeam.Name == team).ToList();
                    foreach (var homeGame in homeGames)
                    {
                        ranking.GoalsScored += homeGame.GoalsHomeTeam ?? 0;
                        ranking.GoalsConceded += homeGame.GoalsAwayTeam ?? 0;

                        if (homeGame.GoalsHomeTeam > homeGame.GoalsAwayTeam)
                            ranking.GamesWon += 1;
                        else if (homeGame.GoalsHomeTeam == homeGame.GoalsAwayTeam)
                            ranking.GamesDrawn += 1;
                        else
                            ranking.GamesLost += 1;

                        ranking.GamesPlayed++;
                    }

                    var awayGames = allPlayedGames.Where(g => g.GameAwayTeam.Name == team).ToList();
                    foreach (var awayGame in awayGames)
                    {
                        ranking.GoalsScored += awayGame.GoalsAwayTeam ?? 0;
                        ranking.GoalsConceded += awayGame.GoalsHomeTeam ?? 0;

                        if (awayGame.GoalsAwayTeam > awayGame.GoalsHomeTeam)
                            ranking.GamesWon += 1;
                        else if (awayGame.GoalsHomeTeam == awayGame.GoalsAwayTeam)
                            ranking.GamesDrawn += 1;
                        else
                            ranking.GamesLost += 1;

                        ranking.GamesPlayed++;
                    }

                    model.Add(ranking);
                }

                foreach (var ranking in model)
                    ranking.Points = (ranking.GamesWon * 3) + ranking.GamesDrawn;

                return model.OrderByDescending(m => m.Points).ThenByDescending(m => m.GamesWon).ThenByDescending(m => (m.GoalsScored - m.GoalsConceded)).ThenByDescending(m => m.GoalsScored).ToList();
            }
        }

        private List<RankModel> CreateZeroRanking(List<string> teams)
        {
            var rankingList = new List<RankModel>();
            foreach (var team in teams)
            {
                rankingList.Add(new RankModel
                {
                    Team = team,
                    GamesPlayed = 0,
                    GamesWon = 0,
                    GamesDrawn = 0,
                    GamesLost = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0,
                    Points = 0
                });
            }

            return rankingList.OrderBy(rl => rl.Team).ToList();
        }
    }
}