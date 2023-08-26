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
    public class CitaService : ICitaService
    {
        private readonly IGenericRepository<Cita> _citaRepository;
        private readonly ICitaRepository _citarepositoryActivarDesactivar;
        private readonly IMapper _mapper;

        public CitaService(IGenericRepository<Cita> citaRepository, ICitaRepository citarepositoryActivarDesactivar, IMapper mapper)
        {
            _citaRepository = citaRepository;
            _citarepositoryActivarDesactivar = citarepositoryActivarDesactivar;
            _mapper = mapper;
        }

        public async Task<List<CitaDTO>> Lista()
        {
            try
            {
                var listaCitas = await _citaRepository.Consultar();
                var queryCitas = listaCitas
                    .Include(p => p.IdPacienteNavigation.IdPersonaNavigation)
                    .Include(d => d.IdDoctorNavigation.IdPersonaNavigation)
                    .Include(e => e.IdDoctorNavigation.IdEspecialidadNavigation)
                    .ToList();
                return _mapper.Map<List<CitaDTO>>(queryCitas);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CitaDTO> Crear(CitaDTO citaDto)
        {
            var citaCreada = await _citaRepository.Crear(_mapper.Map<Cita>(citaDto));
            if (citaCreada.IdCita == 0)
            {
                throw new Exception("No se pudo crear");
            }
            var query = await _citaRepository.Consultar(c => c.IdCita == citaCreada.IdCita);

            citaCreada = query
                .Include(p => p.IdPacienteNavigation)
                .Include(d => d.IdDoctorNavigation)
                .ThenInclude(e => e.IdEspecialidadNavigation)
                .First();

            return _mapper.Map<CitaDTO>(citaCreada);
        }

        public async Task<bool> Aprobar(int id)
        {
            try
            {
                var queryCita = await _citaRepository.Consultar(c => c.IdCita == id);

                if (queryCita.Count() == 0)
                {
                    throw new Exception("La cita no existe");
                }

                var cita = queryCita.First();

                cita.Estado = 2;

                bool respuestaCita = await _citarepositoryActivarDesactivar.Aprobar(cita);

                if (!respuestaCita)
                {
                    throw new Exception("No se pudo Aceptar la cita");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Rechazar(int id)
        {
            try
            {
                var queryCita = await _citaRepository.Consultar(c => c.IdCita == id);

                if (queryCita.Count() == 0)
                {
                    throw new Exception("La cita no existe");
                }

                var cita = queryCita.First();

                cita.Estado = 3;

                bool respuestaCita = await _citarepositoryActivarDesactivar.Rechazar(cita);

                if (!respuestaCita)
                {
                    throw new Exception("No se pudo Aceptar la cita");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> Reprogramar(string fechaInicio, string fechaFin)
        {
            throw new NotImplementedException();
        }
    }
}
