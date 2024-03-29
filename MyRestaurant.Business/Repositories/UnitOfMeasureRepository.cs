﻿using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Net;

namespace MyRestaurant.Business.Repositories
{
    public class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfMeasureService _unitOfMeasure;
        public UnitOfMeasureRepository(IMapper mapper, IUnitOfMeasureService unitOfMeasure)
        {
            _mapper = mapper;
            _unitOfMeasure = unitOfMeasure;
        }

        private async Task CheckUnitOfMeasureAsync(int id, string code)
        {
            var dbUnitOfMeasure = await _unitOfMeasure.GetUnitOfMeasureAsync(d => d.Code == code && d.Id != id);
            if (dbUnitOfMeasure != null)
                throw new RestException(HttpStatusCode.Conflict, $"Unit of measure \"{code}\" is already available.");
        }

        public async Task<GetUnitOfMeasureDto> CreateUnitOfMeasureAsync(CreateUnitOfMeasureDto unitOfMeasureDto)
        {
            await CheckUnitOfMeasureAsync(0, unitOfMeasureDto.Code);

            var unitOfMeasure = _mapper.Map<UnitOfMeasure>(unitOfMeasureDto);
            unitOfMeasure = await _unitOfMeasure.AddUnitOfMeasureAsync(unitOfMeasure);

            return _mapper.Map<GetUnitOfMeasureDto>(unitOfMeasure);
        }

        private async Task<UnitOfMeasure> GetUnitOfMeasureId(int id)
        {
            var unitOfMeasure = await _unitOfMeasure.GetUnitOfMeasureAsync(d => d.Id == id);

            if (unitOfMeasure == null)
                throw new RestException(HttpStatusCode.NotFound, "Unit of measure not found.");

            return unitOfMeasure;
        }

        public async Task DeleteUnitOfMeasureAsync(int id)
        {
            var unitOfMeasure = await GetUnitOfMeasureId(id);

            await _unitOfMeasure.DeleteUnitOfMeasureAsync(unitOfMeasure);
        }

        public async Task<GetUnitOfMeasureDto> GetUnitOfMeasureAsync(int id)
        {
            var unitOfMeasure = await GetUnitOfMeasureId(id);

            return _mapper.Map<GetUnitOfMeasureDto>(unitOfMeasure);
        }

        public async Task<IEnumerable<GetUnitOfMeasureDto>> GetUnitOfMeasuresAsync()
        {
            var unitOfMeasures = await _unitOfMeasure.GetUnitOfMeasuresAsync();

            return _mapper.Map<IEnumerable<GetUnitOfMeasureDto>>(unitOfMeasures.OrderBy(d => d.Code));
        }

        public async Task<GetUnitOfMeasureDto> UpdateUnitOfMeasureAsync(int id, EditUnitOfMeasureDto unitOfMeasureDto)
        {
            var unitOfMeasure = await GetUnitOfMeasureId(id);

            await CheckUnitOfMeasureAsync(id, unitOfMeasureDto.Code);

            unitOfMeasure = _mapper.Map(unitOfMeasureDto, unitOfMeasure);

            await _unitOfMeasure.UpdateUnitOfMeasureAsync(unitOfMeasure);

            return _mapper.Map<GetUnitOfMeasureDto>(unitOfMeasure);
        }
    }
}
