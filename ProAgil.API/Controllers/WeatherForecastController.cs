using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.API.Data;
using ProAgil.Domain;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {       
        public DataContext Context { get; }

        public WeatherForecastController(DataContext context)
        {
            this.Context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Evento>> Get()
        {
           return this.Context.Eventos.ToList();
        }

    }
}
