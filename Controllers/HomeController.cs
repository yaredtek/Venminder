using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new BowlingViewModel());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Bowling(BowlingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rolls = new List<int>() { model.Roll1, model.Roll2, model.Roll3, model.Roll4, model.Roll5,
                                         model.Roll6, model.Roll7, model.Roll8, model.Roll9, model.Roll10,
                                         model.Roll11, model.Roll12, model.Roll13, model.Roll14, model.Roll15, 
                                         model.Roll16, model.Roll17, model.Roll18, model.Roll19, model.Roll20, model.Roll21 };

                if (rolls != null && rolls.Count() > 0)
                {
                    model.FinalScore = CalcualteFinalScore(rolls);
                }
            }

            return View("Index", model);
        }

        private int CalcualteFinalScore(List<int> rolls)
        {
            int totalScore = 0;
            int index = 0;

            if (rolls != null && rolls.Count > 0)
            {
                for (int frame = 0; frame < 10; frame++)
                {
                    if(rolls.Count() <= index)
                    {
                        break;
                    }

                    if (rolls[index] == 10) //This is a strike
                    {
                        totalScore += 10 + rolls[index + 1] + rolls[index + 2];
                        index++;
                    }
                    else if (rolls[index] + rolls[index + 1] == 10) //This is spare
                    {
                        totalScore += 10 + rolls[index + 2];
                        index += 2;
                    }
                    else //Neither a strike nor a spare
                    {
                        totalScore += rolls[index] + rolls[index + 1];
                        index += 2;

                    }
                }

            }

            return totalScore;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}