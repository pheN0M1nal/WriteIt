using WriteIt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace WriteIt.Controllers
{
    public class WritingController : Controller
    {
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mediumDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private IWebHostEnvironment hostingEnv;

        string imagename = "default";
        public WritingController(IWebHostEnvironment env)
        {
            this.hostingEnv = env;
        }
        public ViewResult Index()
        {
            ViewData["writerName"] = HttpContext.Session.GetString("writername");
            return View();
        }

        public class ArticleBody
        {
            public string title { get; set; }

            public string time { get; set; }

            public string category { get; set; }
            public string data { get; set; }

        }
        [HttpPost]
        public void Upload(IFormFile file)
        {
            var FileDic = "Files";
            var fileName = "";
            string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDic);
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);
            if (file != null)
            {
                imagename = file.FileName;
                HttpContext.Session.SetString("imageName", imagename);
            }
            var filePath = Path.Combine(FilePath, imagename);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
            }
        }

        public bool uploadArticleTODB(Article article)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            string query = "insert into content ( writername, title, timeofsubmission, timeofreading, img, body, category) values (@writername, @title, @timeofsubmission, @timeofreading, @img, @body, @category)";


            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@writername", article.writername.ToString() ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@title", article.title ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@timeofsubmission", article.timeofsubmission ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@timeofreading", article.timeofreading ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@img", article.img ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@body", article.body ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@category", article.category ?? (object)DBNull.Value);


            int rows = 0;
            rows = cmd.ExecuteNonQuery();
            if (rows != 0)
            {
                con.Close();
                cmd.Parameters.Clear();
                Dispose();
                return true;
            }
            Dispose();
            cmd.Parameters.Clear();
            con.Close();
            return false;
        }

        [HttpPost]
        public ActionResult uploadContent([FromBody] ArticleBody json)
        {
            Article currentWriting = new Article();
            currentWriting.title = json.title;
            currentWriting.category = json.category;
            DateTime now = DateTime.Now;
            currentWriting.timeofreading = json.time;
            currentWriting.timeofsubmission = now.ToString();
            // checking if img not null
            if(HttpContext.Session == null){
                currentWriting.writername = "session is null(writername).";
            }
            else
                currentWriting.writername = HttpContext.Session.GetString("writername");
            // checking if img not null
            if(HttpContext.Session == null){
                currentWriting.img = "session is null(img).";
            }
            else
                currentWriting.img = HttpContext.Session.GetString("imageName");
            currentWriting.body = json.data;
            if (uploadArticleTODB(currentWriting))
                return Json(currentWriting);
            else
                return Json("status: 'not_uploaded'");
        }
    }
}