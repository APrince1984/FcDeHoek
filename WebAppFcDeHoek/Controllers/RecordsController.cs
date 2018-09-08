using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppFcDeHoek.Data;
using WebAppFcDeHoek.Data.Queries;
using WebAppFcDeHoek.Data.Tables;
using WebAppFcDeHoek.Models;
using WebAppFcDeHoek.Structs;

namespace WebAppFcDeHoek.Controllers
{
    public class RecordsTempData
    {
        public int IdPlayer { get; set; }
        public string Player { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Penalties { get; set; }
    }

    public class RecordsController : Controller
    {
        // GET: Records
        public ActionResult Index()
        {
            using (var context = new FcDeHoekContext())
            {
                var model = new List<RecordsModel>();
                model.Add(GetTotals(context));
                model.AddRange(GetTotalsBySeason(context));
                return View(model);
            }
            
        }

        public ActionResult GoalDetail(int idSeason)
        {
            using (var context = new FcDeHoekContext())
            {
                var tempData = GetTempDataForDetail(idSeason, context).OrderByDescending(td => td.Goals).ToList();
                var season = SeasonQueries.GetById(context, idSeason);
                var keyValue = idSeason == -1 || season == null
                    ? "All Time Goals"
                    : $"Goals {season.SeasonStartYear} - {season.SeasonEndYear}";

                var model = new Dictionary<string, List<RecordsPlayer>>();
                var playerRecords = new List<RecordsPlayer>();
                foreach (var data in tempData)
                {
                    if (data.Goals != 0)
                    {
                        playerRecords.Add(new RecordsPlayer
                        {
                            Player = data.Player,
                            Stat = data.Goals
                        });
                    }
                }

                model.Add(keyValue, playerRecords);

                return View("Detail", model);
            }
        }

        private List<RecordsTempData> GetTempDataForDetail(int idSeason, FcDeHoekContext context)
        {
            List<RecordsTempData> tempData;
            if (idSeason == -1)
            {
                var historicData = GetHistoricData(context);
                tempData = GetCurrentData(context, historicData);
            }
            else
            {
                var season = SeasonQueries.GetById(context, idSeason);
                tempData = season != null
                    ? season.SeasonStartYear >= eStatsSince.SiteStartYear
                        ? GetCurrentData(context, new List<RecordsTempData>(), idSeason)
                        : GetHistoricData(context, idSeason)
                    : new List<RecordsTempData>();
            }

            return tempData;
        }

        public ActionResult AssistDetail(int idSeason)
        {
            using (var context = new FcDeHoekContext())
            {
                var tempData = GetTempDataForDetail(idSeason, context).OrderByDescending(td => td.Assists).ToList();
                var season = SeasonQueries.GetById(context, idSeason);

                var keyValue = idSeason == -1 || season == null
                    ? "All Time Assists"
                    : $"Assists {season.SeasonStartYear} - {season.SeasonEndYear}";

                var model = new Dictionary<string,List<RecordsPlayer>>();
                var playerRecords = new List<RecordsPlayer>();
                foreach (var data in tempData)
                {
                    if (data.Assists != 0)
                    {
                        playerRecords.Add(new RecordsPlayer
                        {
                            Player = data.Player,
                            Stat = data.Assists
                        });
                    }
                }

                model.Add(keyValue, playerRecords);

                return View("Detail", model);
            }
        }

        private List<RecordsModel> GetTotalsBySeason(FcDeHoekContext context)
        {
            var seasons = context.Seasons.OrderByDescending(s => s.SeasonStartYear).ToList();
            var modelList = new List<RecordsModel>();
            foreach (var season in seasons)
            {
                if (season.SeasonStartYear >= eStatsSince.SiteStartYear)
                    modelList.Add(GetCurrentDataBySeason(context, season));
                else
                    modelList.Add(GetHistoricDataBySeason(context, season));
            }

            return modelList;

        }

        private RecordsModel GetHistoricDataBySeason(FcDeHoekContext context, Season season)
        {
            var tempData = GetHistoricData(context, season.IdSeason);
            return BuildRecordsModel($"{season.SeasonStartYear} - {season.SeasonEndYear}", tempData, season.IdSeason);
        }

