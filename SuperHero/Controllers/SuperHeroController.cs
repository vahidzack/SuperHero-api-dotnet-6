using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await _context.SuperHero.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHero.FindAsync(id);
            if (hero == null)
                return BadRequest("The hero not found ");

            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHero.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHero.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Update(SuperHero request)
        {
            var SuperHero = await _context.SuperHero.FindAsync(request.Id);
            if (SuperHero == null)

                return BadRequest("The hero not found ");
            SuperHero.Name = request.Name;
            SuperHero.FirstName = request.FirstName;
            SuperHero.LastName = request.LastName;
            SuperHero.Place = request.Place;
            await _context.SaveChangesAsync();


            return Ok(await _context.SuperHero.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var SuperHero = await _context.SuperHero.FindAsync(id);
            if (SuperHero == null)
                return BadRequest("The hero not found ");
            _context.SuperHero.Remove(SuperHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHero.ToListAsync()); 
        }

    }
}

