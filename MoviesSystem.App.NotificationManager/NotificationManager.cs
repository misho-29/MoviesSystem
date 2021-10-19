using MoviesSystem.App.EmailService;
using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Services;
using MoviesSystem.ExternalService;
using MoviesSystem.ExternalService.Models;
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
        private readonly IMovieApiService _movieApiService;
        private readonly IEmailSender _emailSender;
        private readonly HtmlGenerator _htmlGenerator;

        public NotificationManager(IWatchlistService watchlistService, IMovieApiService movieApiService, IEmailSender emailSender
            , HtmlGenerator htmlGenerator)
        {
            _watchlistService = watchlistService;
            _movieApiService = movieApiService;
            _emailSender = emailSender;
            _htmlGenerator = htmlGenerator;
        }

        public async Task NotifyAboutUnwatchedMovies()
        {
            List<UnwatchedMoviesGetModel> watchlists = _watchlistService.GetUnwatchedMovies(3, 30);

            foreach (var userWatchlist in watchlists)
            {
                GetMovieDetailsResponseModel topRatedMovieDetails = new GetMovieDetailsResponseModel();
                float highestRating = 0;

                foreach (var userMovie in userWatchlist.Movies)
                {
                    GetMovieDetailsResponseModel movieDetails = await _movieApiService.GetMovieDetails(userMovie.MovieId);

                    if (topRatedMovieDetails != null && float.Parse(movieDetails.ImDbRating) > highestRating)
                    {
                        topRatedMovieDetails = movieDetails;
                        highestRating = float.Parse(movieDetails.ImDbRating);
                    }
                }

                var recipients = new string[] { "oonneexx@gmail.com" };

                var content = _htmlGenerator.GenerateReminderMessage(topRatedMovieDetails.Title, topRatedMovieDetails.Wikipedia.PlotShort.Html,
                                                            topRatedMovieDetails.ImDbRating, topRatedMovieDetails.Image);
                var message = new Message(recipients, "Reminder", content);


                await _emailSender.SendEmailAsync(message);
            }
        }
    }
}
