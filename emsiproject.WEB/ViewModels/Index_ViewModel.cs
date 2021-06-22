using emsiproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emsiproject.WEB.ViewModels
{
    public class Index_ViewModel
    {
        public string search_predicate { get; set; }
        public List<Areas> AreaList { get; set; }
       
    }
}
