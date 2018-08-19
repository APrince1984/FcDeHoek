using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("Team", Schema = "dbo")]
    public class Team
    {
        [Column("ID"), Key]
        public virtual int IdTeam { get; set; }

        [Column("Name"), Required]
        public virtual string Name { get; set; }
    }
}