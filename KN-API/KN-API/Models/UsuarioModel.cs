using KN_API.BaseDatos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace KN_API.Models
{
    public class UsuarioModel
    {
        public RespuestaUsuarioObj Validar_Usuario(UsuarioObj usuario)
        {
            using (var con = new dbMartesEntities())
            {
                var resultado = con.Consultar_Datos_Usuario(usuario.Correo, usuario.Contrasenna).FirstOrDefault();
                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();
                
                if (resultado != null)
                {
                    UsuarioObj usuarioEncontrado = new UsuarioObj();

                    usuarioEncontrado.Correo = resultado.Correo;
                    usuarioEncontrado.NombreCompleto = resultado.NombreCompleto;
                    usuarioEncontrado.Estado = resultado.Estado;
                    usuarioEncontrado.TipoUsuario = resultado.TipoUsuario;
                    usuarioEncontrado.Token = CrearToken(usuario.Correo);

                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Ok";
                    respuesta.objeto = usuarioEncontrado;
                }
                else
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "No se encontraron resultados";
                }

                return respuesta;
            }
        }

        public RespuestaUsuarioObj Registrar_Usuario(UsuarioObj usuario)
        {
            using (var con = new dbMartesEntities())
            {
                var resultado = con.Registrar_Datos_Usuario(usuario.Correo, usuario.Contrasenna, usuario.NombreCompleto, usuario.TipoUsuario);
                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();

                if (resultado > 0)
                {
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Ok";
                }
                else
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "No se realizó la transacción";
                }

                return respuesta;
            }
        }

        public RespuestaUsuarioObj Consultar_Usuarios_Estado(bool activo)
        {
            using (var con = new dbMartesEntities())
            {
                var resultado = (from x in con.TUSUARIO
                                 where x.Estado == activo
                                 select x).ToList();

                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();

                if (resultado.Count > 0)
                {
                    List<UsuarioObj> datos = new List<UsuarioObj>();

                    foreach (var item in resultado)
                    {
                        datos.Add(new UsuarioObj
                        {
                            Correo = item.Correo,
                            NombreCompleto = item.NombreCompleto,
                            Estado = item.Estado,
                            TipoUsuario = item.TipoUsuario
                        });
                    }

                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Ok";
                    respuesta.lista = datos;
                }
                else
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "No se encontraron resultados";
                }

                return respuesta;
            }
        }

        private string CrearToken(string Correo)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, Correo)
            };

            var llave = "c3e59tjnx02ovqdd51nwjjyzmmylbdfh";
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(llave));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(3),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RespuestaUsuarioObj EnviarCorreo(ClaseCorreo email)
        {
            if (email.Destinatario.Trim() != string.Empty)
            {
                string servidor = "smtp.office365.com";
                int puerto = 587;
                string CorreoNotificacion = ConfigurationManager.AppSettings["UsuarioCorreo"].ToString();
                string Clave = ConfigurationManager.AppSettings["ClaveCorreo"].ToString();

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(CorreoNotificacion);
                correo.To.Add(email.Destinatario);

                if (email.CopiaDestinatario.Trim() != string.Empty)
                {
                    MailAddress copy = new MailAddress(email.CopiaDestinatario);
                    correo.CC.Add(copy);
                }

                correo.Subject = email.Asunto;
                correo.Body = email.Mensaje;
                correo.IsBodyHtml = false;
                correo.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = servidor;
                smtp.Port = puerto;
                smtp.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate
                (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                smtp.Credentials = new System.Net.NetworkCredential(CorreoNotificacion, Clave);

                smtp.Send(correo);

                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();
                respuesta.Codigo = 1;
                respuesta.Mensaje = "Correo enviado";
                return respuesta;
            }
            else
            {
                RespuestaUsuarioObj respuesta = new RespuestaUsuarioObj();
                respuesta.Codigo = 0;
                respuesta.Mensaje = "Correo no enviado";
                return respuesta;
            }
        }

    }
}