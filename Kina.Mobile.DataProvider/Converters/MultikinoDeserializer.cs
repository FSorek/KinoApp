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
    class MultikinoDeserializer
    {
        private Multikino root;
        private string json;

        public List<Movie> Deserialize(Stream dataStream)
        {
            using (var reader = new StreamReader(dataStream))
            {
                json = reader.ReadToEnd();
                root = Multikino.FromJson(json);
            }

            return MapMovie(root);
        }

        public List<Movie> MapMovie(Multikino from)
        {
            List<Movie> mappedList = new List<Movie>();

            foreach (Film film in from.Films)
            {
                mappedList.Add(new Movie
                {
                    //id?
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
                    Shows = MapShow(film),
            });
                
            }
            return mappedList;
        }

        private static List<Show> MapShow(Film from)
        {
            List<Show> mappedList = new List<Show>();

            foreach (Showing show in from.Showings)
            {
                foreach (Time time in show.Times)
                {
                    mappedList.Add(new Show
                    {
                        //id?
                        ShowDate = show.DateTime,
                        Start = time.PurpleTime,
                        is3D = (time.ScreenType == "3D"),
                        Language = time.Tags[0].Name,

                        Room = -1
                    });
                }
            }
            return mappedList;
        }
    }
}
