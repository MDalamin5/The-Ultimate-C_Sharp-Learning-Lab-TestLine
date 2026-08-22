using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/categories")]
    public class CategoryController: ControllerBase
    {
        private static List<Category> categories = new List<Category>();

        public IActionResult GetCategories([FromQuery] string searchVale = "")
        {
            if (!string.IsNullOrEmpty(searchVale))
            {
                var srcCategory = categories.Where(c => c.Name.Contains(searchVale, StringComparison.OrdinalIgnoreCase)).ToList();
                return Ok(srcCategory);
            }
            return Ok(categories);
        }
    }
}