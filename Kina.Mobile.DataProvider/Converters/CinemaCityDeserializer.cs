using DataModel;
using Kina.Mobile.DataProvider.Models.AccessModels.CinemaCity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.DataProvider.Converters
{
    public class CinemaCityDeserializer
    {
        private CinemaCity root;
        //private string json;

        public List<Movie> Deserialize(string json, int cinemaId)
        {
            //using (var reader = new StreamReader(dataStream))
            //{
            //    json = reader.ReadToEnd();
            //    root = CinemaCity.FromJson(json);
            //}
            root = CinemaCity.FromJson(json);

            return MapMovie(root, cinemaId);
        }

        public List<Movie> MapMovie(CinemaCity from, int cinemaId)
        {
            List<Movie> mappedList = new List<Movie>();

            foreach (Film film in from.Body.Films)
            {
                List<string> genres = new List<string>();
                foreach(var genre in film.AttributeIds)
                {
                    genres.Add(genre);
                }
                mappedList.Add(new Movie
                {
                    Id_Movie = film.Id,
                    Name = film.Name,
                    Director = null,
                    Storyline = null,
                    Trailer = film.VideoLink,
                    Length = film.Length.ToString(),
                    Original_Name = null,
                    Writers = null,
                    Stars = null,
                    Music = null,
                    Cinematography = null,
                    Rating = null,
                    Shows = MapShow(from, film.Id, cinemaId),
                    Genre = genres
                });

            }
            return mappedList;
        }

        private static List<Show> MapShow(CinemaCity from, string id, int cinemaId)
        {
            List<Show> mappedList = new List<Show>();

            foreach (Event show in from.Body.Events)
            {
                if (show.FilmId != id) continue;
                    mappedList.Add(new Show
                    {
                        Id_Movie = id,
                        Id_Cinema = cinemaId,
                        ShowDate = show.BusinessDay,
                        Start = show.EventDateTime.Remove(0,10),
                        is3D = (show.AttributeIds.Contains("2d")),
                        Language = LanguageFinder(show.AttributeIds),

                        Room = -1
                    });
            }
            return mappedList;
        }

        private static string LanguageFinder(string[] toSearch)
        {
            if (toSearch.Contains("dubbed") || toSearch.Contains("original-lang-pl"))
                return "PL";
            else return "EN";
        }
    }
}
