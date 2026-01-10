using AutoMapper;
using HospitalManagementElasticSearch.Application.DTO;
using HospitalManagementElasticSearch.Application.Interfaces;
using HospitalManagementElasticSearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _repo;
        private readonly IMapper _mapper;
        public HospitalService(IHospitalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HospitalDetailDTO>> GetAll()
        {
            var hospital = await _repo.GetAll();
            return _mapper.Map<IEnumerable<HospitalDetailDTO>>(hospital);
        }

        public async Task<HospitalDetailDTO?> GetById(Guid id)
        {
            var hospital = await _repo.GetById(id);
            if (hospital is null)
                throw new KeyNotFoundException($"Hospital with id {id} not found.");
            return _mapper.Map<HospitalDetailDTO>(hospital);
        }

        public async Task<Guid> Create(HospitalCreateDTO hospitaldto)
        {
            var hospital = _mapper.Map<Hospital>(hospitaldto);

            // Save to repository
            await _repo.Create(hospital);

            return hospital.Id;
        }

        public async Task UpdateById(Guid id, HospitalUpdateDTO hospitaldto)
        {
            var hospital = await _repo.GetById(id);
            if (hospital is null)
                throw new KeyNotFoundException($"Hospital with id {id} not found.");
            var updatedHospital = _mapper.Map(hospitaldto, hospital);
            await _repo.UpdateById(id, updatedHospital);
        }


        public async Task DeleteById(Guid id)
        {
            var hospital = await _repo.GetById(id);
            if (hospital is null)
                throw new KeyNotFoundException($"Hospital with id {id} not found.");
            await _repo.DeleteById(id);
        }
    }
}
