using Files.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Files.ViewModels
{
    public class WpdListViewModel
    {
        [Required(ErrorMessage = "Введите название дисциплины")]
        [StringLength(50)]
        [Display(Name = "Название дисциплины")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите статус")]
        [Display(Name = "Статус")]
        public string Status { get; set; }
        public IFormFile file { get; set; }
        public Author Author { get; set; }
        public IEnumerable<WPD> WPDs { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Statuses { get; set; }
        public SelectList Disciplines { get; set; }
        public SelectList Specializations { get; set; }
        public SelectList Subspecializations { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsTitlePrinted { get; set; }
        public bool IsSigned { get; set; }

    }
}
