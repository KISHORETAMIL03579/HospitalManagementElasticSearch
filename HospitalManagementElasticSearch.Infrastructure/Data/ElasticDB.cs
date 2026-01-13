using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using HospitalManagementElasticSearch.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Infrastructure.Data 
{
    public class ElasticDB
    {
        private readonly ElasticsearchClient _client;
        private readonly ILogger<ElasticDB> _logger;
        private const string HospitalIndex = "hospitals";
        private const string DoctorIndex = "doctors";

        public ElasticDB(ElasticsearchClient client, ILogger<ElasticDB> logger)
        {
            _client = client;
            _logger = logger;
        }

        #region Create All Indices

        // Create all indices
        public async Task<bool> CreateAllIndicesAsync()
        {
            try
            {
                var hospital = await CreateHospitalIndexAsync();
                var doctor = await CreateDoctorIndexAsync();
                return hospital && doctor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating indices");
                return false;
            }
        }

        #endregion

        #region Create Hospital Index
        // Create hospital index
        public async Task<bool> CreateHospitalIndexAsync()
        {
            var existsResponse = await _client.Indices.ExistsAsync(HospitalIndex);
            if (existsResponse.Exists)
            {
                _logger.LogInformation("Hospital index already exists");
                return true;
            }
            var response = await _client.Indices.CreateAsync(HospitalIndex, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                    .Analysis(a => a
                        .Analyzers(an => an
                            .Custom("hospital_analyzer", ca => ca
                                .Tokenizer("standard")
                                .Filter("lowercase", "stop")
                            )
                        )
                    )
                )
                .Mappings(m => m
                //   .Properties(ps => ps
                //        //.IntegerNumber(n => n.Name(nameof(ElasticHospital.Id)))
                //        .Text(t => t.Name(nameof(ElasticHospital.Name)))
                //        .Text(t => t.Name(nameof(ElasticHospital.Location)))
                //    )
                //)
                .Properties(new Properties
                    {
                        {
                            nameof(Hospital.Name),
                            new TextProperty
                            {
                                Analyzer = "hospital_analyzer"
                            }
                        } ,

                        {
                            nameof(Hospital.Location),
                            new TextProperty
                            {
                                Analyzer = "hospital_analyzer"
                            }
                        }
                    })
                )
            );

            return response.IsValidResponse;
        
        }

        #endregion

        #region Delete Hospital By Id
        // Delete hospital by id
        public async Task<bool> DeleteHospitalAsync(Guid id)
        {
            try
            {
                var response = await _client.DeleteAsync<Hospital>(id.ToString(), d => d.Index(HospitalIndex));

                if (response.IsValidResponse)
                {
                    _logger.LogInformation("Hospital document with ID {Id} deleted successfully", id);
                    return true;
                }

                _logger.LogError("Failed to delete hospital document with ID {Id}: {Error}", id, response.DebugInformation);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting hospital document with ID {Id}", id);
                return false;
            }
        }

        #endregion

        #region Create Doctor Index

        // Create doctor index
        public async Task<bool> CreateDoctorIndexAsync()
        {
            var existsResponse = await _client.Indices.ExistsAsync(DoctorIndex);
            if (existsResponse.Exists)
            {
                _logger.LogInformation("Doctor index already exists");
                return true;
            }
            var response = await _client.Indices.CreateAsync(DoctorIndex, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                    .Analysis(a => a
                        .Analyzers(an => an
                            .Custom("doctor_analyzer", ca => ca
                                .Tokenizer("standard")
                                .Filter("lowercase", "stop")
                            )
                        )
                    )
                )
                .Mappings(m => m
                .Properties(new Properties
                    {
                        {
                            nameof(Doctor.Name),
                            new TextProperty
                            {
                                Analyzer = "doctor_analyzer"
                            }
                        } ,
                        {
                            nameof(Doctor.Specialization),
                            new TextProperty
                            {
                                Analyzer = "doctor_analyzer"
                            }
                        }
                    })
                )
            );
            // Implementation for creating Doctor index can be added here
            return response.IsValidResponse;
        }

        #endregion

        #region Delete Doctor By Id
        // Delete doctor by id
        public async Task<bool> DeleteDoctorAsync(Guid id)
        {
            try
            {
                var response = await _client.DeleteAsync<Doctor>(id.ToString(), d => d.Index(DoctorIndex));
                if (response.IsValidResponse)
                {
                    _logger.LogInformation("Doctor document with ID {Id} deleted successfully", id);
                    return true;
                }
                _logger.LogError("Failed to delete doctor document with ID {Id}: {Error}", id, response.DebugInformation);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting doctor document with ID {Id}", id);
                return false;
            }
        }

        #endregion

        #region Delete All Indices
        // Delete all indicies
        public async Task<bool> DeleteAllIndicesAsync()
        {
            var hospitalDeleted = true;
            var doctorDeleted = true;

            var hospitalExists = await _client.Indices.ExistsAsync(HospitalIndex);
            if (hospitalExists.Exists)
            {
                var resp = await _client.Indices.DeleteAsync(HospitalIndex);
                hospitalDeleted = resp.IsValidResponse;
            }

            var doctorExists = await _client.Indices.ExistsAsync(DoctorIndex);
            if (doctorExists.Exists)
            {
                var resp = await _client.Indices.DeleteAsync(DoctorIndex);
                doctorDeleted = resp.IsValidResponse;
            }

            return hospitalDeleted && doctorDeleted;
        }

        #endregion

    }
}

