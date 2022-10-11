using KN_API.BaseDatos;
using KN_API.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;

namespace KN_API.Controllers
{
    public class UsuarioController : ApiController
    {
        UsuarioModel model = new UsuarioModel();

        [HttpPost]
        [Route("api/Usuario/Validar_Usuario")]
        public UsuarioObj Validar_Usuario(UsuarioObj usuario)
        {
            return model.Validar_Usuario(usuario);
        }

        [HttpPost]
        [Route("api/Usuario/Registrar_Usuario")]
        public RespuestaObj Registrar_Usuario(UsuarioObj usuario)
        {
            return model.Registrar_Usuario(usuario);
        }

        [HttpGet]
        [Route("api/Usuario/Consultar_Usuarios_Estado")]
        public List<UsuarioObj> Consultar_Usuarios_Estado(bool activo)
        {
            return model.Consultar_Usuarios_Estado(activo);
        }

    }
}
