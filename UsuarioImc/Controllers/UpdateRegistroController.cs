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
    public class UpdateRegistroController : ApiController
    {
        private UsuarioImcContext db = new UsuarioImcContext();

        // PUT: api/Usuarios/5
        /// <summary>
        /// Actualiza el registro correspondiente al id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="registro"></param>
        /// <returns>Devuelve todos los registros del usuario especificado en registro</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, RegistroUsuario registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario usuario = db.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!usuario.IdUsuario.Equals(registro.IdUsuario))
            {
                return Ok("El id del registro no corresponde al usuario");
                //return BadRequest();
            }

            usuario.altura = registro.altura;
            usuario.peso = registro.peso;

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();

                return Ok(db.Usuarios.Where(s => s.IdUsuario.Equals(registro.IdUsuario)));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Usuarios/
        /// <summary>
        /// Actualiza el ultimo registro del usuario indicado en registro
        /// </summary>
        /// <param name="registro"></param>
        /// <returns>Devuelve todos los registros del usuario</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(RegistroUsuario registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Usuario usuario = db.Usuarios.Where(s => s.IdUsuario.Equals(registro.IdUsuario))
                .OrderByDescending(s => s.Id).First();

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.altura = registro.altura;
            usuario.peso = registro.peso;

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();

                return Ok(db.Usuarios.Where(s => s.IdUsuario.Equals(registro.IdUsuario)));
            }
            catch (DbUpdateConcurrencyException)
            {
                /*if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }*/
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.Count(e => e.Id == id) > 0;
        }
    }
}