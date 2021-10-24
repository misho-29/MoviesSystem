using Microsoft.Extensions.Configuration;
using MoviesSystem.App.EmailService;
using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Models.Responses;
using MoviesSystem.Domain.Services;
using MoviesSystem.ExternalService;
using MoviesSystem.ExternalService.Models;
using MoviesSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.App.NotificationManager
{
    public class NotificationManager : INotificationManager
    {
        private readonly IWatchlistService _watchlistService;
        private readonly IMoviesApiService _movieApiService;
        private readonly IEmailSender _emailSender;
        private readonly HtmlGenerator _htmlGenerator;
        private readonly IConfiguration _configuration;

        public NotificationManager(IWatchlistService watchlistService, IMoviesApiService movieApiService, IEmailSender emailSender
            , HtmlGenerator htmlGenerator, IConfiguration configuration)
        {
            _watchlistService = watchlistService;
            _movieApiService = movieApiService;
            _emailSender = emailSender;
            _htmlGenerator = htmlGenerator;
            _configuration = configuration;
        }

        public async Task NotifyAboutUnwatchedMovies()
        {
            int unwatcheMoviesMinCount = int.Parse(_configuration["UnwatcheMoviesMinCount"]);
            int excludedDaysCount = int.Parse(_configuration["ExcludedDaysCount"]);

            List<UnwatchedMoviesResponse> watchlists = _watchlistService.GetUnwatchedMovies(unwatcheMoviesMinCount, excludedDaysCount);

            foreach (var userWatchlist in watchlists)
            {
                MovieDetailsResponse topRatedMovie = new MovieDetailsResponse();
                float highestRating = 0;

                foreach (var userMovie in userWatchlist.Movies)
                {
                    MovieDetailsResponse movie = await _movieApiService.GetMovieDetails(userMovie.MovieId);

                    if (movie.ImDbRating != null && float.Parse(movie.ImDbRating) > highestRating)
                    {
                        topRatedMovie = movie;
                        highestRating = float.Parse(movie.ImDbRating);
                    }
                }

                var recipients = new string[] { "oonneexx@gmail.com" };
                var content = _htmlGenerator.GenerateReminderMessage(topRatedMovie.Title, topRatedMovie.Wikipedia.PlotShort.Html,
                                                            topRatedMovie.ImDbRating, topRatedMovie.Image);
                var message = new Message(recipients, "Reminder", content);


                await _emailSender.SendEmailAsync(message);
            }
        }
    }
}
