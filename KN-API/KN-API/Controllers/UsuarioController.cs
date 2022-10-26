using KN_API.BaseDatos;
using KN_API.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.UI;

namespace KN_API.Controllers
{
    public class UsuarioController : ApiController
    {
        UsuarioModel instanciaUsuario = new UsuarioModel();
        BitacoraModel instanciaBitacora = new BitacoraModel();

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Usuario/Validar_Usuario")]
        public RespuestaUsuarioObj Validar_Usuario(UsuarioObj usuario)
        {
            try
            {
                return instanciaUsuario.Validar_Usuario(usuario);
            }
            catch (Exception ex)
            {
                instanciaBitacora.Registrar_Bitacora(usuario.Correo, ex, MethodBase.GetCurrentMethod().Name);

                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();
                respuesta.Codigo = -1;
                respuesta.Mensaje = "Se presentó un error inesperado";
                return respuesta;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Usuario/Registrar_Usuario")]
        public RespuestaUsuarioObj Registrar_Usuario(UsuarioObj usuario)
        {
            try
            {
                //Consultar usuarios por correo
                return instanciaUsuario.Registrar_Usuario(usuario);
            }
            catch (Exception ex)
            {
                instanciaBitacora.Registrar_Bitacora(usuario.Correo, ex, MethodBase.GetCurrentMethod().Name);
                
                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();
                respuesta.Codigo = -1;
                respuesta.Mensaje = "Se presentó un error inesperado";
                return respuesta;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Usuario/Consultar_Usuarios_Estado")]
        public RespuestaUsuarioObj Consultar_Usuarios_Estado(bool activo)
        {
            var correoToken = Thread.CurrentPrincipal.Identity.Name;
            try
            {                
                return instanciaUsuario.Consultar_Usuarios_Estado(activo);
            }
            catch (Exception ex)
            {
                instanciaBitacora.Registrar_Bitacora(correoToken, ex, MethodBase.GetCurrentMethod().Name);

                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();
                respuesta.Codigo = -1;
                respuesta.Mensaje = "Se presentó un error inesperado";
                return respuesta;
            }
        }

        //Resolución de Caso 2
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Usuario/Notificar")]
        public RespuestaUsuarioObj Notificar(ClaseCorreo email)
        {
            try
            {
                return instanciaUsuario.EnviarCorreo(email);
            }
            catch (Exception)
            {
                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();
                respuesta.Codigo = -1;
                respuesta.Mensaje = "Se presentó un error al enviar el correo";
                return respuesta;
            }
        }

    }
}

public class ClaseCorreo
{
    public string Destinatario { get; set; }
    public string CopiaDestinatario { get; set; }
    public string Asunto { get; set; }
    public string Mensaje { get; set; }
}