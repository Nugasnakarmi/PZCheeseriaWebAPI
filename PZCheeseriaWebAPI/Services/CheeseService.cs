using PZCheeseriaWebAPI.Interfaces;
using System.Data;

namespace PZCheeseriaWebAPI.Services;

public class CheeseService: ICheeseService
{
    public DataTable GetCheeseTable()
    {
        DataTable table = new DataTable("CheeseTable");

        // Define columns
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("ImageUrl", typeof(string));
        table.Columns.Add("PricePerKilo", typeof(decimal));
        table.Columns.Add("Color", typeof(string));

        // Add rows
        table.Rows.Add("1","Cheddar", "assets/images/Abbaye-de-citeaux.jpg", 20.00m, "Yellow");
        table.Rows.Add("2", "Brie", "assets/images/Abondance.jpg", 25.00m, "White");
        table.Rows.Add("3","Gouda", "assets/images/AJCB_5_Grunge.jpg", 22.00m, "Yellow");
        table.Rows.Add("4","Blue Cheese", "assets/images/Anster_1.jpg", 30.00m, "Blue");
        table.Rows.Add("5", "Mozzarella", "assets/images/Appenzeller_9M_3.jpg", 18.00m, "White");

        return table;

    }
}

