using data_access;
using System.Configuration;
using System.Text;

namespace _01_connected_mode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            SportShopManager manager = new(ConfigurationManager.ConnectionStrings["SportShopDb"].ConnectionString);

            manager.ShowAllProducts();
            manager.ShowSalles(1);

            string name = "Ball"; //Console.ReadLine();
            var item = manager.GetProduct(name);
            Console.WriteLine(item);

            var items = manager.GetProductsByClient(2);
            foreach (var i in items) Console.WriteLine(i);

            manager.DeleteSalles(1);
            manager.UpdatePrice(4200, 2);
            manager.AddProduct(new()
            {
                Name = "HeavyGrip",
                Price = 430,
                CostPrice = 340,
                Producer = "USA",
                Quantity = 12,
                Type = "Espander"
            });

            manager.ShowAllProducts();
        }
    }
}