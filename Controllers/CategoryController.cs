using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minimal.DataAccess;
using Minimal.Models;

namespace Minimal.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryRepository _repo;

        public CategoryController(CategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_repo.GetAllCategories());
        }

        [HttpGet("getCategoryByName/{categoryName}")]
        public IActionResult GetCategoryByName(String categoryName)
        {
            return Ok(_repo.GetCategoryByName(categoryName));
        }

        [HttpGet("getCategoryById/{categoryId}")]
        public IActionResult GetCategoryById(Guid categoryId)
        {
            return Ok(_repo.GetCategoryById(categoryId));
        }

        [HttpPost]
        public IActionResult CreateNewCategory(Category newCategory)
        {
            _repo.CreateNewCategory(newCategory);

            return Created($"/api/categories/{newCategory.CategoryId}", newCategory);
        }

        [HttpDelete("deleteCategory/{categoryId}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            _repo.DeleteCategory(categoryId);

            return Ok();
        }

        [HttpPut("updateCategory/{categoryId}")]
        public IActionResult UpdateCategory(Guid categoryId, Category category)
        {
            var categoryToUpdate = _repo.GetCategoryById(categoryId);

            if (categoryToUpdate == null)
            {
                return NotFound($"Could not find category with the id {categoryId} for updating");
            }

            var updatedCategory = _repo.UpdateCategory(categoryId, category);

            return Ok(updatedCategory);
        }
    }
}
