using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcDeHoek.Data.Tables
{
    [Table("Games", Schema = "dbo")]
    public class Game
    {
        [Column("ID"), Key]
        public virtual int IdGame { get; set; }

        [Column("MatchDay")]
        public virtual DateTime MatchDate { get; set; }

        [Column("ID_HomeTeam"), ForeignKey("GameHomeTeam")]
        public virtual int IdHomeTeam { get; set; }
        public virtual Team GameHomeTeam { get; set; }

        [Column("ID_AwayTeam"), ForeignKey("GameAwayTeam")]
        public virtual int IdAwayTeam { get; set; }
        public virtual Team GameAwayTeam { get; set; }

        [Column("GoalsHomeTeam")]
        public virtual int? GoalsHomeTeam { get; set; }

        [Column("GoalsAwayTeam")]
        public virtual int? GoalsAwayTeam { get; set; }

        [Column("PostPoned")]
        public virtual bool NotPlayed { get; set; }

        [Column("Forfait")]
        public virtual bool Forfait { get; set; }

        [Column("ID_Competition"), ForeignKey("GameCompetition")]
        public virtual int IdCompetition { get; set; }
        public virtual BaseDomain GameCompetition { get; set; }

        [Column("ID_Season")]
        public virtual int IdSeason { get; set; }

        [Column("ID_PostPonedGame"), ForeignKey("GamePostPonedGame")]
        public virtual int? IdPostPonedGame { get; set; }
        public virtual Game GamePostPonedGame { get; set; }
    }
}