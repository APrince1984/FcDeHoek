namespace WebAppFcDeHoek.Structs
{
    public struct eEntry
    {
        public const int LogIn = 1;
        public const int LogOut = 2;

        public static string EntryDescription(int entry)
        {
            switch (entry)
            {
                case LogOut:
                    return "Log Out";
                default:
                    return "Log In";
            }
        }
    }
}