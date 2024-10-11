using PZCheeseriaWebAPI.DTO;
using PZCheeseriaWebAPI.Interfaces;
using System.Data;
using System.Data.Common;

namespace PZCheeseriaWebAPI.Services;

public class CheeseService: ICheeseService
{
    private static DataTable table;
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
    public DataTable GetCheeseTable()
    {
        
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
        table.Rows.Add(null,"Cheddar", "assets/images/Abbaye-de-citeaux.jpg", 20.00m, "Yellow");
        table.Rows.Add(null, "Brie", "assets/images/Abondance.jpg", 25.00m, "White");
        table.Rows.Add(null, "Gouda", "assets/images/AJCB_5_Grunge.jpg", 22.00m, "Yellow");
        table.Rows.Add(null, "Blue Cheese", "assets/images/Anster_1.jpg", 30.00m, "Blue");
        table.Rows.Add(null, "Mozzarella", "assets/images/Appenzeller_9M_3.jpg", 18.00m, "White");

        return table;

    }

    public List<CheeseDTO> GetCheeseList()
    {
        List<CheeseDTO> cheeseList = new List<CheeseDTO>();
        foreach (DataRow row in DataTable.Rows)
        {
            int id = int.Parse(row["Id"].ToString());
            string name = row["Name"].ToString();
            string imageUrl = row["ImageUrl"].ToString();
            decimal pricePerKilo = decimal.Parse(row["PricePerKilo"].ToString());
            string color = row["Color"].ToString();
            cheeseList.Add(new CheeseDTO(id, name, imageUrl, pricePerKilo, color));
        }
        return cheeseList;
    }

    public DataTable AddCheeseToTable( CheeseDTO cheese)
    {
        table.Rows.Add( null, cheese.Name, cheese.ImageUrl, cheese.PricePerKilo, cheese.Color); 
        return table;
    }
}

