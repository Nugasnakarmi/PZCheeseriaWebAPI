using PZCheeseriaWebAPI.DTO;
using PZCheeseriaWebAPI.Helpers;
using PZCheeseriaWebAPI.Interfaces;
using System.Data;
using System.Data.Common;

namespace PZCheeseriaWebAPI.Services;

public class CheeseService : ICheeseService
{
    private static DataTable table;
    /* The data is being stored in-memory but with more time it could have been stored in a physical database, possibly a relational
     database like SQL Server preferrably, but can also use non-relational databases like MongoDB etc.
     
    TODO: Replace in-memory Datatable with physical database.
     */

    public static DataTable DataTable
    {
        get
        {
            if (table == null)
            {
                table = new DataTable();
                // Initialize DataTable with columns and initial data
                DataColumn id = new DataColumn("Id", typeof(int))
                {
                    AutoIncrement = true,
                    AutoIncrementSeed = 1,
                    AutoIncrementStep = 1
                };
                // Define columns
                table.Columns.Add(id);
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("ImageUrl", typeof(string));
                table.Columns.Add("PricePerKilo", typeof(decimal));
                table.Columns.Add("Color", typeof(string));

                // Add rows
                table.Rows.Add(null, "Cheddar", "assets/images/Abbaye-de-citeaux.jpg", 20.00m, "Yellow");
                table.Rows.Add(null, "Brie", "assets/images/Abondance.jpg", 25.00m, "White");
                table.Rows.Add(null, "Gouda", "assets/images/AJCB_5_Grunge.jpg", 22.00m, "Yellow");
                table.Rows.Add(null, "Blue Cheese", "assets/images/Anster_1.jpg", 30.00m, "Blue");
                table.Rows.Add(null, "Mozzarella", "assets/images/Appenzeller_9M_3.jpg", 18.00m, "White");
            }
            return table;
        }
    }

    public async Task<List<CheeseDTO>> GetCheeseListAsync()
    {
        List<CheeseDTO> cheeseList = new List<CheeseDTO>();
        await Task.Run(() =>
         {
             foreach (DataRow cheeseRow in DataTable.Rows)
             {
                 cheeseList.Add(MakeNewCheese(cheeseRow));
             }
         });

        return cheeseList;
    }

    public async Task<CheeseDTO> GetCheeseAsync(int cheeseId)
    {
        try
        {
            string selectExpression = $"Id = {cheeseId}";
            /*Since querying a datatable is a CPU-bound operation, we can run it on a backgground thread*/

            DataRow cheeseRow = await Task.Run(() => DataTable.Select(selectExpression).FirstOrDefault());
            return MakeNewCheese(cheeseRow);
        }
        catch (Exception ex)
        {
            throw new ExceptionHelper($"Error getting cheese: {ex.Message}");
        }
    }

    public CheeseDTO MakeNewCheese(DataRow cheeseRow)
    {
        try
        {
            int id = int.Parse(cheeseRow["Id"].ToString());
            string name = cheeseRow["Name"].ToString();
            string imageUrl = cheeseRow["ImageUrl"].ToString();
            decimal pricePerKilo = decimal.Parse(cheeseRow["PricePerKilo"].ToString());
            string color = cheeseRow["Color"].ToString();

            return new CheeseDTO(id, name, imageUrl, pricePerKilo, color);
        }
        catch (Exception ex)
        {
            throw new ExceptionHelper($"Error making new cheese: {ex.Message}");
        }
    }

    public DataTable AddCheeseToTable(CheeseDTO cheese)
    {
        try
        {
            table.Rows.Add(null, cheese.Name, cheese.ImageUrl, cheese.PricePerKilo, cheese.Color);
            return table;
        }
        catch (Exception ex)
        {
            throw new ExceptionHelper($"Error adding cheese: {ex.Message}");
        }
    }

    public async Task<CheeseDTO> UpdateCheeseAsync(int cheeseId, CheeseDTO cheese)
    {
        try
        {
            string selectExpression = $"Id = {cheeseId}";
            DataRow cheeseRow = await Task.Run(() => DataTable.Select(selectExpression).FirstOrDefault());

            cheeseRow["Name"] = cheese.Name;
            cheeseRow["Imageurl"] = cheese.ImageUrl;
            cheeseRow["Color"] = cheese.Color;
            cheeseRow["PricePerKilo"] = cheese.PricePerKilo;

            return MakeNewCheese(cheeseRow);
        }
        catch (Exception ex)
        {
            throw new ExceptionHelper($"Error updating cheese: {ex.Message}");
        }
    }

    public async Task<bool> DeleteCheeseAsync(int cheeseId)
    {
        try
        {
            string selectExpression = $"Id = {cheeseId}";
            DataRow cheeseRow = await Task.Run(() => DataTable.Select(selectExpression).FirstOrDefault());

            if (cheeseRow == null)
                return false;

            DataTable.Rows.Remove(cheeseRow);
            return true;
        }
        catch (Exception ex)
        {
            throw new ExceptionHelper($"Error deleting cheese: {ex.Message}");
        }
    }
}