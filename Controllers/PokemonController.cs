using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using ProfileAPI.Filters;

namespace ProfileAPI.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    //[ApiKeyAuth]
    public class PokemonController : ProfileControllerBase
    {
        public PokemonController(IHostingEnvironment _env)
        {
            env = _env;
            pokePath = env.ContentRootPath +  @"\Profiles\Pokemon\";
        }

        private Pokemon poke;

        [HttpGet]
        public ActionResult Get(string name)
        {
            try
            {
                using (FileStream file = System.IO.File.Open(pokePath + name + @"\" + name.ToLower() + ".txt", FileMode.OpenOrCreate))
                {
                    poke = (Pokemon)pokemonSerializer.Deserialize(file);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            

            return Ok(poke);
        }

        [HttpPost]
        public ActionResult Post(Pokemon newPokemon)
        {
            try
            {
                SerializePokemon(newPokemon);
                Console.WriteLine("Added " + newPokemon.Name);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Created("", newPokemon);
        }

        
    }
}