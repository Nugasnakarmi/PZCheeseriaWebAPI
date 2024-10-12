using PZCheeseriaWebAPI.DTO;
using System.Data;

namespace PZCheeseriaWebAPI.Interfaces
{
    public interface ICheeseService
    {
        public List<CheeseDTO> GetCheeseList();

        public CheeseDTO AddCheeseToTable(CheeseDTO cheese);

        public CheeseDTO UpdateCheese(int cheeseId, CheeseDTO cheese);

        public CheeseDTO GetCheese(int id);

        public bool DeleteCheese(int cheeseId);
    }
}