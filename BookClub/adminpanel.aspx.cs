using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookClub.classes.GoogleAPI;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Web.UI.HtmlControls;

namespace BookClub
{
    public partial class adminpanel : System.Web.UI.Page
    {
        //public string apiKey = "AIzaSyAVbJ6aCml5pLODHHIPEexJAd8GJ9Ev6l4";

        protected void Page_Load(object sender, EventArgs e)
        {

            SQLFunctions conn = new SQLFunctions();

            //Check who is logged in
            string currentUser = HttpContext.Current.User.Identity.Name;
            int userId = conn.getUserID(currentUser);


            HtmlTableRow row;
            HtmlTableCell cell;
            TextBox txtBox;



            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            txtBox = new TextBox();

            cell.InnerText = "Author";
            row.Cells.Add(cell);

            txtBox.Width = 200;
            cell = new HtmlTableCell();
            cell.Controls.Add(txtBox);
            row.Cells.Add(cell);

            tableContent.Rows.Add(row);

            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            txtBox = new TextBox();

            cell.InnerText = "Title";
            row.Cells.Add(cell);

            txtBox.Width = 200;
            cell = new HtmlTableCell();
            cell.Controls.Add(txtBox);
            row.Cells.Add(cell);

            tableContent.Rows.Add(row);

            //cell = new HtmlTableCell();
            //cell.InnerText = "Author";
            //row.Cells.Add(cell);

            //cell = new HtmlTableCell();
            //cell.InnerText = "Title";
            //row.Cells.Add(cell);


        }

        protected void LogoffButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }

        public void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        public async void btnSubmit_ClickAsync(object sender, EventArgs e)
        {
            string apiURL = "https://www.googleapis.com/books/v1/volumes?";
            string apiKEY = "AIzaSyAVbJ6aCml5pLODHHIPEexJAd8GJ9Ev6l4";
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string author = "William Gibson";
            string title = "Neuromancer";

            foreach (HtmlTableRow row in tableContent.Rows)
            {
                foreach (Control ctl in row.Cells[1].Controls.OfType<TextBox>())
                {
                    if (row.Cells[0].InnerText == "Author")
                    {
                        author = ((TextBox)ctl).Text;
                    } else if (row.Cells[0].InnerText == "Title")
                    {
                        title = ((TextBox)ctl).Text;
                    }
                }
            }

            HttpResponseMessage response = await client.GetAsync(apiURL + "q=" + title + "+inauthor:" + author + "&key=" + apiKEY);
            //HttpResponseMessage response = await client.GetAsync(apiURL + "q=neuromancer+inauthor:william+gibson&key=" + apiKEY);
            //HttpResponseMessage response = await client.GetAsync("https://www.googleapis.com/books/v1/volumes?q=storm+front+inauthor:butcher&key=AIzaSyAVbJ6aCml5pLODHHIPEexJAd8GJ9Ev6l4");
            googleAPIModel bamresult;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                bamresult = JsonConvert.DeserializeObject<googleAPIModel>(result);
            } else
            {
                bamresult = null;
            }

            if (bamresult != null) { draw_table(bamresult); }
            
        }


        public void draw_table (googleAPIModel gamresult)
        {
            HtmlTableRow row;
            HtmlTableCell cell;
            RadioButton rb;
            //TextBox txtBox;
            int x = 0;

            tableContent.Rows.Clear();


            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            cell.InnerText = "";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = "Author";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = "Title";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = "Year Published";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = "ISBN";
            row.Cells.Add(cell);

            tableContent.Rows.Add(row);

            for (x = 0; x <= 5; x++)
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                rb = new RadioButton();
                cell.Controls.Add(rb);
                row.Cells.Add(cell);


                cell = new HtmlTableCell();
                cell.InnerText = gamresult.items[x].volumeInfo.authors[0];
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = gamresult.items[x].volumeInfo.title;
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = gamresult.items[x].volumeInfo.publishedDate;
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = gamresult.items[x].volumeInfo.industryIdentifier[0].ISBN_identifier;
                row.Cells.Add(cell);


                tableContent.Rows.Add(row);
            }
        }
    }
}