using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YGHMS.API.DTO.ResponseModels.UserDTOs;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Const;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;
using Uri = YGHMS.API.Common.Uri;

namespace YGHMS.API.Services.AccountServices
{
    public interface IProfileService
    {
        public EditableProfileUserDto GetEditableProfile(int userId);
        public void UpdateProfile(int userId, EditableProfileUserDto updatedProfile);
        public DisplayOnlyProfileUserDto GetDisplayOnlyProfileById(int userId);
    }

    public class ProfileService : IProfileService
    {
        private readonly RentalManagementContext _context;
        private readonly IMapper _mapper;

        public ProfileService(RentalManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public EditableProfileUserDto GetEditableProfile(int userId)
        {
            var result = _context.Users.Select(user => new EditableProfileUserDto
            {
                AvatarUrl = Uri.BuildUrlWithHost(user.Avatar!.Url),
                BackgroundUrl = Uri.BuildUrlWithHost(user.CoverImageNavigation!.Url),
                UserName = user.UserName!,
                FullName = user.FullName ?? "",
                Address = user.Address,
                Contact = new Contact
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                },
                AccountStatus = (AccountStatus)user.Account!.Status,
                Introduce = user.UserHighlights.FirstOrDefault(u => u.UserId == user.Id).Description,
                UserId = user.Id
            }).FirstOrDefault(x => x.UserId == userId);
            if (result == null) { throw new(ResponseMessage.COMMON_ERROR); }
            else
                return result;
        }
        public void UpdateProfile(int userId, EditableProfileUserDto updatedProfile)
        {
            var selectedUser = _context.Users.Include(u => u.UserHighlights).Include(u=>u.Account).FirstOrDefault(u => u.Id == userId);
            if (_context.Users.FirstOrDefault(u => u.Id!=userId && u.UserName == updatedProfile.UserName) != null)
            {
                throw new(ResponseMessage.USERNAME_ALREADY_EXIST);
            }

            if (selectedUser == null) { throw new(ResponseMessage.USER_NOT_FOUND); }
            if (updatedProfile.UserName.Length > 51
                || updatedProfile.FullName?.Length > 150
                || updatedProfile.Address?.Length > 200
                || updatedProfile.Contact?.PhoneNumber?.Length > 20)
            { throw new(ResponseMessage.LIMITED_LENGTH); }
            else if (updatedProfile.UserName.Length < 5) throw new(ResponseMessage.USERNAME_MUST_HAS_5_OR_MORE_CHARACTERS);
            else
            {
                selectedUser.UserName = updatedProfile.UserName;
                selectedUser.Account.UserName = updatedProfile.UserName;
                selectedUser.FullName = updatedProfile.FullName;
                selectedUser.Address = updatedProfile.Address ?? "";
            }
            var userIntroduce = selectedUser.UserHighlights.FirstOrDefault(x => x.UserId == userId);
            if (userIntroduce != null)
            {
                userIntroduce.Description = updatedProfile.Introduce;
            }
            else
            {
                userIntroduce = new UserHighlight { Description = updatedProfile.Introduce,UserId=userId,IsDelete=0 };
                selectedUser.UserHighlights.Add(userIntroduce);
            }
            if (updatedProfile.Contact?.PhoneNumber != null && !updatedProfile.Contact.PhoneNumber.All(char.IsDigit))
            {
                throw new(ResponseMessage.INVALID_PHONENUMBER);
            }
            else
                selectedUser.PhoneNumber = updatedProfile.Contact?.PhoneNumber;
            _context.SaveChanges();
        }
        public DisplayOnlyProfileUserDto GetDisplayOnlyProfileById(int userId)
        {
            var result = _context.Users
                .Select(u => new DisplayOnlyProfileUserDto
                {
                    UserId = u.Id,
                    FullName = u.FullName,
                    AvatarUrl = Uri.BuildUrlWithHost(u.Avatar!.Url),
                    UserName = u.UserName!,
                    BackgroundUrl = Uri.BuildUrlWithHost(u.CoverImageNavigation!.Url),
                    AccountStatus = (AccountStatus)u.Account!.Status
                })
                .FirstOrDefault(u => u.UserId == userId && u.AccountStatus != AccountStatus.Deleted);
            if (result == null) { throw new(ResponseMessage.USER_NOT_FOUND); }
            else return result;
        }
    }
}
