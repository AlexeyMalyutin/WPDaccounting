using Files.Models;
using Files.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Controllers
{
    public class HomeController : Controller
    {
        private readonly FileContext context;
        public HomeController(FileContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string name, int? authorId, string status)
        {
            IQueryable<WPD> wpds = context.WPDs.Include(a => a.Author);

            if (!string.IsNullOrEmpty(name))
            {
                wpds = wpds.Where(x => x.Name.Contains(name) == true);
            }

            if(authorId != 0 && authorId != null)
            {
                wpds = wpds.Where(x => x.Author.Id == authorId);
            }

            if(status!=null)
            {
                wpds = wpds.Where(x => x.Status == status);
            }

            var authors = context.Authors.ToList();
            var statuses = context.WPDs.Select(x => x.Status).ToList();

            var viewModel = new WpdListViewModel
            {
                WPDs = wpds,
                Name = name,
                Authors = new SelectList(authors, "Id", "Name"),
                Statuses = new SelectList(statuses)
            };

            //authors.Insert(0, new Author { FirstName = "all", LastName=" ",Patronymic=" ", Id = 0 });
            ViewBag.Message = TempData["Message"];
            return View(viewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadFile(List<IFormFile> files, string description)
        //{
        //    foreach (var file in files)
        //    {
        //        var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
        //        bool basePathExists = System.IO.Directory.Exists(basePath);
        //        if (!basePathExists) Directory.CreateDirectory(basePath);
        //        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        //        var filePath = Path.Combine(basePath, file.FileName);
        //        var extension = Path.GetExtension(file.FileName);
        //        if (!System.IO.File.Exists(filePath))
        //        {
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }
        //            var fileModel = new WPD
        //            {
        //                CreatedOn = DateTime.UtcNow,
        //                FileType = file.ContentType,
        //                Extension = extension,
        //                Name = fileName,
        //                FilePath = filePath
        //            };
        //            context.WPDs.Add(fileModel);
        //            context.SaveChanges();
        //        }
        //    }
        //    TempData["Message"] = "File successfully uploaded to File System.";
        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> DownloadFile(int id)
        {
            var wpd = await context.WPDs.FindAsync(id);

            if (wpd == null) return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(wpd.FilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(wpd.FilePath));
        }

        public IActionResult Create()
        {
            var authors = context.Authors.ToList();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WpdCreateViewModel wpdModel)
        {
            if (wpdModel.File != null)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                var filePath = Path.Combine(basePath, wpdModel.File.FileName);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await wpdModel.File.CopyToAsync(stream);
                    }

                    var wpd = new WPD
                    {
                        CreatedOn = DateTime.UtcNow,
                        DateOfApproval = wpdModel.DateOfApproval,
                        DateOfFormalApproval = wpdModel.DateOfFormalApproval,
                        Specialization = wpdModel.Specialization,
                        Subspecialization = wpdModel.Subspecialization,
                        Status = wpdModel.Status,
                        Year = wpdModel.Year,
                        Name = wpdModel.Name,
                        Author = context.Authors.Find(wpdModel.AuthorId),
                        FilePath = filePath
                    };
                    context.WPDs.Add(wpd);
                    context.SaveChanges();
                }
            }
            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteFile(int id)
        {
            var wpd = await context.WPDs.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (wpd == null) return null;

            if (System.IO.File.Exists(wpd.FilePath))
            {
                System.IO.File.Delete(wpd.FilePath);
                context.WPDs.Remove(wpd);
                context.SaveChanges();
            }
            TempData["Message"] = $"Removed {wpd.Name} successfully from File System.";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var wpd = context.WPDs.Find(id);
            return View(wpd);
        }
    }
}
