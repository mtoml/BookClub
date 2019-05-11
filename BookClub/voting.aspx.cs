using BookClub.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BookClub
{
    public partial class voting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Load Book information

            HtmlTableRow row;
            HtmlTableCell cell;
            TextBox txtBox;
            int x = 0;



            row = new HtmlTableRow();
            cell = new HtmlTableCell();

            cell.InnerText = "Vote Preference";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = "Author";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = "Title";
            row.Cells.Add(cell);

            tableContent.Rows.Add(row);


            SQLFunctions conn = new SQLFunctions();

            //Check if user has already voted
            string currentUser = HttpContext.Current.User.Identity.Name;
            int userId = conn.getUserID(currentUser);

            if (conn.hasUserVoted(userId, 2019))
            {
                tableContent.Visible = false;
                btnSubmit.Visible = false;
                ThankYouForVoting.Text = "You have already voted!";
                ThankYouForVoting.Visible = true;
            } else
            {

                //int count = conn.getBookCount();
                List<bookModel> book = conn.getBookAuthorTitle(2018);


                foreach (bookModel items in book)
                {
                    x++;
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    txtBox = new TextBox();
                    //txtBox.ID = x.ToString();

                    txtBox.Width = 50;
                    cell.Controls.Add(txtBox);
                    row.Cells.Add(cell);
                    //cell.InnerText = "Vote!";
                    //row.Cells.Add(txtBox);

                    cell = new HtmlTableCell();
                    cell.InnerText = items.bookAuthor;
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = items.bookTitle;
                    row.Cells.Add(cell);

                    tableContent.Rows.Add(row);
                }
            }
        }

        private class bookVote
        {
            public int votePref { get; set; }
            public string bookTitle { get; set; }
            //public int bookID { get; set; }
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {

            string currentUser = HttpContext.Current.User.Identity.Name;


            if (validateSubmission())
            {
                SQLFunctions conn = new SQLFunctions();
                int userId = conn.getUserID(currentUser);
                annualVoteModel voting = new annualVoteModel();
                voting.voterID = userId;
                voting.voteDate = DateTime.Now;

                List<bookVote> choiceList = new List<bookVote>();
                bookVote choices;

                foreach (HtmlTableRow row in tableContent.Rows)
                {
                    choices = new bookVote();

                    foreach (Control ctl in row.Cells[0].Controls.OfType<TextBox>())
                    {
                        string S = ((TextBox)ctl).Text;
                        choices.votePref = Convert.ToInt32(S);
                    }

                    if (row.Cells[2].InnerText != "Title")
                    {
                        choices.bookTitle = row.Cells[2].InnerText;
                        choiceList.Add(choices);
                    }
                    else
                    {
                        // Do nothing
                    }
                }

                for (int X = 0; X <= choiceList.Count - 1; X++)
                {
                    switch (choiceList[X].votePref)
                    {
                        case 1:
                            voting.votePref_1 = conn.getBookID(choiceList[X].bookTitle);
                            break;
                        case 2:
                            voting.votePref_2 = conn.getBookID(choiceList[X].bookTitle);
                            break;
                        case 3:
                            voting.votePref_3 = conn.getBookID(choiceList[X].bookTitle);
                            break;
                        case 4:
                            voting.votePref_4 = conn.getBookID(choiceList[X].bookTitle);
                            break;
                        case 5:
                            voting.votePref_5 = conn.getBookID(choiceList[X].bookTitle);
                            break;
                        case 6:
                            voting.votePref_6 = conn.getBookID(choiceList[X].bookTitle);
                            break;
                        case 7:
                            voting.votePref_7 = conn.getBookID(choiceList[X].bookTitle);
                            break;
                        default:
                            break;
                    }
                }

                // Submit answers
                conn.submitAnnualVote(voting);
                tableContent.Visible = false;
                btnSubmit.Visible = false;
                ThankYouForVoting.Visible = true;
            } else
            {
                // Testing
            }

        }

        public bool isNumeric(string Input)
        {
            int n;
            return int.TryParse(Input, out n);
        }

        public bool validateSubmission()
        {
            int X = 0;
            foreach (HtmlTableRow row in tableContent.Rows)
            {
                foreach (Control ctl in row.Cells[0].Controls.OfType<TextBox>())
                {
                    string S = ((TextBox)ctl).Text;
                    if (isNumeric(S))
                    {
                        X = X + Convert.ToInt32(S);
                    } else
                    {
                        return false;
                    }
                }
            }
            
            if (X == 28)
            {
                return true;
            } else
            {
                return false;
            }
        }

        protected void LogoffButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }
    }
}