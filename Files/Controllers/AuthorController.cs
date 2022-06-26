using Files.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult> Index(bool isFailed = false)
        {
            ViewBag.IsFailed = isFailed;
            ViewBag.Message = TempData["Message"];
            var authors = await context.Authors.ToListAsync();
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
        public async  Task<ActionResult> Create(Author author)
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
                await context.AddAsync(author);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                return View();
            }
            
        }

        // GET: AuthorController/Edit/5
        public async Task<ActionResult> Edit(int id, bool isFailed = false)
        {
            ViewBag.IsFailed = isFailed;
            var author = await context.Authors.FindAsync(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Author author)
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
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var isFailed = context.WPDs.Any(w => w.Author.Id == id);
            if(isFailed)
            {
                TempData["Message"] =  
                    "Невозможно удалить преподавателя, т.к. за ним закреплена как минимум одна дисциплина!\nМожно удалить только тех преподавателей, которые не являются авторами той или иной РПД!";
                return RedirectToAction(nameof(Index), new { isFailed = isFailed });
            }
            var author = await context.Authors.FindAsync(id);
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
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
