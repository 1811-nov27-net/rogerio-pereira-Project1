using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models.ViewModels
{
    public class Search
    {
        public string searchBy { get; set; }
        public string searchText { get; set; }
        public int order { get; set; }
    }
}
