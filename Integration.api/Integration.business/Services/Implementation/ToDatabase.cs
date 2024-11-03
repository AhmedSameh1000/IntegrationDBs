using AutoRepairPro.Data.Models;
using AutoRepairPro.Data.Repositories.Interfaces;
using Integration.business.DTOs.FromDTOs;
using Integration.business.Services.Interfaces;

namespace Integration.business.Services.Implementation
{
    public class ToDatabase : IToDataBase
    {
        private readonly IGenericRepository<ToDb> _ToDataBase;

        public ToDatabase(IGenericRepository<ToDb> ToDataBase)
        {
            _ToDataBase = ToDataBase;
        }
        public async Task<bool> AddToDataBase(DbToAddDTO ToDbToAddDTO)
        {
            var ToDB = new ToDb()
            {
                DbName = ToDbToAddDTO.Name,
                ConnectionString = ToDbToAddDTO.Connection
            };
            await _ToDataBase.Add(ToDB);
            return await _ToDataBase.SaveChanges();
        }

        public async Task<bool> EditToDataBase(DbToEditDTO ToDbToEditDTO)
        {
            var ToDb = await _ToDataBase.GetFirstOrDefault(c => c.Id == ToDbToEditDTO.Id);

            if (ToDb == null)
                return false;

            ToDb.DbName = ToDbToEditDTO.Name;
            ToDb.ConnectionString = ToDbToEditDTO.Connection;
            _ToDataBase.Update(ToDb);
            return await _ToDataBase.SaveChanges();
        }

        public async Task<DbToReturn> GetToById(int DbId)
        {
            var ToDb = await _ToDataBase.GetFirstOrDefault(c => c.Id == DbId);

            if (ToDb is null)
                return null;

            var Result = new DbToReturn()
            {
                Id = DbId,
                Connection = ToDb.ConnectionString,
                Name = ToDb.DbName,
                IsSelected = ToDb.IsSelected
            };

            return Result;
        }

        public async Task<List<DbToReturn>> GetToList()
        {
            var ToDbs = await _ToDataBase.GetAllAsNoTracking();

            return ToDbs.Select(c => new DbToReturn()
            {
                Connection = c.ConnectionString,
                Id = c.Id,
                Name = c.DbName,
                IsSelected = c.IsSelected
            }).ToList();
        }

        public async Task<bool> SelectToDataBase(int DbId)
        {
            var DbToToSelect = await _ToDataBase.GetFirstOrDefault(c => c.Id == DbId);
            if (DbToToSelect is null)
                return false;

            var SelectedToDb = await _ToDataBase.GetFirstOrDefault(c => c.IsSelected);
            if (SelectedToDb is null)
                return false;
            SelectedToDb.IsSelected = false;
            DbToToSelect.IsSelected = true;
            _ToDataBase.Update(SelectedToDb);
            _ToDataBase.Update(DbToToSelect);
            return await _ToDataBase.SaveChanges();
        }
    }
}
