using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Domain.Entities
{
    public class Hospital
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
