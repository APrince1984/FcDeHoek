using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public static class TeamQueries
    {
        public static Team GetTeamById(FcDeHoekContext context, int id)
        {
            return context.Teams.FirstOrDefault(t => t.IdTeam == id);
        }
    }
}