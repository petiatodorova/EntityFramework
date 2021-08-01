using Newtonsoft.Json;
using System;

namespace ConsoleApp2
{
    class SerializeDeserialize
    {
        static void Main(string[] args)
        {
            var product = new Product
            {
                Id = 1,
                Name = "Oil pump",
                Description = null,
                Cost = 25
            };

            var jsonProduct = JsonConvert.SerializeObject(product, Formatting.Indented);

            Console.WriteLine(jsonProduct);

            var objProduct = JsonConvert.DeserializeObject<Product>(jsonProduct);
            Console.WriteLine($"id = {objProduct.Id}");
            Console.WriteLine($"Name = {objProduct.Name}");
            Console.WriteLine($"Desc = {objProduct.Description}");
            Console.WriteLine($"Cost = {objProduct.Cost}");

            
        }
    }
}
