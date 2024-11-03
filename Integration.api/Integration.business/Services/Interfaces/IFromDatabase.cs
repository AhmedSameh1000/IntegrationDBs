using Integration.business.DTOs.FromDTOs;

namespace Integration.business.Services.Interfaces
{
    public interface IFromDatabase
    {
        Task<bool> AddFromDataBase(DbToAddDTO fromDbToAddDTO);
        Task<bool> EditFromDataBase(DbToEditDTO fromDbToEditDTO);
        Task<bool> SelectFromDataBase(int DbId);
        Task<DbToReturn> GetFromById(int DbId);
        Task<List<DbToReturn>> GetFromList();

    }

}
