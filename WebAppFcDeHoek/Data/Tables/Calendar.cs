using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("Calendar", Schema = "dbo")]
    public class Calendar
    {
        [Column("ID"), Key]
        public virtual int IdCalendar { get; set; }

        [Column("ID_Season"), ForeignKey("CalendarSeason")]
        public virtual int IdSeason { get; set; }

        public virtual Season CalendarSeason { get; set; }

        [Column("Date")]
        public virtual DateTime MatchDay { get; set; }

        [Column("ID_HomeTeam"), ForeignKey("CalendarHomeTeam")]
        public virtual int IdHomeTeam { get; set; }
        public virtual Team CalendarHomeTeam { get; set; }

        [Column("ID_AwayTeam"), ForeignKey("CalendarAwayTeam")]
        public virtual int IdAwayTeam { get; set; }
        public virtual Team CalendarAwayTeam { get; set; }

        [Column("ID_Competition"), ForeignKey("CalendarCompetition")]
        public virtual int IdCompetition { get; set; }
        public virtual BaseDomain CalendarCompetition { get; set; }

        [Column("GoalsHome")]
        public virtual int? GoalsHomeTeam { get; set; }

        [Column("GoalsAway")]
        public virtual int? GoalsAwayTeam { get; set; }

        [Column("Postponed")]
        public virtual bool Postponed { get; set; }

        [Column("ForFait")]
        public virtual bool Forfait { get; set; }

    }
}