﻿using System;
using Microsoft.AspNetCore.Mvc;
using Minimal.DataAccess;
using Minimal.Models;

namespace Minimal.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : FireBaseController
    {
        readonly ItemRepository _repo;
        public ItemController(ItemRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("createItem")]
        public IActionResult CreateItem(Item newItem)
        {
            _repo.CreateItem(newItem);

            return Created($"/api/items/{newItem.ItemId}", newItem);
        }

        [HttpGet("getAllItems")]
        public IActionResult GetAllItems()
        {
            return Ok(_repo.GetAllItems());
        }

        [HttpGet("getItemById/{itemId}")]
        public IActionResult GetItemById(Guid itemId)
        {
            return Ok(_repo.GetItemById(itemId));
        }

        [HttpGet("getItemByName/{itemName}")]
        public IActionResult GetItemByName(String itemName)
        {
            return Ok(_repo.GetItemByName(itemName));
        }

        [HttpGet("getDuplicateItems")]
        public IActionResult GetDuplicateItems()
        {
            return Ok(_repo.GetDuplicateItems());
        }

        [HttpGet("getFiveMostUselessItems")]
        public IActionResult GetFiveMostUslessItems()
        {
            return Ok(_repo.GetFiveMostUselessItems());
        }

        [HttpGet("getPageItems/page={pageNumber}")]
        public IActionResult GetPageItems(int pageNumber)
        {
            return Ok(_repo.GetPageItems(pageNumber));
        }

        [HttpPut("updateItem/{itemId}")]
        public IActionResult UpdateItem(Guid itemId, Item item)
        {
            var itemToUpdate = _repo.GetItemById(itemId);

            if (itemToUpdate == null)
            {
                return NotFound($"Could not find item with the id {itemId} for updating");
            }

            var updatedItem = _repo.UpdateItem(itemId, item);

            return Ok(updatedItem);
        }

        [HttpPut("removeItem/{itemId}")]
        public IActionResult RemoveItem(Guid itemId, Item item)
        {
            var itemToRemove = _repo.GetItemById(itemId);

            if (itemToRemove == null)
            {
                return NotFound($"Could not find item with the id {itemId} for removal");
            }

            var removedItem = _repo.RemoveItem(itemId, item);

            return Ok(removedItem);
        }

        [HttpDelete("deleteItem/{itemId}")]
        public IActionResult DeleteItem(Guid itemId)
        {
            _repo.DeleteItem(itemId);

            return Ok();
        }
    }
}
