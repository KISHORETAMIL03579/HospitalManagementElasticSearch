using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.DTO
{
    public class HospitalCreateShowDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
    }
    public class HospitalCreateDTO
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
    }

    public class HospitalUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

    public class HospitalDetailDTO
    {
        public string HospitalName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        //public List<HospitalDoctorDTO> Doctors { get; set; } = new List<HospitalDoctorDTO>();
    }
}
