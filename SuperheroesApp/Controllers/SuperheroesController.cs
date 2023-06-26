using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperheroesApp.Data;
using SuperheroesApp.Models;

namespace SuperheroesApp.Controllers
{
    public class SuperheroesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;

        public SuperheroesController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var superheroes = _context.Superheroes.ToList();
            return View(superheroes);
        }

        // GET: SuperheroesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SuperheroesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuperheroesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,AlterEgo,Catchphrase,PrimaryAbility,SecondaryAbility")] Superhero hero)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(hero); // _context is an instance of your database context class
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                // In the case of an error, you could add a ModelState error or log the exception
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(hero);
        }

        // GET: SuperheroesController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Superhero superhero = _context.Superheroes.Find(id);
                return View(superhero);
            }
            catch (Exception er)
            {
                return View(er);
            }
        }

        // POST: SuperheroesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,AlterEgo,Catchphrase,PrimaryAbility,SecondaryAbility")] Superhero hero)
        {
            // Check if the ID in the URL matches the ID in the form data. If they don't match, return a NotFound result.
            if (id != hero.Id)
            {
                return NotFound();
            }

            // Check if the model state is valid - this checks if the submitted data is valid according to data annotations on the model.
            if (ModelState.IsValid)
            {
                try
                {
                    var super = _context.Superheroes.Find(id);
                    if (super == null)
                    {
                        return NotFound();
                    }

                    super.Name = hero.Name;
                    super.AlterEgo = hero.AlterEgo;
                    super.Catchphrase = hero.Catchphrase;
                    super.PrimaryAbility = hero.PrimaryAbility;
                    super.SecondaryAbility = hero.SecondaryAbility;

                    _context.Superheroes.Update(super);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception er)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            // If the model state is not valid, redisplay the form with the current values.
            return View(hero);
        }

        // This private method checks if a superhero exists given its ID. It does this by calling Any() on the Superheroes DbSet, 
        // with a predicate that checks if any superheroes have the given ID.
        private bool SuperheroExists(int id)
        {
            return _context.Superheroes.Any(e => e.Id == id);
        }
        // GET: SuperheroesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SuperheroesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
