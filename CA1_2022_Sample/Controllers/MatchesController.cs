using CA1_2022_Sample.Models;
using CA1_2022_Sample_With_Repo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA1_2022_Sample.Controllers
{
    public class MatchesController : Controller
    {

        static int currentID = 0;
        IMatchRepo _repo;
        /// <summary>
        /// Constructor that allows us to inject a Repository
        /// NB : you need to add the following line to Program.cs =>
        ///    builder.Services.AddScoped<IMatchRepo, MockMatchRepo>();
        /// </summary>
        /// <param name="repo"></param>
        public MatchesController(IMatchRepo repo)
        {            
                _repo = repo;
        }
      

        // GET: MatchesController
        public ActionResult Index()
        {
            //................................................................................
            //Calculate Stats for Games played
            //NB : The ViewBage Values below *SHOULD* be placed in a sepearte model (e.g GamesStats)
            //then a new page should be created for this model
            //(Alternatively a viewmodel combining Matches and gamesStats could be created and passed to the index view) 
            //This is an excercise i will leave for yourself!!
            //Adding the values below to the viewbag is *NOT* the way i would expect CA to be completed)!!!
            //
            var matches = _repo.GetFixtures();
            IEnumerable<Match> PlayedMatches = matches.Where(p => p.MatchDate < DateTime.Today.Date);

            ViewBag.PlayedCount = PlayedMatches.Count();
            ViewBag.Won = PlayedMatches.Where(p => (p.GoalsFor > p.GoalsAgainst)).ToList().Count;
            ViewBag.Lost = PlayedMatches.Where(p => (p.GoalsFor < p.GoalsAgainst)).ToList().Count;
            ViewBag.Drew = PlayedMatches.Where(p => (p.GoalsFor == p.GoalsAgainst)).ToList().Count;


            ViewBag.Points = PlayedMatches.Select(p => 
                                                 {
                                                    if (p.GoalsFor > p.GoalsFor) return 3; //Win
                                                    if (p.GoalsFor == p.GoalsFor) return 1; //Draw
                                                    return 0;                              //Lose                                            
                                                 }).Sum();                                //Now sum list of ints

            ViewBag.GoalDiff = PlayedMatches.Sum(p => p.GoalsFor) - PlayedMatches.Sum(p => p.GoalsAgainst);
            //.....................................................................................


            return View(matches);
        }

      

        // GET: MatchesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MatchesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MatchesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Match match )
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    _repo.CreateFixture(match);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(match);
                }                    
            }
            catch
            {
                return View();
            }
        }

        // GET: MatchesController/Edit/5
        public ActionResult Edit(int id)
        {
            var match = _repo.GetFixture(id); 
            return View(match);
        }

        // POST: MatchesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Match m)
        {
            try
            {
                _repo.EditFixture(m);              
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
    }
}
