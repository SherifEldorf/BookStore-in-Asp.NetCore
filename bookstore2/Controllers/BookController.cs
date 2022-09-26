using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bookstore2.Models;
using bookstore2.Repositories;
using bookstore2.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookstore2.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IHostingEnvironment hosting;

        public IBookstoreRepository<Author> AuthorRepository { get; }

        public BookController(IBookstoreRepository<Book> bookRepository ,IBookstoreRepository<Author> AuthorRepository
                              ,  IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.AuthorRepository = AuthorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();

            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel NewBook)
        {
            try
            {
                string FileName = string.Empty;
                if ( NewBook.File != null )
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "Uploads");// to arrive to uploads folder in the Project
                    FileName = NewBook.File.FileName;
                    string FullPath =Path.Combine(uploads,FileName);
                    NewBook.File.CopyTo(new FileStream (FullPath, FileMode.Create));

                }
                if ( NewBook.AuthorId==-1 )
                {
                    ViewBag.Message = "please select an Author from the List";
                    var vmodel = new BookAuthorViewModel
                    {
                        Authors = FillSelectList()
                    };
                    return View(vmodel);
                }
                Book book = new Book
                {
                    Title = NewBook.Title,
                    Description= NewBook.Description,
                    Author=AuthorRepository.Find(NewBook.AuthorId),
                    ImageUrl= FileName
                };
                bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book=bookRepository.Find(id);
            int author_id;
            if ( book.Author==null )
            {
                author_id = 0;
            }
            else { author_id = book.Author.id;  }

            var viewModel = new BookAuthorViewModel
            {
                AuthorId = author_id,
                Authors = AuthorRepository.List().ToList(),
                Title = book.Title,
                Description = book.Description,
                BookId=id,
                ImgUrl=book.ImageUrl,
                
                
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel bookAuthorViewModel)
        {
            try
            {
               string FileName = string.Empty;
                if (bookAuthorViewModel.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "Uploads");// to arrive to uploads folder in the Project
                    FileName = bookAuthorViewModel.File.FileName;
                    string FullPath = Path.Combine(uploads, FileName);
                    //delete Old path
                    string OldFilePath = bookAuthorViewModel.ImgUrl;
                    string FullOldPath = Path.Combine(uploads, OldFilePath);
                    if ( OldFilePath !=FullPath )
                    {
                        System.IO.File.Delete(FullPath);
                        //save new file
                        bookAuthorViewModel.File.CopyTo(new FileStream(FullPath,FileMode.Create));

                    }

                }
                var book = new Book 
                {
                    Id=bookAuthorViewModel.BookId,
                    Title=bookAuthorViewModel.Title,
                    Description=bookAuthorViewModel.Description,
                    Author=AuthorRepository.Find(bookAuthorViewModel.AuthorId),
                    ImageUrl=FileName
                };
                bookRepository.Update( book.Id, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book =bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: BookController/Search/list
        public ActionResult Search(string search_text)
        {
            var result = bookRepository.Search(search_text);
            return View("Index", result);
        }


        List<Author> FillSelectList() 
        {
            var authors = AuthorRepository.List().ToList();
            authors.Insert(0, new Author { id = -1, FullName = "---- Please insert An Author  ----" });
            return authors;
        }
    }
}
