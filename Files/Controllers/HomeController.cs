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
        public async Task<IActionResult> Index(string name, int? authorId, string status, string specialization, 
            string subspecialization, bool? isPrinted, bool? isTitlePrinted, bool? isSigned)
        {
            IQueryable<WPD> wpds = context.WPDs.Include(a => a.Author);

            if (name!=null)
            {
                wpds = wpds.Where(x => x.Name == name);
            }

            if(authorId != 0 && authorId != null)
            {
                wpds = wpds.Where(x => x.Author.Id == authorId);
            }

            if(status!=null)
            {
                wpds = wpds.Where(x => x.Status == status);
            }

            if(specialization!=null)
            {
                wpds = wpds.Where(x => x.Specialization == specialization);
            }

            if(subspecialization!=null)
            {
                wpds = wpds.Where(x => x.Subspecialization == subspecialization);
            }

            if(isPrinted!=null)
            {
                wpds = wpds.Where(x => x.IsPrinted == isPrinted);
            }

            if (isTitlePrinted != null)
            {
                wpds = wpds.Where(x => x.IsTitlePrinted == isTitlePrinted);
            }

            if (isSigned != null)
            {
                wpds = wpds.Where(x => x.IsSigned == isSigned);
            }

            var authors = await context.Authors.ToListAsync();
            var statuses = await context.WPDs.Select(x => x.Status).Distinct().ToListAsync();
            var disciplines = await context.WPDs.Select(x => x.Name).Distinct().ToListAsync();
            var specializations = await context.WPDs.Select(x => x.Specialization).Distinct().ToListAsync();
            var subspecializations = await context.WPDs.Select(x => x.Subspecialization).Distinct().ToListAsync();

            var viewModel = new WpdListViewModel
            {
                WPDs = await wpds.ToListAsync(),
                Name = name,
                Authors = new SelectList(authors, "Id", "Name"),
                Statuses = new SelectList(statuses),
                Disciplines = new SelectList(disciplines),
                Specializations = new SelectList(specializations),
                Subspecializations = new SelectList(subspecializations)
            };

            ViewBag.Message = TempData["Message"];
            return View(viewModel);
        }

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

        public async Task<IActionResult> Create()
        {
            //TempData["Message"] = null;
            var authors = await context.Authors.ToListAsync();
            var statuses = new List<string>();
            var specializations = new List<string>();
            var subspecializations = new List<string>();
            using (var file = new StreamReader("statuses.txt"))
            {
                while (!file.EndOfStream)
                {
                    statuses.Add(file.ReadLine());
                }
            }
            using (var file = new StreamReader("specializations.txt"))
            {
                while (!file.EndOfStream)
                {
                    specializations.Add(file.ReadLine());
                }
            }
            using (var file = new StreamReader("subspecializations.txt"))
            {
                while (!file.EndOfStream)
                {
                    subspecializations.Add(file.ReadLine());
                }
            }

            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            ViewBag.Statuses = new SelectList(statuses);
            ViewBag.Specializations = new SelectList(specializations);
            ViewBag.Subspecializations = new SelectList(subspecializations);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WpdCreateViewModel wpdModel)
        {
            if (wpdModel.File != null)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory(),"Files");
                var filePath = Path.Combine(basePath, wpdModel.File.FileName);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await wpdModel.File.CopyToAsync(stream);
                    }

                    var wpd = new WPD
                    {
                        DateOfApproval = wpdModel.DateOfApproval,
                        DateOfFormalApproval = wpdModel.DateOfFormalApproval,
                        Specialization = wpdModel.Specialization,
                        Subspecialization = wpdModel.Subspecialization,
                        Status = wpdModel.Status,
                        Year = wpdModel.Year,
                        Name = wpdModel.Name,
                        Author = context.Authors.Find(wpdModel.AuthorId),
                        IsPrinted = wpdModel.isPrinted,
                        IsTitlePrinted = wpdModel.isTitlePrinted,
                        IsSigned = wpdModel.isSigned,
                        FilePath = filePath
                    };
                    await context.WPDs.AddAsync(wpd);
                    await context.SaveChangesAsync();
                    TempData["Message"] = $"{wpdModel.File.FileName} successfully uploaded to File System.";
                }
                else
                {

                    TempData["Message"] = $"{wpdModel.File.FileName} УЖЕ БЫЛ ЗАГРУЖЕН!";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteFile(int id)
        {
            var wpd = await context.WPDs.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (wpd == null) return null;

            if (System.IO.File.Exists(wpd.FilePath))
            {
                System.IO.File.Delete(wpd.FilePath);
                context.WPDs.Remove(wpd);
                await context.SaveChangesAsync();
            }
            TempData["Message"] = $"Removed {wpd.Name} successfully from File System.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return RedirectToAction(nameof(Index));
            }
            var wpd = await context.WPDs.Include(x => x.Author).Where(y => y.Id == id).FirstOrDefaultAsync();
            var authors = await context.Authors.ToListAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            var wpdEdit = new WpdCreateViewModel
            {
                AuthorId = wpd.Author.Id,
                DateOfApproval = wpd.DateOfApproval,
                isPrinted = wpd.IsPrinted,
                isSigned = wpd.IsSigned,
                isTitlePrinted = wpd.IsTitlePrinted,
                Name = wpd.Name,
                Specialization = wpd.Specialization,
                Status = wpd.Status,
                Subspecialization = wpd.Subspecialization,
                Year = wpd.Year,
                DateOfFormalApproval = wpd.DateOfFormalApproval
            };
            ViewBag.CurrentFile = Path.GetFileName(wpd.FilePath);
            return View(wpdEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WpdCreateViewModel wpdToElit, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fileToDelete = await context.WPDs.FindAsync(id); //include author

                    if (wpdToElit.File != null)
                    {
                        var fileToUpload = Path.Combine(Directory.GetCurrentDirectory(), "Files", wpdToElit.File.FileName);
                        if (!System.IO.File.Exists(fileToUpload))
                        {

                            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileToDelete.FilePath);
                            var fileInfo = new FileInfo(filepath);

                            if (fileInfo.Exists)
                            {
                                System.IO.File.Delete(filepath);
                                fileInfo.Delete();
                            }

                            using (var stream = new FileStream(fileToUpload, FileMode.Create))
                            {
                                wpdToElit.File.CopyTo(stream);
                            }

                            fileToDelete.FilePath = fileToUpload;
                        }
                        else
                        {
                            TempData["Message"] = $"НЕ УДАЛОСЬ ИЗМЕНИТЬ ЗАПИСЬ!\nФайл {wpdToElit.File.FileName} УЖЕ СУЩЕСТВУЕТ!";
                            return RedirectToAction(nameof(Index));
                        }
                    }

                    fileToDelete.DateOfApproval = wpdToElit.DateOfApproval; //context.WPD.Update.....
                    fileToDelete.Specialization = wpdToElit.Specialization;
                    fileToDelete.Subspecialization = wpdToElit.Subspecialization;
                    fileToDelete.Status = wpdToElit.Status;
                    fileToDelete.Year = wpdToElit.Year;
                    fileToDelete.Name = wpdToElit.Name;
                    fileToDelete.Author = context.Authors.Find(wpdToElit.AuthorId);
                    fileToDelete.IsPrinted = wpdToElit.isPrinted;
                    fileToDelete.IsTitlePrinted = wpdToElit.isTitlePrinted;
                    fileToDelete.IsSigned = wpdToElit.isSigned;
                    fileToDelete.DateOfFormalApproval = wpdToElit.DateOfFormalApproval;

                    context.WPDs.Update(fileToDelete);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
