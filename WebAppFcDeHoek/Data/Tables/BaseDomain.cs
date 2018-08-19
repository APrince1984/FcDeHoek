using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("BaseDomain", Schema = "dbo")]
    public class BaseDomain
    {
        [Column("ID"), Key]
        public virtual int IdBaseDomain { get; set; }
        [Column("ID_Group")]
        public virtual int IdGroupDomain { get; set; }
        [Column("Description")]
        public virtual string Description { get; set; }
    }
}