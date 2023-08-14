using _03_linq_to_sql;

internal class Program
{
    private static void Main(string[] args)
    {
        string conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        ShopDbContext context = new ShopDbContext(conn);

        
    }
}