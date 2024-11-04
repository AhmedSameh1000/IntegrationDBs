using Google.Protobuf.WellKnownTypes;
using Integration.data.Data;
using Integration.data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Integration.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly FromDbContext _fromDbContext;
        private readonly ToDbContext _toDbContext;

        public TestController(
            AppDbContext appDbContext,
            FromDbContext fromDbContext,
            ToDbContext toDbContext)
        {
            _appDbContext = appDbContext;
            _fromDbContext = fromDbContext;
            _toDbContext = toDbContext;
        }
        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //            .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    if (module is null)
        //        return BadRequest("Module not found.");
        //    dynamic demoClass = new DynamicClass2();
        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();
        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
        //    var data = "";
        //    var UpdateQuery = "";
        //    var resultList = new List<Dictionary<string, object>>();
        //    var ColumnsFromAndTo = await _appDbContext.columnFroms.Where(c => c.tableFromId == module.TableFromId).ToListAsync();
        //    using (var connection = _fromDbContext.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = queryFrom;


        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    var row = new Dictionary<string, object>();
        //                    var row2 = new Dictionary<string, object>();
        //                      UpdateQuery = "";
        //                    var dictionary = new Dictionary<string, string>();
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {

        //                        var d = ColumnsFromAndTo.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName))
        //                        {
        //                            dictionary.Add(d.ColumnToName, (string)reader.GetValue(i));
        //                            demoClass.AddProperty(d.ColumnToName, reader.GetValue(i));
        //                            data += $"{d.ColumnToName}={reader.GetValue(i)},";
        //                            UpdateQuery = $"Update {module.TableTo.Name} Set {data} ";
        //                        }
        //                    }
        //                    UpdateQuery = $"Update {module.TableTo.Name} Set {string.Join()}";
        //                    UpdateQuery = UpdateQuery.Substring(0, UpdateQuery.Length - 1) + " ";
        //                    UpdateQuery += $"WHERE {string.Join(" OR ", module.ConditionTos?.Select(c => c.Operation) ?? new List<string>())}";

        //                    resultList.Add(row);

        //                }
        //            }
        //        }
        //    }

        //   ;
        //    return Ok(new { UpdateQuery });
        //}


        [HttpGet]
        public async Task<IActionResult> Test(int moduleId)
        {
            // Retrieve the module from the database
            var module = await _appDbContext.modules
                .Include(c => c.TableFrom)
                    .ThenInclude(t => t.TableTo)
                        .ThenInclude(c => c.ColumnToList)
                .Include(c => c.conditionFroms)
                .Include(c => c.ConditionTos)
                .FirstOrDefaultAsync(c => c.Id == moduleId);

            // Check if the module was found
            if (module is null)
                return BadRequest("Module not found.");

            dynamic demoClass = new DynamicClass2();

            // Retrieve columns related to the module's TableFrom
            var columnFrom = await _appDbContext.columnFroms
                .Where(c => c.tableFromId == module.TableFromId)
                .ToListAsync();

            // Construct the SELECT query
            var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
            var Result = new List<string>();
            var resultList = new List<Dictionary<string, object>>();
            var ColumnsFromAndTo = await _appDbContext.columnFroms.Where(c => c.tableFromId == module.TableFromId).ToListAsync();
            var UpdateQuery = "";
            using (var connection = _fromDbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryFrom;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Read each row from the result set
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            var dictionary = new Dictionary<string, string>();
                            var data = "";
                            if (!string.IsNullOrEmpty(UpdateQuery))
                            {
                                Result.Add(UpdateQuery);
                            }
                            UpdateQuery = "";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var d = ColumnsFromAndTo.FirstOrDefault(c => c.Name == reader.GetName(i));
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);

                                if (d != null && !string.IsNullOrEmpty(d.ColumnToName))
                                {
                                    string value = reader.IsDBNull(i) ? "NULL" : $"'{reader.GetValue(i)}'";
                                    dictionary.Add(d.ColumnToName, value);
                                    demoClass.AddProperty(d.ColumnToName, reader.GetValue(i));
                                    data += $"{d.ColumnToName} = {value},";
                                }
                            }

                            // Construct the UpdateQuery
                            if (!string.IsNullOrEmpty(data))
                            {
                                UpdateQuery = $"Update {module.TableTo.Name} Set {data.TrimEnd(',')} ";
                                UpdateQuery += $"WHERE {string.Join(" OR ", module.ConditionTos?.Select(c => c.Operation) ?? new List<string>())}";
                            }

                            resultList.Add(row);
                        }
                    }
                }
            }

            return Ok(Result);
        }

        [HttpGet("Dynamic")]
        public async Task<IActionResult> Test2()
        {
            var module = await _appDbContext.modules
                .Include(c => c.TableFrom)
                    .ThenInclude(t => t.TableTo)
                    .ThenInclude(c => c.ColumnToList)
                .Include(c => c.conditionFroms)
                .Include(c => c.ConditionTos)
                .FirstOrDefaultAsync(c => c.Id == 5);

            if (module is null)
                return NotFound();

            dynamic demoClass = new DynamicClass2();
            var ColumnsTo = module.TableTo.ColumnToList.ToList();

            foreach (var item in ColumnsTo)
            {
                var name = item.Name;
                var value = item.Name;

                demoClass.AddProperty(name, value);
            }

            return Ok(((DynamicClass2)demoClass).ToDictionary());
        }
    }
}
