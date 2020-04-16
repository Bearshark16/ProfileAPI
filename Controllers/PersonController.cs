using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileAPI.Filters;

namespace ProfileAPI.Controllers
{
    [Route("api/person")]
    [ApiController]
    //[ApiKeyAuth]
    public class PersonController : ProfileControllerBase
    {
        public PersonController(IHostingEnvironment _env)
        {
            env = _env;
            persPath = env.ContentRootPath + @"\Profiles\People\";
        }

        private Person person;

        [HttpGet]
        public ActionResult Get(string name)
        {
            try
            {
                using (FileStream file = System.IO.File.Open(persPath + name + @"\" + name.ToLower() + ".txt", FileMode.OpenOrCreate))
                {
                    person = (Person)personSerializer.Deserialize(file);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }


            return Ok(person);
        }

        [HttpPost]
        public ActionResult Post(Person newPerson)
        {
            SerializePerson(newPerson);
            Console.WriteLine("Added " + newPerson.Name);
            return Created("", newPerson);
        }
    }
}