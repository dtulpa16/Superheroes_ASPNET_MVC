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
        private readonly IWebHostEnvironment _hostEnvironment;
        public SuperheroesController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var superheroes = _context.Superheroes.Include(s=>s.SuperType).ToList();
            return View(superheroes);
        }

        // GET: SuperheroesController/Details/5
        public ActionResult Details(int id)
        {
            var hero = _context.Superheroes.Find(id);
            return View(hero);
        }

        // GET: SuperheroesController/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Villain()
        {
            var villains = _context.Superheroes.Where(s => s.SuperType.Type == "Villain");
            return View(villains);
        }
        public ActionResult Hero(int id)
        {
            var heroes = _context.Superheroes.Where(s => s.SuperType.Type == "Hero");
            return View(heroes);
        }
        // POST: SuperheroesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,AlterEgo,Catchphrase,PrimaryAbility,SecondaryAbility,SuperTypeId")] SuperheroViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Superhero newHero = new Superhero
                    {
                        Name = viewModel.Name,
                        AlterEgo = viewModel.AlterEgo,
                        Catchphrase = viewModel.Catchphrase,
                        PrimaryAbility = viewModel.PrimaryAbility,
                        SecondaryAbility = viewModel.SecondaryAbility,
                        SuperTypeId = viewModel.SuperTypeId
                    };

                    _context.Superheroes.Add(newHero);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception er)
            {
                // In the case of an error, you could add a ModelState error or log the exception
                Console.WriteLine(er);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(viewModel);

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
        public ActionResult Edit(int id, [Bind("Id,Name,AlterEgo,Catchphrase,PrimaryAbility,SecondaryAbility,SuperTypeId")] Superhero hero)
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
                    super.SuperType = _context.SuperType.Find(hero.SuperTypeId);

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
            var super = _context.Superheroes.Find(id);
            return View(super);
        }

        // POST: SuperheroesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var super = _context.Superheroes.Find(id);
                _context.Superheroes.Remove(super);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        [ActionName("AddImageAsync")]
        public async Task<ActionResult> AddImageAsync(int id, Image value)
        {
            // Save the image from the Image object to the Images folder.
            // The SaveImage method returns the name of the image, which is then assigned to the Title property.
            value.Title = await SaveImage(value.ImageFile);

            // Add the Image object to the Image table in the database and save changes.
            _context.Image.Add(value);
            _context.SaveChanges();

            // Return a 201 Created status code and the Image object.
            return StatusCode(201, value);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            // Create a new image name by taking the first 10 characters of the original file name (without the extension),
            // replacing spaces with dashes, and appending a timestamp. Then append the original extension to this name.
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            // Combine the root path of the project, the Images folder, and the image name to get the image path.
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);

            // Create a new file at the image path and copy the contents of the image file to it.
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // Return the name of the image.
            return imageName;
        }
    }
}
