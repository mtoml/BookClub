using BookClub.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace BookClub
{
    public class SQLFunctions
    {
        public SqlConnection conn;

        public SQLFunctions()
        {
            conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);
        }

        ~SQLFunctions()
        {
            // Destructor
        }

        public void OpenSqlConnection()
        {
            conn.Open();
            conn.Close();
        }

        public List<string> whoHasntVoted()
        {
            string userNames;
            List<string> usrs = new List<string>();

            using (SqlCommand cmd = new SqlCommand("sp_notYetVoted"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    userNames = rdr.GetString(0);
                    usrs.Add(userNames);
                }

                rdr.Close();
                conn.Close();
            }


            return usrs;
        }

        public List<String> getAllUserFullNames()
        {
            List<string> allFN = new List<string>();
            string fn;

            SqlCommand cmd = new SqlCommand("select dbo.userAccounts.FullName from dbo.userAccounts", conn);
            conn.Open();

            SqlDataReader rdr = cmd.ExecuteReader();

            try
            {
                while (rdr.Read())
                {
                    fn = rdr.GetString(0);
                    allFN.Add(fn);
                }

                rdr.Close();
                conn.Close();
                return allFN;

            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
                return null;
            }
        }
        
        public bookModel getBookByID(int bookID)
        {
            bookModel bm = null;

            SqlCommand command = new SqlCommand("select bookTitle, bookAuthor from bookSelection where bookID = " + bookID, conn);
            conn.Open();
            SqlDataReader rdr = command.ExecuteReader();

            try
            {
                if (rdr.Read())
                {
                    bm = new bookModel();
                    bm.bookTitle = rdr.GetString(0);
                    bm.bookAuthor = rdr.GetString(1);
                }

                rdr.Close();
                conn.Close();
                return bm;
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int getTotalPagesRead(int year)
        {
            int pages = 0;

            SqlCommand command = new SqlCommand("SELECT bookPageLength FROM bookSelection WHERE YEAR(bookChosenDate) = '" + year + "'", conn);
            conn.Open();

            SqlDataReader rdr = command.ExecuteReader();

            try
            {
                while (rdr.Read())
                {
                    pages = pages + rdr.GetInt32(0);
                }
                rdr.Close();
                conn.Close();
                return pages;
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
                return -1;
            }
        }

        public int getAvgBookPublishYear(int yr)
        {
            int year = 0;

            SqlCommand command = new SqlCommand("select YEAR(bookPublishYear) from bookSelection WHERE YEAR(bookChosenDate) = '" +
                yr + "' ORDER BY bookPublishYear DESC", conn);

            conn.Open();
            SqlDataReader rdr = command.ExecuteReader();

            try
            {
                while (rdr.Read())
                {
                    year = year + rdr.GetInt32(0);
                }
                rdr.Close();
                conn.Close();

                year = year / 7;

                return year;

            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
                return -1;
            }

        }

        //Get all books
        public List<bookModel> getBookAuthorTitle()
        {
            List<bookModel> books = new List<bookModel>();
            bookModel bm = null;

            SqlCommand command = new SqlCommand("select dbo.bookSelection.bookAuthor,dbo.bookSelection.bookTitle from dbo.bookSelection", conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    bm = new bookModel();
                    bm.bookAuthor = reader.GetValue(0).ToString();
                    bm.bookTitle = reader.GetValue(1).ToString();

                    books.Add(bm);
                }

                reader.Close();
                conn.Close();
                return books;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Get specific years books
        public List<bookModel> getBookAuthorTitle(int year)
        {
            List<bookModel> books = new List<bookModel>();
            bookModel bm = null;

            SqlCommand command = new SqlCommand("select dbo.bookSelection.bookAuthor,dbo.bookSelection.bookTitle from dbo.bookSelection WHERE year(dbo.bookSelection.bookChosenDate)='" + year + "'", conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    bm = new bookModel();
                    bm.bookAuthor = reader.GetValue(0).ToString();
                    bm.bookTitle = reader.GetValue(1).ToString();

                    books.Add(bm);
                }

                reader.Close();
                conn.Close();
                return books;
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool hasUserVoted(int userID, int year)
        {
            SqlCommand command = new SqlCommand("SELECT whoVoted from annualVoteSubmission WHERE whoVoted='" + userID + "' and YEAR(dateVoted)='" + year + "'", conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    if (userID == reader.GetInt32(0))
                    {
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                } else
                {
                    conn.Close();
                    return false;
                }
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public int getUserID(string username)
        {
            int userID = 0;
            SqlCommand command = new SqlCommand("SELECT userID from userAccounts WHERE userName='" + username + "'", conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    userID = reader.GetInt32(0);
                    conn.Close();
                    return userID;
                }
                else
                {
                    return -1;
                }
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int getUserIDByFullName(string fn)
        {
            int userID = 0;
            SqlCommand command = new SqlCommand("SELECT userID from userAccounts WHERE fullName='" + fn + "'", conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    userID = reader.GetInt32(0);
                    conn.Close();
                    return userID;
                } else
                {
                    return -1;
                }
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int getBookCount()
        {
            int count = 0;
            SqlCommand command = new SqlCommand("SELECT count(dbo.bookSelection.bookID) from dbo.bookSelection", conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                reader.Read();
                count = reader.GetInt32(0);
                conn.Close();
                return count;
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int getBookID(string bookTitle)
        {
            int ID = 0;
            SqlCommand command = new SqlCommand("select bookSelection.bookID from dbo.bookSelection WHERE bookTitle = '" + bookTitle + "'", conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (bookTitle != null)
                {
                    reader.Read();
                    ID = reader.GetInt32(0);
                    conn.Close();
                    return ID;
                } else
                {
                    conn.Close();
                    return -1;
                }
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public List<resultsModel> get2018Results()
        {
            List<resultsModel> results = new List<resultsModel>();
            resultsModel rs = null;


            using (SqlCommand cmd = new SqlCommand("get2018SortedRanking"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    rs = new resultsModel();
                    rs.book_id = rdr.GetInt32(0);
                    rs.totalPoints = rdr.GetInt32(1);
                    rs.bookTitle = rdr.GetString(2);
                    rs.bookAuthor = rdr.GetString(3);

                    results.Add(rs);
                }

                rdr.Close();
                conn.Close();
            }

            return results;
        }

        public List<resultsTally> getNumberOnePlurality()
        {
            List<resultsTally> lrt = new List<resultsTally>();
            resultsTally rt;

            using (SqlCommand cmd = new SqlCommand("sp_getBestBookPlurality"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    rt = new resultsTally();
                    rt.book_id = rdr.GetInt32(0);
                    rt.numberVotes = rdr.GetInt32(1);
                    rt.bookTitle = rdr.GetString(2);
                    rt.bookAuthor = rdr.GetString(3);

                    lrt.Add(rt);
                }
                rdr.Close();
                conn.Close();
            }

            return lrt;
        }

        public List<resultsTally> getNumberOneBook()
        {
            List<resultsTally> lrt = new List<resultsTally>();
            resultsTally rt;

            using (SqlCommand cmd = new SqlCommand("sp_getBestBook"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    rt = new resultsTally();
                    rt.book_id = rdr.GetInt32(0);
                    rt.numberVotes = rdr.GetInt32(1);
                    rt.bookTitle = rdr.GetString(2);
                    rt.bookAuthor = rdr.GetString(3);

                    lrt.Add(rt);
                }

                rdr.Close();
                conn.Close();
            }

            return lrt;
        }

        public List<resultsTally> getLastPlaceBook()
        {
            List<resultsTally> lrt = new List<resultsTally>();
            resultsTally rt;

            using (SqlCommand cmd = new SqlCommand("sp_getWorstBook"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    rt = new resultsTally();
                    rt.book_id = rdr.GetInt32(0);
                    rt.numberVotes = rdr.GetInt32(1);
                    rt.bookTitle = rdr.GetString(2);
                    rt.bookAuthor = rdr.GetString(3);

                    lrt.Add(rt);
                }

                rdr.Close();
                conn.Close();
            }

            return lrt;
        }

        public List <resultsModel> get2018PersonalizedRankings(int userID)
        {
            List<resultsModel> persRank = new List<resultsModel>();
            resultsModel rm;

            using (SqlCommand cmd = new SqlCommand("get2018PersonalRanking"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    rm = new resultsModel();
                    rm.book_id = rdr.GetInt32(0);
                    rm.bookTitle = rdr.GetString(1);
                    rm.bookAuthor = rdr.GetString(2);

                    persRank.Add(rm);
                }

                rdr.Close();
                conn.Close();

                return persRank;

            }
        }

        public void submitAnnualVote(annualVoteModel vote)
        {
            using (SqlCommand cmd = new SqlCommand("SubmitVote"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@whoVoted", vote.voterID);
                cmd.Parameters.AddWithValue("@dateVoted", vote.voteDate);
                cmd.Parameters.AddWithValue("@votePref1", vote.votePref_1);
                cmd.Parameters.AddWithValue("@votePref2", vote.votePref_2);
                cmd.Parameters.AddWithValue("@votePref3", vote.votePref_3);
                cmd.Parameters.AddWithValue("@votePref4", vote.votePref_4);
                cmd.Parameters.AddWithValue("@votePref5", vote.votePref_5);
                cmd.Parameters.AddWithValue("@votePref6", vote.votePref_6);
                cmd.Parameters.AddWithValue("@votePref7", vote.votePref_7);
                cmd.Connection = conn;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public bookModel getHighPageCount()
        {
            bookModel bm = new bookModel();

            using (SqlCommand cmd = new SqlCommand("sp_getHighPageCount"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                bm.bookTitle = rdr.GetString(0);
                bm.bookAuthor = rdr.GetString(1);
                bm.bookPageLength = rdr.GetInt32(2);

                rdr.Close();
                conn.Close();
            }

            return bm;
        }

        public bookModel getLowPageCount()
        {
            bookModel bm = new bookModel();

            using (SqlCommand cmd = new SqlCommand("sp_getLowPageCount"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                bm.bookTitle = rdr.GetString(0);
                bm.bookAuthor = rdr.GetString(1);
                bm.bookPageLength = rdr.GetInt32(2);

                rdr.Close();
                conn.Close();
            }

            return bm;
        }


        public bool authenticateUser(string userName, string userPassword)
        {
            int userID = 0;
            //            SqlCommand command = new SqlCommand("SELECT userPassword FROM userAccounts WHERE userName = '" + userName + "'", conn);
            
            using (SqlCommand cmd = new SqlCommand("Validate_User"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", userName);
                cmd.Parameters.AddWithValue("@Password", userPassword);
                cmd.Connection = conn;

                conn.Open();
                userID = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }

            switch (userID)
            {
                case -1:
                    return false;
                default:
                    return true;
            }



           /* conn.Open();
            //SqlDataReader reader = command.ExecuteReader();
            String dbPassword;
            try
            {
                reader.Read();
                dbPassword = reader.GetString(0);
                if (String.Equals(userPassword, dbPassword))
                {
                    conn.Close();
                    return true;
                } else
                {
                    conn.Close();
                    return false;
                }
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return false;*/
        }
    }
}