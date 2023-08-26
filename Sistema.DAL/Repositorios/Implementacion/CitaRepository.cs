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
    public class CitaRepository : GenericRepository<Cita>, ICitaRepository
    {
        private readonly DbcitasmedicasContext _dbcontext;

        public CitaRepository(DbcitasmedicasContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Aprobar(Cita cita)
        {
            using (var trasaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    _dbcontext.Cita.Update(cita);
                    await _dbcontext.SaveChangesAsync();

                    trasaction.Commit();
                }
                catch (Exception)
                {
                    trasaction.Rollback();
                    throw;
                }

                return true;
            }
        }

        public async Task<bool> Rechazar(Cita cita)
        {
            using (var trasaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    _dbcontext.Cita.Update(cita);
                    await _dbcontext.SaveChangesAsync();

                    trasaction.Commit();
                }
                catch (Exception)
                {
                    trasaction.Rollback();
                    throw;
                }

                return true;
            }
        }
    }
}
