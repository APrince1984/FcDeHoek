using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("PersonStats", Schema = "dbo")]
    public class PersonStats
    {
        [Column("ID"), Key]
        public virtual int IdPersonStats { get; set; }

        [Column("ID_Person"), ForeignKey("PersonStatsPerson")]
        public virtual int IdPerson { get; set; }
        public virtual Team PersonStatsPerson { get; set; }

        [Column("ID_Game"), ForeignKey("PersonStatsGame")]
        public virtual int IdGame { get; set; }
        public virtual Game PersonStatsGame { get; set; }

        [Column("Goals")]
        public virtual int? Goals { get; set; }

        [Column("Assists")]
        public virtual int Assists { get; set; }

        [Column("Played")]
        public virtual bool? Played { get; set; }
    }
}