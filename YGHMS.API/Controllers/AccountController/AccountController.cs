using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YGHMS.API.DTO.AccountDTOs;
using YGHMS.API.DTO.ResponseModels;
using YGHMS.API.Infra.Const;
using YGHMS.API.Services.AccountServices;

namespace YGHMS.API.Controllers.AccountController
{
	[Route("api")]
	[ApiController]
	public class AccountController : BaseApiController
	{
		private readonly IAccountService _accountService;
		private readonly ILogger<AccountController> _logger;
		public AccountController(IAccountService accountService, ILogger<AccountController> logger)
		{
			_accountService = accountService;
			_logger = logger;
		}

		[HttpGet("account")]
		public IActionResult GetAccountInformations()
		{
			try
			{
				if (UserHeader is null) return Unauthorized();
				return Ok(_accountService.GetAccountInformations(UserHeader.UserId));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("change-password")]
		public IActionResult ChangePassword(ChangePasswordRequestDTO request)
		{
			try
			{
				if (UserHeader is null) return Unauthorized();
				_accountService.ChangePassword(UserHeader.UserId, request);
				return Ok(new UpdateResponseBase { Status = true, Message = ResponseMessage.PASSWORD_CHANGED });
			}
			catch (Exception ex)
			{
				return BadRequest(new UpdateResponseBase { Status = false, Message = ex.Message });
			}
		}

		[HttpGet("update-info")]
		public IActionResult GetAccountInformationsToUpdate()
		{
			try
			{
				if (UserHeader is null) return Unauthorized();
				else return Ok(_accountService.GetAccountInfoToUpdate(UserHeader.UserId));
			}
			catch (Exception ex)
			{
				return BadRequest(new UpdateResponseBase { Status = false, Message = ex.Message });
			}
		}

		[HttpPost("upgrade-account")]
		public IActionResult UpgradeAccount(UpdateAccountDTO updateRequest)
		{
			try
			{

				if (UserHeader is null) return Unauthorized();
				else
				{
					_accountService.UpgradeAccountRole(UserHeader.UserId, updateRequest);
					return Ok(new UpdateResponseBase { Status = true, Message = ResponseMessage.UPDATE_SUCCESSFULLY });
				}
			}
			catch (Exception e)
			{
				return BadRequest(new UpdateResponseBase { Status = false, Message = ResponseMessage.COMMON_ERROR });
			}
		}
		[HttpPost("delete-account")]
		public IActionResult DeteleAccount(string password)
		{
			try
			{
				if (UserHeader is null) return Unauthorized();
				else
				{
					_accountService.LockAccount(UserHeader.UserId, password);
					return Ok(new UpdateResponseBase { Status = true, Message = ResponseMessage.ACCOUNT_DELETED });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new UpdateResponseBase { Status = false, Message = ex.Message });
			}
		}

	}
}
