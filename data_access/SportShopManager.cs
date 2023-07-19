using System.Data.SqlClient;

namespace data_access
{
    public class SportShopManager
    {
        private SqlConnection connection = null;

        public SportShopManager(string connectionStr)
        {
            connection = new(connectionStr);
            connection.Open();
        }

        private void ShowTable(SqlDataReader reader)
        {
            // відображається назви всіх колонок таблиці
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i),-10}\t");
            }
            Console.WriteLine('\n' + new string('-', 100));

            // відображаємо всі значення кожного рядка
            while (reader.Read()) // go to the next row
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i],-10}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine('\n' + new string('_', 100));
        }

        // ------------ public interface
        public void ShowAllProducts()
        {
            const string cmd = "select * from Products;";
            SqlCommand sql = new(cmd, connection);

            using var reader = sql.ExecuteReader();
            ShowTable(reader);
        }
        public void ShowSalles(int productId)
        {
            string cmd = $"select * from Salles where ProductId = {productId};";
            SqlCommand sql = new(cmd, connection);

            using var reader = sql.ExecuteReader();
            ShowTable(reader);
        }

        public Product? GetProduct(string name)
        {
            string cmd = $"select * from Products where Name = @name;";
            SqlCommand sql = new(cmd, connection);
            sql.Parameters.AddWithValue("@name", name);

            using var reader = sql.ExecuteReader();

            if (!reader.Read())
                return null;

            return new Product()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Price = (int)reader["Price"],
                CostPrice = (int)reader["CostPrice"],
                Quantity = (int)reader["Quantity"],
                Producer = (string)reader["Producer"],
                Type = (string)reader["TypeProduct"],
            };
        }
        public IEnumerable<Product> GetProductsByClient(int clientId)
        {
            string cmd = "select * from Products as p JOIN Salles as s ON s.ProductId = p.Id where s.ClientId = @id;";
            SqlCommand sql = new(cmd, connection);
            sql.Parameters.AddWithValue("@id", clientId);

            using var reader = sql.ExecuteReader();

            while (reader.Read())
            {
                yield return new Product()
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Price = (int)reader["Price"],
                    CostPrice = (int)reader["CostPrice"],
                    Quantity = (int)reader["Quantity"],
                    Producer = (string)reader["Producer"],
                    Type = (string)reader["TypeProduct"],
                };
            }
        }

        public void DeleteSalles(int id)
        {
            string cmd = $"delete from Salles where Id = {id}";
            SqlCommand sql = new(cmd, connection);

            sql.ExecuteNonQuery();
        }
        public void UpdatePrice(decimal newPrice, int productId)
        {
            string cmd = $"update Products set Price = {newPrice} where Id = {productId}";
            SqlCommand sql = new(cmd, connection);

            sql.ExecuteNonQuery();
        }

        public void AddProduct(Product product)
        {
            string cmd = $"insert Products values (@name, @type, @quantity, @cost, @producer, @price);";
            SqlCommand sql = new(cmd, connection);

            sql.Parameters.AddWithValue("@name", product.Name);
            sql.Parameters.AddWithValue("@type", product.Type);
            sql.Parameters.AddWithValue("@quantity", product.Quantity);
            sql.Parameters.AddWithValue("@cost", product.CostPrice);
            sql.Parameters.AddWithValue("@producer", product.Producer);
            sql.Parameters.AddWithValue("@price", product.Price);

            sql.ExecuteNonQuery();
        }
    }
}
