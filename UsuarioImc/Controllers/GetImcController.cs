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
    public class GetImcController : ApiController
    {
        private UsuarioImcContext db = new UsuarioImcContext();

        // GET: api/Usuarios
        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuarios;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(string id)
        {
            var registrosUsuario = db.Usuarios.Where(s => s.IdUsuario.Equals(id));
            if (registrosUsuario.Count() == 0)
            {
                return NotFound();
            }

            decimal promedioIMC = 0;

            foreach(Usuario usuario in registrosUsuario)
            {
                decimal IMC = usuario.peso / (usuario.altura * usuario.altura);

                promedioIMC = promedioIMC + IMC;
            }

            promedioIMC = promedioIMC / registrosUsuario.Count();

            return Ok(
                new
                {
                    promedioIMC,
                    registrosUsuario
                }
                );
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