using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProfileAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ProfileControllerBase
    {
        private string envPath;

        public AccountController(IHostingEnvironment _env)
        {
            env = _env;
            userPath = env.ContentRootPath + @"\User_Accounts\";
        }

        private UserProfile user;
        private string apiKey;

        [HttpGet]
        public ActionResult LogIn(string username, string password)
        {
            try
            {
                using (FileStream file = System.IO.File.Open(userPath + username + ".txt", FileMode.OpenOrCreate))
                {
                    user = (UserProfile)accountSerializer.Deserialize(file);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            
            if (!password.Equals(user.Password))
            {
                return NotFound("Incorrect Password");
            }
            else
            {
                return Ok(user.ApiKey);
            }
            
        }

        [HttpPost]
        public ActionResult Register(UserProfile newUser)
        {
            try
            {
                apiKey = KeyGenerator();
                newUser.ApiKey = apiKey;
                SerializeUser(newUser);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message.ToString());
            }

            return Ok(apiKey);
        }

        private string KeyGenerator()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);

            return apiKey;
        }
    }
}