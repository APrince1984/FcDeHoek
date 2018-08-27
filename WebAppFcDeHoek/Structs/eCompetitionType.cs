using System.Collections.Generic;
using System.Web.Mvc;
using WebAppFcDeHoek.Structs;

namespace WebAppFcDeHoek.Structs
{
    public class CompetitionType
    {
        public int IdCompetition { get; set; }
        public string Description { get; set; }
    }
    

    public struct eCompetitionType
    {
        public const int League = 2;
        public const int Cup = 3;
        public const int Friendly = 4;

        public static string GetCompetitionDescription(int competitionType)
        {
            switch (competitionType)
            {
                case League:
                    return "League";
                case Cup:
                    return "Cup";
                default:
                    return "Friendly";
            }
        }

        public static List<CompetitionType> GetAllCompetitionTypes()
        {
            return new List<CompetitionType>
            {
                new CompetitionType{ Description = "League", IdCompetition = League},
                new CompetitionType{ Description = "Friendly", IdCompetition = Friendly},
                new CompetitionType{ Description = "Cup", IdCompetition = Cup}
            };
        }
    }
}