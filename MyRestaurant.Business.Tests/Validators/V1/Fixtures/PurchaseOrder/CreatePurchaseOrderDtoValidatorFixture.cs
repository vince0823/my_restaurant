﻿using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreatePurchaseOrderDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreatePurchaseOrderDto Model { get; set; }
        public CreatePurchaseOrderDtoValidator Validator { get; private set; }

        public CreatePurchaseOrderDtoValidatorFixture()
        {
            Validator = new CreatePurchaseOrderDtoValidator();

            Model = new CreatePurchaseOrderDto
            {
                SupplierId = 1,
                Description = "Purchase order description"
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
