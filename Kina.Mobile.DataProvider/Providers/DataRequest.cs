using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using DataModel;

namespace Kina.Mobile.DataProvider.Providers
{
    public enum CinemaType
    {
        multikino, cinemacity,
    }

    class DataRequestService
    {
        private static string GetMultikinoUri(int cinemaId) => String.Format("https://multikino.pl/data/filmswithshowings/{0}", cinemaId);
        private static string GetCinemaCityUri(int cinemaId)
        {
            var today = DateTime.Today;
            return String.Format("https://www.cinema-city.pl/pl/data-api-service/v1/quickbook/10103/film-events/in-cinema/{0}/at-date/{1}-{2}-{3}?attr=&lang=pl_PL",
                cinemaId, today.Year, today.Month, today.Day);
        }

        public async Task<Stream> GetResponse(CinemaType cinemaType, int cinemaId)
        {
            var client = new HttpClient();
            string uri = null;

            switch (cinemaType)
            {
                case CinemaType.multikino: uri = GetMultikinoUri(cinemaId); break;
                case CinemaType.cinemacity: uri = GetCinemaCityUri(cinemaId); break;
            }

            var httpResponse = await client.GetAsync(uri);
            httpResponse.EnsureSuccessStatusCode();
            var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            return responseStream;
        }

        public async Task<List<Movie>> ProvideData(CinemaType cinemaType, int cinemaId)
        {
            Stream dataString = await GetResponse(cinemaType, cinemaId);
            switch (cinemaType)
            {
                case CinemaType.multikino:

                    break;
                case CinemaType.cinemacity:
                    break;
            }


        }
    }
}
