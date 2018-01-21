using Kina.Mobile.DataProvider.Helpers;
using Kina.Mobile.Droid.Helpers;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Kina.Mobile.Droid.Helpers
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}