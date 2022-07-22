using Microsoft.AspNetCore.Mvc;
using WriteIt.Models;

namespace WriteIt.Controllers
{
    public class ArticlePageController : Controller
    {
        public ActionResult Index()
        {

            // dummy article objects
            Article art = new Article
            {
                id = "1",
                writername = "Noman Ashraf",
                title = "Any title for artcle 2 .... ?",
                body = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Praesentium minima aspernatur earum, autem voluptate asperiores dolorum iure non exercitationem. Nam nesciunt sunt et doloribus quisquam atque eaque mollitia id modi veniam soluta ad ducimus quibusdam impedit, doloremque quis magnam iure exercitationem voluptate consequuntur! Magni aliquam recusandae ipsa inventore vero molestias consectetur quae veniam dolorem numquam nesciunt ex eius sequi officiis possimus aspernatur impedit, dicta suscipit reiciendis delectus placeat. Itaque accusantium deleniti nam similique! Nulla rerum harum recusandae repudiandae dolore quam aperiam aut nesciunt nihil sed veniam esse, optio doloribus odio fugit eligendi, ab ipsum modi vero culpa maxime aliquam. Magnam.  Any title for artcle 2 ... Lorem ipsum, dolor sit amet consectetur adipisicing elit. Praesentium minima aspernatur earum, autem voluptate asperiores dolorum iure non exercitationem. Nam nesciunt sunt et doloribus quisquam atque eaque mollitia id modi veniam soluta ad ducimus quibusdam impedit, doloremque quis magnam iure exercitationem voluptate consequuntur! Magni aliquam recusandae ipsa inventore vero molestias consectetur quae veniam dolorem numquam nesciunt ex eius sequi officiis possimus aspernatur impedit, dicta suscipit reiciendis delectus placeat. Itaque accusantium deleniti nam similique! Nulla rerum harum recusandae repudiandae dolore quam aperiam aut nesciunt nihil sed veniam esse, optio doloribus odio fugit eligendi, ab ipsum modi vero culpa maxime aliquam. Magnam. ",
                timeofsubmission = "23 May 2023",
                img = "~/lib/Images/Article2.png",
                timeofreading = "12"
            };

            return View(art);
        }
    }
}