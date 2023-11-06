using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YGHMS.API.DTO.AuthenDTO;
using YGHMS.API.DTO.Common;
using YGHMS.API.Helpers;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Const;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.AuthenServices
{
	public interface IAuthenService
	{
		public ResponseDTO<LoginResponseDTO> Login(LoginRequestDTO request);
		public ResponseDTO<RegisterResponseDTO> Register(RegisterResquestDTO request);
		public ResponseDTO<LoginResponseDTO> InputNickname(int userId, string nickname);
	}
	public class AuthenService : IAuthenService
	{
		private readonly RentalManagementContext _context;
		private readonly ILogger<AuthenService> _logget;
		private readonly IMapper _mapper;
		public AuthenService(RentalManagementContext context, ILogger<AuthenService> logget, IMapper mapper)
		{
			_context = context;
			_logget = logget;
			_mapper = mapper;
		}
		private UserRole ConvertToUserRole(string role)
		{
			switch (role)
			{
				case "TENANT":
					return UserRole.TENANT;
				case "LANDLORD":
					return UserRole.LANDLORD;
				case "ADMIN":
					return UserRole.ADMIN;
				default: return UserRole.UNKNOW;
			}
		}
		public ResponseDTO<LoginResponseDTO> Login(LoginRequestDTO request)
		{
			string username = request.Username;
			string password = request.Password;
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return new ResponseDTO<LoginResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.COMMON_ERROR);
			var account = _context.Accounts.Include(e => e.Role).FirstOrDefault(e => e.UserName == username && e.Status!=(int)AccountStatus.Deleted);
			if (account == null)
			{
				account = _context.Accounts.Include(e => e.Role).FirstOrDefault(e => e.Emai == username);
				if (account == null)
					return new ResponseDTO<LoginResponseDTO>((int)RESPONSE_CODE.Unauthorized, ResponseMessage.LOGIN_FAIL);
			}
			var pass = account.Password;
			var passDecrypt = Base64CryptoHelper.Decrypt(pass, StaticVariable.PsSecretKey);
			if (passDecrypt != password)
			{
				return new ResponseDTO<LoginResponseDTO>((int)RESPONSE_CODE.Unauthorized, ResponseMessage.LOGIN_FAIL);
			}
			else
			{
				var response = new LoginResponseDTO();
				response.Status = (AccountStatus)account.Status;
				response.UserId = account.UserId;
				response.Email = account.Emai ?? "";
				response.UserName = account.UserName;
				response.Role = ConvertToUserRole(account.Role.Name ?? "");
				var UserHeader = new UserHeader
				{
					Email = account.Emai ?? "",
					UserId = account.UserId,
					UserName = account.UserName,
					Status = (AccountStatus)account.Status,
					Role = ConvertToUserRole(account.Role.Name ?? ""),
					Expires = DateTime.UtcNow.AddDays(1)
				};
				response.Token = AesCryptoHelper.EncryptString(JsonConvert.SerializeObject(UserHeader), StaticVariable.KeyHeaderToken.SecretKey);
				return new ResponseDTO<LoginResponseDTO>
				{
					Code = 200,
					Data = response,
					Message = ResponseMessage.LOGIN_SUCCESS,
				};
			}
		}
		private bool CheckFormatPassword(string pass)
		{
			return pass.Count() >= 6 && !pass.Contains(" ");
		}
		private string SuggestUserName(string gmail)
		{
			int pos = gmail.IndexOf("@");
			var username = gmail.Substring(0, pos - 1);
			if (_context.Accounts.FirstOrDefault(e => e.UserName == username) == null) return username;
			while (true)
			{
				Random rnd = new Random();
				var username1 = username + rnd.Next();
				if (_context.Accounts.FirstOrDefault(e => e.UserName == username) == null) return username;
			}
		}
		private bool CheckFormatEmail(string email)
		{
			Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
			return regex.IsMatch(email);
		}
		public ResponseDTO<RegisterResponseDTO> Register(RegisterResquestDTO request)
		{
			var email = request.Gmail;
			var password = request.Password;
			var rePassword = request.RePassword;
			if (password != rePassword)
			{
				return new ResponseDTO<RegisterResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.REPASSWORD_DONT_MATCH);
			}
			if (!CheckFormatPassword(password)) return new ResponseDTO<RegisterResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.WRONG_PASSWORD_FORMAT);
			if (!CheckFormatEmail(email)) return new ResponseDTO<RegisterResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.WRONG_EMAIL_FORMAT);
			if (_context.Accounts.FirstOrDefault(e => e.Emai == email) != null)
			{
				return new ResponseDTO<RegisterResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.EMAIL_ALREADY_EXIST);
			}
			var username = SuggestUserName(email);
			var user = new User
			{
				Email = request.Gmail,
				Status = (int)AccountStatus.InputNickname,
				RoleId = 1,
				UserName = username
			};
			_context.Users.Add(user);
			_context.SaveChanges();
			var account = new Account
			{
				UserId = user.Id,
				Emai = request.Gmail,
				Password = Base64CryptoHelper.Encrypt(password, StaticVariable.PsSecretKey),
				Status = (int)AccountStatus.InputNickname,
				RoleId = 1,
				UserName = username
			};
			_context.Accounts.Add(account);
			_context.SaveChanges();
			var UserHeader = new UserHeader
			{
				Email = account.Emai ?? "",
				UserId = account.UserId,
				Role = UserRole.TENANT,
				Status = AccountStatus.InputNickname,
				Expires = DateTime.UtcNow.AddDays(1)
			};
			var token = AesCryptoHelper.EncryptString(JsonConvert.SerializeObject(UserHeader), StaticVariable.KeyHeaderToken.SecretKey);
			return new ResponseDTO<RegisterResponseDTO>
			{
				Code = 200,
				Data = new RegisterResponseDTO
				{
					UsernameSuggest = username,
					Token = token
				}
			};
		}

		public ResponseDTO<LoginResponseDTO> InputNickname(int userId, string nickname)
		{
			if (_context.Accounts.FirstOrDefault(e => e.UserName == nickname && e.UserId != userId) != null)
			{
				return new ResponseDTO<LoginResponseDTO>(200, ResponseMessage.USERNAME_ALREADY_EXIST);
			}
			var account = _context.Accounts.FirstOrDefault(e => e.UserId == userId);
			if (account == null) return new ResponseDTO<LoginResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.COMMON_ERROR);
			if(account.Status != (int) AccountStatus.InputNickname) return new ResponseDTO<LoginResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.COMMON_ERROR);
            account.UserName = nickname;
			account.Status = (int)AccountStatus.Active;
			var user = _context.Users.Include(e => e.Role).FirstOrDefault(e => e.Id == userId);
			if (user == null) return new ResponseDTO<LoginResponseDTO>((int)RESPONSE_CODE.BadRequest, ResponseMessage.COMMON_ERROR);
			user.UserName = nickname;
			user.Status = (int) AccountStatus.Active;
			_context.SaveChanges();
			var response = new LoginResponseDTO();
			response.Status = (AccountStatus)account.Status;
			response.UserId = account.UserId;
			response.Email = account.Emai ?? "";
			response.UserName = account.UserName;
			response.Role = ConvertToUserRole(account.Role.Name ?? "");
			var UserHeader = new UserHeader
			{
				Email = account.Emai ?? "",
				UserId = account.UserId,
				UserName = account.UserName,
				Status = (AccountStatus)account.Status,
				Role = ConvertToUserRole(account.Role.Name ?? ""),
				Expires = DateTime.UtcNow.AddDays(1)
			};
			response.Token = AesCryptoHelper.EncryptString(JsonConvert.SerializeObject(UserHeader), StaticVariable.KeyHeaderToken.SecretKey);
			return new ResponseDTO<LoginResponseDTO>
			{
				Code = 200,
				Data = response,
				Message = ResponseMessage.LOGIN_SUCCESS,
			};
		}
	}
}
