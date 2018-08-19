namespace WebAppFcDeHoek.Structs
{
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