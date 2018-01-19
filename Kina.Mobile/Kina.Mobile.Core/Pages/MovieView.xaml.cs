using MvvmCross.Forms.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kina.Mobile.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieView : MvxContentPage
    {
        public MovieView()
        {
            InitializeComponent();
        }
    }
}