using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.ViewModels
{
    public class RatingBoxViewModel : MvxViewModel
    {
        private string starName;
        private string starGroup;
        private int starRating;

        public string StarName
        {
            get { return starName; }
            set { SetProperty(ref starName, value); }
        }

        public string StarGroup
        {
            get { return starGroup; }
            set { SetProperty(ref starGroup, value); }
        }

        public int StarRating
        {
            get { return starRating; }
            set { SetProperty(ref starRating, value); }
        }

        public RatingBoxViewModel()
        {

        }
    }
}
