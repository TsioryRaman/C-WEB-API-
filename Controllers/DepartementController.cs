using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tache.Entities.Contexte;

namespace Tache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private readonly TacheContext context;

        public DepartementController(TacheContext context)
        {
            this.context = context;
        }

    }
}
