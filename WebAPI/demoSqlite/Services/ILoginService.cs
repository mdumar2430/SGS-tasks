namespace demoSqlite.Services
{
    public interface ILoginService
    {
        public bool validateUser(string email, string encryptedPassword);
        public string encrpt(string inputPassword);
    }
}