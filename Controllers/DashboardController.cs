using WriteIt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace WriteIt.Controllers
{
    public class DashboardController : Controller
    {

        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            // sql connection string
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mediumDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // making list of article and passing in view from database
            List<Article> listOfArticle = new List<Article>();
            
            // making a connection
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            string username = HttpContext.Session.GetString("writername");
            string query = "select * from content where writername = '" + "admin4" +"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.HasRows){
                while(dr.Read()){
                    Article readingArticlefromDB = new Article();

                    // setting article values
                    //id
                    if(!dr.IsDBNull(0))
                        readingArticlefromDB.id = dr.GetInt32(0).ToString();
                    else
                        readingArticlefromDB.id = string.Empty;
                    //writername
                    if(!dr.IsDBNull(1))
                        readingArticlefromDB.writername = dr.GetString(1);
                    else
                        readingArticlefromDB.writername = string.Empty;
                    // title
                    if(!dr.IsDBNull(2))
                        readingArticlefromDB.title =  dr.GetString(2);
                    else
                        readingArticlefromDB.title = string.Empty;
                        // time of submission
                    if(!dr.IsDBNull(3))
                        readingArticlefromDB.timeofsubmission = dr.GetString(3);
                    else
                        readingArticlefromDB.timeofsubmission = string.Empty; 
                        // time of reading
                    if(!dr.IsDBNull(4))
                        readingArticlefromDB.timeofreading =  dr.GetString(4);
                    else
                        readingArticlefromDB.timeofreading = string.Empty;         
                        // img
                    if(!dr.IsDBNull(5))
                        readingArticlefromDB.img = dr.GetString(5);
                    else
                        readingArticlefromDB.img = string.Empty; 
                        // body
                    if(!dr.IsDBNull(6))
                        readingArticlefromDB.body =  dr.GetString(6);
                    else
                        readingArticlefromDB.body = string.Empty; 
                        // category
                    if(!dr.IsDBNull(7))
                        readingArticlefromDB.category =  dr.GetString(7);
                    else
                        readingArticlefromDB.category = string.Empty; 

                    listOfArticle.Add(readingArticlefromDB);

                    // _logger.LogInformation("id: " +readingArticlefromDB.id.ToString() +

                    //                         "\nwritername: " +readingArticlefromDB.writername +

                    //                         "\ntitle: "+ readingArticlefromDB.title +

                    //                         "\ntime of submission: "+readingArticlefromDB.timeofsubmission +

                    //                         "\ntime of reading: "+ readingArticlefromDB.timeofreading+

                    //                         "\nimg: "+ readingArticlefromDB.img +

                    //                         "\nbody: "+ readingArticlefromDB.body +

                    //                         "\ncategory: "+readingArticlefromDB.category);

                }
                
            }
            con.Close();
                cmd.Parameters.Clear();
                Dispose();
            // getting Value from session
            if(HttpContext.Session == null)
                 ViewData["writerName"] = "not Logged In";
            else
                ViewData["writerName"] = HttpContext.Session.GetString("writername");
            return View(listOfArticle);
        }
    }
}