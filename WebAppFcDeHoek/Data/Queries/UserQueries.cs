
using System.Linq;
using WebAppFcDeHoek.Data.Tables;

namespace WebAppFcDeHoek.Data.Queries
{
    public class UserQueries
    {
        public static User ValidateUser(FcDeHoekContext context, string userName, string password)
        {
            return context.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password && !u.Obsolete);
        }

        public static User GetById(FcDeHoekContext context, int idUser)
        {
            return context.Users.FirstOrDefault(u => u.IdUser == idUser);
        }

    }
}