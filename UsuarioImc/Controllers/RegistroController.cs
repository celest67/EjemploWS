using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UsuarioImc.Models;

namespace UsuarioImc.Controllers
{
    public class RegistroController : ApiController
    {
        private UsuarioImcContext db = new UsuarioImcContext();
        
        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(RegistroUsuario registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario usuario = new Usuario();
            usuario.altura = registro.altura;
            usuario.fecha = DateTime.Now;
            usuario.IdUsuario = registro.IdUsuario;
            usuario.peso = registro.peso;

            db.Usuarios.Add(usuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuario.Id }, usuario);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}