using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IGaseosasM gaseosasM;
        public ValuesController(IGaseosasM gaseosas_M)
        {
            gaseosasM = gaseosas_M;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<Gaseosas>> Get() => gaseosasM.Get();

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Gaseosas> GetID([FromBody]string nombre)
        {
            return gaseosasM.GetID(nombre);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Create([FromBody] string nombre, [FromBody] string sabor, [FromBody] string volumen, [FromBody] double precio, [FromBody] string casa)
        {
            if (gaseosasM.verificar(nombre))
            {
                Gaseosas nuevaGaseosa = new Gaseosas();
                nuevaGaseosa.Nombre = nombre;
                nuevaGaseosa.Sabor = sabor;
                nuevaGaseosa.Volumen = volumen;
                nuevaGaseosa.Precio = precio;
                nuevaGaseosa.CasaP = casa;
                gaseosasM.crearGaseosa(nuevaGaseosa);
                return nuevaGaseosa.Nombre + "se ha agregado";
            }
            else
            {
                return nombre + " ya existe, inserte un nuevo objeto";
            }
            
        }


    }
}
