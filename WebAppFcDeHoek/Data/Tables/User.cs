
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("Users", Schema = "dbo")]
    public class User
    {
        [Column("ID"), Key]
        public virtual int IdUser { get; set; }

        [Column("UserName")]
        public virtual string UserName { get; set; }

        [Column("Password")]
        public virtual string Password { get; set; }

        [Column("Admin")]
        public virtual bool IsAdmin { get; set; }

        [Column("ID_Person"), ForeignKey("UserPerson")]
        public virtual int? IdPerson { get; set; }
        public virtual Person UserPerson { get; set; }

        [Column("Obsolete")]
        public virtual bool Obsolete { get; set; }
    }
}