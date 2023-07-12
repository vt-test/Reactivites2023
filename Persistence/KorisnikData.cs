using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{

    public class KorisnikData : IKorisnikData
    {
        private readonly IDataAccess _db;

        public KorisnikData(IDataAccess db)
        {
            _db = db;
        }


        public Task<IEnumerable<Korisnik>> KetKorisnici() =>
            _db.SQL<Korisnik, dynamic>("SELECT * FROM sp_getkorisnikall();", new { });


        public Task<IEnumerable<Korisnik>> SviKorisnici() =>
            _db.SQL<Korisnik, dynamic>("SELECT * FROM korisnik;", new { });


        //public async Task<Korisnik?> GetKorisnik(int id)
        //{
        //    var result = await _db.LoadData<Korisnik, dynamic>(
        //        "sp_getkorisnik", new { id_korisnik = id });

        //    return result.FirstOrDefault();
        //}


        public async Task<Korisnik?> GetKorisnik(int id)
        {
            var result = await _db.SQL<Korisnik, dynamic>("SELECT * FROM sp_getkorisnik(@id_korisnik)", new { id_korisnik = id });

            return result.FirstOrDefault();
        }

        public async Task<Korisnik?> GetMulti(int id)
        {
            var result = await _db.SQL_Multi<Korisnik, dynamic>("BEGIN; SELECT abc(@id_korisnik, 'V1', 'V2'); FETCH ALL IN \"V1\"; FETCH ALL IN \"V2\";", new { id_korisnik = id });

            return result.FirstOrDefault();
        }

        //public Task InsertKorisnik(Korisnik korisnik) =>
        //    _db.SaveData("sp_korisnik_add", new
        //    {
        //        korisnik.ime,
        //        korisnik.prezime
        //    });

        public Task InsertKorisnik(Korisnik korisnik)
        {

            DynamicParameters _params = new DynamicParameters();
            _params.Add("@ime", korisnik.ime, DbType.String);
            _params.Add("@prezime", korisnik.prezime, DbType.String);
            _params.Add("@new_id", DbType.Int16, direction: ParameterDirection.Output);

            return _db.SaveData("sp_korisnik_add", _params);
        }

        public Task UpdateKorisnik(Korisnik korisnik) =>
            _db.SaveData("sp_korisnik_upd", new
            {
                pid = korisnik.id,
                pime = korisnik.ime,
                pprezime = korisnik.prezime
            });

        public Task DeleteKorisnik(int id) =>
            _db.SaveData("sp_korisnik_del", new { pid = id });
    }
}
