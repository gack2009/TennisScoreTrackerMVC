using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TennisScoreTracker.Controllers
{
    public class HomeController : Controller
    {
        private ScoreService scoreService = new ScoreService();

        public ActionResult Index()
        {
            var viewModel = new ScoreboardModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetNewScore(ScoreboardModel model)
         {
            try
            {
                ActionType action;
                if(!Enum.TryParse(model.scoreAction, out action))
                {
                    throw new Exception("Entered action is not valid.");
                }
                scoreService.GetScoreByAction(model, action);
                return View("CurrentScoreboared", model);
            }
            catch (Exception ex)
            {
                return View("Feedback", ex.Message as object);
            }        
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}