﻿using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IGoodsReceivedNoteFreeItemRepository
    {
        Task<IEnumerable<GetGoodsReceivedNoteFreeItemDto>> GetGoodsReceivedNoteFreeItemsAsync(long goodsReceivedNoteId);
        Task<GetGoodsReceivedNoteFreeItemDto> CreateGoodsReceivedNoteFreeItemAsync(CreateGoodsReceivedNoteFreeItemDto goodsReceivedNoteFreeItemDto);
        Task<GetGoodsReceivedNoteFreeItemDto> GetGoodsReceivedNoteFreeItemAsync(long id);
        Task<GetGoodsReceivedNoteFreeItemDto> UpdateGoodsReceivedNoteFreeItemAsync(long id, EditGoodsReceivedNoteFreeItemDto goodsReceivedNoteFreeItemDto);
        Task DeleteGoodsReceivedNoteFreeItemAsync(long id);
    }
}
