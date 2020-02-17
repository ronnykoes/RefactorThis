using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Models;

namespace RefactorThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Product
        [HttpGet]
        public List<Product> LoadProducts()
        {
            return Products.LoadProducts(null);
        }

        [HttpGet("name={name}")]
        public Product Get(string name)
        {
            Product product = new Product().GetProduct(name);
            return product;
        }

        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            Product product = new Product().GetProduct(id);
            return product;
        }

        [HttpPost]
        public void Post(Product product)
        {
            product.Save();
        }

        [HttpPut("update/{id}")]
        public void Update(Guid id, Product product)
        {
            var orig = new Product(id)
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };

            if (!orig.IsNew)
                orig.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var product = new Product(id);
            product.Delete();
        }

        // Product Option
        [HttpGet("{productId}/options")]
        public List<ProductOption> GetOptions(Guid productId)
        {
            return ProductOptions.LoadProductOptions($"where productid = '{productId}' collate nocase");
        }

        [HttpGet("{productId}/options/{id}")]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = new ProductOption(id);
            if (option.IsNew)
                throw new Exception();

            return option;
        }

        [HttpPost("{productId}/options")]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            option.Save();
        }

        [HttpPut("{productId}/options/{id}")]
        public void UpdateOption(Guid id, ProductOption option)
        {
            var orig = new ProductOption(id)
            {
                Name = option.Name,
                Description = option.Description
            };

            if (!orig.IsNew)
                orig.Save();
        }

        [HttpDelete("{productId}/options/{id}")]
        public void DeleteOption(Guid id)
        {
            var opt = new ProductOption(id);
            opt.Delete();
        }
    }
}