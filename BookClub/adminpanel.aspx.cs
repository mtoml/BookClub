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
        }

        protected void LogoffButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }

        public async void btnSubmit_ClickAsync(object sender, EventArgs e)
        {
            string apiURL = "https://www.googleapis.com/books/v1/volumes?";
            string apiKEY = "AIzaSyAVbJ6aCml5pLODHHIPEexJAd8GJ9Ev6l4";
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiURL + "q=neuromancer+inauthor:william+gibson&key=" + apiKEY);
            //HttpResponseMessage response = await client.GetAsync("https://www.googleapis.com/books/v1/volumes?q=storm+front+inauthor:butcher&key=AIzaSyAVbJ6aCml5pLODHHIPEexJAd8GJ9Ev6l4");

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                //var obj = JsonConvert.DeserializeObject(result);
                googleAPIModel bamresult = JsonConvert.DeserializeObject<googleAPIModel>(result);

                Console.WriteLine("test");
            }


        }
    }
}