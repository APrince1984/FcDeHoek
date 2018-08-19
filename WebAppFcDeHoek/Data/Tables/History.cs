using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("History", Schema = "dbo")]
    public class History
    {
        [Column("ID"), Key]
        public virtual int IdHistory { get; set; }

        [Column("Version")]
        public virtual decimal Version { get; set; }

        [Column("Text")]
        public virtual string Text { get; set; }

        [Column("Obsolete")]
        public virtual bool Obsolete { get; set; }
    }
}