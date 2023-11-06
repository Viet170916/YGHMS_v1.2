using YGHMS.API.DTO.Common;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Const;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Services.FollowServices
{
	public interface IFollowService
	{
		public ResponseDTO<bool> FollowPost(int userId, int postId);
		public ResponseDTO<bool> UnFollowPost(int userId, int postId);
	}
	public class FollowService : IFollowService
	{
		private readonly RentalManagementContext _context;
		private readonly ILogger<FollowService> _logger;
		public FollowService(RentalManagementContext context, ILogger<FollowService> logger)
		{
			_context = context;
			_logger = logger;
		}

		public ResponseDTO<bool> FollowPost(int userId, int postId)
		{

			var post = _context.Accommodations.FirstOrDefault(e => e.Id == postId && !e.IsDeleted);
			if (post == null)
			{
				return new ResponseDTO<bool>
				{
					Code = (int)RESPONSE_CODE.BadRequest,
					Message = ResponseMessage.CANNOT_FIND_POST,
					Data = false
				};
			}
			var follow = _context.FollowUserAccoms.FirstOrDefault(e => e.AccomodationId == postId && e.UserId == userId);
			if (follow != null) follow.IsDeleted = false;
			else
			{
				_context.FollowUserAccoms.Add(new FollowUserAccom
				{
					UserId = userId,
					AccomodationId = postId,
				});
				_context.SaveChanges();
			}
			return new ResponseDTO<bool>(true);
		}

		public ResponseDTO<bool> UnFollowPost(int userId, int postId)
		{
			var follow = _context.FollowUserAccoms.FirstOrDefault(e => e.AccomodationId == postId && e.UserId == userId && !e.IsDeleted);
            if (follow == null)
            {
                return new ResponseDTO<bool>
                {
                    Code = (int)RESPONSE_CODE.BadRequest,
                    Data = false
                };
            }
            follow.IsDeleted = true;
			_context.SaveChanges();
			return new ResponseDTO<bool>(true);
		}
	}
}
