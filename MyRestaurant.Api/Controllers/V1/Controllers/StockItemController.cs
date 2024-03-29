﻿using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class StockItemController : BaseApiController
    {
        private readonly IStockItemRepository _repository;
        public StockItemController(IStockItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStockItems()
        {
            var result = await _repository.GetStockItemsAsync();
            return Ok(result);
        }

        [HttpGet("type/{typeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStockItemsByType(int typeId, int? limit, int? offset)
        {
            var result = await _repository.GetStockItemsByTypeAsync(typeId, limit, offset);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStockItem(int id)
        {
            var result = await _repository.GetStockItemAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStockItem(CreateStockItemDto stockTypeDto, ApiVersion version)
        {
            var result = await _repository.CreateStockItemAsync(stockTypeDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStockItem(int id, EditStockItemDto stockTypeDto)
        {
            var result = await _repository.UpdateStockItemAsync(id, stockTypeDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStockItem(int id)
        {
            await _repository.DeleteStockItemAsync(id);
            return NoContent();
        }
    }
}
