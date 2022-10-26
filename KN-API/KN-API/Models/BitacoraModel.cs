using KN_API.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace KN_API.Models
{
    public class BitacoraModel
    {

        public void Registrar_Bitacora(string correo, Exception ex, string origen)
        {
            using (var con = new dbMartesEntities())
            {
                con.Registrar_Bitacora(correo, DateTime.Now, ex.HResult, ex.Message, origen);
            }
        }

    }
}