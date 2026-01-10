using HospitalManagementElasticSearch.Application.DTO;
using HospitalManagementElasticSearch.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementElasticSearch.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _service;
        public HospitalController(IHospitalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hospital = await _service.GetAll();
            return Ok(hospital);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var hospital = await _service.GetById(id);
            return Ok(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HospitalCreateDTO hospitaldto)
        {
            await _service.Create(hospitaldto);
            return Ok("Elastic Hospital Created Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateById(Guid id, HospitalUpdateDTO hospitaldto)
        {
            await _service.UpdateById(id, hospitaldto);
            return Ok("Elastic Hospital Updated Successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await _service.DeleteById(id);
            return Ok("Elastic Hospital Deleted Successfully");
        }
    }
}
