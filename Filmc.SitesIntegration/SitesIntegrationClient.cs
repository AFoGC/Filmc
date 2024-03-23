using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Globalization;

namespace Filmc.SitesIntegration
{
    public class SitesIntegrationClient
    {
        private readonly HttpClient _client;

        public SitesIntegrationClient()
        {
            _client = new HttpClient();
        }

        public EntityResponse? GetMovieInfo(string url, CultureInfo lang)
        {
            if (url.Contains("www.imdb.com"))
                return GetImdbInfo(url, lang);

            if (url.Contains("shikimori.one"))
                return GetShikimoriInfo(url);

            return null;
        }

        public EntityResponse? GetBookInfo(string url)
        {
            if (url.Contains("shikimori.one"))
                return GetShikimoriInfo(url);

            return null;
        }

        public EntityResponse GetInfoByUrl(string url, CultureInfo lang)
        {
            if (url.Contains("www.imdb.com"))
                return GetImdbInfo(url, lang);

            if (url.Contains("shikimori.one"))
                return GetShikimoriInfo(url);

            return new EntityResponse
            {
                Status = DetailedStatus.UrlNotFounded
            };
        }

        private EntityResponse GetImdbInfo(string url, CultureInfo lang)
        {
            try
            {
                string langCode = GetLanguageValue(lang);
                _client.DefaultRequestHeaders.Add("Accept-Language", langCode);
                string page = _client.GetStringAsync(url).Result;

                HtmlDocument htmlSnippet = new HtmlDocument();
                htmlSnippet.LoadHtml(page);

                string name = htmlSnippet.DocumentNode
                    .SelectNodes("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[2]/div[1]/h1/span")
                    .First()
                    .InnerText;

                string year = htmlSnippet.DocumentNode
                    .SelectNodes("//*[@id=\"__next\"]/main/div/section[1]/section/div[3]/section/section/div[2]/div[1]/ul/li[2]/a")
                    .First()
                    .InnerText;

                _client.DefaultRequestHeaders.Remove("Accept-Language");

                return new EntityResponse
                {
                    Name = name,
                    Year = Convert.ToInt32(year),
                    Status = DetailedStatus.IsFilm
                };
            }
            catch
            {
                return new EntityResponse
                {
                    Status = DetailedStatus.IsEmpty
                };
            }
        }

        private EntityResponse GetShikimoriInfo(string url)
        {
            try
            {
                Classifiation cl = GetUrlClassification(url);

                string resp = _client.GetAsync($"https://shikimori.one/api/{cl.Category}{cl.Id}")
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;

                MangaResponse response = JsonConvert.DeserializeObject<MangaResponse>(resp);
                EntityResponse entityResponse = new EntityResponse();
                entityResponse.Year = Convert.ToInt32(response.Year);

                if (response.Russian == String.Empty)
                    entityResponse.Name = response.Name;
                else
                    entityResponse.Name = response.Russian;

                if (cl.Category == "animes/")
                    entityResponse.Status = DetailedStatus.IsFilm;
                else
                    entityResponse.Status = DetailedStatus.IsBook;

                return entityResponse;
            }
            catch
            {
                return new EntityResponse
                {
                    Status = DetailedStatus.IsEmpty
                };
            }
        }

        private string GetLanguageValue(CultureInfo lang)
        {
            return lang.Name;
        }

        private Classifiation GetUrlClassification(string url)
        {
            Classifiation classifiation = new Classifiation();

            Uri uri = new Uri(url);
            int length = uri.Segments.Length;

            string id = uri.Segments[length - 1];
            int idPosition = id.IndexOf('-');

            classifiation.Id = id.Substring(0, idPosition);
            classifiation.Category = uri.Segments[length - 2];

            return classifiation;
        }
    }

    public class EntityResponse
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public DetailedStatus Status { get; set; }

        public EntityResponse()
        {
            Name = String.Empty;
            Status = DetailedStatus.IsEmpty;
        }
    }

    internal class Classifiation
    {
        public string Id { get; set; }
        public string Category { get; set; }
    }

    internal class MangaResponse
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("russian")] public string Russian { get; set; }
        [JsonProperty("aired_on")] public string AiredOn { get; set; }
        public int? Year
        {
            get
            {
                DateTime date;

                if (AiredOn != null)
                    if (DateTime.TryParseExact(AiredOn, "yyyy-MM-dd", null, DateTimeStyles.None, out date))
                        return date.Year;

                return null;
            }
        }
    }

    public enum DetailedStatus
    {
        IsFilm,
        IsBook,
        IsEmpty,
        UrlNotFounded
    }
}