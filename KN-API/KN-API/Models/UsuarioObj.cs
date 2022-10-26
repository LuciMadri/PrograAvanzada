using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_API.Models
{
    //Clase, Objeto, Entidad

    public class UsuarioObj
    {
        //Atributos o Propiedades de una clase

        public long ConsecutivoUsuario { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public string NombreCompleto { get; set; }
        public int TipoUsuario { get; set; }
        public bool Estado { get; set; }
        public string Token { get; set; }

    }

    public class RespuestaUsuarioObj
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        public UsuarioObj objeto { get; set; }
        public List<UsuarioObj> lista { get; set; }
    }

}