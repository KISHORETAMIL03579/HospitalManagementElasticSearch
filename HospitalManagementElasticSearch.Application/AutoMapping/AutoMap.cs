using AutoMapper;
using HospitalManagementElasticSearch.Application.DTO;
using HospitalManagementElasticSearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.AutoMapping
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            // Entity → DetailDTO
            CreateMap<Hospital, HospitalDetailDTO>()
                .ForMember(dest => dest.HospitalName, opt => opt.MapFrom(src => src.Name));

            // CreateDTO → Entity
            CreateMap<HospitalCreateDTO, Hospital>().ReverseMap();

            // UpdateDTO → Entity
            CreateMap<HospitalUpdateDTO, Hospital>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dest => dest.HospitalName, opt => opt.MapFrom(src => src.Hospital.Name));

            CreateMap<DoctorCreateDTO, Doctor>().ReverseMap();

            CreateMap<DoctorUpdateDTO, Doctor>()
                .ForMember(dest => dest.HospitalId, opt => opt.Ignore())
                .ForMember(dest => dest.Hospital, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
