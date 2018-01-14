using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using System.IO;

namespace CoreMultikinoJson
{
    public class CoreMultikinoJson
    {
        public void getData()
        {

            var data = Multikino.FromJson("");

        }
        
    }

    public partial class Multikino
    {
        [J("films")] public List<Film> Films { get; set; }
        [J("cdate")] public string Cdate { get; set; }
        [J("SiteRootPath")] public string SiteRootPath { get; set; }
        [J("Site")] public string Site { get; set; }
        [J("Lang")] public string Lang { get; set; }
    }

    public partial class Film
    {
        [J("original_s_count")] public long OriginalSCount { get; set; }
        [J("sortable")] public long Sortable { get; set; }
        [J("showings")] public List<Showing> Showings { get; set; }
        [J("show_showings")] public bool ShowShowings { get; set; }
        [J("film_page_name")] public string FilmPageName { get; set; }
        [J("title")] public string Title { get; set; }
        [J("id")] public string Id { get; set; }
        [J("image_hero")] public string ImageHero { get; set; }
        [J("image_poster")] public string ImagePoster { get; set; }
        [J("cert_image")] public string CertImage { get; set; }
        [J("cert_desc")] public object CertDesc { get; set; }
        [J("synopsis_short")] public string SynopsisShort { get; set; }
        [J("info_release")] public string InfoRelease { get; set; }
        [J("info_runningtime_visible")] public bool InfoRunningtimeVisible { get; set; }
        [J("info_runningtime")] public string InfoRunningtime { get; set; }
        [J("info_age")] public string InfoAge { get; set; }
        [J("pegi_class")] public string PegiClass { get; set; }
        [J("info_director")] public string InfoDirector { get; set; }
        [J("info_cast")] public object InfoCast { get; set; }
        [J("availablecopy")] public string Availablecopy { get; set; }
        [J("videolink")] public string Videolink { get; set; }
        [J("filmlink")] public string Filmlink { get; set; }
        [J("timeslink")] public string Timeslink { get; set; }
        [J("video")] public string Video { get; set; }
        [J("hidden")] public bool Hidden { get; set; }
        [J("coming_soon")] public bool ComingSoon { get; set; }
        [J("comming_soon")] public bool CommingSoon { get; set; }
        [J("announcement")] public bool Announcement { get; set; }
        [J("virtual_reality")] public bool VirtualReality { get; set; }
        [J("genres")] public Categories Genres { get; set; }
        [J("tags")] public Categories Tags { get; set; }
        [J("categories")] public Categories Categories { get; set; }
        [J("showing_type")] public ShowingType ShowingType { get; set; }
        [J("rank_votes")] public string RankVotes { get; set; }
        [J("rank_value")] public string RankValue { get; set; }
        [J("promo_labels")] public PromoLabels PromoLabels { get; set; }
        [J("ReleaseDate")] public DateTime ReleaseDate { get; set; }
        [J("type")] public string Type { get; set; }
        [J("wantsee")] public string Wantsee { get; set; }
        [J("showwantsee")] public bool Showwantsee { get; set; }
        [J("newsletterurl")] public string Newsletterurl { get; set; }
    }

    public partial class Showing
    {
        [J("date_prefix")] public string DatePrefix { get; set; }
        [J("date_day")] public string DateDay { get; set; }
        [J("date_short")] public string DateShort { get; set; }
        [J("date_long")] public string DateLong { get; set; }
        [J("date_time")] public DateTime DateTime { get; set; }
        [J("date_formatted")] public string DateFormatted { get; set; }
        [J("times")] public List<Time> Times { get; set; }
        [J("date")] public string Date { get; set; }
        [J("cdate")] public DateTime Cdate { get; set; }
        [J("clone")] public bool Clone { get; set; }
    }

    public partial class Time
    {
        [J("session_id")] public string SessionId { get; set; }
        [J("version_id")] public string VersionId { get; set; }
        [J("time")] public string PurpleTime { get; set; }
        [J("screen_type")] public string ScreenType { get; set; }
        [J("screen_number")] public object ScreenNumber { get; set; }
        [J("lang")] public object Lang { get; set; }
        [J("tags")] public List<Tag> Tags { get; set; }
        [J("event_info")] public object EventInfo { get; set; }
        [J("hidden")] public bool Hidden { get; set; }
        [J("date")] public string Date { get; set; }
    }

    public partial class Tag
    {
        [J("name")] public string Name { get; set; }
        [J("fullname")] public string Fullname { get; set; }
    }

    public partial class ShowingType
    {
        [J("name")] public string Name { get; set; }
        [J("active")] public bool Active { get; set; }
    }

    public partial class PromoLabels
    {
        [J("names")] public List<PromoLabelsName> Names { get; set; }
        [J("position")] public string Position { get; set; }
        [J("isborder")] public bool Isborder { get; set; }
    }

    public partial class PromoLabelsName
    {
        [J("name")] public string Name { get; set; }
        [J("class")] public string Class { get; set; }
        [J("short_name")] public string ShortName { get; set; }
    }

    public partial class Categories
    {
        [J("names")] public List<CategoriesName> Names { get; set; }
        [J("active")] public bool Active { get; set; }
    }

    public partial class CategoriesName
    {
        [J("name")] public string Name { get; set; }
        [J("url")] public string Url { get; set; }
        [J("highlighted")] public bool Highlighted { get; set; }
    }

    public partial class Multikino
    {
        public static Multikino FromJson(string json) => JsonConvert.DeserializeObject<Multikino>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Multikino self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
