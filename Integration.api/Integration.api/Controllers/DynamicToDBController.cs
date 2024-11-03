using Integration.data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Integration.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicToDBController : ControllerBase
    {
        private readonly ToDbContext _toDbContext;

        public DynamicToDBController(ToDbContext toDbContext)
        {
            _toDbContext = toDbContext;
        }

        [HttpGet("tables")]
        public IActionResult GetTableNames()
        {
            var tableNames = new List<string>();

            using (var connection = new MySqlConnection(_toDbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand("SHOW TABLES;", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableNames.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return Ok(tableNames);
        }

        [HttpGet("columns/{tableName}")]
        public IActionResult GetTableColumns(string tableName)
        {
            var columnNames = new List<string>();

            using (var connection = new MySqlConnection(_toDbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand($"SHOW COLUMNS FROM `{tableName}`;", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Column name is in the first column of the result
                            columnNames.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return Ok(columnNames);
        }
    }
}
