﻿using System.Data.Entity;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data
{
    public class FcDeHoekContext : DbContext
    {
        public FcDeHoekContext() : base("FcDeHoek")
        {
        }

        public DbSet<History> Histories { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<BaseDomain> BaseDomains { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PersonStats> PersonStatistics { get; set; }
        public DbSet<HistoricStats> HistoricStatses { get; set; }
    }
}