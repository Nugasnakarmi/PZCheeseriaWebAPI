using PZCheeseriaWebAPI.DTO;
using PZCheeseriaWebAPI.Helpers;
using PZCheeseriaWebAPI.Interfaces;
using System.Data;

namespace PZCheeseriaWebAPI.Services;

public class CheeseService : ICheeseService
{
    private static DataTable table;
    /* The data is being stored in-memory but with more time it could have been stored in a physical database, possibly a relational
     database like SQL Server preferrably, but can also use non-relational databases like MongoDB etc.
     TODO: Replace in-memory Datatable with physical database. Make async functions when communicating with physical db.
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
        set
        {
            table = value;
        }
    }

    public List<CheeseDTO> GetCheeseList()
    {
        List<CheeseDTO> cheeseList = new List<CheeseDTO>();
        if (DataTable.Rows.Count == 0)
        {
            return [];
        }
        foreach (DataRow cheeseRow in DataTable.Rows)
        {
            cheeseList.Add(MakeNewCheese(cheeseRow));
        }

        return cheeseList;
    }

    public CheeseDTO GetCheese(int cheeseId)
    {
        try
        {
            string selectExpression = $"Id = {cheeseId}";

            DataRow cheeseRow = DataTable.Select(selectExpression).FirstOrDefault();
            if (cheeseRow == null)
            {
                return null;
            }
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

    public CheeseDTO AddCheeseToTable(CheeseDTO cheese)
    {
        try
        {
            table.Rows.Add(null, cheese.Name, cheese.ImageUrl, cheese.PricePerKilo, cheese.Color);
            var newRow = table.Rows[table.Rows.Count - 1];
            return MakeNewCheese(newRow);
        }
        catch (Exception ex)
        {
            throw new ExceptionHelper($"Error adding cheese: {ex.Message}");
        }
    }

    public CheeseDTO UpdateCheese(int cheeseId, CheeseDTO cheese)
    {
        try
        {
            string selectExpression = $"Id = {cheeseId}";
            DataRow cheeseRow = DataTable.Select(selectExpression).FirstOrDefault();
            if (cheeseRow == null)
            {
                return null;
            }

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

    public bool DeleteCheese(int cheeseId)
    {
        try
        {
            string selectExpression = $"Id = {cheeseId}";
            DataRow cheeseRow = DataTable.Select(selectExpression).FirstOrDefault();

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