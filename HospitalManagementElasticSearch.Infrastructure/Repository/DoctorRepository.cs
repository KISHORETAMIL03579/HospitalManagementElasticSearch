using Elastic.Clients.Elasticsearch;
using HospitalManagementElasticSearch.Application.Interfaces;
using HospitalManagementElasticSearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Infrastructure.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ElasticsearchClient _client;
        private const string IndexName = "doctors";
        public DoctorRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            var response = await _client.SearchAsync<Doctor>(s => s
                .Indices(IndexName)
            );
            return response.Documents;
        }

        public async Task<Doctor?> GetById(Guid id)
        {
            var response = await _client.GetAsync<Doctor>(id.ToString(), g => g.Index(IndexName));
            return response.Found ? response.Source : null;
        }

        public async Task Create(Doctor doctor)
        {
            await _client.IndexAsync(doctor, i => i
                .Index(IndexName)
                .Id(doctor.Id.ToString())
            );
        }

        public async Task UpdateById(Guid id, Doctor doctor)
        {
            var updateRequest = new UpdateRequest<Doctor, Doctor>(IndexName, id)
            {
                Doc = doctor
            };
            await _client.UpdateAsync(updateRequest);
        }

        public async Task DeleteById(Guid id)
        {
            await _client.DeleteAsync<Doctor>(id.ToString(), d => d.Index(IndexName));
        }
    } 
}
