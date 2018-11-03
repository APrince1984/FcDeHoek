using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("HistoricStats", Schema = "dbo")]
    public class HistoricStats
    {
        [Column("ID"), Key]
        public virtual int IdHistoricStats { get; set; }

        [Column("ID_Person"), ForeignKey("HistoricStatsPerson")]
        public virtual int IdPerson { get; set; }
        public virtual Team HistoricStatsPerson { get; set; }

        [Column("ID_Season"), ForeignKey("HistoricStatsSeason")]
        public virtual int IdSeason { get; set; }
        public virtual Season HistoricStatsSeason { get; set; }

        [Column("Goals")]
        public virtual int? Goals { get; set; }
       

        [Column("Assists")]
        public virtual int? Assists { get; set; }

        [Column("Penalties")]
        public virtual int? Penalties { get; set; }
        
    }
}