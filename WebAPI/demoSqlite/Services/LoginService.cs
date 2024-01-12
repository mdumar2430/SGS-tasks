using demoSqlite.Controllers.Data;
using demoSqlite.RSA;

namespace demoSqlite.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IRSAHelper _rsaHelper;
        public LoginService(IAppDbContext appDbContext, IRSAHelper rsaHelper)
        {
            _appDbContext = appDbContext;
            _rsaHelper = rsaHelper;
        }

        public bool validateUser(string email, string encryptedPassword)
        {
            var user = this._appDbContext.Users.FirstOrDefault(e => e.Email.Equals(email));
            if (user != null)
            {
                var decryptedPassword = this._rsaHelper.Decrypt(encryptedPassword);
                if (decryptedPassword.Equals(user.Password))
                {
                    return true;
                }
                return false;
            }
            else
            {
                throw new Exception("User mail not found!");
            }
        }

        public string encrpt(string inputPassword)
        {
            return this._rsaHelper.Encrypt(inputPassword);
        }
    }
}
