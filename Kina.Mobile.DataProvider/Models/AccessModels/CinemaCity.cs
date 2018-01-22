using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Kina.Mobile.DataProvider.Models.AccessModels.CinemaCity
{
    public partial class CinemaCity
    {
        [J("body")] public Body Body { get; set; }
    }

    public partial class Body
    {
        [J("films")] public List<Film> Films { get; set; }
        [J("events")] public Event[] Events { get; set; }
    }

    public partial class Event
    {
        [J("id")] public string Id { get; set; }
        [J("filmId")] public string FilmId { get; set; }
        [J("cinemaId")] public string CinemaId { get; set; }
        [J("businessDay")] public DateTime BusinessDay { get; set; }
        [J("eventDateTime")] public string EventDateTime { get; set; }
        [J("attributeIds")] public string[] AttributeIds { get; set; }
        [J("bookingLink")] public string BookingLink { get; set; }
        [J("soldOut")] public bool SoldOut { get; set; }
    }

    public partial class Film
    {
        [J("id")] public string Id { get; set; }
        [J("name")] public string Name { get; set; }
        [J("length")] public long Length { get; set; }
        [J("posterLink")] public string PosterLink { get; set; }
        [J("videoLink")] public string VideoLink { get; set; }
        [J("link")] public string Link { get; set; }
        [J("weight")] public long Weight { get; set; }
        [J("releaseYear")] public string ReleaseYear { get; set; }
        [J("attributeIds")] public string[] AttributeIds { get; set; }
    }

    public partial class CinemaCity
    {
        public static CinemaCity FromJson(string json) => JsonConvert.DeserializeObject<CinemaCity>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CinemaCity self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
