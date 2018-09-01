using System.Collections.Generic;

namespace WebAppFcDeHoek.Structs
{
    public struct eFunction
    {
        public const int Player = 1;
        public const int Delege = 2;
        public const int PenningMeester = 3;
        public const int Voorzitter = 4;
        public const int EventManager = 5;

        public static string GetFunctionDescription(int idFunction)
        {
            switch (idFunction)
            {
                case Delege:
                    return "Afgevaardigde";
                case PenningMeester:
                    return "Secretaris";
                case Voorzitter:
                    return "Voorzitter";
                case EventManager:
                    return "Event Manager";
                default:
                    return "Speler";
            }
        }

        public static Dictionary<int, string> GetAllFunctions()
        {
            return new Dictionary<int, string>
            {
                {Player, GetFunctionDescription(Player) },
                {Delege, GetFunctionDescription(Delege) },
                {PenningMeester, GetFunctionDescription(PenningMeester) },
                {Voorzitter, GetFunctionDescription(Voorzitter) },
                {EventManager, GetFunctionDescription(EventManager) }
            };
        }
    }

    public struct eResult
    {
        public const int NotPlayed = 0;
        public const int Won = 1;
        public const int Lost = 2;
        public const int Draw = 3;
        

        public static int GetResult(int? goalsHomeTeam, int? goalsAwayTeam)
        {
            if (goalsHomeTeam == null || goalsAwayTeam == null)
                return NotPlayed;

            if (goalsHomeTeam > goalsAwayTeam)
                return Won;

            if (goalsHomeTeam < goalsAwayTeam)
                return Lost;

            return Draw;
        }
    }
}