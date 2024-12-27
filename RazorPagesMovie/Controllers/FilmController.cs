using Microsoft.AspNetCore.Mvc;
using RazorPagesMovie.Models;
using System.Threading.Tasks;

namespace RazorPagesMovie.Controllers
{
    public class FilmController : Controller
    {
        public static List<Film> filmsList;
        public FilmController()
        {
            if (filmsList is null) 
            {
                Film f1 = new Film(1, "Batman", DateTime.Now);
                Film f2 = new Film(2, "Padrino", DateTime.Now);
                Film f3 = new Film(3, "Super man", DateTime.Now);
                filmsList = new List<Film>
                { f1,f2,f3 };
            }
        }

        public async Task<IActionResult> Index() 
            =>await Task.Run(() => View(filmsList));

        //public async Task<IActionResult> Index() =>
        //{
        //    return await Task.Run(() => View(filmsList));
        //}

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            Film film = filmsList.First(x => x.Id == id);

            if (film == null) {
                return NotFound();
            }

            return View("Edit", film);
            ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost([FromForm] Film film) 
        {
            if (film.Id == null)
            {
                return NotFound();
            }

            try
            {
                var filmToUpdate = filmsList.FirstOrDefault(x => x.Id == film.Id);

                if (filmToUpdate != null)
                {
                    filmToUpdate.Name = film.Name;
                    filmToUpdate.commingOut = film.commingOut;
                }
            }
            catch 
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                          "Try again, and if the problem persists " +
                          "see your system administrator.");
            }


            return View("Index", filmsList);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Film film = filmsList.First(x => x.Id == id);

            if (film == null)
            {
                return NotFound();
            }

            return View("Delete", film);
            ;
        }

        [HttpPost]
        public IActionResult DeleteFilm([FromForm] Film film) 
        {
            if (film.Id == null) 
            {
                return NotFound();
            }
            try
            {
                var filmToDelete = filmsList.FirstOrDefault(f => f.Id == film.Id);
                filmsList.Remove(filmToDelete);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                          "Try again, and if the problem persists " +
                          "see your system administrator.");
            }
            return View("Index", filmsList);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFilm([FromForm] Film film) 
        {
            try
            {
                filmsList.Add(film);
            }
            catch (Exception) 
            {
                ModelState.AddModelError("", "Unable to save changes. " +
          "Try again, and if the problem persists " +
          "see your system administrator.");
            }
            return View("Index", filmsList);
        }
    }
}
