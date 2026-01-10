using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public Guid HospitalId { get; set; }
        public Hospital Hospital { get; set; } = null!;
    }
}
