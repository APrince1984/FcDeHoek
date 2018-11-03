using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Extensions;
using WebAppFcDeHoek.Models;
using WebAppFcDeHoek.Structs;

namespace WebAppFcDeHoek.Controllers
{
    public class GameManagerController : Controller
    {
        // GET: GameManager
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                startDate = DateTime.Now.GetFirstDayOfWeek(CultureInfo.CurrentCulture);
                endDate = ((DateTime)startDate).AddDays(6);
            }
            
            startDate = DateTime.Now.AddDays(-50);
            endDate = DateTime.Now.AddDays(300);

            return View(new GameManagerModel{StartDate = (DateTime) startDate, EndDate = (DateTime) endDate, Games = GetGames((DateTime)startDate, (DateTime)endDate)});
        }

        public ActionResult AddMultipleGames()
        {
            using (var context = new FcDeHoekContext())
            {
                var model = new GameManagerModel
                {
                    AddAnother = false,
                    CompetitionTypes = eCompetitionType.GetAllCompetitionTypes(),
                    Teams = context.Teams.ToList(),
                    Games = new List<GameModel>
                    {
                        new GameModel(),
                        new GameModel(),
                        new GameModel(),
                        new GameModel()
                    }

                };
                return View(model);
            }
        }

        public ActionResult EditGame(int idGame)
        {
            using (var context = new FcDeHoekContext())
            {
                GameManagerModel manageModel;

                if (idGame == 0)
                {
                    manageModel = new GameManagerModel
                    {
                        Games = new List<GameModel> { new GameModel()},
                        Teams = context.Teams.ToList(),
                        CompetitionTypes = eCompetitionType.GetAllCompetitionTypes(),
                        AddAnother = false
                    };
                }
                else
                {
                    var game = GameQueries.GetById(context, idGame);
                    manageModel = new GameManagerModel
                    {
                        Games = new List<GameModel> { new GameModel
                        {
                            AwayTeam = game.GameAwayTeam.Name,
                            Competition = game.GameCompetition.Description,
                            Forfait = game.Forfait,
                            GoalsHomeTeam = game.GoalsHomeTeam,
                            GoalsAwayTeam = game.GoalsAwayTeam,
                            GameResult = 0,
                            HomeTeam = game.GameHomeTeam.Name,
                            IdAwayTeam = game.IdAwayTeam,
                            IdCompetition = game.IdCompetition,
                            IdGame = game.IdGame,
                            IdHomeTeam = game.IdHomeTeam,
                            MatchDay = game.MatchDate
                        } },
                        Teams = context.Teams.ToList(),
                        CompetitionTypes = eCompetitionType.GetAllCompetitionTypes(),
                        AddAnother = false
                    };
                }

                return View("Detail", manageModel);
            }
            
        }

        public ActionResult Save(GameManagerModel model)
        {
            using (var context = new FcDeHoekContext())
            {
                var game = GameQueries.GetById(context, model.Games[0].IdGame);
                if (game == null)
                    game = new Game();

                foreach (var gameModel in model.Games)
                {
                    game.GoalsAwayTeam = gameModel.GoalsAwayTeam;
                    game.GoalsHomeTeam = gameModel.GoalsHomeTeam;
                    game.Forfait = gameModel.Forfait;
                    game.IdCompetition = gameModel.IdCompetition;
                    game.IdAwayTeam = gameModel.IdAwayTeam;
                    game.IdHomeTeam = gameModel.IdHomeTeam;
                    game.IdPostPonedGame = gameModel.IdPostPonedGame;
                    game.IdSeason = SeasonQueries.GetSeasonByDate(context, gameModel.MatchDay)?.IdSeason ?? SeasonQueries.GetCurrentSeason(context).IdSeason;
                    game.MatchDate = gameModel.MatchDay;
                    game.NotPlayed = gameModel.IsPostPoned;

                    context.Games.AddOrUpdate(game);
                    context.SaveChanges();
                }
                

                if (model.AddAnother)
                    if (model.Games.Count > 1)
                        return RedirectToAction("AddMultipleGames", "GameManager", new { idGame = 0 });
                    else
                        return RedirectToAction("EditGame", "GameManager", new {idGame = 0});
                    
                
                return RedirectToAction("Index", "GameManager");
            }
            
        }

        private List<GameModel> GetGames(DateTime startDate, DateTime endDate)
        {
            using (var context = new FcDeHoekContext())
            {
                var gameModels = new List<GameModel>();
                var games = GameQueries.GetAllGamesBetweenDates(context, startDate, endDate).OrderBy(g => g.MatchDate).ToList();
                foreach (var game in games)
                {
                    //var season = SeasonQueries.GetSeasonByDate(context, startDate);
                    gameModels.Add(new GameModel
                    {
                        IdGame = game.IdGame,
                        AwayTeam = game.GameAwayTeam.Name,
                        Competition = game.GameCompetition.Description,
                        HomeTeam = game.GameHomeTeam.Name,
                        MatchDay = game.MatchDate,
                        IdAwayTeam = game.IdAwayTeam,
                        IdHomeTeam = game.IdHomeTeam,
                        IdCompetition = game.IdCompetition,
                        IdSeason = game.IdSeason,
                        IsPostPoned = game.NotPlayed,
                        Forfait = game.Forfait,
                        IdPostPonedGame = game.IdPostPonedGame
                    });
                }

                return gameModels;
            }
            
            
            
        }

    }
}