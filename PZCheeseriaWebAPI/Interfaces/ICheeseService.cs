using PZCheeseriaWebAPI.DTO;
using System.Data;

namespace PZCheeseriaWebAPI.Interfaces
{
    public interface ICheeseService
    {
        public DataTable GetCheeseTable();
        public List<CheeseDTO> GetCheeseList();
        public DataTable AddCheeseToTable( CheeseDTO cheese);
    }
}
