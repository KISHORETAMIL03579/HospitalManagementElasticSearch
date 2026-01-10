using HospitalManagementElasticSearch.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.Interfaces
{
    public interface IHospitalService
    {
        Task<IEnumerable<HospitalDetailDTO>> GetAll();
        Task<HospitalDetailDTO?> GetById(Guid id);
        Task<Guid> Create(HospitalCreateDTO hospitaldto);
        Task UpdateById(Guid id, HospitalUpdateDTO hospitaldto);
        Task DeleteById(Guid id);
    }
}
