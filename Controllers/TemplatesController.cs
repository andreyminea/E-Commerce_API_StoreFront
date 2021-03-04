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
    [ApiController]
    [Route("templates/")]
    public class TemplatesController : ControllerBase
    {
        private readonly DatabaseContext context;

        public TemplatesController(DatabaseContext databaseContext)
        {
            context = databaseContext;
        }

        [HttpGet("categories")]
        public async Task<List<Category>> GetCategories()
        {
            return await context.GetCategories();
        }

        [HttpGet("subcategories/{categoryName}")]
        public async Task<List<Subcategory>> GetSubcategories(string categoryName)
        {
            return await context.GetSubcategories(categoryName);
        }
    }
}
