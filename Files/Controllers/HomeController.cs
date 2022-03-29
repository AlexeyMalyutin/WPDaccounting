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
        public IActionResult Index(string name, int? smthId)
        {
            IQueryable<WPD> wpds = context.WPDs;

            if(!string.IsNullOrEmpty(name))
            {
                wpds = wpds.Where(x=>x.Name.Contains(name)==true);
            }

            if(smthId != 0 && smthId!=null)
            {
                wpds = wpds.Where(x => x.Id == smthId);
            }

            var smth = context.WPDs.ToList();

            var viewModel = new WpdListViewModel
            {
                WPDs = wpds,
                Name = name,
                Authors = new SelectList(smth, "Id", "Name")
            };

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

        //public async Task<IActionResult> DownloadFile(int id)
        //{
        //    var file = await context.WPDs.Where(x => x.Id == id).FirstOrDefaultAsync();
        //    if (file == null) return null;
        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(file.FilePath, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }
        //    memory.Position = 0;
        //    return File(memory, file.FileType, file.Name + file.Extension);
        //}

        //public async Task<IActionResult> DeleteFile(int id)
        //{

        //    var file = await context.WPDs.Where(x => x.Id == id).FirstOrDefaultAsync();
        //    if (file == null) return null;
        //    if (System.IO.File.Exists(file.FilePath))
        //    {
        //        System.IO.File.Delete(file.FilePath);
        //    }
        //    context.WPDs.Remove(file);
        //    context.SaveChanges();
        //    TempData["Message"] = $"Removed {file.Name + file.Extension} successfully from File System.";
        //    return RedirectToAction("Index");
        //}

        public IActionResult Create()
        {
            var wpd = new WPD();
            return View(wpd);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, WPD wpd)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
            //bool basePathExists = System.IO.Directory.Exists(basePath);
            //if (!basePathExists) Directory.CreateDirectory(basePath);
            //var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var filePath = Path.Combine(basePath, file.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var newWpd = new WPD
                {
                    CreatedOn = DateTime.UtcNow,
                    DateOfApproval = wpd.DateOfApproval,
                    DateOfFormalApproval = wpd.DateOfFormalApproval,
                    Specialization = wpd.Specialization,
                    Subspecialization = wpd.Subspecialization,
                    Status = wpd.Status,
                    Year = wpd.Year,
                    Name = wpd.Name
                };
                context.WPDs.Add(newWpd);
                context.SaveChanges();
            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }
    }
}
