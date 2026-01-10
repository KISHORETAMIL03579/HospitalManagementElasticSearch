using Elastic.Clients.Elasticsearch;
using HospitalManagementElasticSearch.Domain.Entities;
using HospitalManagementElasticSearch.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementElasticSearch.Infrastructure.Repository
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly ElasticsearchClient _client;
        private const string IndexName = "hospitals";

        public HospitalRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            var response = await _client.SearchAsync<Hospital>(s => s
                .Indices(IndexName)
                .Size(1000)
            );
            return response.Documents;
        }

        public async Task<Hospital?> GetById(Guid id)
        {
            var response = await _client.GetAsync<Hospital>(id.ToString(), g => g.Index(IndexName));
            return response.Found ? response.Source : null;
        }

        public async Task Create(Hospital hospital)
        {
            await _client.IndexAsync(hospital, i => i
                .Index(IndexName)
                .Id(hospital.Id.ToString())
            );
        }

        public async Task UpdateById(Guid id, Hospital hospital)
        {
            //await _client.UpdateAsync<Hospital, Hospital>(IndexName, id.ToString(), u => u.Doc(hospital));
            //await _client.UpdateAsync<Hospital, Hospital>(u => u
            //    .Index(IndexName)   // specify the index
            //    .Id(id)             // document ID
            //    .Doc(hospital)      // partial update object
            //);

            var updateRequest = new UpdateRequest<Hospital, Hospital>(IndexName, id)
            {
                Doc = hospital
            };

            await _client.UpdateAsync(updateRequest);

        }

        public async Task DeleteById(Guid id)
        {
            await _client.DeleteAsync(IndexName, id.ToString());
        }
    }
}
