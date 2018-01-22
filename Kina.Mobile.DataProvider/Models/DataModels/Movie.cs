﻿using System;
using System.Collections.Generic;

namespace DataModel
{
    public class Movie
    {
        public String Id_Movie{ get; set; }
        public String Name{ get; set; }
        public String Original_Name{ get; set; }
        public String Length{ get; set; }
        public String Director{ get; set; }
        public String Writers{ get; set; }
        public String Stars{ get; set; }
        public String Storyline{ get; set; }
        public String Trailer{ get; set; }
        public String Music{ get; set; }
        public String Cinematography{ get; set; }
        public String Rating{ get; set; }
        //public Webscore Id_Webscore{ get; set; }

        // For easier providing data to shows view
        public List<Show> Shows{ get; set; }
    }
}