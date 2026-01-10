using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.DTO
{
    public class DoctorDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string HospitalName { get; set; } = string.Empty;
    }
    public class DoctorCreateDTO
    {
        public required string Name { get; set; }
        public required string Specialization { get; set; }
    }

    public class DoctorUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string HospitalName { get; set; } = string.Empty;
    }

}
