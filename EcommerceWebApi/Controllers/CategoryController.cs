using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{   
    [ApiController]
    [Route("/api/v1/categories/")]
    public class CategoryController: ControllerBase
    {
        private static List<Category> categories = new List<Category>();
        // TO read teh category => api/v1/categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

                return Ok(searchCategories);
            }
            else
                return Ok(categories);
        }
    }
}