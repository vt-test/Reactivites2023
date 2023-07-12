using Domain;

namespace Persistence
{
    public interface IKorisnikData
    {
        Task DeleteKorisnik(int id);
        Task<Korisnik> GetKorisnik(int id);
        Task<Korisnik> GetMulti(int id);
        Task InsertKorisnik(Korisnik korisnik);
        Task<IEnumerable<Korisnik>> KetKorisnici();
        Task<IEnumerable<Korisnik>> SviKorisnici();
        Task UpdateKorisnik(Korisnik korisnik);
    }
}