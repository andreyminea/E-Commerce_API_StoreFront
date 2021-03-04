using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext context;

        public ProductsController(DatabaseContext databaseContext)
        {
            context = databaseContext;
        }

        [HttpGet("{category}/{subcategory}")]
        public async Task<List<Product>> GetProducts(string category, string subcategory)
        {
            return await context.GetProducts(category, subcategory);
        }

        [HttpGet("{name}")]
        public async Task<List<Product>> GetProductsByName(string name)
        {
            return await context.GetProductsByName(name);
        }
    }
}
