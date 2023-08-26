using Microsoft.EntityFrameworkCore;
using Sistema.DAL.DBContext;
using Sistema.DAL.Repositorios.Contrato;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DAL.Repositorios.Implementacion
{
    public class UsuarioRepository: GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly DbcitasmedicasContext _dbcontext;

        public UsuarioRepository(DbcitasmedicasContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Usuario> Crear(Usuario usuario)
        {
            Usuario usuarioCreado = new Usuario();
            using (var trasaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    await _dbcontext.Usuarios.AddAsync(usuario);                     
                    await _dbcontext.SaveChangesAsync();

                    usuarioCreado = usuario;

                    trasaction.Commit();
                }
                catch (Exception)
                {
                    trasaction.Rollback();
                    throw;
                }

                return usuarioCreado;
            }
        }
    }
}
