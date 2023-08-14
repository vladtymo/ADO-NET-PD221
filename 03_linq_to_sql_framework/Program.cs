using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_linq_to_sql_framework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportShop;Integrated Security=True;Connect Timeout=3;Encrypt=False;";
            ShopDbContext context = new ShopDbContext(conn);

            // --------------- Read
            var all = context.Products;

            var filtered = context.Products.Where(x => x.Price < 1000);

            foreach (var item in filtered)
            {
                Console.WriteLine($"{item.Name} - {item.Price}$");
            }

            // find by 
            var prod = context.Products.FirstOrDefault(x => x.Producer == "USA");

            if (prod != null)
                Console.WriteLine($"{prod.Name} {prod.Producer} - {prod.Price}$");
            else
                Console.WriteLine("Not found!");

            // update data
            prod.Price += 120;

            // insert data
            context.Employees.InsertOnSubmit(new Employee()
            {
                Id = 1000, // ?
                FullName = "Martin Cuper",
                Gender = "M",
                HireDate = DateTime.Now,
                Salary = 3100
            });

            // remove data
            context.Salles.DeleteOnSubmit(context.Salles.First());

            // submit changes to databse (INSER, UPDATE, DELETE)
            context.SubmitChanges();
        }
    }
}
