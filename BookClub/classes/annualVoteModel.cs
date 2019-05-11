using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClub.classes
{
    public class annualVoteModel
    {
        public int voteID { get; set; }
        public int voterID { get; set; }
        public DateTime voteDate { get; set; }
        public int votePref_1 { get; set; }
        public int votePref_2 { get; set; }
        public int votePref_3 { get; set; }
        public int votePref_4 { get; set; }
        public int votePref_5 { get; set; }
        public int votePref_6 { get; set; }
        public int votePref_7 { get; set; }
    }

}