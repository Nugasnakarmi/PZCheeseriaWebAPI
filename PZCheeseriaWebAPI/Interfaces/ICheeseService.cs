using System.Data;

namespace PZCheeseriaWebAPI.Interfaces
{
    public interface ICheeseService
    {
        public DataTable GetCheeseTable();
    }
}
