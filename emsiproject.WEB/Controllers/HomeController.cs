using emsiproject.Models;
using emsiproject.WEB.APIClasses;
using emsiproject.WEB.Models;
using emsiproject.WEB.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace emsiproject.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly emsiproject_API _emsiproject_API;
        private  static List<Areas> FullListOfAreas;
        public HomeController(ILogger<HomeController> logger, emsiproject_API emsiproject_API_obj)
        {
            _logger = logger;
            _emsiproject_API = emsiproject_API_obj;
            FullListOfAreas = new List<Areas>();
        }
       
        [HttpGet]
        public IActionResult  Index()
        {
            Index_ViewModel model = new Index_ViewModel();
            FullListOfAreas = _emsiproject_API.GetAllAreaList();
            SetFullListOfAreasIntoSession(ref FullListOfAreas);
            model.AreaList = FullListOfAreas.FindAll(x => x.parent == 0);
            return View(model);
        }

        private void SetFullListOfAreasIntoSession(ref List<Areas> AutoCompleteList)
        {
            HttpContext.Session.SetString("InitialListOfAreas", JsonConvert.SerializeObject(AutoCompleteList));
        }
        private void FetchFullListOfAreasFromSession()
        {
            var data = HttpContext.Session.GetString("InitialListOfAreas");
            FullListOfAreas = JsonConvert.DeserializeObject<List<Areas>>(data);
        }

        [HttpPost]
        public IActionResult Index(Index_ViewModel model)
        {
            model.AreaList = _emsiproject_API.GetAreaList_WithSearchPredicate(model.search_predicate);
            return View(model);
        }

        [HttpPost]
        public JsonResult GetAutoComplete(string Prefix)
        {
            FetchFullListOfAreasFromSession();
            SetFullListOfAreasIntoSession(ref FullListOfAreas);

            HashSet<AutoCompleteWord> listOfConvertedAutoCompleteWord = new HashSet<AutoCompleteWord>();
            ConvertFullListIntoHashSetForAutoComplete(Prefix, ref listOfConvertedAutoCompleteWord);
            
            return Json(listOfConvertedAutoCompleteWord);
        }

        private static HashSet<AutoCompleteWord> ConvertFullListIntoHashSetForAutoComplete(string Prefix, ref HashSet<AutoCompleteWord> listOfConvertedAutoCompleteWord)
        {
            int index = 1;
            foreach (Areas a in FullListOfAreas)
            {
                if (a.name.StartsWith(Prefix))
                {
                    listOfConvertedAutoCompleteWord.Add(new AutoCompleteWord { val = "" + (index++), label = "" + a.name });
                }
            }
            return listOfConvertedAutoCompleteWord;
        }

        
    }
}
