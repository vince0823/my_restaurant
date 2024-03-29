﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MyRestaurant.Services.Tests
{
    public class JwtTokenServiceFixture : IDisposable
    {
        private bool _disposed;
        public Mock<UserManager<User>> MockUserManager { get; private set; }
        public IOptions<JwtSettings> JwtSettings { get; private set; }
        public IList<Claim> Claims { get; private set; }

        public JwtTokenServiceFixture()
        {
            MockUserManager = GetUserManagerMock<User>();
            JwtSettings = Options.Create(new JwtSettings
            {
                AccessTokenSecret = "4ad84612-320e-415e-98e0-ca28fd5cca7b",
                RefreshTokenSecret = "4ad84612-320e-415e-98e0-ca28fd5cca7c",
                Issuer = "https://localhost:44301",
                Audience = "https://localhost:44301",
                AccessTokenExpirationInMinutes = 60,
                RefreshTokenExpirationInMinutes = 43200
            });

            Claims = new List<Claim> { };
        }
        static Mock<UserManager<TIDentityUser>> GetUserManagerMock<TIDentityUser>() where TIDentityUser : IdentityUser<Guid>
        {
            return new Mock<UserManager<TIDentityUser>>(
                    new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    Array.Empty<IUserValidator<TIDentityUser>>(),
                    Array.Empty<IPasswordValidator<TIDentityUser>>(),
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object);
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
                    MockUserManager = null;
                    JwtSettings = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
