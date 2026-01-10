using AutoMapper;
using HospitalManagementElasticSearch.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;
        public DoctorService(IDoctorRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }
    }
}
