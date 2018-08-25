
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("Person", Schema = "dbo")]
    public class Person
    {
        [Column("ID"), Key]
        public virtual int IdPerson { get; set; }

        [Column("Name")]
        public virtual string Name { get; set; }

        [Column("FirstName")]
        public virtual string FirstName { get; set; }

        [Column("PerkezNumber")]
        public virtual int? PerkezNumber { get; set; }

        [Column("Player")]
        public virtual bool IsPlayer { get; set; }

        [Column("Staff")]
        public virtual bool IsStaff { get; set; }

        [Column("Picture")]
        public virtual string PicturePath { get; set; }

        [Column("BirthDate")]
        public virtual DateTime? BirthDate { get; set; }

        [Column("PhoneNumber")]
        public virtual int? PhoneNumber { get; set; }
    }
}