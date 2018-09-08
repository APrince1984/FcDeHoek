namespace WebAppFcDeHoek.Models
{
    public class RecordsPlayer
    {
        public string Player { get; set; }
        public int Stat { get; set; }
        public int? ExtraStat { get; set; }
    }
    public class RecordsModel
    {
        public string Title { get; set; }
        public int IdSeason { get; set; }
        public RecordsPlayer GoalRecord { get; set; } 
        public RecordsPlayer AssistRecord { get; set; }
    }
}