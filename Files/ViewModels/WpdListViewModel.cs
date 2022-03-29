using Files.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.ViewModels
{
    public class WpdListViewModel
    {
        //private readonly FileContext context;
        //public WwpListViewModel(FileContext context)
        //{
        //    this.context = context;
        //}

        public IEnumerable<WPD> WPDs { get; set; }
        public string Name { get; set; }
        public SelectList Authors { get; set; }
    }
}
