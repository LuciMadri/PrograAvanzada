using KN_API.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_API.Models
{
    public class UsuarioModel
    {
        public UsuarioObj Validar_Usuario(UsuarioObj usuario)
        {

            using (var con = new dbMartesEntities())
            {
                //Esto es LinQ, es un lenguaje de consultas incorporado a C#

                //Select

                //LINQ  
                var resultado = (from x in con.TUSUARIO
                                 where x.NombreCompleto == "Eduardo"
                                 select x).FirstOrDefault();

                //SP
                //var resultado2 = con.Consultar_Datos_Usuario(" Eduardo ").FirstOrDefault(); I

                //Insert
                con.TUSUARIO.Add(resultado);
                con.SaveChanges();

                //Delete
                con.TUSUARIO.Remove(resultado);
                con.SaveChanges();

                //Update
                resultado.Password = "123456";
                con.SaveChanges();

            }


            //vamos a ir a la base de datos para ver si el corre y al conta hacen match
            return new UsuarioObj
                {
                    Correo = "lmadrigal@gmail.com",
                    NombreCompleto = "Luciana Madrigal"
                };
            
        }
    }
}