namespace WebAppFcDeHoek.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool Obsolete { get; set; }
        
    }
}