        private RecordsModel GetCurrentDataBySeason(FcDeHoekContext context, Season season)
        {
            var tempData = GetCurrentData(context, new List<RecordsTempData>(), season.IdSeason);
            return BuildRecordsModel($"{season.SeasonStartYear} - {season.SeasonEndYear}", tempData, season.IdSeason);
        }


        private RecordsModel GetTotals(FcDeHoekContext context)
        {
            var historicData = GetHistoricData(context);
            var tempData = GetCurrentData(context, historicData);
            return BuildRecordsModel($"All time records (Goals since {eStatsSince.GoalsStartYear}, Assists since {eStatsSince.AssistsStartYear}", tempData);
        }

        private static RecordsModel BuildRecordsModel(string title, List<RecordsTempData> tempData, int idSeason = -1)
        {
            var recordModel = new RecordsModel {Title = title, IdSeason = idSeason};

            var topScorer = tempData.OrderByDescending(td => td.Goals).ThenBy(td => td.Penalties)
                .ThenByDescending(td => td.Assists).ThenBy(td => td.Player).FirstOrDefault();
            if (topScorer != null)
                recordModel.GoalRecord = new RecordsPlayer
                {
                    ExtraStat = topScorer.Penalties,
                    Player = topScorer.Player,
                    Stat = topScorer.Goals
                };

            var assistKing = tempData.OrderByDescending(td => td.Assists).ThenByDescending(td => td.Goals)
                .ThenBy(td => td.Penalties).ThenBy(td => td.Player).FirstOrDefault();
            if (assistKing != null)
            {
                recordModel.AssistRecord = assistKing.Assists != 0 
                    ? new RecordsPlayer { Stat = assistKing.Assists, Player = assistKing.Player }
                    : new RecordsPlayer { Stat = 0, Player = "No Statistics"};
            }
                
            return recordModel;
        }

        private List<RecordsTempData> GetCurrentData(FcDeHoekContext context, List<RecordsTempData> historicData, int idSeason = -1)
        {
            var allPlayers = PersonQueries.GetPlayers(context).ToList();
            foreach (var player in allPlayers)
            {
                var stats = PersonStatisticsQueries.GetByIdPerson(context, player.IdPerson).ToList();
                if (stats.Any())
                {
                    
                    var tempData = new RecordsTempData
                    {
                        IdPlayer = player.IdPerson,
                        Player = $"{player.Name} {player.FirstName}"
                    };

                    foreach (var stat in stats)
                    {
                        var game = GameQueries.GetById(context, stat.IdGame);
                        if (game != null && game.IdCompetition == eCompetitionType.League && (game.IdSeason == idSeason || idSeason == -1))
                        {
                            if ((stat.Goals != null && stat.Goals != 0) || stat.Assists != 0)
                            {
                                var playersHistoricData =
                                    historicData.FirstOrDefault(hd => hd.IdPlayer == player.IdPerson);
                                if (playersHistoricData != null)
                                {
                                    playersHistoricData.Assists += stat.Assists;
                                    playersHistoricData.Goals += stat.Goals ?? 0;
                                }
                                else
                                {
                                    tempData.Assists += stat.Assists;
                                    tempData.Goals += stat.Goals ?? 0;
                                    historicData.Add(tempData);
                                }
                            }
                        }
                    }
                }
            }

            return historicData;
        }

        private static List<RecordsTempData> GetHistoricData(FcDeHoekContext context, int idSeason = -1)
        {
            var tempDataList = new List<RecordsTempData>();
            var allPlayers = PersonQueries.GetPlayers(context).ToList();
            foreach (var player in allPlayers)
            {
                var historicStats = idSeason != -1 
                    ? HistoricStatsQueries.GetByIdPlayer(context, player.IdPerson).Where(hs => hs.IdSeason == idSeason).ToList()
                    : HistoricStatsQueries.GetByIdPlayer(context, player.IdPerson).ToList();

                if (historicStats.Any())
                {
                    var tempData = new RecordsTempData
                    {
                        IdPlayer = player.IdPerson,
                        Player = $"{player.Name} {player.FirstName}"
                    };

                    foreach (var stat in historicStats)
                    {
                        tempData.Goals += stat.Goals ?? 0;
                        tempData.Assists += stat.Assists ?? 0;
                        tempData.Penalties += stat.Penalties ?? 0;
                    }

                    tempDataList.Add(tempData);
                }
            }

            return tempDataList;
        }
    }
}