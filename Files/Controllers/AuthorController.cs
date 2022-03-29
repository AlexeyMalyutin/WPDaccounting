using Files.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Controllers
{
    public class AuthorController : Controller
    {
        private readonly FileContext context;
        public AuthorController(FileContext context)
        {
            this.context = context;
        }


    }
}
