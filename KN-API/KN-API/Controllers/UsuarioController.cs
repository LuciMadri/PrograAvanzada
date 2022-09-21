using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KN_API.Controllers
{
    public class UsuarioController : ApiController
    {

        [HttpGet]
        [Route("api/Validar_Usuario")]

        public bool Validar_Usuario()
        {
            return false;
        }

        //Registrar Usuario
        //Validar Usuario
        //Recuperar password
        //Cambiar password
        //Validar permisos de usuario
    }
}
