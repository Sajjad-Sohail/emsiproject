using emsiproject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace emsiproject.WEB.APIClasses
{
    public class ManifestForEveryApplyAPI
    {
        internal static string GetAllApplications = "/Areas?name=";
        internal static string GetAllAreas        = "/Areas/GetAllAreas";
    }
    public class emsiproject_API
    {
        private HttpClient Client;
        public emsiproject_API() {}

        public emsiproject_API(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new ArgumentNullException("httpClient", "If provided, httpClient cannot be null");
            Client = httpClient;
        }

        public List<Areas> GetAreaList_WithSearchPredicate(string name)
        {
            List<Areas> AllApplications = null;
            try
            {
                AllApplications = GetAreaList_WithSearchPredicateAsync(name).Result;
            }
            catch (Exception exc)
            {
                
            }
            return AllApplications;
        }
        public async Task<List<Areas>> GetAreaList_WithSearchPredicateAsync(string name)
        {
            string valueReturned = "";
            List<Areas> listOfAreas = null;
           
            var getResult = await Client.GetAsync($"{ManifestForEveryApplyAPI.GetAllApplications+name}");
                if (getResult.IsSuccessStatusCode)
                {
                    valueReturned = await getResult.Content.ReadAsStringAsync();
                      listOfAreas =  JsonConvert.DeserializeObject<List<Areas>>(valueReturned); 
                }
           
            return listOfAreas;
        }

        public List<Areas> GetAllAreaList()
        {
            List<Areas> AllApplications = null;
            try
            {
                AllApplications = GetAllAreaListAsync().Result;
            }
            catch (Exception exc)
            {
                
            }
            return AllApplications;
        }
        public async Task<List<Areas>> GetAllAreaListAsync()
        {
            string valueReturned = "";
            List<Areas> listOfAreas = null;

            var getResult = await Client.GetAsync($"{ManifestForEveryApplyAPI.GetAllAreas}");
            if (getResult.IsSuccessStatusCode)
            {
                valueReturned = await getResult.Content.ReadAsStringAsync();
                listOfAreas = JsonConvert.DeserializeObject<List<Areas>>(valueReturned);
            }

            return listOfAreas;
        }
    }
}
