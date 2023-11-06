using System.Data.Entity;
using AutoMapper;
using YGHMS.API.DTO.AccountDTOs;
using YGHMS.API.Helpers;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Const;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.AccountServices
{
	public interface IAccountService
	{
		public AccountDTO GetAccountInformations(int userId);
		public void ChangePassword(int userId, ChangePasswordRequestDTO changePasswordRequestDTO);

		public UpdateAccountDTO GetAccountInfoToUpdate(int userId);

		public void UpgradeAccountRole(int userId, UpdateAccountDTO request);
		public void LockAccount(int userId, string password);
	}

	public class AccountService : IAccountService
	{
		private readonly RentalManagementContext _context;
		private readonly ILogger<AccountService> _logger;
		private readonly IMapper _mapper;

		public AccountService(RentalManagementContext context, ILogger<AccountService> logger, IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}

		public AccountDTO GetAccountInformations(int userId)
		{
			var result = _context.Accounts
				 .Select(a => new AccountDTO
				 {
					 UserId = a.UserId,
					 Email = a.User.Email!,
					 Username = a.User.UserName!,
					 Balance = a.User.Balance,
					 AccountStatus = (AccountStatus)a.Status,
					 Role = (UserRole)a.RoleId
				 }).FirstOrDefault(a => a.UserId == userId);
			if (result == null) throw new(ResponseMessage.USER_NOT_FOUND);
			return result;
		}
		private bool CheckFormatPassword(string pass)
		{
			return pass.Count() >= 6 && !pass.Contains(" ");
		}
		public void ChangePassword(int userId, ChangePasswordRequestDTO changePasswordRequestDTO)
		{
			string currentPassword = changePasswordRequestDTO.CurrentPassword;
			string newPassword = changePasswordRequestDTO.NewPassword;
			string rePassword = changePasswordRequestDTO.RePassword;
			if (rePassword != newPassword)
			{
				throw new(ResponseMessage.REPASSWORD_DONT_MATCH);
			}
			if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(rePassword))
				throw new(ResponseMessage.COMMON_ERROR);
			if (newPassword == currentPassword) { throw new(ResponseMessage.PASSWORD_MUST_DIFFERENT); }
			var selectedAccount = _context.Accounts.FirstOrDefault(a => a.UserId == userId);
			if (selectedAccount == null)
				throw new(ResponseMessage.USER_NOT_FOUND);
			string pass = selectedAccount.Password;
			string passDecrypted = Base64CryptoHelper.Decrypt(pass, StaticVariable.PsSecretKey);
			if (currentPassword != passDecrypted)
				throw new(ResponseMessage.WRONG_PASSWORD);
			else
			{
				if (!CheckFormatPassword(newPassword)) throw new(ResponseMessage.WRONG_PASSWORD_FORMAT);
				selectedAccount.Password = Base64CryptoHelper.Encrypt(newPassword, StaticVariable.PsSecretKey);
				_context.SaveChanges();
			}
		}


		public UpdateAccountDTO GetAccountInfoToUpdate(int userId)
		{
			var result = _context.Users.Select(u => new UpdateAccountDTO
			{
				UserId = u.Id,
				Fullname = u.FullName ?? "",
				Email = u.Email!,
				PhoneNumber = u.PhoneNumber ?? "",
				Address = u.Address ?? "",
				Introduce = u.UserHighlights.FirstOrDefault(u => u.Id == userId).Description ?? "",
			}).FirstOrDefault(u => u.UserId == userId);
			if (result is null) throw new(ResponseMessage.USER_NOT_FOUND);
			else
				return result;
		}

		public void UpgradeAccountRole(int userId, UpdateAccountDTO request)
		{
			var selectedUser = _context.Users.Include(u => u.UserHighlights).FirstOrDefault(u => u.Id == userId);
			if (selectedUser is null) { throw new(ResponseMessage.USER_NOT_FOUND); }
			selectedUser.Account = _context.Accounts.FirstOrDefault(a => a.UserId == userId);//vi mot li do nao do ma khong the lay duoc account bang Include
			if (selectedUser.Account is null) { throw new(ResponseMessage.USER_NOT_FOUND); }
			if (selectedUser.RoleId != (int)UserRole.TENANT) throw new(ResponseMessage.COMMON_ERROR);
			if (request.Fullname.Length > 150
				|| request.Address.Length > 200
				|| request.PhoneNumber?.Length > 20)
			{ throw new(ResponseMessage.LIMITED_LENGTH); }
			else if (string.IsNullOrEmpty(request.Fullname)
				|| string.IsNullOrEmpty(request.PhoneNumber)
				|| string.IsNullOrEmpty(request.Address)
				) throw new(ResponseMessage.COMMON_ERROR);
			else
			{
				selectedUser.FullName = request.Fullname;
				selectedUser.Address = request.Address;
			}
			var userIntroduce = selectedUser.UserHighlights.FirstOrDefault(x => x.UserId == userId);
			if (userIntroduce != null)
			{
				userIntroduce.Description = request.Introduce;
			}
			else
			{
				userIntroduce = new UserHighlight { Description = request.Introduce, UserId = userId, IsDelete = 0 };
				selectedUser.UserHighlights.Add(userIntroduce);
			}
			if (!request.PhoneNumber.All(char.IsDigit))
			{
				throw new(ResponseMessage.INVALID_PHONENUMBER);
			}
			else
				selectedUser.PhoneNumber = request.PhoneNumber;
			selectedUser.RoleId = (int)UserRole.LANDLORD;
			selectedUser.Account.RoleId = (int)UserRole.LANDLORD;
			_context.SaveChanges();
		}
		public void LockAccount(int userId, string password)
		{
			var selectedUser = _context.Accounts.FirstOrDefault(u => u.UserId == userId);
			if (selectedUser == null) { throw new(ResponseMessage.USER_NOT_FOUND); }
			else
			{
				string pass = selectedUser.Password;
				string passDecrypted = Base64CryptoHelper.Decrypt(pass, StaticVariable.PsSecretKey);
				if (password != passDecrypted)
				{
					throw new(ResponseMessage.WRONG_PASSWORD);
				}
				else
				{
					selectedUser.Status = (int)AccountStatus.Deleted;
					_context.SaveChanges();
				}
			}

		}



	}
}
