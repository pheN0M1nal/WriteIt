using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WriteIt.Controllers
{
    public class HomeController : Controller
    {
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mediumDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public class Message
        {
            public string data { get; set; }
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult login(string username, string password)
        {
            bool flag = false;
            if (_login(username, password))
            {
                getinfo(username);
                flag = true;
            }
            if (flag)
            {
                return Redirect("../Dashboard/index");
            }
            return View(flag);
        }

        public ActionResult signup(string login, string writername, string password)
        {

            bool flag = false;
            if (_signup(login, writername, password))
            {
                return Redirect("login");
            }
            return View(flag);
        }
        [HttpPost]
        public IActionResult checkUser([FromBody] Message json)
        {
            string message = "";
            if (json == null)
            {
                message = "null";
            }
            else
            {
                if (checkUserExist(json.data))
                {
                    message = "already taken";
                }
                else
                    message = "username available";
            }
            return Json(message);
        }
        private bool checkUserExist(string login)
        {

            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlParameter p1 = new SqlParameter("l", login);
            string query = "select * from users " +
            $"where login = @l";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@l", login ?? (object)DBNull.Value);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
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
        private void getinfo(string login)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlParameter p1 = new SqlParameter("l", login);
            string query = "select login,writerName from users " +
            $"where login = @l";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@l", login ?? (object)DBNull.Value);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    HttpContext.Session.SetString("loggedInUser", dr[0].ToString()); // setting the value in session
                    HttpContext.Session.SetString("writerName", dr[1].ToString());
                    break;
                }
            }
            Dispose();
            cmd.Parameters.Clear();
            con.Close();
        }

        // ----- SIGNUP -----
        private bool _signup(string login, string writername, string password)
        {

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(writername))
                return false;

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            if (checkUserExist(login))
            {
                return false;
            }
            else
            {
                string query = "insert into users(login,password,writername) values(@l, @p, @w)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@l", login ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@p", password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@w", writername ?? (object)DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    return true;
                else
                    return false;
            }
        }

        // ---- LOGIN ----
        private bool _login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return false;

            SqlConnection con = new SqlConnection(connString);
            con.Open();
            string query = "select * from users " +
            $"where login = @l and password = @p";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@l", login ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@p", password ?? (object)DBNull.Value);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
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
    }
}

