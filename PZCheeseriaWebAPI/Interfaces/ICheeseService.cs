using PZCheeseriaWebAPI.DTO;
using System.Data;

namespace PZCheeseriaWebAPI.Interfaces
{
    public interface ICheeseService
    {
        public Task<List<CheeseDTO>> GetCheeseListAsync();

        public DataTable AddCheeseToTable(CheeseDTO cheese);

        public Task<CheeseDTO> UpdateCheeseAsync(int cheeseId, CheeseDTO cheese);

        public Task<CheeseDTO> GetCheeseAsync(int id);

        public Task<bool> DeleteCheeseAsync(int cheeseId);
    }
}