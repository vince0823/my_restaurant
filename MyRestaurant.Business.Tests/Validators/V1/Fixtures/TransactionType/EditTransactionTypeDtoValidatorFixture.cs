﻿using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditTransactionTypeDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public EditTransactionTypeDto Model { get; set; }
        public EditTransactionTypeDtoValidator Validator { get; private set; }
        public EditTransactionTypeDtoValidatorFixture()
        {
            Validator = new EditTransactionTypeDtoValidator();

            Model = new EditTransactionTypeDto
            {
                Type = "Rent"
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
                    Model = null;
                    Validator = null;
                }

                _disposed = true;
            }
        }
    }
}
