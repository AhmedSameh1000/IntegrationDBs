using Integration.data.Data;
using Integration.data.Migrations;
using Integration.data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography.Xml;
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


        #region v1
        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module from the database
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    // Check if the module was found
        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    dynamic demoClass = new DynamicClass2();

        //    // Retrieve columns related to the module's TableFrom
        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    // Construct the SELECT query
        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
        //    var Result = new List<string>();
        //    var resultList = new List<Dictionary<string, object>>();
        //    var ColumnsFromAndTo = await _appDbContext.columnFroms.Where(c => c.tableFromId == module.TableFromId).ToListAsync();
        //    var UpdateQuery = "";
        //    using (var connection = _fromDbContext.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = queryFrom;

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                // Read each row from the result set
        //                while (await reader.ReadAsync())
        //                {
        //                    var row = new Dictionary<string, object>();
        //                    var dictionary = new Dictionary<string, string>();
        //                    var data = "";
        //                    if (!string.IsNullOrEmpty(UpdateQuery))
        //                    {
        //                        Result.Add(UpdateQuery);
        //                    }
        //                    UpdateQuery = "";
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = ColumnsFromAndTo.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);

        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName))
        //                        {
        //                            string value = reader.IsDBNull(i) ? "NULL" : $"'{reader.GetValue(i)}'";
        //                            dictionary.Add(d.ColumnToName, value);
        //                            demoClass.AddProperty(d.ColumnToName, reader.GetValue(i));
        //                            data += $"{d.ColumnToName} = {value},";
        //                        }
        //                    }

        //                    // Construct the UpdateQuery
        //                    if (!string.IsNullOrEmpty(data))
        //                    {
        //                        UpdateQuery = $"Update {module.TableTo.Name} Set {data.TrimEnd(',')} ";
        //                        UpdateQuery += $"WHERE {string.Join(" OR ", module.ConditionTos?.Select(c => c.Operation) ?? new List<string>())}";
        //                    }

        //                    resultList.Add(row);
        //                }
        //            }
        //        }
        //    }

        //    return Ok(Result);
        //}


        #endregion

        #region v2
        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module from the database
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    // Check if the module was found
        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    // Construct the SELECT query
        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
        //    var updateQueries = new List<string>();
        //    var ColumnsFromAndTo = await _appDbContext.columnFroms.Where(c => c.tableFromId == module.TableFromId).ToListAsync();

        //    using (var connection = _fromDbContext.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = queryFrom;

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                // Read each row from the result set
        //                while (await reader.ReadAsync())
        //                {
        //                    var data = "";
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = ColumnsFromAndTo.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName))
        //                        {
        //                            string value = reader.IsDBNull(i) ? "NULL" : $"'{reader.GetValue(i)}'";
        //                            data += $"{d.ColumnToName} = {value},";
        //                        }
        //                    }

        //                    // Construct the UpdateQuery
        //                    if (!string.IsNullOrEmpty(data))
        //                    {
        //                        var updateQuery = $"Update {module.TableTo.Name} Set {data.TrimEnd(',')} ";
        //                        updateQuery += $"WHERE {string.Join(" OR ", module.ConditionTos?.Select(c => c.Operation) ?? new List<string>())}";
        //                        updateQueries.Add(updateQuery);
        //                    }
        //                    /// here i need to update this query in 

        //                }
        //            }
        //        }
        //    }

        //    // Return the list of UpdateQueries
        //    return Ok(updateQueries);
        //}

        #endregion

        #region v3

        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module from the database
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
        //    var updateQueries = new List<string>();
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
        //                    var data = "";
        //                    var id = -1;
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = ColumnsFromAndTo.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        var Name = reader.GetName(i);
        //                        if (Name == module.fromPrimaryKeyName)
        //                        {
        //                            id = await GetToPrimaryKeyId(module.ToPrimaryKeyName, module.TableTo.Name, module.ToLocalIdName, (int)reader.GetValue(i));
        //                        }
        //                        if (id == -1)
        //                        {

        //                        }
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName))
        //                        {
        //                            string value = reader.IsDBNull(i) ? "NULL" : $"'{reader.GetValue(i)}'";
        //                            data += $"{d.ColumnToName} = {value},";
        //                        }
        //                    }
        //                    if (id == -1)
        //                    {

        //                    }
        //                    else
        //                    {
        //                        if (!string.IsNullOrEmpty(data))
        //                        {
        //                            var updateQuery = $"Update {module.TableTo.Name} Set {data.TrimEnd(',')} ";
        //                            updateQuery += $"WHERE {module.ToPrimaryKeyName}={id}";
        //                            updateQueries.Add(updateQuery);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Return the list of UpdateQueries
        //    return Ok(updateQueries);
        //}

        #endregion

        #region v4
        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module from the database
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
        //    var updateQueries = new List<string>();
        //    var ColumnsFromAndTo = await _appDbContext.columnFroms.Where(c => c.tableFromId == module.TableFromId).ToListAsync();
        //    string wordToRemove = "AM";
        //    string wordToRemove2 = "PM";
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
        //                    var data = "";
        //                    var id = -1;
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = ColumnsFromAndTo.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        var Name = reader.GetName(i);
        //                        if (Name == module.fromPrimaryKeyName)
        //                        {
        //                            id = await GetToPrimaryKeyId(module.ToPrimaryKeyName, module.TableTo.Name, module.ToLocalIdName, (int)reader.GetValue(i));
        //                        }
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName))
        //                        {
        //                            string value = reader.IsDBNull(i) ? "NULL" : $"'{reader.GetValue(i)}'";
        //                            data += $"{d.ColumnToName} = {value},";
        //                        }
        //                    }
        //                    if (id == -1)
        //                    {
        //                        var insertColumns = string.Join(", ", ColumnsFromAndTo
        //                              .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                              .Select(c => c.ColumnToName));

        //                        var insertValues = string.Join(", ", ColumnsFromAndTo
        //                            .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                            .Select(c =>
        //                            {
        //                                var value = reader.GetValue(ColumnsFromAndTo.IndexOf(c));
        //                                return value == DBNull.Value ? "NULL" : $"'{value}'";
        //                            }));

        //                        var insertQuery = $"INSERT INTO {module.TableTo.Name} ({insertColumns}) VALUES ({insertValues})";

        //                        insertQuery = insertQuery.Replace(wordToRemove, "");
        //                        insertQuery = insertQuery.Replace(wordToRemove2, "");
        //                        updateQueries.Add(insertQuery);
        //                    }
        //                    else
        //                    {
        //                        if (!string.IsNullOrEmpty(data))
        //                        {
        //                            var updateQuery = $"Update {module.TableTo.Name} Set {data.TrimEnd(',')} ";
        //                            updateQuery += $"WHERE {module.ToPrimaryKeyName}={id}";
        //                            updateQuery = updateQuery.Replace(wordToRemove, "");
        //                            updateQuery = updateQuery.Replace(wordToRemove2, "");


        //                            updateQueries.Add(updateQuery);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Return the list of UpdateQueries
        //    return Ok(updateQueries);
        //}


        #endregion

        #region v5
        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module from the database
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
        //    var updateQueries = new List<string>();
        //    var ColumnsFromAndTo = await _appDbContext.columnFroms.Where(c => c.tableFromId == module.TableFromId).ToListAsync();
        //    string wordToRemove = "AM";
        //    string wordToRemove2 = "PM";

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
        //                    var data = "";
        //                    var id = -1;

        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = ColumnsFromAndTo.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        var Name = reader.GetName(i);

        //                        // Ensure we correctly retrieve the primary key for the 'TableTo' record
        //                        if (Name == module.fromPrimaryKeyName)
        //                        {
        //                            id = await GetToPrimaryKeyId(module.ToPrimaryKeyName, module.TableTo.Name, module.ToLocalIdName, (int)reader.GetValue(i));
        //                        }
        //                        if (id == -1)
        //                        {
        //                            var insertColumns = string.Join(", ", ColumnsFromAndTo
        //                             .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                             .Select(c => c.ColumnToName));

        //                            var insertValues = string.Join(", ", ColumnsFromAndTo
        //                                .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                                .Select(c =>
        //                                {
        //                                    var value = reader.GetValue(ColumnsFromAndTo.IndexOf(c));
        //                                    return value == DBNull.Value ? "NULL" : $"'{value.ToString().Trim()}'"; // Trim and handle DBNull values
        //                                }));

        //                            var insertQuery = $"INSERT INTO {module.TableTo.Name} ({insertColumns}) VALUES ({insertValues})";

        //                            insertQuery = insertQuery.Replace(wordToRemove, "");
        //                            insertQuery = insertQuery.Replace(wordToRemove2, "");
        //                            updateQueries.Add(insertQuery);
        //                        }
        //                        // Handle column mapping from 'From' table to 'To' table
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName))
        //                        {
        //                            string value = reader.IsDBNull(i) ? "NULL" : $"'{reader.GetValue(i).ToString().Trim()}'"; // Trim any spaces in the value
        //                            data += $"{d.ColumnToName} = {value},";
        //                        }
        //                    }

        //                    else
        //                    {
        //                        if (!string.IsNullOrEmpty(data))
        //                        {
        //                            var updateQuery = $"UPDATE {module.TableTo.Name} SET {data.TrimEnd(',')} ";
        //                            updateQuery += $"WHERE {module.ToPrimaryKeyName}={id}";
        //                            updateQuery = updateQuery.Replace(wordToRemove, "");
        //                            updateQuery = updateQuery.Replace(wordToRemove2, "");
        //                            updateQueries.Add(updateQuery);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Return the list of UpdateQueries
        //    return Ok(updateQueries);
        //}

        #endregion

        #region v6

        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module from the database
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";

        //    var updateQueries = new List<string>();
        //    var allValues = new List<Dictionary<string, string>>(); // لتخزين كل القيم

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
        //                    var data = "";
        //                    var id = -1;
        //                    var rowValues = new Dictionary<string, string>(); // لتخزين القيم في كل صف

        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = columnFrom.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        var Name = reader.GetName(i);

        //                        // حفظ القيمة في القاموس
        //                        string value = reader.IsDBNull(i) ? "NULL" : reader.GetValue(i).ToString().Trim();
        //                        rowValues[Name] = value; // تخزين القيمة في القاموس

        //                        // تأكد من استرجاع المفتاح الأساسي بشكل صحيح
        //                        if (Name == module.fromPrimaryKeyName)
        //                        {
        //                            id = await GetToPrimaryKeyId(module.ToPrimaryKeyName, module.TableTo.Name, module.ToLocalIdName, (int)reader.GetValue(i));
        //                        }

        //                        // Handle column mapping from 'From' table to 'To' table
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName))
        //                        {
        //                            data += $"{d.ColumnToName} = '{value}',"; // استخدم القيمة المأخوذة من القاموس
        //                        }
        //                    }

        //                    // تخزين القيم لكل صف
        //                    allValues.Add(rowValues);

        //                    if (id == -1)
        //                    {
        //                        var insertValues = string.Join(", ", columnFrom
        //                            .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                            .Select(c => rowValues[c.Name]));

        //                        var insertQuery = $"INSERT INTO {module.TableTo.Name} ({string.Join(",", columnFrom.Where(c => !string.IsNullOrEmpty(c.ColumnToName)).Select(c => c.ColumnToName))}) VALUES ({insertValues})";
        //                        updateQueries.Add(insertQuery);
        //                    }

        //                    // تصحيح موضع الـ else block
        //                    if (!string.IsNullOrEmpty(data))
        //                    {
        //                        var updateQuery = $"UPDATE {module.TableTo.Name} SET {data.TrimEnd(',')} WHERE {module.ToPrimaryKeyName}={id}";
        //                        updateQueries.Add(updateQuery);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Return all values and update queries
        //    return Ok(new { Values = allValues, UpdateQueries = updateQueries });
        //}


        #endregion

        #region v7

        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module from the database
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";

        //    var updateQueries = new List<string>();
        //    var allValues = new List<Dictionary<string, string>>(); // لتخزين كل القيم

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
        //                    var data = "";
        //                    var id = -1;
        //                    var rowValues = new Dictionary<string, string>(); // لتخزين القيم في كل صف

        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = columnFrom.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        var Name = reader.GetName(i);

        //                        // حفظ القيمة في القاموس
        //                        string value = reader.IsDBNull(i) ? "NULL" : reader.GetValue(i).ToString().Trim();
        //                        rowValues[Name] = value; // تخزين القيمة في القاموس

        //                        // تأكد من استرجاع المفتاح الأساسي بشكل صحيح
        //                        if (Name == module.fromPrimaryKeyName)
        //                        {
        //                            id = await GetToPrimaryKeyId(module.ToPrimaryKeyName, module.TableTo.Name, module.ToLocalIdName, (int)reader.GetValue(i));
        //                        }

        //                        // Handle column mapping from 'From' table to 'To' table
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName) && id != -1)
        //                        {
        //                            data += $"{d.ColumnToName} = '{value}',"; // استخدم القيمة المأخوذة من القاموس
        //                        }
        //                    }

        //                    // تخزين القيم لكل صف
        //                    allValues.Add(rowValues);

        //                    if (id == -1)
        //                    {
        //                        var insertValues = string.Join(", ", columnFrom
        //                            .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                            .Select(c => $"'{rowValues[c.Name]}'"));

        //                        var insertQuery = $"INSERT INTO {module.TableTo.Name} ({string.Join(",", columnFrom.Where(c => !string.IsNullOrEmpty(c.ColumnToName)).Select(c => c.ColumnToName))}) VALUES ({insertValues})";

        //                        insertQuery = insertQuery.Replace("AM", string.Empty);
        //                        insertQuery = insertQuery.Replace("PM", string.Empty);
        //                        updateQueries.Add(insertQuery);
        //                    }

        //                    // تصحيح موضع الـ else block
        //                    if (!string.IsNullOrEmpty(data))
        //                    {
        //                        var updateQuery = $"UPDATE {module.TableTo.Name} SET {data.TrimEnd(',')} WHERE {module.ToPrimaryKeyName}={id}";
        //                        updateQueries.Add(updateQuery);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Return all values and update queries
        //    return Ok(updateQueries);
        //}


        #endregion


        #region v8
        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module from the database
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";

        //    var updateQueries = new List<string>();
        //    var allValues = new List<Dictionary<string, string>>(); // لتخزين كل القيم

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
        //                    var data = "";
        //                    var id = -1;
        //                    var rowValues = new Dictionary<string, string>(); // لتخزين القيم في كل صف

        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = columnFrom.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        var Name = reader.GetName(i);

        //                        // معالجة القيمة بحسب نوعها
        //                        string value;
        //                        if (reader.IsDBNull(i))
        //                        {
        //                            value = "NULL";
        //                        }
        //                        else
        //                        {
        //                            // إذا كان النوع DateTime، قم بتنسيق القيمة
        //                            if (reader.GetFieldType(i) == typeof(DateTime))
        //                            {
        //                                DateTime dateTimeValue = reader.GetDateTime(i);
        //                                value = $"{dateTimeValue:yyyy-MM-dd HH:mm:ss}"; // تنسيق التاريخ
        //                            }
        //                            else
        //                            {
        //                                value = reader.GetValue(i).ToString().Trim();
        //                            }
        //                        }

        //                        // تخزين القيمة في القاموس
        //                        rowValues[Name] = value;

        //                        // تأكد من استرجاع المفتاح الأساسي بشكل صحيح
        //                        if (Name == module.fromPrimaryKeyName)
        //                        {
        //                            id = await GetToPrimaryKeyId(module.ToPrimaryKeyName, module.TableTo.Name, module.ToLocalIdName, (int)reader.GetValue(i));
        //                        }

        //                        // Handle column mapping from 'From' table to 'To' table
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName) && id != -1)
        //                        {
        //                            data += $"{d.ColumnToName} = '{value}',"; // استخدم القيمة المأخوذة من القاموس
        //                        }
        //                    }

        //                    // تخزين القيم لكل صف
        //                    allValues.Add(rowValues);

        //                    if (id == -1)
        //                    {
        //                        var insertValues = string.Join(", ", columnFrom
        //                            .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                            .Select(c => $"'{rowValues[c.Name]}'"));

        //                        var insertQuery = $"INSERT INTO {module.TableTo.Name} ({string.Join(",", columnFrom.Where(c => !string.IsNullOrEmpty(c.ColumnToName)).Select(c => c.ColumnToName))}) VALUES ({insertValues})";

        //                        insertQuery = insertQuery.Replace("AM", string.Empty);
        //                        insertQuery = insertQuery.Replace("PM", string.Empty);
        //                        updateQueries.Add(insertQuery);
        //                    }

        //                    // تصحيح موضع الـ else block
        //                    if (!string.IsNullOrEmpty(data))
        //                    {
        //                        var updateQuery = $"UPDATE {module.TableTo.Name} SET {data.TrimEnd(',')} WHERE {module.ToPrimaryKeyName}={id}";
        //                        updateQueries.Add(updateQuery);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Return all values and update queries
        //    return Ok(updateQueries);
        //}


        #endregion

        #region v9
        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);
        //    if (module is null)
        //        return BadRequest("Module not found.");
        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();
        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
        //    var updateQueries = new List<string>();
        //    var allValues = new List<Dictionary<string, string>>(); // لتخزين كل القيم
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
        //                    var data = "";
        //                    var id = -1;
        //                    var rowValues = new Dictionary<string, string>(); // لتخزين القيم في كل صف

        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var d = columnFrom.FirstOrDefault(c => c.Name == reader.GetName(i));
        //                        var Name = reader.GetName(i);

        //                        // معالجة القيمة بحسب نوعها
        //                        string value;
        //                        if (reader.IsDBNull(i))
        //                        {
        //                            value = "NULL";
        //                        }
        //                        else
        //                        {
        //                            // إذا كان النوع DateTime، قم بتنسيق القيمة
        //                            if (reader.GetFieldType(i) == typeof(DateTime))
        //                            {
        //                                DateTime dateTimeValue = reader.GetDateTime(i);
        //                                value = $"{dateTimeValue:yyyy-MM-dd HH:mm:ss}"; // تنسيق التاريخ
        //                            }
        //                            else
        //                            {
        //                                value = reader.GetValue(i).ToString().Trim();
        //                            }
        //                        }

        //                        // تخزين القيمة في القاموس
        //                        rowValues[Name] = value;

        //                        // تأكد من استرجاع المفتاح الأساسي بشكل صحيح
        //                        if (Name == module.fromPrimaryKeyName)
        //                        {
        //                            id = await GetToPrimaryKeyId(module.ToPrimaryKeyName, module.TableTo.Name, module.ToLocalIdName, (int)reader.GetValue(i));
        //                        }

        //                        // Handle column mapping from 'From' table to 'To' table
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName) && id != -1)
        //                        {
        //                            data += $"{d.ColumnToName} = '{value}',"; // استخدم القيمة المأخوذة من القاموس
        //                        }
        //                    }

        //                    allValues.Add(rowValues);

        //                    if (id == -1)
        //                    {
        //                        var insertValues = string.Join(", ", columnFrom
        //                            .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                            .Select(c => $"'{rowValues[c.Name]}'"));

        //                        var insertQuery = $"INSERT INTO {module.TableTo.Name} ({string.Join(",", columnFrom.Where(c => !string.IsNullOrEmpty(c.ColumnToName)).Select(c => c.ColumnToName))}) VALUES ({insertValues})";

        //                        insertQuery = insertQuery.Replace("AM", string.Empty);
        //                        insertQuery = insertQuery.Replace("PM", string.Empty);
        //                        updateQueries.Add(insertQuery);
        //                    }

        //                    // تصحيح موضع الـ else block
        //                    if (!string.IsNullOrEmpty(data))
        //                    {
        //                        var updateQuery = $"UPDATE {module.TableTo.Name} SET {data.TrimEnd(',')} WHERE {module.ToPrimaryKeyName}={id}";
        //                        updateQueries.Add(updateQuery);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    await _toDbContext.Database.ExecuteSqlRawAsync(string.Join(";", updateQueries));

        //    // Return all values and update queries
        //    return Ok(updateQueries);
        //}

        #endregion

        #region v10
        //private async Task<int> GetToPrimaryKeyId(string ToPrimaryKeyName, string ToTableName, string ToLocalIdName, int value)
        //{
        //    MySqlConnection conn = new MySqlConnection(_toDbContext.Database.GetConnectionString());
        //    try
        //    {
        //        Console.WriteLine("Connecting to MySQL...");
        //        await conn.OpenAsync();  // Use async method for non-blocking IO

        //        string sql = $"SELECT count(*) FROM {ToTableName} WHERE {ToLocalIdName} = {value}";
        //        //string sql = $"SELECT id FROM categories WHERE local_id = 1";
        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        MySqlDataReader rdr = cmd.ExecuteReader();  // Use async for non-blocking IO

        //        if (await rdr.ReadAsync())  // Check if there is a result
        //        {
        //            int id = rdr.GetInt32(0);  // Assuming 'id' is an integer. Adjust based on your schema.
        //            return id;  // Return the 'id' in a structured response
        //        }
        //        else
        //        {
        //            return -1;  // If no result is found
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return -1;  // Return an internal server error if something goes wrong
        //    }
        //    finally
        //    {
        //        conn.Close();  // Ensure the connection is closed in the finally block
        //        Console.WriteLine("Done.");
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    // Retrieve the module along with related tables and conditions
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);

        //    if (module is null)
        //        return BadRequest("Module not found.");

        //    // Fetch all columns related to the TableFrom for this module
        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();

        //    // Build the SELECT query dynamically
        //    var selectColumns = string.Join(',', columnFrom.Select(c => c.Name));
        //    var conditions = module.conditionFroms?.Select(c => c.Operation) ?? new List<string>();
        //    var queryFrom = $"SELECT {selectColumns} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", conditions)}";

        //    // Initialize lists to store update queries and row values
        //    var updateQueries = new List<string>();
        //    var allValues = new List<Dictionary<string, string>>();

        //    // Open a connection to retrieve data based on the constructed query
        //    using var connection = _fromDbContext.Database.GetDbConnection();
        //    await connection.OpenAsync();

        //    using var command = connection.CreateCommand();
        //    command.CommandText = queryFrom;

        //    using var reader = await command.ExecuteReaderAsync();
        //    while (await reader.ReadAsync())
        //    {
        //        var rowData = new Dictionary<string, string>();
        //        var data = "";
        //        int id = -1;

        //        // Iterate over each field in the row to retrieve values
        //        for (int i = 0; i < reader.FieldCount; i++)
        //        {
        //            var column = columnFrom.FirstOrDefault(c => c.Name == reader.GetName(i));
        //            var columnName = reader.GetName(i);
        //            string value = GetFormattedValue(reader, i); // New method to handle formatting

        //            rowData[columnName] = value;

        //            if (columnName == module.fromPrimaryKeyName)
        //            {
        //                id = await GetToPrimaryKeyId(module.ToPrimaryKeyName, module.TableTo.Name, module.ToLocalIdName, Convert.ToInt32(reader.GetValue(i)));
        //            }

        //            // Map column from TableFrom to TableTo based on defined mappings
        //            if (column != null && !string.IsNullOrEmpty(column.ColumnToName) && id != -1)
        //            {
        //                data += $"{column.ColumnToName} = '{value}',";
        //            }
        //        }

        //        allValues.Add(rowData);

        //        // Generate either INSERT or UPDATE queries based on the existence of primary key
        //        if (id == -1)
        //        {
        //            updateQueries.Add(GenerateInsertQuery(module.TableTo.Name, columnFrom, rowData)); // New method for inserts
        //        }
        //        else if (!string.IsNullOrEmpty(data))
        //        {
        //            updateQueries.Add($"UPDATE {module.TableTo.Name} SET {data.TrimEnd(',')} WHERE {module.ToPrimaryKeyName}={id}");
        //        }
        //    }

        //    var SqlASUpdate = string.Join(";", updateQueries);
        //    using (var connection2 = _toDbContext.Database.GetDbConnection())
        //    {
        //        await connection2.OpenAsync();
        //        await _toDbContext.Database.ExecuteSqlRawAsync(SqlASUpdate);
        //    }



        //    // Return the generated update queries for inspection or logging
        //    return Ok(updateQueries);
        //}

        //// Helper method to format values based on data type
        //private string GetFormattedValue(DbDataReader reader, int i)
        //{
        //    if (reader.IsDBNull(i)) return "NULL";

        //    if (reader.GetFieldType(i) == typeof(DateTime))
        //    {
        //        return $"{reader.GetDateTime(i):yyyy-MM-dd HH:mm:ss}";
        //    }
        //    return reader.GetValue(i).ToString().Trim();
        //}

        //// Helper method to construct an INSERT query
        //private string GenerateInsertQuery(string tableName, List<ColumnFrom> columns, Dictionary<string, string> rowData)
        //{
        //    var columnNames = string.Join(",", columns.Where(c => !string.IsNullOrEmpty(c.ColumnToName)).Select(c => c.ColumnToName));
        //    var values = string.Join(", ", columns.Where(c => !string.IsNullOrEmpty(c.ColumnToName)).Select(c => $"'{rowData[c.Name]}'"));

        //    var insertQuery = $"INSERT INTO {tableName} ({columnNames}) VALUES ({values})";
        //    return insertQuery.Replace("AM", string.Empty).Replace("PM", string.Empty);
        //}

        #endregion


        #region v11

        //[HttpGet]
        //public async Task<IActionResult> Test(int moduleId)
        //{
        //    var module = await _appDbContext.modules
        //        .Include(c => c.TableFrom)
        //            .ThenInclude(t => t.TableTo)
        //                .ThenInclude(c => c.ColumnToList)
        //        .Include(c => c.conditionFroms)
        //        .Include(c => c.ConditionTos)
        //        .FirstOrDefaultAsync(c => c.Id == moduleId);
        //    if (module is null)
        //        return BadRequest("Module not found.");
        //    var references = await _appDbContext.References
        //          .Include(c => c.TableFrom)
        //          .Include(c => c.TableTo)
        //          .Where(c => c.ModuleId == 10)
        //          .ToListAsync();

        //    var ReferencesIds = await GetReferenceASync(references);



        //    var columnFrom = await _appDbContext.columnFroms
        //        .Where(c => c.tableFromId == module.TableFromId)
        //        .ToListAsync();
        //    //var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
        //    var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name}";
        //    var updateQueries = new List<string>();
        //    var allValues = new List<Dictionary<string, string>>(); // لتخزين كل القيم
        //    var AllIdsAndLocalIdsOnCloud = await FetchIdsAsync(module.ToPrimaryKeyName, module.ToLocalIdName, module.TableTo.Name);

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
        //                    var data = "";
        //                    var id = 0;
        //                    var rowValues = new Dictionary<string, string>(); // لتخزين القيم في كل صف

        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        var n = reader.GetName(i);
        //                        var d = columnFrom.FirstOrDefault(c => c.Name == n);
        //                        var Name = reader.GetName(i);

        //                        // معالجة القيمة بحسب نوعها
        //                        string value;
        //                        if (reader.IsDBNull(i))
        //                        {
        //                            value = "NULL";
        //                        }
        //                        else
        //                        {
        //                            // إذا كان النوع DateTime، قم بتنسيق القيمة
        //                            if (reader.GetFieldType(i) == typeof(DateTime))
        //                            {
        //                                DateTime dateTimeValue = reader.GetDateTime(i);
        //                                value = $"{dateTimeValue:yyyy-MM-dd HH:mm:ss}"; // تنسيق التاريخ
        //                            }
        //                            else
        //                            {
        //                                value = reader.GetValue(i).ToString().Trim();
        //                            }
        //                        }

        //                        // تخزين القيمة في القاموس
        //                        rowValues[Name] = value;

        //                        // تأكد من استرجاع المفتاح الأساسي بشكل صحيح
        //                        if (Name == module.fromPrimaryKeyName)
        //                        {
        //                            int key = Convert.ToInt32(reader.GetValue(i).ToString()); // Get the primary key value as a string

        //                            // Fetch the newId from the dictionary
        //                            if (AllIdsAndLocalIdsOnCloud.TryGetValue(key.ToString(), out var newId))
        //                            {
        //                                id = Convert.ToInt32((newId as Dictionary<dynamic, object>)[module.ToPrimaryKeyName]); // Assuming "id" is the key you want
        //                            }
        //                            else
        //                            {
        //                                id = -1;
        //                            }


        //                        }

        //                        // Handle column mapping from 'From' table to 'To' table
        //                        if (d != null && !string.IsNullOrEmpty(d.ColumnToName) && id != -1)
        //                        {
        //                            data += $"{d.ColumnToName} = '{value}',"; // استخدم القيمة المأخوذة من القاموس
        //                        }
        //                    }

        //                    allValues.Add(rowValues);

        //                    if (id == -1)
        //                    {
        //                        var insertValues = string.Join(", ", columnFrom
        //                            .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
        //                            .Select(c => $"'{rowValues[c.Name]}'"));

        //                        var insertQuery = $"INSERT INTO {module.TableTo.Name} ({string.Join(",", columnFrom.Where(c => !string.IsNullOrEmpty(c.ColumnToName)).Select(c => c.ColumnToName))}) VALUES ({insertValues})";

        //                        insertQuery = insertQuery.Replace("AM", string.Empty);
        //                        insertQuery = insertQuery.Replace("PM", string.Empty);
        //                        updateQueries.Add(insertQuery);
        //                    }

        //                    // تصحيح موضع الـ else block
        //                    if (!string.IsNullOrEmpty(data))
        //                    {
        //                        var updateQuery = $"UPDATE {module.TableTo.Name} SET {data.TrimEnd(',')} WHERE {module.ToPrimaryKeyName}={id}";
        //                        updateQueries.Add(updateQuery);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    //var Res=  await _toDbContext.Database.ExecuteSqlRawAsync(string.Join(";", updateQueries));

        //    // Return all values and update queries
        //    return Ok(updateQueries);
        //}



        //private async Task<Dictionary<dynamic, object>>
        //    FetchIdsAsync(string CloudIdName, string CloudLocalIdName, string CloudTable)
        //{
        //    var dataDictionary = new Dictionary<dynamic, object>();

        //    using (var connection = new MySqlConnection(_toDbContext.Database.GetConnectionString()))
        //    {
        //        await connection.OpenAsync();
        //        using (var command = new MySqlCommand($"SELECT {CloudIdName}, {CloudLocalIdName} FROM {CloudTable}", connection))
        //        {
        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    var id = reader[CloudLocalIdName].ToString(); // Assuming local_id is a string
        //                    var value = new Dictionary<dynamic, object>();

        //                    // Assuming you want to store the id and its corresponding values
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        value[reader.GetName(i)] = reader.GetValue(i);
        //                    }
        //                    dataDictionary[id] = value; // Use local_id as the key
        //                }
        //            }
        //        }
        //    }

        //    return dataDictionary; // Return the dictionary with local_id as the key
        //}



        //private async Task<Dictionary<string, Dictionary<dynamic, object>>> GetReferenceASync(List<TableReference> references)
        //{
        //    var result = new Dictionary<string, Dictionary<dynamic, object>>();

        //    foreach (var reference in references)
        //    {
        //        result.Add(reference.TableFrom.Name, await FetchRefsAsync(reference.LocalPrimary, reference.cloudLocalName, reference.TableFrom.Name));
        //    }
        //    return result;
        //}
        //private async Task<Dictionary<dynamic, object>> FetchRefsAsync(string cloudPrimaryName, string cloudLocalIdName, string tableName)
        //{
        //    var dataDictionary = new Dictionary<dynamic, object>();

        //    try
        //    {
        //        using (var connection = new MySqlConnection(_toDbContext.Database.GetConnectionString()))
        //        {
        //            await connection.OpenAsync();
        //            using (var command = new MySqlCommand($"SELECT {cloudPrimaryName}, {cloudLocalIdName} FROM {tableName};", connection))
        //            {
        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        var localId = reader[cloudLocalIdName].ToString();

        //                        var obj = new
        //                        {
        //                            Id = reader[cloudPrimaryName],
        //                            LocalId = reader[cloudLocalIdName]
        //                        };

        //                        dataDictionary[localId] = obj;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (use your preferred logging framework)
        //        Console.WriteLine(ex.Message);
        //    }

        //    return dataDictionary; // Return the dictionary with local_id as the key
        //}

        #endregion

        [HttpGet]
        public async Task<IActionResult> Test(int moduleId)
        {
            var module = await _appDbContext.modules
                .Include(c => c.TableFrom)
                    .ThenInclude(t => t.TableTo)
                        .ThenInclude(c => c.ColumnToList)
                .Include(c => c.conditionFroms)
                .Include(c => c.ConditionTos)
                .FirstOrDefaultAsync(c => c.Id == moduleId);
            if (module is null)
                return BadRequest("Module not found.");
            var references = await _appDbContext.References
                  .Include(c => c.TableFrom)
                  .Include(c => c.TableTo)
                  .Where(c => c.ModuleId == moduleId)
                  .ToListAsync();

            var ReferencesIds=await GetReferenceASync(references);



            var columnFrom = await _appDbContext.columnFroms
                .Where(c => c.tableFromId == module.TableFromId)
                .ToListAsync();
            //var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name} WHERE {string.Join(" OR ", module.conditionFroms?.Select(c => c.Operation) ?? new List<string>())}";
            var queryFrom = $"SELECT {string.Join(',', columnFrom.Select(c => c.Name))} FROM {module.TableFrom?.Name}";
            var updateQueries = new List<string>();
            var allValues = new List<Dictionary<string, string>>(); // لتخزين كل القيم
            var AllIdsAndLocalIdsOnCloud =await  FetchIdsAsync(module.ToPrimaryKeyName,module.ToLocalIdName,module.TableTo.Name);

            using (var connection = _fromDbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryFrom;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var data = "";
                            var id = 0;
                            var rowValues = new Dictionary<string, string>(); // لتخزين القيم في كل صف

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var n = reader.GetName(i);
                                var d = columnFrom.FirstOrDefault(c => c.Name == n);
                                var Name = reader.GetName(i);

                                // معالجة القيمة بحسب نوعها
                                dynamic value;
                                if (reader.IsDBNull(i))
                                {
                                    value = "NULL";
                                }
                                else
                                {
                                    // إذا كان النوع DateTime، قم بتنسيق القيمة
                                    if (reader.GetFieldType(i) == typeof(DateTime))
                                    {
                                        DateTime dateTimeValue = reader.GetDateTime(i);
                                        value = $"{dateTimeValue:yyyy-MM-dd HH:mm:ss}"; // تنسيق التاريخ
                                    }
                                    else
                                    {
                                         value=  reader.GetValue(i).ToString().Trim(); 
                                        
                                    }

                                    if (d != null)
                                    {
                                        if (d.isReference)
                                        {
                                            if (ReferencesIds.ContainsKey(d.TableToName))
                                            {
                                                if (ReferencesIds[d.TableToName].TryGetValue(value, out object newid))
                                                {
                                                    var NewValued = newid as dynamic;
                                                    if (NewValued != null)
                                                    {
                                                        if (NewValued.Id is int idValue)
                                                        {
                                                            value = idValue.ToString(); 
                                                        }
                                                        else
                                                        {
                                                            value = NewValued.Id.ToString(); 
                                                        }
                                                    }
                                                }

                                            }
                                        }


                                    }

                                }




                                rowValues[Name] = value;

                                if (Name == module.fromPrimaryKeyName)
                                {
                                    int key = Convert.ToInt32(reader.GetValue(i).ToString()); 

                                    if (AllIdsAndLocalIdsOnCloud.TryGetValue(key.ToString(), out var newId))
                                    {
                                        id = Convert.ToInt32((newId as Dictionary<dynamic, object>)[module.ToPrimaryKeyName]);
                                    }
                                    else
                                    {
                                        id = -1;   
                                    }
                                }

                                if (d != null && !string.IsNullOrEmpty(d.ColumnToName) && id != -1)
                                {
                                    data += $"{d.ColumnToName} = '{value}',"; 
                                }
                            }

                            allValues.Add(rowValues);

                            if (id == -1)
                            {
                                var insertValues = string.Join(", ", columnFrom
                                    .Where(c => !string.IsNullOrEmpty(c.ColumnToName))
                                    .Select(c => $"'{rowValues[c.Name]}'"));

                                var insertQuery = $"INSERT INTO {module.TableTo.Name} ({string.Join(",", columnFrom.Where(c => !string.IsNullOrEmpty(c.ColumnToName)).Select(c => c.ColumnToName))}) VALUES ({insertValues})";

                                insertQuery = insertQuery.Replace("AM", string.Empty);
                                insertQuery = insertQuery.Replace("PM", string.Empty);
                                updateQueries.Add(insertQuery);
                            }

                            // تصحيح موضع الـ else block
                            if (!string.IsNullOrEmpty(data))
                            {
                                var updateQuery = $"UPDATE {module.TableTo.Name} SET {data.TrimEnd(',')} WHERE {module.ToPrimaryKeyName}={id}";
                                updateQueries.Add(updateQuery);
                            }
                        }
                    }
                }
            }

              await _toDbContext.Database.ExecuteSqlRawAsync(string.Join(";", updateQueries));

            // Return all values and update queries
            return Ok(updateQueries);
        }



        private async Task<Dictionary<dynamic, object>> 
            FetchIdsAsync(string CloudIdName,string CloudLocalIdName,string CloudTable )
        {
            var dataDictionary = new Dictionary<dynamic, object>();

            using (var connection = new MySqlConnection(_toDbContext.Database.GetConnectionString()))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand($"SELECT {CloudIdName}, {CloudLocalIdName} FROM {CloudTable}", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var id = reader[CloudLocalIdName].ToString(); // Assuming local_id is a string
                            var value = new Dictionary<dynamic, object>();

                            // Assuming you want to store the id and its corresponding values
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                value[reader.GetName(i)] = reader.GetValue(i);
                            }
                            dataDictionary[id] = value; // Use local_id as the key
                        }
                    }
                }
            }

            return dataDictionary; // Return the dictionary with local_id as the key
        }

      
        
        private async Task<Dictionary<string,Dictionary<dynamic,object>>> GetReferenceASync(List<TableReference> references)
        {
            var result = new Dictionary<string, Dictionary<dynamic, object>>(); 

            foreach (var reference in references)
            {
                result.Add(reference.TableFrom.Name,await FetchRefsAsync(reference.LocalPrimary, reference.cloudLocalName, reference.TableFrom.Name));
            }
            return result;
        }
        private async Task<Dictionary<dynamic, object>> FetchRefsAsync(string cloudPrimaryName, string cloudLocalIdName, string tableName)
        {
            var dataDictionary = new Dictionary<dynamic, object>();

            try
            {
                using (var connection = new MySqlConnection(_toDbContext.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand($"SELECT {cloudPrimaryName}, {cloudLocalIdName} FROM {tableName};", connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var localId = reader[cloudLocalIdName].ToString();

                                var obj = new
                                {
                                    Id = reader[cloudPrimaryName],
                                    LocalId = reader[cloudLocalIdName]
                                };

                                if (localId != null)
                                {
                                    dataDictionary[localId] = obj;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use your preferred logging framework)
                Console.WriteLine(ex.Message);
            }

            return dataDictionary; // Return the dictionary with local_id as the key
        }
        [HttpGet("GetIds")]
        public async Task<IActionResult> GetIds()
        {

            var references = await _appDbContext.References
                .Include(c => c.TableFrom)
                .Include(c => c.TableTo)
                .Where(c => c.ModuleId == 10)
                .ToListAsync();

            var ReferencesIds = await GetReferenceASync(references);


            return Ok(ReferencesIds);


        }
    }
}
