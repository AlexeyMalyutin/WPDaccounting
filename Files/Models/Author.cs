using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Department { get; set; }
        public ICollection<WPD> WPDs { get; set; }
    }
}
