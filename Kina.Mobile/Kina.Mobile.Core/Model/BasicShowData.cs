namespace Kina.Mobile.Core.Model
{
    public class BasicShowData
    {
        public long IdCinema;
        public long IdMovie;
        public string CinemaName;
        public string MovieName;

        public BasicShowData(long IdCinema, long IdMovie, string CinemaName)
        {
            this.IdCinema = IdCinema;
            this.IdMovie = IdMovie;
            this.CinemaName = CinemaName;
        }
    }
}
