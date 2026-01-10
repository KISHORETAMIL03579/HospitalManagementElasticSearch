using HospitalManagementElasticSearch.Application.DTO;
using HospitalManagementElasticSearch.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementElasticSearch.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctor = await _service.GetAll();
            return Ok(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var doctor = await _service.GetById(id);
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateDTO doctordto)
        {
            await _service.Create(doctordto);
            return Ok("Elastic Doctor Created Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateById(Guid id, DoctorUpdateDTO doctordto)
        {
            await _service.UpdateById(id, doctordto);
            return Ok("Elastic Doctor Updated Successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await _service.DeleteById(id);
            return Ok("Elastic Doctor Deleted Successfully");
        }
    }
}
