using Dapper;
using emsiproject.Database;
using emsiproject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace emsiproject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
         
        private readonly DatabaseConfig databaseConfig;
         
        public AreasController(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        
        [HttpGet]
        public async Task<List<Areas>> Get(string name)
        {
            Log.Information($"GET Areas called at {DateTime.Now}");
            string query = "";
            //IEnumerable<Areas> areasList = null;
            List<Areas> listOfAreasResult = null;
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"WITH RECURSIVE under_alice(display_id, name, level, abbr, child) AS" +
                        "(" +
                           "VALUES((select display_id from areas where name = $name), $name, " +
                              "(select level from areas where name = $name)," +
                              "(select abbr from areas where name = $name)," +
                              "(select child from areas where name = $name)) " +
                              "UNION ALL " +
                              "SELECT areas.display_id,areas.name, under_alice.level + 1,areas.abbr,areas.child FROM areas " +
                              "JOIN under_alice " +
                              "ON areas.parent = under_alice.child order by 3 " +
                        ") " +
                        "SELECT distinct name,abbr FROM under_alice; ";
                listOfAreasResult = new List<Areas>();
                cmd.Parameters.AddWithValue("$name", name);
                
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        Areas a = new Areas();
                        a.name = reader.GetString(0);
                        a.abbr = reader.GetString(1);
                        listOfAreasResult.Add(a);
                    }
                }
                
            }
            catch(Exception e)
            {
                Log.Fatal($"Error occured executing query : {query}");
                Log.Fatal($"Error details : {e.Message}");
            }
            return listOfAreasResult;
        }

        [HttpGet]
        [Route("GetAllAreas")]
        public async Task<List<Areas>> GetAllAreas()
        {
            Log.Information($"GetAllAreas called at {DateTime.Now}");
            List<Areas> listOfAreasResult = null;
            string query = "";
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct name, abbr,parent FROM Areas";
                listOfAreasResult = new List<Areas>();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        Areas a = new Areas();
                        a.name = reader.GetString(0);
                        a.abbr = reader.GetString(1);
                        a.parent = Convert.ToInt32(reader.GetString(2));
                        listOfAreasResult.Add(a);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Fatal($"Error occured executing query : {query}");
                Log.Fatal($"Error details : {e.Message}");
            }

            return listOfAreasResult;
        }
    }
}
