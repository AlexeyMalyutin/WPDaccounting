using Files.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Files.ViewModels
{
    public class WpdCreateViewModel
    {
        [Required(ErrorMessage = "Введите название дисциплины")]
        [StringLength(50)]
        [Display(Name = "Название дисциплины")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите статус")]
        [Display(Name = "Статус")]
        public string Status { get; set; }

        [BindProperty ,DataType(DataType.Date)]
        [Required(ErrorMessage = "Выберите дату утверждения")]
        [Display(Name = "Дата утверждения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateOfApproval { get; set; }

        [BindProperty, DataType(DataType.Date)]
        [Required(ErrorMessage = "Выберите дату формального утверждения")]
        [Display(Name = "Дата формального утверждения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateOfFormalApproval { get; set; }

        [Required(ErrorMessage = "Введите учебный год")]
        [Display(Name = "Учебный год")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Введите название специальности")]
        [Display(Name = "Специальность")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Введите название профиля")]
        [Display(Name = "Профиль")]
        public string Subspecialization { get; set; }

        [Required(ErrorMessage = "Укажите распечатан ли документ")]
        [Display(Name = "Распечатан полностью")]
        public bool isPrinted { get; set; }

        [Required(ErrorMessage = "Укажите распечатан ли титульник")]
        [Display(Name = "Распечатан титульник")]
        public bool isTitlePrinted { get; set; }

        [Required(ErrorMessage = "Укажите, есть ли подпись преподавателя")]
        [Display(Name = "Подпись преподавателя")]
        public bool isSigned { get; set; }
        public int AuthorId { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        //public string FilePath { get; set; }
    }
}
