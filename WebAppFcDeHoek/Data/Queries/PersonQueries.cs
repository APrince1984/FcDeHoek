using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public static class PersonQueries
    {
        public static Person GetById(FcDeHoekContext context, int idPerson)
        {
            return context.Persons.FirstOrDefault(p => p.IdPerson == idPerson);
        }

        public static IQueryable<Person> GetStaff(FcDeHoekContext context)
        {
            return context.Persons.Where(p => p.IsStaff);
        }

        public static IQueryable<Person> GetPlayers(FcDeHoekContext context)
        {
            return context.Persons.Where(p => p.IsPlayer);
        }
    }
}