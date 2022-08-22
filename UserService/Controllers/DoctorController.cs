using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorsService doctorService;

        private readonly string bootstrapServers = "localhost:9092";
        private readonly string topic = "test";

        public DoctorController(DoctorsService doctorsService)
        {
            this.doctorService = doctorsService;
        }

        private async Task<bool> SendDoctorRequest
        (string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");
                return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }

    [Route("/users/doctors")]
        [HttpPost]
        public async Task<IActionResult> Post(Doctor doctor)
        {
            var user = await doctorService.CreateAsync(doctor);
            UserDto userDto = new UserDto();
            userDto.Email = doctor.Email;
            userDto.Password = doctor.Password;
            userDto.Role = UserRoleDto.Doctor;

            string message = JsonSerializer.Serialize(userDto);
            Console.WriteLine(message);
            await SendDoctorRequest(topic, message);
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
