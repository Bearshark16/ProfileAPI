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
    [Route("api/person/upload")]
    [ApiController]
    //[ApiKeyAuth]
    public class PersonUploadController : ControllerBase
    {
        private IHostingEnvironment env;

        private string envPath;

        public PersonUploadController(IHostingEnvironment _env)
        {
            env = _env;
            envPath = env.ContentRootPath + @"\Profiles\People\";
        }

        [HttpGet]
        public async Task<ActionResult> ImageFileDownload(string name)
        {
            string path = envPath + name.ToLower() + @"\" + name.ToLower() + ".png";
            MemoryStream memory = new MemoryStream();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "multipart/form-data", Path.GetFileName(path));
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public ActionResult ImageFileUpload(IFormFile file)
        {
            var dir = env.ContentRootPath;

            try
            {
                using (FileStream fileStream = new FileStream(ProfileControllerBase.imagePath + ProfileControllerBase.imageName + ".png", FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            }
            catch (Exception)
            {

                return NotFound("null");
            }

            return Ok();
        }
    }
}