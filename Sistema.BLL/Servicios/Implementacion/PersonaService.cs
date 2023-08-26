using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema.BLL.Recursos;
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
    public class PersonaService: IPersonaService
    {
        private readonly IGenericRepository<Persona> _personaRepositorio;
        private readonly IMapper _mapper;

        public PersonaService(IGenericRepository<Persona> personaRepositorio, IMapper mapper)
        {
            _personaRepositorio = personaRepositorio;
            _mapper = mapper;
        }

        public async Task<PersonaDTO> Crear(PersonaDTO personaDto)
        {
            var personaCreada = await _personaRepositorio.Crear(_mapper.Map<Persona>(personaDto));
            if (personaCreada.IdUsuario == 0)
            {
                throw new Exception("No se pudo crear");
            }
            var query = await _personaRepositorio.Consultar(p => p.IdPersona == personaCreada.IdPersona);
            personaCreada = query.Include(u => u.IdUsuarioNavigation).First();
            return _mapper.Map<PersonaDTO>(personaCreada);
        }
    }
}
