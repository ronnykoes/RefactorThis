using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        [JsonIgnore] public bool IsNew { get; set; }

        public Product()
        {
        }

        public Product(Guid id)
        {
            Id = id;
        }

        public Product(bool newProduct)
        {
            Id = Guid.NewGuid();
            IsNew = newProduct;
        }

        public Product GetProduct(string name)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from Products where Name like '%{name}%'";

            Product product = new Product();

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
            {
                product.IsNew = true;
            }
            else
            {
                product.IsNew = false;
                product.Id = Guid.Parse(rdr["Id"].ToString());
                product.Name = rdr["Name"].ToString();
                product.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
                product.Price = decimal.Parse(rdr["Price"].ToString());
                product.DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString());
            }
            return product;
        }

        public Product GetProduct(Guid id)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from Products where id = '{id}' collate nocase";

            Product product = new Product();

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
            {
                product.IsNew = true;
            }
            else
            {
                product.IsNew = false;
                product.Id = Guid.Parse(rdr["Id"].ToString());
                product.Name = rdr["Name"].ToString();
                product.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
                product.Price = decimal.Parse(rdr["Price"].ToString());
                product.DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString());
            }
            return product;
        }

        public void Save()
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = IsNew
                ? $"insert into Products (id, name, description, price, deliveryprice) values ('{Id}', '{Name}', '{Description}', {Price}, {DeliveryPrice})"
                : $"update Products set name = '{Name}', description = '{Description}', price = {Price}, deliveryprice = {DeliveryPrice} where id = '{Id}' collate nocase";

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete()
        {
            var options = ProductOptions.LoadProductOptions($"where productid = '{Id}' collate nocase");
            foreach (var option in options)
                option.Delete();

            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = $"delete from Products where id = '{Id}' collate nocase";
            cmd.ExecuteNonQuery();
        }
    }
}
