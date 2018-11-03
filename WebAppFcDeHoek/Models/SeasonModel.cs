namespace WebAppFcDeHoek.Models
{
    public class SeasonModel
    {
        public int IdSeason { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int Division { get; set; }
        public bool IsCurrentSeason { get; set; }
    }
}