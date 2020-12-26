using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Tache.Entities.Contexte;
using Tache.Entities.Personnel;
using Tache.Entities.Tache;

namespace Tache.Controllers
{
    [Route("api/")]
    [ApiController]
/*    [Authorize]*/
    public class TacheController : ControllerBase
    {
        private readonly TacheContext context;

        public TacheController(TacheContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("newtask")]
        public IActionResult CreateTasks([FromBody]Taches task)
        {
            IActionResult response = BadRequest(new { error = "Server not responding correctly" });

            this.context.Add(task);
            this.context.SaveChanges();
            
           response = Ok(new { success = "Data saves" });
            
            return response;
        }

        [HttpPost]
        [Route("addpersonneltotask")]
        public IActionResult AddPersonnel(int idTache,int idPersonnel)
        {
            IActionResult response = BadRequest(new { error = "undefined" });
            var _task = this.context.Tache.Find(idTache);
            var _personnel = this.context.Personnel.Find(idPersonnel);
            if (_task != null && _personnel != null)
            {
           
                if (_task.personnels == null)
                {
                    _task.personnels = new List<Personnels>();
                }
                if (_personnel.taches == null)
                {
                    _personnel.taches = new List<Taches>();
                }
                try
                {
                    _task.personnels.Add(_personnel);
                    _personnel.taches.Add(_task);

                    this.context.SaveChanges();

                    response = Ok(new { success = "cool" });

                }
                catch (Exception e)
                {
                    response = BadRequest(new { error = e });
                }
            }
            return response;
        }

        [HttpGet]
        [Route("gettask")]

        public IActionResult getTask(int idTache,int page)
        {
            IActionResult response = BadRequest();

            var tache = Taches.GetTask(idTache:idTache,page:page);

            if (tache.Count != 0)
            {
                response = Ok(new { tache });
            }

            return response;
        }


    }
}
