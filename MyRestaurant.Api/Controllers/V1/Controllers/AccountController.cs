﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Api.PolicyHandlers;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1.Controllers
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController<AccountController>
    {
        private readonly IAccountRepository _repository;
        public AccountController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("RegisterAdminUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(ApplicationClaimPolicy.SuperAdminOnly)]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminDto registerDto)
        {
            var result = await _repository.RegisterAdminAsync(registerDto);
            return Ok(result);
        }

        [HttpPost("RegisterNormalUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(ApplicationClaimPolicy.AdminOnly)]
        public async Task<IActionResult> RegisterNormalUser(RegisterNormalDto registerDto)
        {
            var result = await _repository.RegisterNormalAsync(registerDto);
            return Ok(result);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _repository.LoginAsync(loginDto, IpAddress());
            return Ok(result);
        }

        [HttpPost("Refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh(RefreshDto refreshDto)
        {
            var result = await _repository.RefreshToken(refreshDto, IpAddress());
            return Ok(result);
        }

        [HttpPost("Revoke")]
        [Authorize(ApplicationClaimPolicy.AdminOnly)]
        public async Task<IActionResult> Revoke(RevokeDto revokeDto)
        {
            await _repository.RevokeToken(revokeDto, IpAddress());
            
            return NoContent();
        }

        [HttpGet("CurrentUser")]
        public IActionResult GetCurrentUser()
        {
            var currentUser =  _repository.GetCurrentUser();
            return Ok(currentUser);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
