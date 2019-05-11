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
    public partial class _2018results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SQLFunctions conn = new SQLFunctions();

            List<String> waitingOnUsers = conn.whoHasntVoted();
            //WaitingOnVotes.Text = "Waiting on: ";
            if (waitingOnUsers.Count > 0)
            {
                //foreach (string s in waitingOnUsers)
                //{



                //    WaitingOnVotes.Text = WaitingOnVotes.Text + s + " to submit\n";
                //}

                //WaitingOnVotes.Visible = true;
                draw_table(waitingOnUsers);

            } else
            {
                if (!IsPostBack)
                {
                    draw_table();
                }

                ddlUsers_Populate();
                ddlUsers.Visible = true;
            }

            
            
            
        }

        protected void LogoffButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }

        protected void ddlUsers_Populate()
        {
            SQLFunctions conn = new SQLFunctions();
            if (!IsPostBack)
            {
                ddlUsers.Items.Clear();
                ddlUsers.Items.Add("---Overall Ranking---");
                List<string> fullNames = conn.getAllUserFullNames();

                foreach (string s in fullNames)
                {
                    ddlUsers.Items.Add(s);
                }

                ddlUsers.Items.Add("---Other Statistics---");
            } 
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLFunctions conn = new SQLFunctions();

            if (ddlUsers.SelectedItem.Text == "---Other Statistics---")
            {
                draw_custom_table();
            } else
            {
                int id = conn.getUserIDByFullName(ddlUsers.SelectedItem.Text);
                if (id == -1)
                {
                    draw_table();
                }
                else
                {
                    draw_table(id);
                }
            }
            
        }

        public resultsTally evaluateTopBooks()
        {
            SQLFunctions conn = new SQLFunctions();

            List<resultsTally> lrt = conn.getNumberOneBook();
            resultsTally rt;


            int book1 = lrt[0].numberVotes;
            int book2 = lrt[1].numberVotes;
            
            if (book1 > book2)
            {
                return lrt[0];
            } else if (book1 == book2)
            {
                rt = new resultsTally();
                rt.book_id = -2;
                return rt;
            }
            {
                rt = new resultsTally();
                rt.book_id = -1;
                return rt;
            }
        }

        public List<resultsTally> returnTiedTopBooks()
        {
            SQLFunctions conn = new SQLFunctions();

            List<resultsTally> lrt = conn.getNumberOneBook();
            List<resultsTally> lrtally = new List<resultsTally>();


            int book1 = lrt[0].numberVotes;
            int book2 = lrt[1].numberVotes;

            lrtally.Add(lrt[0]);
            lrtally.Add(lrt[1]);
            return lrtally;
        }

        //protected List<resultsTally> evaluateTopBookPlurality()
        //{
        //    SQLFunctions conn = new SQLFunctions();
        //    int X = -1;
        //    List<resultsTally> lrtresult = new List<resultsTally>();

        //    List<resultsTally> lrt = conn.getNumberOnePlurality();
        //    foreach (resultsTally rt in lrt)
        //    {
        //        // Check for a tie
        //        if (rt.numberVotes == 3)
        //        {

        //        }


        //        if (X > -1)
        //        {
        //            if (X == rt.numberVotes)
        //            {
        //                lrtresult.Add(rt);
        //            }
        //        } else
        //        {
        //            X = rt.numberVotes;
        //            lrtresult.Add(rt);
        //        }
        //    }

        //    return lrt;
        //}

        protected resultsTally evaluateBottomBooks()
        {
            SQLFunctions conn = new SQLFunctions();

            List<resultsTally> lrt = conn.getLastPlaceBook();
            resultsTally rt;


            int book1 = lrt[0].numberVotes;
            int book2 = lrt[1].numberVotes;

            if (book1 > book2)
            {
                return lrt[0];
            }
            else
            {
                rt = new resultsTally();
                rt.book_id = -1;
                return rt;
            }
        }

        protected void draw_custom_table()
        {
            SQLFunctions conn = new SQLFunctions();
            TableRow row;
            TableCell cell;

            tableContent.Rows.Clear();

            resultsTally rt = evaluateTopBooks();
            List<resultsTally> lrt = new List<resultsTally>();


            row = new TableRow();
            cell = new TableCell();
            
            if (rt.book_id > -1)
            {
                cell.Text = "With " + rt.numberVotes + " number one votes, the clear #1 is : " + rt.bookTitle;
                row.Cells.Add(cell);
            } else if (rt.book_id == -2)
            {
                lrt = returnTiedTopBooks();
                cell.Text = "Tied with " + lrt[0].numberVotes + " number one votes, the two best books are : " + lrt[0].bookTitle + " and " + lrt[1].bookTitle;
                row.Cells.Add(cell);
            } else
            {
                cell.Text = "There no was no clear #1 pick for best book!";
                row.Cells.Add(cell);

                //cell = new TableCell();
                //List<resultsTally> lrt = evaluateTopBookPlurality();
                //cell.Text = "With " + lrt[0].numberVotes + " the plurality of people voted #1 as : " + lrt[0].bookTitle;

            }

            tableContent.Rows.Add(row);


            rt = new resultsTally();
            rt = evaluateBottomBooks();

            row = new TableRow();
            cell = new TableCell();

            if (rt.book_id > -1)
            {
                cell.Text = "With " + rt.numberVotes + " number seven votes, the most disliked book is : " + rt.bookTitle;
                row.Cells.Add(cell);
            }
            else if (rt.book_id == -2)
            {
                lrt = new List<resultsTally>();
                //lrt = returnTiedBottomBooks();
                cell.Text = "Tied with " + lrt[0].numberVotes + " number seven votes, the two worst books are : " + lrt[0].bookTitle + " and " + lrt[1].bookTitle;
                row.Cells.Add(cell);
            }
            else
            {
                cell.Text = "There no was no clear #1 pick for worst book!";
                row.Cells.Add(cell);
            }

            tableContent.Rows.Add(row);

            int pages = conn.getTotalPagesRead(2018);
            if (pages > -1)
            {
                row = new TableRow();
                cell = new TableCell();
                cell.Text = "We have read " + pages + " number of pages in 2018";
                row.Cells.Add(cell);
                tableContent.Rows.Add(row);
            }

            bookModel highCount = conn.getHighPageCount();
            if (highCount.bookPageLength > -1)
            {
                row = new TableRow();
                cell = new TableCell();

                cell.Text = "The high page count is " + highCount.bookTitle + " with " + highCount.bookPageLength + " pages.";
                row.Cells.Add(cell);
                tableContent.Rows.Add(row);
                
            }

            bookModel lowCount = conn.getLowPageCount();
            if (highCount.bookPageLength > -1)
            {
                row = new TableRow();
                cell = new TableCell();

                cell.Text = "The low page count is " + lowCount.bookTitle + " with " + lowCount.bookPageLength + " pages.";
                row.Cells.Add(cell);
                tableContent.Rows.Add(row);

            }

            int pubdate = conn.getAvgBookPublishYear(2018);
            if (pubdate > -1)
            {
                row = new TableRow();
                cell = new TableCell();
                cell.Text = "The average first published year is " + pubdate;
                row.Cells.Add(cell);
                tableContent.Rows.Add(row);
            }
        }

        protected void draw_table()
        {

            SQLFunctions conn = new SQLFunctions();
            TableRow row;
            TableCell cell;

            int x = 0;
            tableContent.Rows.Clear();

            row = new TableRow();
            cell = new TableCell();

            cell.Text = "Ranking";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Author";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Title";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Total Points";
            row.Cells.Add(cell);

            tableContent.Rows.Add(row);



            List<resultsModel> results = conn.get2018Results();

            foreach (resultsModel rs in results)
            {
                x++;
                row = new TableRow();
                cell = new TableCell();

                cell.Text = x.ToString();
                row.Cells.Add(cell);
                //cell.InnerText = "Vote!";
                //row.Cells.Add(txtBox);

                cell = new TableCell();
                cell.Text = rs.bookAuthor;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = rs.bookTitle;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = rs.totalPoints.ToString();
                row.Cells.Add(cell);

                tableContent.Rows.Add(row);
            }
        }

        protected void draw_table(int userID)
        {
            SQLFunctions conn = new SQLFunctions();
            TableRow row;
            TableCell cell;
            int x = 0;

            tableContent.Rows.Clear();

            row = new TableRow();
            cell = new TableCell();

            cell.Text = "Ranking";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Author";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Title";
            row.Cells.Add(cell);

            tableContent.Rows.Add(row);

            List<resultsModel> results = conn.get2018PersonalizedRankings(userID);

            foreach (resultsModel rs in results)
            {
                x++;
                row = new TableRow();
                cell = new TableCell();

                cell.Text = x.ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = rs.bookAuthor;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = rs.bookTitle;
                row.Cells.Add(cell);

                tableContent.Rows.Add(row);
            }
        }

        protected void draw_table(List<string> voted)
        {
            TableRow row;
            TableCell cell;

            tableContent.Rows.Clear();

            row = new TableRow();
            cell = new TableCell();

            cell.Text = "Waiting on the following to vote before results are unlocked: ";
            row.Cells.Add(cell);
            tableContent.Rows.Add(row);

            foreach (string s in voted)
            {
                row = new TableRow();
                cell = new TableCell();

                cell.Text = s;
                row.Cells.Add(cell);
                tableContent.Rows.Add(row);
            }
        }
    }
}
