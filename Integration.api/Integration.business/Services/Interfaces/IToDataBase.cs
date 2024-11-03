using Integration.business.DTOs.FromDTOs;

namespace Integration.business.Services.Interfaces
{
    public interface IToDataBase
    {
        Task<bool> AddToDataBase(DbToAddDTO ToDbToAddDTO);
        Task<bool> EditToDataBase(DbToEditDTO ToDbToEditDTO);
        Task<bool> SelectToDataBase(int DbId);
        Task<DbToReturn> GetToById(int DbId);
        Task<List<DbToReturn>> GetToList();
    }
}
