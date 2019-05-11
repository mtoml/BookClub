using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClub.classes
{
    public class bookModel
    {
        public int bookID { get; set; }
        public int bookISBN { get; set; }
        public string bookTitle { get; set; }
        public string bookAuthor { get; set; }
        public string chosenBy { get; set; }
        public DateTime chosenDate { get; set; }
        public string bookCoverURL { get; set; }
        public int bookPageLength { get; set; }
        public DateTime bookPublishYear { get; set; }

    }
}