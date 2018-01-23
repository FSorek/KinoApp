using DataModel;
using Kina.Mobile.DataProvider.Models.AccessModels.Multikino;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.DataProvider.Converters
{
    public class MultikinoDeserializer
    {
        private Multikino root;
        //private string json;

        public List<Movie> Deserialize(string json, int cinemaId)
        {
            //using (var reader = new StreamReader(dataStream))
            //{
            //    json = reader.ReadToEnd();
            //    root = Multikino.FromJson(json);
            //}
            root = Multikino.FromJson(json);

            return MapMovie(root, cinemaId);
        }

        public List<Movie> MapMovie(Multikino from, int cinemaId)
        {
            List<Movie> mappedList = new List<Movie>();

            foreach (Film film in from.Films)
            {
                List<string> genres = new List<string>();
                foreach (var genre in film.Genres.Names)
                {
                    genres.Add(genre.Name);
                }
                mappedList.Add(new Movie
                {
                    Id_Movie = film.Id,
                    Name = film.Title,
                    Director = film.InfoDirector,
                    Storyline = film.SynopsisShort,
                    Trailer = film.Videolink,
                    Length = film.InfoRunningtime,

                    Original_Name = null,
                    Writers = null,
                    Stars = null,
                    Music = null,
                    Cinematography = null,
                    Rating = null,
                    Shows = MapShow(film, cinemaId),
                    Genre = genres
            });
                
            }
            return mappedList;
        }

        private static List<Show> MapShow(Film from, int cinemaId)
        {
            List<Show> mappedList = new List<Show>();
            var today = DateTime.Today;

            foreach (Showing show in from.Showings)
            {
                foreach (Time time in show.Times)
                {
                    if (show.DateTime.Date.Equals(today.Date))
                    {
                        mappedList.Add(new Show
                        {
                            Id_Movie = from.Id,
                            Id_Cinema = cinemaId,
                            ShowDate = show.DateTime,
                            Start = time.PurpleTime,
                            is3D = (time.ScreenType == "3D"),
                            Language = time.Tags[0].Name,

                            Room = -1
                        });
                    }
                }
            }
            return mappedList;
        }
    }
}
