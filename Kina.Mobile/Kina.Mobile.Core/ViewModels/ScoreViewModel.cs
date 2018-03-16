using Kina.Mobile.Core.Model;
using Kina.Mobile.DataProvider.Models;
using Kina.Mobile.DataProvider.Providers;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    class ScoreViewModel : MvxViewModel<BasicShowData>
    {
        private readonly IMvxNavigationService _navigationService;

        private BasicShowData _parameter;
        private List<UserScore> userScore;

        public List<ScoreRow> ScoreRows { get; set; }

        public string HeaderLabel { get; set; }
        public string FooterLabel { get; set; }
        public int counter;

        public ScoreViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(BasicShowData parameter)
        {
            _parameter = parameter;

            long cinemaID = _parameter.IdCinema;
            long movieID = _parameter.IdMovie;

            DataRequest dataRequest = new DataRequest();
            GetScore(movieID, cinemaID, dataRequest);
            userScore = dataRequest.ShowScore;

            string title = _parameter.MovieName;
            string cinema = _parameter.CinemaName;

            HeaderLabel = String.Format("User ratings of the {0} movie in {1}", title, cinema);

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

            FooterLabel = String.Format("Our users have scored this show {0} times.", counter);

            ScoreRows.Add(new ScoreRow(avgCleanliness, "Cleanliness"));
            ScoreRows.Add(new ScoreRow(avgScreen, "Screen"));
            ScoreRows.Add(new ScoreRow(avgSeats, "Seats"));
            ScoreRows.Add(new ScoreRow(avgSound, "Sound"));
            ScoreRows.Add(new ScoreRow(avgPopcorn, "Snacks"));
        }

        private void GetScore(long movieId, long cinemaId, DataRequest dataRequest)
        {
            Task.Run(() => dataRequest.ProvideScoreData(movieId, cinemaId)).Wait();
        }
    }
}
