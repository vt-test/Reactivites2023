using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class KorisnikController : BaseApiController
    {
        private readonly IAccountData _ctx;

        public KorisnikController(IAccountData ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Korisnik>>> GetKorisnik()
        {
            var result = await _ctx.SviKorisnici();

            return Ok(result);  

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Korisnik>> GetKorisnikById(int id)
        {
            var result = await _ctx.GetKorisnikById(id);

            return Ok(result);

        }
    }
}
