using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("Season",Schema = "dbo")]
    public class Season
    {
        [Column("ID"), Key]
        public virtual int IdSeason { get; set; }

        [Column("SeasonStartYear")]
        public virtual int SeasonStartYear { get; set; }

        [Column("SeasonEndYear")]
        public virtual int SeasonEndYear { get; set; }

        [Column("CurrentDivision")]
        public virtual int Division { get; set; }
    }
}