using Files.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Controllers
{
    public class AuthorController : Controller
    {
        private readonly FileContext context;
        public AuthorController(FileContext context)
        {
            this.context = context;
        }
        public ActionResult Index()
        {
            var authors = context.Authors.ToList();
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthorController/Create
        public ActionResult Create(bool isFailed = false)
        {
            ViewBag.IsFailed = isFailed;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            var isFailed = context.Authors.Any(
                i => i.FirstName == author.FirstName &&
                i.LastName == author.LastName &&
                i.Patronymic == author.Patronymic &&
                i.Department == author.Department);

            if (isFailed)
            {
                return RedirectToAction(nameof(Create), new { isFailed = true });
            }
            try
            {
                context.Add(author);
                context.SaveChanges();
                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                return View();
            }
            
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = context.Authors.Find(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            try
            {
                context.Authors.Update(author);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = context.Authors.Find(id);
            context.Authors.Remove(author);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: AuthorController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Author author)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
