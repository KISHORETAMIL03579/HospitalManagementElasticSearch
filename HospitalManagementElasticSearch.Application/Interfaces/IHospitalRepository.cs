using HospitalManagementElasticSearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.Interfaces
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAll();
        Task<Hospital?> GetById(Guid id);
        Task Create(Hospital hospital);
        Task UpdateById(Guid id, Hospital hospital);
        Task DeleteById(Guid id);
    }
}
