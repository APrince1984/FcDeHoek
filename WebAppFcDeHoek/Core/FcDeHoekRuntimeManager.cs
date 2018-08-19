using System.Configuration;
using System.Web;

namespace WebAppFcDeHoek.Core
{
    public static class FcDeHoekRuntimeManager
    {
        public static string ClubName;
        
        public static void InitializeSite()
        {
            ClubName = ConfigurationManager.AppSettings["ClubName"];
            
        }
    }
}