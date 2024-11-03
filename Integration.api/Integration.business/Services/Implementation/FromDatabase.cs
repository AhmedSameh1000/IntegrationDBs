using AutoRepairPro.Data.Models;
using AutoRepairPro.Data.Repositories.Interfaces;
using Integration.business.DTOs.FromDTOs;
using Integration.business.Services.Interfaces;

namespace Integration.business.Services.Implementation
{
    public class FromDatabase : IFromDatabase
    {
        private readonly IGenericRepository<FromDb> _fromDataBase;

        public FromDatabase(IGenericRepository<FromDb> FromDataBase)
        {
            _fromDataBase = FromDataBase;
        }
        public async Task<bool> AddFromDataBase(DbToAddDTO fromDbToAddDTO)
        {
            var FromDB = new FromDb()
            {
                DbName=fromDbToAddDTO.Name,
                ConnectionString=fromDbToAddDTO.Connection
            };
            await _fromDataBase.Add(FromDB);
            return await _fromDataBase.SaveChanges();
        }

        public async Task<bool> EditFromDataBase(DbToEditDTO fromDbToEditDTO)
        {
            var FromDb=await _fromDataBase.GetFirstOrDefault(c=>c.Id==fromDbToEditDTO.Id);

            if (FromDb == null)
                return false;

            FromDb.DbName = fromDbToEditDTO.Name;
            FromDb.ConnectionString = fromDbToEditDTO.Connection;
            _fromDataBase.Update(FromDb);
            return await _fromDataBase.SaveChanges();
        }

        public async Task<DbToReturn> GetFromById(int DbId)
        {
            var FromDb=await _fromDataBase.GetFirstOrDefault(c=>c.Id==DbId);

            if (FromDb is null)
                return null;

            var Result = new DbToReturn()
            {
                Id = DbId,
                Connection= FromDb.ConnectionString,
                Name=FromDb.DbName,
                IsSelected=FromDb.IsSelected,
            };

            return Result;
        }

        public async Task<List<DbToReturn>> GetFromList()
        {
            var FromDbs = await _fromDataBase.GetAllAsNoTracking();

            return FromDbs.Select(c => new DbToReturn()
            {
                Connection=c.ConnectionString,
                Id=c.Id,
                Name=c.DbName,
                IsSelected = c.IsSelected
            }).ToList();
        }

        public async Task<bool> SelectFromDataBase(int DbId)
        {
            var DbFromToSelect=await _fromDataBase.GetFirstOrDefault(c=>c.Id== DbId);
            if (DbFromToSelect is null)
                return false;

            var SelectedFromDb = await _fromDataBase.GetFirstOrDefault(c => c.IsSelected);
            if (SelectedFromDb is not null)
            {
                SelectedFromDb.IsSelected = false;
               _fromDataBase.Update(SelectedFromDb);
            }
            DbFromToSelect.IsSelected = true;
            _fromDataBase.Update(DbFromToSelect);
            return await _fromDataBase.SaveChanges();
        }
    }
}
