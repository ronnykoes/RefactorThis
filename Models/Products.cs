using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RefactorThis.Models
{
    public static class Products
    {
        private static List<Product> Items;

        public static List<Product> LoadProducts(string where)
        {
            Items = new List<Product>();
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select id from Products {where}";

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr.GetString(0));
                Items.Add(new Product(id));
            }

            return Items;
        }
    }
}