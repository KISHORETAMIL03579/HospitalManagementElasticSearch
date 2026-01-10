using HospitalManagementElasticSearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor?> GetById(Guid id);
        Task Create(Doctor doctor);
        Task UpdateById(Guid id, Doctor doctor);
        Task DeleteById(Guid id);
    }
}
