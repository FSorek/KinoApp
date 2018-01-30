using DataModel;
using Kina.Mobile.Core.Model;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    class ScoreViewModel : MvxViewModel<MovieDataSet>
    {
        private readonly IMvxNavigationService _navigationService;

        private MovieDataSet _parameter;
        private List<UserScore> userScore;

        public List<ScoreRow> ScoreRows { get; set; }

        public string HeaderLabel { get; set; }
        public string FooterLabel { get; set; }
        public int counter;

        public ScoreViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task Initialize(MovieDataSet parameter)
        {
            _parameter = parameter;

            int cinemaID = _parameter.MovieData.Shows[0].Id_Cinema;
            string movieID = _parameter.MovieData.Id_Movie;

            GetScore(movieID, cinemaID);

            string Title = _parameter.MovieData.Name;
            string Cinema = _parameter.CinemaName;

            HeaderLabel = String.Format("{0} in {1}", Title, Cinema);

            ScoreRows = new List<ScoreRow>();

            double avgSeats = 0;
            double avgScreen = 0;
            double avgCleanliness = 0;
            double avgPopcorn = 0;
            double avgSound = 0;
            counter = 0;

            if (userScore.Count != 0)
            {
                foreach (var score in userScore)
                {
                    counter++;
                    avgSeats += score.Seat;
                    avgScreen += score.Screen;
                    avgCleanliness += score.Cleanliness;
                    avgPopcorn += score.Popcorn;
                    avgSound += score.Sound;
                }
                avgSeats /= counter;
                avgScreen /= counter;
                avgCleanliness /= counter;
                avgPopcorn /= counter;
                avgSound /= counter;
            }

            FooterLabel = String.Format(" {0} ", counter);

            ScoreRows.Add(new ScoreRow(avgCleanliness, "Cleanliness"));
            ScoreRows.Add(new ScoreRow(avgScreen, "Screen"));
            ScoreRows.Add(new ScoreRow(avgSeats, "Seats"));
            ScoreRows.Add(new ScoreRow(avgSound, "Sound"));
            ScoreRows.Add(new ScoreRow(avgPopcorn, "Snacks"));

            return Task.FromResult(true);
        }

        private void GetScore(string movieId, int cinemaId)
        {
            Task.Run(() => GetScoreAsync(movieId, cinemaId)).Wait();
        }

        private async Task GetScoreAsync(string movieId, int cinemaId)
        {
            userScore = await MvxApp.Database.GetUserScoreAsync(cinemaId, movieId);
        }
    }
}
