using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class AccountData : IAccountData
    {
        private readonly IDataAccess _db;

        public AccountData(IDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<Korisnik>> SviKorisnici() =>
            _db.SQL<Korisnik, dynamic>("SELECT * FROM public.korisnik;", new { });

        public async Task<Korisnik> GetKorisnikById(int id)
        {
            var result = await _db.SQL<Korisnik, dynamic>("SELECT * FROM public.korisnik WHERE id = @id;", new { id = id });

            return result.FirstOrDefault();
        }

        public async Task<AppUser> FindByEmaiAsync(string email)
        {
            var result = await _db.SQL<AppUser, dynamic>("SELECT * FROM sp_account_byemail(@account_email)", new { account_email = email });

            return result.FirstOrDefault();
        }

        public async Task<bool> CheckPassAsync(string email, string pass)
        {
            var result = await _db.SQL<int, dynamic>("SELECT * FROM sp_account_checkpass(@account_email, @account_pass)", new { account_email = email, account_pass = pass });
            var brojac = result.FirstOrDefault();
            return brojac > 0;
        }
    }
}
