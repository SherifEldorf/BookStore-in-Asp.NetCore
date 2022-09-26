using bookstore2.Models;
using bookstore2.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookstore2.Controllers
{
    public class AuthorController : Controller
    {
        public IBookstoreRepository<Author> AuthorRepository { get; }

        public AuthorController( IBookstoreRepository<Author> AuthorRepository )
        {
            this.AuthorRepository = AuthorRepository;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            var authors=AuthorRepository.List();
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = AuthorRepository.Find(id);
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create(  )
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author newAuthor)
        {
            try
            {
                AuthorRepository.Add(newAuthor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = AuthorRepository.Find(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                AuthorRepository.Update( id , author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id )
        {
            var author = AuthorRepository.Find(id);

            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id ,Author author )
        {
            try
            {
                AuthorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
