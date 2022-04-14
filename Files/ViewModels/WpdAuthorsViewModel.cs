using Files.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.ViewModels
{
    public class WpdAuthorsViewModel
    {
        public IEnumerable<WPD> WPDs { get; set; }
        public WPD Wpd { get; set; }
        public SelectList Authors { get; set; }
    }
}
