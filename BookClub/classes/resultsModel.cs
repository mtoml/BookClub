using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClub.classes
{
    public class resultsModel
    {
        public int book_id { get; set; }
        public int totalPoints { get; set; }
        public string bookTitle { get; set; }
        public string bookAuthor { get; set; }
    }

    public class resultsTally : resultsModel
    {
        public int numberVotes { get; set; }
    }
}