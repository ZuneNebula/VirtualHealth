using Microsoft.AspNetCore.Mvc;
using UserService.Services;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientsService patientService;

        public PatientController(PatientsService patientService)
        {
            this.patientService = patientService;
        }

        [Route("/users/patients")]
        [HttpPost]
        public async Task<IActionResult> Post(Patient patient)
        {
            var user = await patientService.CreateAsync(patient);

            return Created("created", user);
                       
        }

        [Route("/users/patients")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await patientService.GetAsync();

            return Ok(users);

        }

        [Route("/users/patients/{email}")]
        [HttpGet]
        public async Task<IActionResult> Get(String email)
        {
            var user = await patientService.GetAsync(email);

            return Ok(user);

        }

        [Route("/users/patients/{email}")]
        [HttpPut]
        public async Task<IActionResult> Put(String email, Patient patient)
        {
            var user = await patientService.UpdateAsync(email, patient);

            return Ok(user);

        }

        [Route("/users/patients/{email}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(String email)
        {
            var user = await patientService.DeleteAsync(email);

            return Ok(user);

        }
    }
}
