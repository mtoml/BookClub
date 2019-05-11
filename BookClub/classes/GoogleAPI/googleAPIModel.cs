using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClub.classes.GoogleAPI
{
    public class googleAPIModel
    {
        [JsonProperty("kind")]
        public string kind { get; set; }

        [JsonProperty("totalitems")]
        public int totalitems { get; set; }

        [JsonProperty("items")]
        public List<bookAPIListDetails> items { get; set; }
    }

    public class bookAPIListDetails
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("volumeInfo")]
        public apiVolumeInfo volumeInfo { get; set; }

    }

    public class apiVolumeInfo
    {
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("subtitle")]
        public string subtitle { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("publishedDate")]
        public string publishedDate { get; set; }

        [JsonProperty("authors")]
        public List<String> authors { get; set; }

        [JsonProperty("pageCount")]
        public string pageCount { get; set; }

        [JsonProperty("industryIdentifiers")]
        public List<industryIdentifiers> industryIdentifier {get; set;}

        //[JsonProperty("imageLinks")]
        //public List<imageLinks> imageLink { get; set; }
    }

    //public class imageLinks
    //{
    //    [JsonProperty("smallThumbnail")]
    //    public string smallThumbnail { get; set; }

    //    [JsonProperty("thumbnail")]
    //    public string thumbnail { get; set; }
    //}

    public class industryIdentifiers
    {
        [JsonProperty("type")]
        public string ISBN_type { get; set; }

        [JsonProperty("identifier")]
        public string ISBN_identifier { get; set; }
    }
}