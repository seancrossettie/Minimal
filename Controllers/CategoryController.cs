using System;
using Microsoft.AspNetCore.Mvc;
using Minimal.DataAccess;
using Minimal.Models;

namespace Minimal.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : FireBaseController
    {
        readonly CategoryRepository _repo;

        public CategoryController(CategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("createCategory")]
        public IActionResult CreateNewCategory(Category newCategory)
        {
            _repo.CreateNewCategory(newCategory);

            return Created($"/api/categories/{newCategory.CategoryId}", newCategory);
        }

        [HttpGet("getAllCategories")]
        public IActionResult GetAllCategories()
        {
            return Ok(_repo.GetAllCategories());
        }

        [HttpGet("getCategoryById/{categoryId}")]
        public IActionResult GetCategoryById(Guid categoryId)
        {
            return Ok(_repo.GetCategoryById(categoryId));
        }

        [HttpGet("getCategoryByName/{categoryName}")]
        public IActionResult GetCategoryByName(String categoryName)
        {
            return Ok(_repo.GetCategoryByName(categoryName));
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

        [HttpDelete("deleteCategory/{categoryId}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            _repo.DeleteCategory(categoryId);

            return Ok();
        }
    }
}
