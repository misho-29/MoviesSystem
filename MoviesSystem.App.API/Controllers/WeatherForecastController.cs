using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesSystem.App.EmailService;
using MoviesSystem.ExternalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSystem.App.API.Controllers
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
        private readonly IEmailSender _emailSender;
        private readonly IMovieApiService _movieApiService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEmailSender emailSender, IMovieApiService movieApiService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _movieApiService = movieApiService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _movieApiService.GetMovieDetails("tt1375666");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}


//[HttpGet]
//public async Task<IEnumerable<WeatherForecast>> Get()
//{
//    var recipients = new string[] { "oonneexx@gmail.com" };
//    var subject = "Subject";
//    var text = "Text";
//    var imageUrl = "https://imdb-api.com/images/original/MV5BODllNWE0MmEtYjUwZi00ZjY3LThmNmQtZjZlMjI2YTZjYmQ0XkEyXkFqcGdeQXVyNTc1NTQxODI@._V1_Ratio0.6791_AL_.jpg";
//    var content = string.Format(@"<h2 style='color:red;'>{0}</h2><center><img src=""{1}""></center>", text, imageUrl);

//    var message = new Message(recipients, subject, content);
//    await _emailSender.SendEmailAsync(message);

//    var rng = new Random();
//    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//    {
//        Date = DateTime.Now.AddDays(index),
//        TemperatureC = rng.Next(-20, 55),
//        Summary = Summaries[rng.Next(Summaries.Length)]
//    })
//    .ToArray();
//}