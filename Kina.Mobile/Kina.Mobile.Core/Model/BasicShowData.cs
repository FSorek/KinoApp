namespace Kina.Mobile.Core.Model
{
    public class BasicShowData
    {
        public long IdCinema;
        public long IdMovie;
        public string CinemaName;
        public string MovieName;
        public double AverageRating;

        public BasicShowData(long idCinema, long idMovie, string cinemaName, double averageRating)
        {
            IdCinema = idCinema;
            IdMovie = idMovie;
            CinemaName = cinemaName;
            AverageRating = averageRating;
        }
    }
}
