using AutoMapper;
using Sistema.BLL.Servicios.Contrato;
using Sistema.DAL.Repositorios.Contrato;
using Sistema.DTO;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL.Servicios.Implementacion
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IGenericRepository<Especialidad> _especialidadRepositorio;
        private readonly IMapper _mapper;

        public EspecialidadService(IGenericRepository<Especialidad> especialidadRepositorio, IMapper mapper)
        {
            _especialidadRepositorio = especialidadRepositorio;
            _mapper = mapper;
        }

        public async Task<List<EspecialidadDTO>> Lista()
        {
            try
            {
                var listaRoles = await _especialidadRepositorio.Consultar();
                return _mapper.Map<List<EspecialidadDTO>>(listaRoles.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
