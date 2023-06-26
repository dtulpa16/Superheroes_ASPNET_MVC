﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Edit(int id, IFormCollection collection)
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