using Domain;

namespace Persistence
{
    public interface IAccountData
    {
        Task<bool> CheckPassAsync(string email, string pass);
        Task<AppUser> FindByEmaiAsync(string email);
        Task<Korisnik> GetKorisnikById(int id);
        Task<IEnumerable<Korisnik>> SviKorisnici();
    }
}