using KN_API.Models;
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

        UsuarioModel model = new UsuarioModel();

        [HttpGet]
        [Route("api/Validar_Usuario")]

        public UsuarioObj Validar_Usuario(UsuarioObj usario)
        {
            return model.Validar_Usuario(usario);
        }

        //Registrar Usuario
        //Validar Usuario
        //Recuperar password
        //Cambiar password
        //Validar permisos de usuario
    }
}
