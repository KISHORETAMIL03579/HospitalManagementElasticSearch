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
        }
    }
}
