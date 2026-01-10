using HospitalManagementElasticSearch.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDTO>> GetAll();
        Task<DoctorDTO?> GetById(Guid id);
        Task<Guid> Create(DoctorCreateDTO doctordto);
        Task UpdateById(Guid id, DoctorUpdateDTO doctordto);
        Task DeleteById(Guid id);
    }
}
