using System.Data.Entity;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data
{
    public class FcDeHoekContext : DbContext
    {
        public FcDeHoekContext() : base("DeHoek")
        {
        }

        public DbSet<History> Histories { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<BaseDomain> BaseDomains { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}