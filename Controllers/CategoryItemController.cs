using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minimal.DataAccess;
using Minimal.Models;

namespace Minimal.Controllers
{
    [Route("api/categoryItems")]
    [ApiController]
    public class CategoryItemController : FireBaseController
    {
        readonly CategoryItemRepository _repo;
        
        public CategoryItemController(CategoryItemRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("createCategoryItem")]
        public IActionResult CreateCategoryItem(CategoryItem newCategoryItem)
        {
            _repo.CreateCategoryItem(newCategoryItem);

            return Created($"/api/categoryItems/{newCategoryItem.CategoryId}", newCategoryItem);
        }

        [HttpGet("getAllCategoryItems")]
        public IActionResult GetAllCategoryItems()
        {
            return Ok(_repo.GetAllCategoryItems());
        }

        [HttpGet("getCategoryItemById/{categoryItemId}")]
        public IActionResult GetCategoryItemById(Guid categoryItemId)
        {
            return Ok(_repo.GetCategoryItemById(categoryItemId));
        }

        [HttpGet("getCategoryItemByCategoryId/{categoryId}")]
        public IActionResult GetCategoryItemByCategoryId(Guid categoryId)
        {
            return Ok(_repo.GetCategoryItemByCategoryId(categoryId));
        }

        [HttpGet("getCategoryItemByItemId/{itemId}")]
        public IActionResult GetCategoryItemByItemId(Guid itemId)
        {
            return Ok(_repo.GetCategoryItemByItemId(itemId));
        }

        [HttpPut("updateCategoryItem/{categoryItemId}")]
        public IActionResult UpdateCategoryItem(Guid categoryItemId, CategoryItem categoryItem)
        {
            var categoryItemToUpdate = _repo.GetCategoryItemById(categoryItemId);

            if (categoryItemToUpdate == null)
            {
                return NotFound($"Could not find category item with the id {categoryItem} for updating");
            }

            var updatedCategoryItem = _repo.UpdateCategoryItem(categoryItemId, categoryItem);

            return Ok(updatedCategoryItem);
        }

        [HttpDelete("deleteCategoryItem/{categoryItemId}")]
        public IActionResult DeleteCategoryItem(Guid categoryItemId)
        {
            _repo.DeleteCategoryItem(categoryItemId);

            return Ok();
        }
    }
}
