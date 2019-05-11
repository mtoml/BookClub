using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClub.classes.GoogleAPI
{
    public class Authors
    {
        [JsonProperty("authors")]
        public string authors { get; set; }
    }
}