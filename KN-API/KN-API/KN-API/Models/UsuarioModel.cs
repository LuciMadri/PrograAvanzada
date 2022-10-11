using KN_API.BaseDatos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace KN_API.Models
{
    public class UsuarioModel
    {
        public UsuarioObj Validar_Usuario(UsuarioObj usuario)
        {
            using (var con = new dbMartesEntities())
            {
                var resultado = con.Consultar_Datos_Usuario(usuario.Correo, usuario.Contrasenna).FirstOrDefault();

                if (resultado != null)
                {
                    return new UsuarioObj
                    {
                        Correo = resultado.Correo,
                        NombreCompleto = resultado.NombreCompleto,
                        Estado = resultado.Estado,
                        TipoUsuario = resultado.TipoUsuario,
                        Token = CrearToken(usuario.Correo)
                    };
                }
                return null;

            }
        }

        public List<UsuarioObj> Consultar_Usuarios_Estado(bool activo)
        {
            using (var con = new dbMartesEntities())
            {
                var resultado = (from x in con.TUSUARIO
                                 where x.Estado == activo
                                 select x).ToList();

                if (resultado.Count > 0)
                {
                    List<UsuarioObj> datos = new List<UsuarioObj>();
                    
                    foreach (var item in resultado)
                    {
                        datos.Add(new UsuarioObj {
                            Correo = item.Correo,
                            NombreCompleto = item.NombreCompleto,
                            Estado = item.Estado,
                            TipoUsuario = item.TipoUsuario
                        });
                    }
                    return datos;
                }
                return null;

            }
        }

        public RespuestaObj Registrar_Usuario(UsuarioObj usuario)
        {
            using (var con = new dbMartesEntities())
            {
                var resultado = con.Registrar_Datos_Usuario(usuario.Correo, usuario.Contrasenna, usuario.NombreCompleto, usuario.TipoUsuario);

                if (resultado > 0)
                {
                    return new RespuestaObj
                    {
                        Codigo = 0,
                        Mensaje = "Registro Exitoso"
                    };
                }
                return null;

            }
        }

        private string CrearToken(string Correo)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, Correo)
            };

            var Token = "b14ca5898a4e4133bbce2ea2315a1916";
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Token));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}