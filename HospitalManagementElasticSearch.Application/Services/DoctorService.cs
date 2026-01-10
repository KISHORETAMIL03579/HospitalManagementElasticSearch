using AutoMapper;
using HospitalManagementElasticSearch.Application.DTO;
using HospitalManagementElasticSearch.Application.Interfaces;
using HospitalManagementElasticSearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IHospitalRepository _hospitalRepo;
        private readonly IMapper _mapper;
        public DoctorService(IDoctorRepository repo, IMapper mapper, IHospitalRepository hospitalRepo) 
        {
            _repo = repo;
            _mapper = mapper;
            _hospitalRepo = hospitalRepo;
        }
        public async Task<IEnumerable<DoctorDTO>> GetAll()
        {
            var doctors = await _repo.GetAll();
            return _mapper.Map<IEnumerable<DoctorDTO>>(doctors);
        }

        public async Task<DoctorDTO?> GetById(Guid id)
        {
            var doctor = await _repo.GetById(id);
            if (doctor == null) return null;
            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task<Guid> Create(DoctorCreateDTO dto)
        {
            var doctor = _mapper.Map<Doctor>(dto);
            await _repo.Create(doctor);
            return doctor.Id;
        }

        public async Task UpdateById(Guid id, DoctorUpdateDTO dto)
        {
            var existingDoctor = await _repo.GetById(id);
            if (existingDoctor == null)
                throw new Exception($"Doctor with ID '{id}' not found");

            if (!string.IsNullOrWhiteSpace(dto.HospitalName))
            {
                var hospital = await _hospitalRepo.GetByNameAsync(dto.HospitalName);
                if (hospital == null)
                    throw new Exception($"Hospital '{dto.HospitalName}' not found");

                existingDoctor.HospitalId = hospital.Id;
                existingDoctor.Hospital = hospital;
            }

            _mapper.Map(dto, existingDoctor);

            await _repo.UpdateById(id, existingDoctor);
        }

        public async Task DeleteById(Guid id)
        {
            var existingDoctor = await _repo.GetById(id);
            if (existingDoctor == null)
                throw new Exception($"Doctor with ID '{id}' not found");

            await _repo.DeleteById(id);
        }
    }
}
