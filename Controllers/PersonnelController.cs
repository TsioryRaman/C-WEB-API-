using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;

using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tache.Entities.Personnel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Tache.Controllers
{

    [Route("api/")]
    [ApiController]
    [EnableCors]
    public class PersonnelController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PersonnelController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Authorize]
        [Route("newpersonnel")]
        public IActionResult NewPersonnel([FromForm]Personnels personnels)
        {
            IActionResult response = BadRequest(new { messages = "Champ non completed" });
            var File = personnels.file;
            try
            {
                if (File.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var fileName = File.FileName.Split(".");
                    string name = fileName[0] + personnels.Name + "." + fileName[1];
            
                    using (var stream = System.IO.File.Create(path + name))
                    {
                        File.CopyTo(stream);
                        personnels.ImagePersonnel = File.FileName;
                        stream.Flush();
                    }
                }
                response = Ok(new { success = "Personnel saves" });

            }
            catch (Exception e)
            {
                response = BadRequest(new { Error = "Error during saving" });

                Console.WriteLine("Erreur :" + e.Message);
            }
            Personnels.create(personnels);


            return response;
        }
        [HttpGet]
        [Route("pesonnel")]
        public IActionResult PesonnelPerPage(int page)
        {
            IActionResult response = BadRequest(new { error="Incorrect user"});

            List<Personnels> personnels = Personnels.GetByPage(page);

            if(personnels.Count > 0)
            {
                response = Ok(new { personnels });
            }


            return response;
        }
    }
}
