﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1.ServiceTypeDtos;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1.Controllers
{
    [ApiVersion("1.0")]
    public class ServiceTypeController : BaseController
    {

        private readonly IServiceTypeRepository _serviceTypeRepository;
        public ServiceTypeController(IServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceTypes()
        {
            var result = await _serviceTypeRepository.GetServicesTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServiceType(int id)
        {
            var result = await _serviceTypeRepository.GetServiceTypeAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateServiceType(CreateServiceTypeDto serviceTypeDto, ApiVersion version)
        {
            var result = await _serviceTypeRepository.CreateServiceTypeAsync(serviceTypeDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateServiceType(int id,  EditServiceTypeDto serviceTypeDto) 
        {
            await _serviceTypeRepository.UpdateServiceTypeAsync(id, serviceTypeDto);
            return NoContent();
        }

        /// <summary>
        /// Delete a specific ServiceType
        /// </summary>
        /// <param name="id">ServiceType id</param>
        /// <returns></returns>
        /// <response code="204">Returns no content</response>
        /// <response code="404">If the specified ServiceType id is not available</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteServiceType(int id)
        {
            await _serviceTypeRepository.DeleteServiceTypeAsync(id);

            return NoContent();
        }
    }
}
