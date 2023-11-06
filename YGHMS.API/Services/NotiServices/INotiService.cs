using AutoMapper;
using YGHMS.API.DTO.Common;
using YGHMS.API.DTO.FollowDTOs;
using YGHMS.API.DTO.NotificationDTOs;
using YGHMS.API.DTO.ReservationDTOs;
using YGHMS.API.DTO.ReviewDTOs;
using YGHMS.API.Infra;
using YGHMS.API.Infra.Enums;

namespace YGHMS.API.Services.NotiServices
{
	public interface INotiService
	{
		public PagedResultDTO<NotificationDTO> GetNotification(int userId, int pageSize, int pageIndex);
		public ResponseDTO<bool> ReadNotification(int userId, int notificationId);
		public ResponseDTO<bool> ReadAll(int userId);
		public ResponseDTO<bool> DeleteNotification(int userId, int notificationId);
	}
	public class NotiService : INotiService
	{
		private readonly RentalManagementContext _context;
		private readonly ILogger<NotiService> _logger;
		private readonly IMapper _mapper;
		public NotiService(RentalManagementContext context, ILogger<NotiService> logger, IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}

		public ResponseDTO<bool> DeleteNotification(int userId, int notificationId)
		{
			var notification = _context.Notifications.FirstOrDefault(e => e.UserId == userId && e.Id == notificationId && !e.IsDeleted);
			if (notification == null) return new ResponseDTO<bool>(false);
			notification.IsDeleted = true;
			_context.SaveChanges();

			if (notification.Type == (int)NotiType.Reservation_Tenant || notification.Type == (int)NotiType.Reservation_LandLord)
			{
				var noti = _context.NotificationReservations.FirstOrDefault(e => e.NotificationId == notificationId && !e.IsDeleted);
				if (noti != null)
				{
					noti.IsDeleted = true;
				}
			}
			if (notification.Type == (int)NotiType.Follow_LandLord)
			{
				var noti = _context.NotificationFollows.FirstOrDefault(e => e.NotificationId == notificationId && !e.IsDeleted);
				if (noti != null)
				{
					noti.IsDeleted = true;
				}
			}
			if (notification.Type == (int)NotiType.Review_LandLord)
			{
				var noti = _context.NotificationReviews.FirstOrDefault(e => e.NotifycatonId == notificationId && !e.IsDeleted);
				if (noti != null)
				{
					noti.IsDeleted = true;
				}
			}
			_context.SaveChanges();
			return new ResponseDTO<bool>(true);
		}
		private ReviewResponseDTO? GetReview(int ReviewId)
		{
			var review = _context.Reviews.FirstOrDefault(e => e.Id == ReviewId && !e.IsDeleted);
			if (review == null) return null;
			return new ReviewResponseDTO
			{
				Id = review.Id,
				UserId = review.UserId,
				ReservationId = review.ReservationId,
				Comment = review.Comment,
				CreateAt = review.CreateAt,
				ModifiAt = review.ModifyAt
			};
		}
		private FollowDTO? GetFollow(int followId)
		{
			var follow = _context.FollowUserAccoms.FirstOrDefault(e => e.Id == followId && !e.IsDeleted);
			if (follow == null) return null;
			return new FollowDTO
			{
				Id = follow.Id,
				UserId = follow.UserId,
				AccomodationId = follow.AccomodationId,
				CreatedAt = follow.CreateAt,
				ModifiedAt = follow.ModifyAt
			};
		}
		private ReservationDTO? GetReservation(int reservationId)
		{
			var reservation = _context.Reservations.FirstOrDefault(e => e.Id == reservationId && !e.IsDeleted);
			if (reservation == null) return null;
			return new ReservationDTO
			{
				Id = reservation.Id,
				UserId = reservation.UserId,
				ApartmentId = reservation.ApartmentId,
				Status = reservation.Status,
				OwnerId = reservation.OwnerId,
				CreatedAt = reservation.CreateAt,
				ModifiedAt = reservation.ModifyAt
			};
		}
		private string GetUsername(int userId)
		{
			var user = _context.Accounts.FirstOrDefault(e => e.UserId == userId);
			if (user == null) return "";
			return user.UserName;
		}
		public PagedResultDTO<NotificationDTO> GetNotification(int userId, int pageSize, int pageIndex)
		{
			var result = new List<NotificationDTO>();
			var notis = _context.Notifications.Where(e => !e.IsDeleted && e.UserId == userId)
											  .Skip((pageIndex - 1) * pageSize)
											  .Take(pageSize)
											  .ToList();
			foreach (var noti in notis)
			{
				if (noti.Type == (int)NotiType.Follow_LandLord)
				{
					var notiFollow = _context.NotificationFollows.FirstOrDefault(e => !e.IsDeleted && e.NotificationId == noti.Id);
					if (notiFollow != null)
					{
						var follow = GetFollow(notiFollow.FollowId);
						if (follow != null)
						{
							result.Add(new NotificationDTO
							{
								Id = noti.Id,
								UserId = noti.UserId,
								IsRead = noti.IsRead == 0 ? false : true,
								Type = noti.Type,
								Follow = follow,
								Author = GetUsername(follow.UserId)
							});
						}
					}
				}
				if (noti.Type == (int)NotiType.Review_LandLord)
				{
					var notiReview = _context.NotificationReviews.FirstOrDefault(e => !e.IsDeleted && e.NotifycatonId == noti.Id);
					if (notiReview != null)
					{
						var review = GetReview(notiReview.ReviewId);
						if (review != null)
						{
							result.Add(new NotificationDTO
							{
								Id = noti.Id,
								UserId = noti.UserId,
								IsRead = noti.IsRead == 0 ? false : true,
								Type = noti.Type,
								Review = review,
								Author = GetUsername(review.UserId)
							});
						}
					}
				}
				if (noti.Type == (int)NotiType.Reservation_LandLord || noti.Type == (int)NotiType.Reservation_Tenant)
				{
					var notiReservation = _context.NotificationReservations.FirstOrDefault(e => !e.IsDeleted && e.NotificationId == noti.Id);
					if (notiReservation != null)
					{
						var reservation = GetReservation(notiReservation.ReservationId);
						if (reservation != null)
						{
							result.Add(new NotificationDTO
							{
								Id = noti.Id,
								UserId = noti.UserId,
								IsRead = noti.IsRead == 0 ? false : true,
								Type = noti.Type,
								Reservation = reservation,
								Author = (noti.Type == (int)NotiType.Reservation_LandLord) ? GetUsername(reservation.UserId)
																						   : GetUsername(reservation.OwnerId)
							});
						}
					}
				}

			}
			return new PagedResultDTO<NotificationDTO>
			{
				CurrentPage = pageIndex,
				PageSize = pageSize,
				Results = result,
				RowCount = result.Count,
			};
		}

		public ResponseDTO<bool> ReadAll(int userId)
		{
			var notis = _context.Notifications.Where(e => e.UserId == userId && !e.IsDeleted).ToList();
			notis.ForEach(e => e.IsRead = 1);
			_context.SaveChanges();
			return new ResponseDTO<bool>(true);
		}

		public ResponseDTO<bool> ReadNotification(int userId, int notificationId)
		{
			var noti = _context.Notifications.FirstOrDefault(e => e.UserId == userId && e.Id == notificationId && !e.IsDeleted);
			if (noti != null) noti.IsRead = 1;
			_context.SaveChanges();
			return new ResponseDTO<bool>(true);
		}
	}
}
