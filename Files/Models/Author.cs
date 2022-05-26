using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите кафедру")]
        [Display(Name = "Кафедра")]
        public int Department { get; set; }
        public ICollection<WPD> WPDs { get; set; }

        [Display(Name = "Преподаватель")]
        public string Name => $"{LastName} {FirstName[0]}. {Patronymic[0]}.";
    }
}
