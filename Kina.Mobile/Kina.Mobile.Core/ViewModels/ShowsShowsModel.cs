using System;

namespace Kina.Mobile.Core.ViewModels
{
    class ShowsShowsModel
    {
        public String Time { get; set; }

        //public ShowsShowsModel(DateTime time)
        //{
        //    Time = time;
        //}
        //StringFormat='{}{0:HH\\:mm}'

        public ShowsShowsModel(string time)
        {
            Time = time; 
        }
    }
}
