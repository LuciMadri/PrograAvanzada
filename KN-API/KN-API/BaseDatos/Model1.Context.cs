﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KN_API.BaseDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class dbMartesEntities : DbContext
    {
        public dbMartesEntities()
            : base("name=dbMartesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TBITACORA> TBITACORA { get; set; }
        public virtual DbSet<TPERFIL> TPERFIL { get; set; }
        public virtual DbSet<TUSUARIO> TUSUARIO { get; set; }
    
        public virtual ObjectResult<Consultar_Datos_Usuario_Result> Consultar_Datos_Usuario(string correo, string contrasenna)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Consultar_Datos_Usuario_Result>("Consultar_Datos_Usuario", correoParameter, contrasennaParameter);
        }
    
        public virtual int Registrar_Datos_Usuario(string correo, string contrasenna, string nombreCompleto, Nullable<int> tipoUsuario)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            var nombreCompletoParameter = nombreCompleto != null ?
                new ObjectParameter("NombreCompleto", nombreCompleto) :
                new ObjectParameter("NombreCompleto", typeof(string));
    
            var tipoUsuarioParameter = tipoUsuario.HasValue ?
                new ObjectParameter("TipoUsuario", tipoUsuario) :
                new ObjectParameter("TipoUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Registrar_Datos_Usuario", correoParameter, contrasennaParameter, nombreCompletoParameter, tipoUsuarioParameter);
        }
    
        public virtual int Registrar_Bitacora(string correo, Nullable<System.DateTime> fechaHora, Nullable<int> codigoError, string descripcion, string origen)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var fechaHoraParameter = fechaHora.HasValue ?
                new ObjectParameter("FechaHora", fechaHora) :
                new ObjectParameter("FechaHora", typeof(System.DateTime));
    
            var codigoErrorParameter = codigoError.HasValue ?
                new ObjectParameter("CodigoError", codigoError) :
                new ObjectParameter("CodigoError", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var origenParameter = origen != null ?
                new ObjectParameter("Origen", origen) :
                new ObjectParameter("Origen", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Registrar_Bitacora", correoParameter, fechaHoraParameter, codigoErrorParameter, descripcionParameter, origenParameter);
        }
    }
}