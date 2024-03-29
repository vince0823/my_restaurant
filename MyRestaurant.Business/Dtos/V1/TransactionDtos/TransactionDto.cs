﻿namespace MyRestaurant.Business.Dtos.V1
{
    public class TransactionDto
    {
        public int TransactionTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string Cashflow { get; set; } = default!;
    }
}
