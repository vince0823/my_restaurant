﻿using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateTransactionDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreateTransactionDto Model { get; set; }
        public CreateTransactionDtoValidator Validator { get; private set; }

        public CreateTransactionDtoValidatorFixture()
        {
            Validator = new CreateTransactionDtoValidator();

            Model = new CreateTransactionDto
            {
                TransactionTypeId = 2,
                PaymentTypeId = 1,
                Date = DateTime.Now.AddDays(-2),
                Description = "Interest from Deposit",
                Amount = 456.5m,
                Cashflow = "Income"
            };
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    Model = null;
                    Validator = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
