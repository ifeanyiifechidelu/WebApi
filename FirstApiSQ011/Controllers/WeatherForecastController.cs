using FirstApiSQ011.Models;
using FirstApiSQ011.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApiSQ011.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IJWTSecurity _jwtSec;

        //private readonly IConfiguration _config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJWTSecurity jWTSecurity)
        {
            _logger = logger;
            _jwtSec = jWTSecurity;
        }   

        [HttpGet]
        [Authorize(Policy = "DecaDevRole")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("get-token")]
        public IActionResult GetToken(string role)
        {
            var user = new User
            {
                LastName = "Owens",
                FirstName = "Mike",
                Email = "michealowengmail.com",
                //role = "Decadev"
            };
            
            //var jwtGen = new JWTSecurity(_config);
            var token = _jwtSec.JWTGen(user, role);
            return Ok(token);
            //return jwtGen.JWTGen();
        }
    }
}
