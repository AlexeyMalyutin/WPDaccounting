using Files.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.ViewModels
{
    public class WpdListViewModel
    {
        public string Name { get; set; }
        public IFormFile file { get; set; }
        public Author Author { get; set; }
        public IEnumerable<WPD> WPDs { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Statuses { get; set; }

    }
}
