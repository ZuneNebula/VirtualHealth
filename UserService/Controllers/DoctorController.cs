using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorsService doctorService;

        public DoctorController(DoctorsService doctorsService)
        {
            this.doctorService = doctorsService;
        }

        [Route("/users/doctors")]
        [HttpPost]
        public async Task<IActionResult> Post(Doctor doctor)
        {
            var user = await doctorService.CreateAsync(doctor);

            return Created("created", user);

        }

        [Route("/users/doctors")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await doctorService.GetAsync();

            return Ok(users);

        }

        [Route("/users/doctors/{email}")]
        [HttpGet]
        public async Task<IActionResult> Get(String email)
        {
            var user = await doctorService.GetAsync(email);

            return Ok(user);

        }

        [Route("/users/doctors/{email}")]
        [HttpPut]
        public async Task<IActionResult> Put(String email, Doctor doctor)
        {
            var user = await doctorService.UpdateAsync(email, doctor);

            return Ok(user);

        }

        [Route("/users/doctors/{email}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(String email)
        {
            var user = await doctorService.DeleteAsync(email);

            return Ok(user);

        }
    }
}
