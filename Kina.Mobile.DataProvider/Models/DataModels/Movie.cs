using System;
using System.Collections.Generic;

namespace DataModel
{
    public class Movie
    {
        public int Id_Movie;
        public String Name;
        public String Original_Name;
        public String Director;
        public String Writers;
        public String Stars;
        public String Storyline;
        public String Trailer;
        public String Music;
        public String Cinematography;
        public String Rating;
        //public Webscore Id_Webscore;

        // For easier providing data to shows view
        private List<Show> shows;

        internal List<Show> Shows { get => shows; set => shows = value; }
    }
}
