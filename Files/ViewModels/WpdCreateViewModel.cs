using Files.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.ViewModels
{
    public class WpdCreateViewModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime? DateOfApproval { get; set; }
        public DateTime? DateOfFormalApproval { get; set; }
        public int Year { get; set; }
        public string Specialization { get; set; }
        public string Subspecialization { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int AuthorId { get; set; }
        public IFormFile File { get; set; }

        //public string FilePath { get; set; }
    }
}
