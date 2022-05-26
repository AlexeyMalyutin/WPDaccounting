using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Models
{
    public class WPD
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название дисциплины")]
        [StringLength(50)]
        [Display(Name = "Название дисциплины")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите статус")]
        [Display(Name = "Статус")]
        public string Status { get; set; }


        [Required(ErrorMessage = "Выберите дату утверждения")]
        [Display(Name = "Дата утверждения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfApproval { get; set; }


        [Required(ErrorMessage = "Выберите дату формального утверждения")]
        [Display(Name = "Дата формального утверждения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
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
        [Display(Name="Распечатан полностью")]
        public bool IsPrinted { get; set; }

        [Required(ErrorMessage = "Укажите распечатан ли титульник")]
        [Display(Name="Распечатан титульник")]
        public bool IsTitlePrinted { get; set; }

        [Required(ErrorMessage = "Укажите, есть ли подпись преподавателя")]
        [Display(Name="Подпись преподавателя")]
        public bool IsSigned { get; set; }
        public Author Author { get; set; }
        public string FilePath { get; set; }
    }
}
