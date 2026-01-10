using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Application.Configuration
{
    public class ElasticsearchSettings
    {
        public string Uri { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
