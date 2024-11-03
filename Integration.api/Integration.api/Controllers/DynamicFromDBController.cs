using Integration.data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicFromDBController : ControllerBase
    {
        private readonly FromDbContext _fromDbContext;

        public DynamicFromDBController(FromDbContext fromDbContext)
        {
            _fromDbContext = fromDbContext;
        }

        [HttpGet("tables")]
        public async Task<IActionResult> GetTables()
        {
            if (!await IsConnectedToDatabase())
            {
                return StatusCode(500, "Could not connect to the database.");
            }

            var query = @"
                    SELECT 
                        t.name AS TableName
                    FROM 
                        sys.tables AS t";

            var results = new List<object>();

            using (var connection = (SqlConnection)_fromDbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(new
                            {
                                TableName = reader["TableName"].ToString(),
                            });
                        }
                    }
                }
            }

            return Ok(results);
        }

        [HttpGet("columns/{tableName}")]
        public async Task<IActionResult> GetTableColumns(string tableName)
        {
            if (!await IsConnectedToDatabase())
            {
                return StatusCode(500, "Could not connect to the database.");
            }

            var query = @"
                    SELECT 
                        COLUMN_NAME 
                    FROM 
                        INFORMATION_SCHEMA.COLUMNS 
                    WHERE 
                        TABLE_NAME = @TableName";

            var columnNames = new List<string>();

            using (var connection = (SqlConnection)_fromDbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableName", tableName);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            columnNames.Add(reader["COLUMN_NAME"].ToString());
                        }
                    }
                }
            }

            return Ok(columnNames);
        }

        private async Task<bool> IsConnectedToDatabase()
        {
            try
            {
                using (var connection = new SqlConnection(_fromDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
