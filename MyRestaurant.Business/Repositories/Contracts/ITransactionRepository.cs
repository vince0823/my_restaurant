﻿using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<GetTransactionDto>> GetTransactionsAsync();
        Task<GetTransactionDto> GetTransactionAsync(int id);
        Task<GetTransactionDto> CreateTransactionAsync(CreateTransactionDto transactionDto);
        Task<GetTransactionDto> UpdateTransactionAsync(int id, EditTransactionDto transactionDto);
        Task DeleteTransactionAsync(int id);
    }
}
