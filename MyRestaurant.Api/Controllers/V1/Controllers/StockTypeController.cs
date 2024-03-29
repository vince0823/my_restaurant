﻿using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class StockTypeController : BaseApiController
    {
        private readonly IStockTypeRepository _repository;
        public StockTypeController(IStockTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStockTypes()
        {
            var result = await _repository.GetStockTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStockType(int id)
        {
            var result = await _repository.GetStockTypeAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStockType(CreateStockTypeDto stockTypeDto, ApiVersion version)
        {
            var result = await _repository.CreateStockTypeAsync(stockTypeDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStockType(int id, EditStockTypeDto stockTypeDto)
        {
            var result = await _repository.UpdateStockTypeAsync(id, stockTypeDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStockType(int id)
        {
            await _repository.DeleteStockTypeAsync(id);
            return NoContent();
        }
    }
}
