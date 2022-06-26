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
        public ActionResult Index(bool isFailed = false)
        {
            ViewBag.IsFailed = isFailed;
            ViewBag.Message = TempData["Message"];
            var authors = context.Authors.ToList();
            return View(authors);
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
            bool isFailed = context.Authors.Any(i => 
                i.FirstName.ToLower() == author.FirstName.ToLower() &&
                i.LastName.ToLower() == author.LastName.ToLower() &&
                i.Patronymic.ToLower() == author.Patronymic.ToLower() &&
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
        public ActionResult Edit(int id, bool isFailed = false)
        {
            ViewBag.IsFailed = isFailed;
            var author = context.Authors.Find(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            bool isFailed = context.Authors.Any(i =>
                i.FirstName.ToLower() == author.FirstName.ToLower() &&
                i.LastName.ToLower() == author.LastName.ToLower() &&
                i.Patronymic.ToLower() == author.Patronymic.ToLower() &&
                i.Department == author.Department);

            if (isFailed)
            {
                return RedirectToAction(nameof(Edit), new {id = author.Id, isFailed = isFailed });
            }

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
            var isFailed = context.WPDs.Any(w => w.Author.Id == id);
            if(isFailed)
            {
                TempData["Message"] =  
                    "Невозможно удалить преподавателя, т.к. за ним закреплена как минимум одна дисциплина!\nМожно удалить только тех преподавателей, котоыре не являются авторами той или иной РПД!";
                return RedirectToAction(nameof(Index), new { isFailed = isFailed });
            }
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
