using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_API.Models
{
    public class UsuarioObj
    {

        //Atributos o Propiedades de una clase

        public long ConsecutivoUsuario { get; set; }

        public string Correo { get; set; }

        public string password { get; set; }

        public string NombreCompleto { get; set; }

        public int TipoUsuario { get; set; }

        public int Estado { get; set; }

        //ID ---> cedula
        

    }
}