﻿using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IStockTypeRepository
    {
        Task<IEnumerable<GetStockTypeDto>> GetStockTypesAsync();
        Task<GetStockTypeDto> GetStockTypeAsync(int id);
        Task<GetStockTypeDto> CreateStockTypeAsync(CreateStockTypeDto stockTypeDto);
        Task UpdateStockTypeAsync(int id, EditStockTypeDto stockTypeDto);
        Task DeleteStockTypeAsync(int id);
    }
}
