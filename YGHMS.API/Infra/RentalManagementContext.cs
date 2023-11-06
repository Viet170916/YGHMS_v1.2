using Microsoft.EntityFrameworkCore;
using YGHMS.API.Infra.Enums;
using YGHMS.API.Infra.Models;

namespace YGHMS.API.Infra;

public partial class RentalManagementContext : DbContext
{
  public RentalManagementContext() { }

  public RentalManagementContext(DbContextOptions<RentalManagementContext> options)
    : base(options) { }

  public virtual DbSet<Accommodation> Accommodations { get; set; }
  public virtual DbSet<AccommodationPublication> AccommodationPublications { get; set; }
  public virtual DbSet<Account> Accounts { get; set; }
  public virtual DbSet<Address> Addresses { get; set; }
  public virtual DbSet<Amenity> Amenities { get; set; }
  public virtual DbSet<Apartment> Apartments { get; set; }
  public virtual DbSet<ApartmentBedType> ApartmentBedTypes { get; set; }
  public virtual DbSet<ApartmentBenefit> ApartmentBenefits { get; set; }
  public virtual DbSet<ApartmentPublication> ApartmentPublications { get; set; }
  public virtual DbSet<ApartmentsAmenity> ApartmentsAmenities { get; set; }
  public virtual DbSet<Chat> Chats { get; set; }
  public virtual DbSet<Estatetype> Estatetypes { get; set; }
  public virtual DbSet<FollowUserAccom> FollowUserAccoms { get; set; }
  public virtual DbSet<Notification> Notifications { get; set; }
  public virtual DbSet<NotificationFollow> NotificationFollows { get; set; }
  public virtual DbSet<NotificationReservation> NotificationReservations { get; set; }
  public virtual DbSet<NotificationReview> NotificationReviews { get; set; }
  public virtual DbSet<Publication> Publications { get; set; }
  public virtual DbSet<Reason> Reasons { get; set; }
  public virtual DbSet<ReasonReport> ReasonReports { get; set; }
  public virtual DbSet<Report> Reports { get; set; }
  public virtual DbSet<Reservation> Reservations { get; set; }
  public virtual DbSet<Review> Reviews { get; set; }
  public virtual DbSet<Role> Roles { get; set; }
  public virtual DbSet<Transaction> Transactions { get; set; }
  public virtual DbSet<Unavailableapartmentdate> Unavailableapartmentdates { get; set; }
  public virtual DbSet<User> Users { get; set; }
  public virtual DbSet<UserHighlight> UserHighlights { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=ygh_rental_management_system;User Id=root;Password=vietvqhe170916;default command timeout=0;");

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Accommodation>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.HasIndex(e => e.AddressId, "Accommodations_addresses_Id_fk");
      entity.HasIndex(e => e.EstateTypesId, "ForeignKey_Accommodations_EstateTypes_idx");
      entity.HasIndex(e => e.OwnerId, "ForeignKey_Accommodations_User_idx");
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.Description).HasMaxLength(6000);
      entity.Property(e => e.DetailAddress).HasMaxLength(200);
      entity.Property(e => e.Expiration).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.Policies).HasMaxLength(3000);
      entity.Property(e => e.Quality).HasPrecision(2, 1);
      entity.Property(e => e.Status).HasDefaultValueSql("'0'");
      entity.Property(e => e.Title).HasMaxLength(200);
      entity.HasOne(d => d.Address)
            .WithMany(p => p.Accommodations)
            .HasForeignKey(d => d.AddressId)
            .HasConstraintName("Accommodations_addresses_Id_fk");
      entity.HasOne(d => d.EstateTypes)
            .WithMany(p => p.Accommodations)
            .HasForeignKey(d => d.EstateTypesId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ForeignKey_Accommodations_EstateTypes");
      entity.HasOne(d => d.Owner)
            .WithMany(p => p.Accommodations)
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ForeignKey_Accommodations_User");
    });
    modelBuilder.Entity<AccommodationPublication>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("accommodation_publication");
      entity.HasIndex(e => e.MediaId, "FK_Accomodation_Media_Media_idx");
      entity.HasIndex(e => e.AccommodationId, "FK_accommodation_media_Accommdation_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Accommodation)
            .WithMany(p => p.AccommodationPublications)
            .HasForeignKey(d => d.AccommodationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Accommodation_Media_Accommodation");
      entity.HasOne(d => d.Media)
            .WithMany(p => p.AccommodationPublications)
            .HasForeignKey(d => d.MediaId)
            .HasConstraintName("FK_Accomodation_Media_Media");
    });
    modelBuilder.Entity<Account>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("accounts");
      entity.HasIndex(e => e.UserId, "UserId_UNIQUE").IsUnique();
      entity.HasIndex(e => e.UserName, "UserName_UNIQUE").IsUnique();
      entity.HasIndex(e => e.RoleId, "accounts_userer_roleId_idx");
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.Emai).HasMaxLength(200);
      entity.Property(e => e.ModifyAt).HasColumnType("datetime");
      entity.Property(e => e.Password).HasMaxLength(200);
      entity.Property(e => e.UserName).HasMaxLength(100);
      entity.HasOne(d => d.Role)
            .WithMany(p => p.Accounts)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ForeignKey_accounts_userer");
      entity.HasOne(d => d.User)
            .WithOne(p => p.Account)
            .HasForeignKey<Account>(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ForignKey_accounts_users");
    });
    modelBuilder.Entity<Address>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("addresses");
      entity.HasIndex(e => e.Id, "Id_UNIQUE1").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.Detail).HasMaxLength(300);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasMany(d => d.MediaNavigation)
            .WithMany(p => p.Addresses)
            .UsingEntity<Dictionary<string, object>>("AddressPublication",
              r => r.HasOne<Publication>()
                    .WithMany()
                    .HasForeignKey("MediaId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Address_Media_detaillmedia_Id_fk"),
              l => l.HasOne<Address>()
                    .WithMany()
                    .HasForeignKey("AddressId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Address_Media_addresses_Id_fk"),
              j =>
              {
                j.HasKey("AddressId", "MediaId").HasName("PRIMARY");
                j.ToTable("Address_Publication");
                j.HasIndex(new[] { "MediaId", }, "Address_Media_detaillmedia_Id_fk");
              });
    });
    modelBuilder.Entity<Amenity>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("amenities");
      entity.HasIndex(e => e.Id, "Id_UNIQUE2").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.Description).HasMaxLength(300);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.Name).HasMaxLength(100);
    });
    modelBuilder.Entity<Apartment>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("apartments");
      entity.HasIndex(e => e.AccommodationId, "FK_Apartment_Accom_idx");
      entity.HasIndex(e => e.OwnerId, "FK_Apartment_User_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE3").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.Description).HasMaxLength(3000);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.Name).HasMaxLength(200);
      entity.HasOne(d => d.Accommodation)
            .WithMany(p => p.Apartments)
            .HasForeignKey(d => d.AccommodationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Apartment_Accom");
      entity.HasOne(d => d.Owner)
            .WithMany(p => p.Apartments)
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Apartment_User");
    });
    modelBuilder.Entity<ApartmentBedType>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("Apartment_BedType");
      entity.HasIndex(e => e.ApartmentId, "Apartment_BedType_ApartmentId_index");
      entity.HasIndex(e => new { e.ApartmentId, e.Type, }, "Apartment_BedType_pk2").IsUnique();
      entity.Property(e => e.CreateAt).HasColumnType("datetime");
      entity.Property(e => e.ModifyAt).HasColumnType("datetime");
      entity.HasOne(d => d.Apartment)
            .WithMany(p => p.ApartmentBedTypes)
            .HasForeignKey(d => d.ApartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Apartment_BedType_apartments_Id_fk");
    });
    modelBuilder.Entity<ApartmentBenefit>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.HasIndex(e => e.ApartmentId, "ApartmentBenefits_ApartmentId_index");
      entity.Property(e => e.Content)
            .HasMaxLength(200)
            .HasColumnName("content");
      entity.Property(e => e.CreateAt).HasColumnType("datetime");
      entity.Property(e => e.IsDelete).HasColumnName("isDelete");
      entity.Property(e => e.ModifyAt).HasColumnType("datetime");
      entity.Property(e => e.Name).HasMaxLength(50);
      entity.HasOne(d => d.Apartment)
            .WithMany(p => p.ApartmentBenefits)
            .HasForeignKey(d => d.ApartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ApartmentBenefits_apartments_Id_fk");
    });
    modelBuilder.Entity<ApartmentPublication>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("apartment_publication");
      entity.HasIndex(e => e.ApartmentId, "FK_Apartment_Media_Apartment_idx");
      entity.HasIndex(e => e.MediaId, "FK_Apartment_Media_Media_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE4").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Apartment)
            .WithMany(p => p.ApartmentPublications)
            .HasForeignKey(d => d.ApartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Apartment_Media_Apartment");
      entity.HasOne(d => d.Media)
            .WithMany(p => p.ApartmentPublications)
            .HasForeignKey(d => d.MediaId)
            .HasConstraintName("FK_Apartment_Media_Media");
    });
    modelBuilder.Entity<ApartmentsAmenity>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("apartments_amenities");
      entity.HasIndex(e => e.AmenityId, "FK_Amenity_idx");
      entity.HasIndex(e => e.ApartmentId, "FK_Apartment_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE5").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Amenity)
            .WithMany(p => p.ApartmentsAmenities)
            .HasForeignKey(d => d.AmenityId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Amenity");
      entity.HasOne(d => d.Apartment)
            .WithMany(p => p.ApartmentsAmenities)
            .HasForeignKey(d => d.ApartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Apartment");
    });
    modelBuilder.Entity<Chat>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("chat");
      entity.HasIndex(e => new { e.SendedId, e.ReceivedId, }, "FK_Chat_Sender_idx");
      entity.HasIndex(e => e.ReceivedId, "FK_Chat_User_Receiver_idx");
      entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();
      entity.Property(e => e.Content).HasMaxLength(500);
      entity.Property(e => e.CreateAt).HasColumnType("datetime");
      entity.Property(e => e.ModifyAt).HasColumnType("datetime");
      entity.HasOne(d => d.Received)
            .WithMany(p => p.ChatReceiveds)
            .HasForeignKey(d => d.ReceivedId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Chat_User_Receiver");
      entity.HasOne(d => d.Sended)
            .WithMany(p => p.ChatSendeds)
            .HasForeignKey(d => d.SendedId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Chat_User");
    });
    modelBuilder.Entity<Estatetype>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("estatetypes");
      entity.HasIndex(e => e.Name, "EstateType_UNIQUE").IsUnique();
      entity.HasIndex(e => e.Id, "Id_UNIQUE7").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.Name).HasMaxLength(100);
    });
    modelBuilder.Entity<FollowUserAccom>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("follow_user_accom");
      entity.HasIndex(e => e.AccomodationId, "FK_Follow_Accom_idx");
      entity.HasIndex(e => e.UserId, "FK_Follow_User_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE8").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Accomodation)
            .WithMany(p => p.FollowUserAccoms)
            .HasForeignKey(d => d.AccomodationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Follow_Accom");
      entity.HasOne(d => d.User)
            .WithMany(p => p.FollowUserAccoms)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Follow_User");
    });
    modelBuilder.Entity<Notification>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("Notification");
      entity.HasIndex(e => e.Id, "Id_UNIQUE9").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.IsRead).HasColumnName("isRead");
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
    });
    modelBuilder.Entity<NotificationFollow>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("Notification_follow");
      entity.HasIndex(e => e.NotificationId, "FK_Notification_Follow_Notification_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE10").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Follow)
            .WithMany(p => p.NotificationFollows)
            .HasForeignKey(d => d.FollowId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Notification_Follow_Follow");
      entity.HasOne(d => d.Notification)
            .WithMany(p => p.NotificationFollows)
            .HasForeignKey(d => d.NotificationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Notification_Follow_Notification");
    });
    modelBuilder.Entity<NotificationReservation>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("Notification_reservation");
      entity.HasIndex(e => e.NotificationId, "FK_Notification_reservation_Notification_idx");
      entity.HasIndex(e => e.ReservationId, "FK_Notification_reservation_Reservation_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE11").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Notification)
            .WithMany(p => p.NotificationReservations)
            .HasForeignKey(d => d.NotificationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Notification_reservation_Notification");
      entity.HasOne(d => d.Reservation)
            .WithMany(p => p.NotificationReservations)
            .HasForeignKey(d => d.ReservationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Notification_reservation_Reservation");
    });
    modelBuilder.Entity<NotificationReview>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("Notification_review");
      entity.HasIndex(e => e.NotifycatonId, "FK_Notification_Review_Notification_idx");
      entity.HasIndex(e => e.ReviewId, "FK_Notification_Review_Review_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE12").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Notifycaton)
            .WithMany(p => p.NotificationReviews)
            .HasForeignKey(d => d.NotifycatonId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Notification_Review_Notification");
      entity.HasOne(d => d.Review)
            .WithMany(p => p.NotificationReviews)
            .HasForeignKey(d => d.ReviewId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Notification_Review_Review");
    });
    modelBuilder.Entity<Publication>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.HasIndex(e => e.Id, "Id_UNIQUE6").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.Description).HasMaxLength(200);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.Url).HasMaxLength(300);
    });
    modelBuilder.Entity<Reason>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("reasons");
      entity.Property(e => e.Content).HasMaxLength(500);
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
    });
    modelBuilder.Entity<ReasonReport>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("reason_report");
      entity.HasIndex(e => e.ReasonId, "FK_Reason_Report_Reason_idx");
      entity.HasIndex(e => e.ReportId, "FK_Reason_Report_Report_idx");
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Reason)
            .WithMany(p => p.ReasonReports)
            .HasForeignKey(d => d.ReasonId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reason_Report_Reason");
      entity.HasOne(d => d.Report)
            .WithMany(p => p.ReasonReports)
            .HasForeignKey(d => d.ReportId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reason_Report_Report");
    });
    modelBuilder.Entity<Report>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("report");
      entity.HasIndex(e => e.AccomodationId, "FK_Report_Accommodation_idx");
      entity.HasIndex(e => e.UserId, "FK_Report_user_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE13").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.HasOne(d => d.Accomodation)
            .WithMany(p => p.Reports)
            .HasForeignKey(d => d.AccomodationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Report_Accommodation");
      entity.HasOne(d => d.User)
            .WithMany(p => p.Reports)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Report_user");
    });
    modelBuilder.Entity<Reservation>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("reservation");
      entity.HasIndex(e => e.ApartmentId, "FK_Reservation_Apartment_idx");
      entity.HasIndex(e => e.OwnerId, "FK_Reservation_User_Owner_idx");
      entity.HasIndex(e => e.UserId, "FK_User_idx");
      entity.HasIndex(e => e.Id, "idReservation_UNIQUE").IsUnique();
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.FromDate).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.ToDate).HasMaxLength(6);
      entity.HasOne(d => d.Apartment)
            .WithMany(p => p.Reservations)
            .HasForeignKey(d => d.ApartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reservation_Apartment");
      entity.HasOne(d => d.Owner)
            .WithMany(p => p.ReservationOwners)
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reservation_User_Owner");
      entity.HasOne(d => d.User)
            .WithMany(p => p.ReservationUsers)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Reservation_User");
    });
    modelBuilder.Entity<Review>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("reviews");
      entity.HasIndex(e => e.Id, "Id_UNIQUE14").IsUnique();
      entity.HasIndex(e => e.ReservationId, "reviews_reservation_Id_fk");
      entity.HasIndex(e => e.UserId, "reviews_users_Id_fk");
      entity.Property(e => e.Comment).HasMaxLength(2000);
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.Rate).HasPrecision(2, 1);
      entity.HasOne(d => d.Reservation)
            .WithMany(p => p.Reviews)
            .HasForeignKey(d => d.ReservationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("reviews_reservation_Id_fk");
      entity.HasOne(d => d.User)
            .WithMany(p => p.Reviews)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("reviews_users_Id_fk");
    });
    modelBuilder.Entity<Role>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("roles");
      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.Description).HasMaxLength(200);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.Name).HasMaxLength(45);
    });
    modelBuilder.Entity<Transaction>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.HasIndex(e => e.Id, "Transactions_Id_index");
      entity.HasIndex(e => e.PostId, "Transactions_PostId_index");
      entity.HasIndex(e => e.UserId, "Transactions_UserId_index");
      entity.Property(e => e.CreateAt).HasColumnType("datetime");
      entity.Property(e => e.Description).HasMaxLength(1000);
      entity.Property(e => e.Name).HasMaxLength(100);
      entity.HasOne(d => d.Post)
            .WithMany(p => p.Transactions)
            .HasForeignKey(d => d.PostId)
            .HasConstraintName("Transactions_Accommodations_Id_fk");
      // entity.HasOne(d => d.User)
      //       .WithMany(p => p.Transactions)
      //       .HasForeignKey(d => d.UserId)
      //       .OnDelete(DeleteBehavior.ClientSetNull)
      //       .HasConstraintName("Transactions_users_Id_fk");
    });
    modelBuilder.Entity<Unavailableapartmentdate>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("unavailableapartmentdate");
      entity.HasIndex(e => e.ApartmentTypeId, "FK_Âprartment_idx");
      entity.HasIndex(e => e.Id, "Id_UNIQUE15").IsUnique();
      entity.Property(e => e.ApartmentTypeId).HasColumnName("Apartment_TypeId");
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.EndDate).HasMaxLength(6);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.StartDate).HasMaxLength(6);
      entity.HasOne(d => d.ApartmentType)
            .WithMany(p => p.Unavailableapartmentdates)
            .HasForeignKey(d => d.ApartmentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Aprartment");
    });
    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("users");
      entity.HasIndex(e => e.RoleId, "ForeignKey_Users_Role_idx");
      entity.HasIndex(e => e.AvatarId, "users_Publications_Id_fk");
      entity.HasIndex(e => e.CoverImage, "users_Publications_Id_fk2");
      entity.HasIndex(e => e.UserName, "users_pk").IsUnique();
      entity.Property(e => e.Address).HasMaxLength(200);
      entity.Property(e => e.CreateAt).HasMaxLength(6);
      entity.Property(e => e.Email).HasMaxLength(100);
      entity.Property(e => e.FullName).HasMaxLength(150);
      entity.Property(e => e.ModifyAt).HasMaxLength(6);
      entity.Property(e => e.PhoneNumber).HasMaxLength(20);
      entity.Property(e => e.UserName).HasMaxLength(100);
      entity.HasOne(d => d.Avatar)
            .WithMany(p => p.UserAvatars)
            .HasForeignKey(d => d.AvatarId)
            .HasConstraintName("users_Publications_Id_fk");
      entity.HasOne(d => d.CoverImageNavigation)
            .WithMany(p => p.UserCoverImageNavigations)
            .HasForeignKey(d => d.CoverImage)
            .HasConstraintName("users_Publications_Id_fk2");
      entity.HasOne(d => d.Role)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ForeignKey_Users_Role");
    });
    modelBuilder.Entity<UserHighlight>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");
      entity.ToTable("user_highlight");
      entity.HasIndex(e => e.PublicationId, "user_highlight_Publications_Id_fk");
      entity.HasIndex(e => new { e.UserId, e.PublicationId, }, "user_highlight_pk2").IsUnique();
      entity.Property(e => e.Id)
            .HasComment("id")
            .HasColumnName("id");
      entity.Property(e => e.CreateAt)
            .HasColumnType("datetime")
            .HasColumnName("createAt");
      entity.Property(e => e.Description)
            .HasMaxLength(500)
            .HasColumnName("description");
      entity.Property(e => e.IsDelete).HasColumnName("isDelete");
      entity.Property(e => e.ModifyAt)
            .HasColumnType("datetime")
            .HasColumnName("modifyAt");
      entity.Property(e => e.PublicationId).HasColumnName("publication_id");
      entity.Property(e => e.Title).HasColumnName("title");
      entity.Property(e => e.UserId).HasColumnName("user_id");
      entity.HasOne(d => d.Publication)
            .WithMany(p => p.UserHighlights)
            .HasForeignKey(d => d.PublicationId)
            .HasConstraintName("user_highlight_Publications_Id_fk");
      entity.HasOne(d => d.User)
            .WithMany(p => p.UserHighlights)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("user_highlight_users_UserId_fk");
    });
    OnModelCreatingPartial(modelBuilder);
  }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
    CancellationToken cancellationToken = new CancellationToken())
    {
        //OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override int SaveChanges()
    {
        var kt = OnBeforeSaving().Result;
        return kt;
    }
    private async Task<int> InsertNotification(int userId, NotiType type)
    {
        var utcNow = DateTime.UtcNow;
        var noti = new Notification
        {
            Type = (int) type,
            UserId = userId,
            IsRead = 0,
            CreateAt = utcNow,
            ModifyAt = utcNow
        };
        Notifications.Add(noti);
        await base.SaveChangesAsync();
        return noti.Id;
    }
    private async Task<int> InsertNotificationFollow(int notiId, int followId)
    {
        var utcNow = DateTime.UtcNow;
        var notiFollow = new NotificationFollow
        {
            NotificationId = notiId,
            FollowId = followId,
            CreateAt = utcNow,
            ModifyAt = utcNow
        };
        NotificationFollows.Add(notiFollow);
        await base.SaveChangesAsync();
        return notiFollow.Id;
    }
    private async Task<int> InsertNotificationReview(int notiId, int reviewId)
    {
        var utcNow = DateTime.UtcNow;
        var notiReview = new NotificationReview
        {
            NotifycatonId = notiId,
            ReviewId = reviewId,
            CreateAt = utcNow,
            ModifyAt = utcNow
        };
        NotificationReviews.Add(notiReview);
        await base.SaveChangesAsync();
        return notiReview.Id;
    }
    private async Task<int> InsertNotificationReservation(int notiId, int reservationId)
    {
        var utcNow = DateTime.UtcNow;
        var reservationNoti = new NotificationReservation
        {
            NotificationId = notiId,
            ReservationId = reservationId,
            CreateAt = utcNow,
            ModifyAt = utcNow
        };
        NotificationReservations.Add(reservationNoti);
        try
        {
            await base.SaveChangesAsync();
        }
        catch(Exception ex) { 
        }
        return reservationNoti.Id;
    }
    protected virtual async Task<int> OnBeforeSaving()
    {
        var entrys = ChangeTracker.Entries().ToList();

		foreach (var entry in entrys)
        {
            var state = entry.State;
            if (state == EntityState.Added)
            {
                ((CommonModel)entry.Entity).CreateAt = DateTime.UtcNow;
            }

            ((CommonModel)entry.Entity).ModifyAt = DateTime.UtcNow;

			await base.SaveChangesAsync();
			if (entry.Entity.GetType() == typeof(FollowUserAccom) && state == EntityState.Added) 
            {
                var follow = (FollowUserAccom)entry.Entity;
                var ownerId = Accommodations.FirstOrDefault(e => e.Id == follow.AccomodationId).OwnerId;
                //Insert Notification
                var notiId = await InsertNotification(ownerId, NotiType.Follow_LandLord);
                //Insert Follow Notification
                await InsertNotificationFollow(notiId, follow.Id);
            }
            if (entry.Entity.GetType() == typeof(Review) && state == EntityState.Added)
            {
                var review = (Review)entry.Entity;
                //Insert Notification
                var notiId = await InsertNotification(review.UserId, NotiType.Review_LandLord);
                //Insert Review Notification
                await InsertNotificationReview(notiId, review.Id);
            }
            if (entry.Entity.GetType() == typeof(Reservation) && state == EntityState.Added)
            {
                var reservation = (Reservation)entry.Entity;
                //Insert Notification
                var notiId = await InsertNotification(reservation.OwnerId, NotiType.Reservation_LandLord);
                //Insert Review Notification
                await InsertNotificationReservation(notiId, reservation.Id);
            }
            if (entry.Entity.GetType() == typeof(Reservation) && state == EntityState.Modified)
            {
                var reservation = (Reservation)entry.Entity;
                //Insert Notification
                var notiId = await InsertNotification(reservation.UserId, NotiType.Reservation_Tenant);
                //Insert Review Notification
                await InsertNotificationReservation(notiId, reservation.Id);
            }
        }
        return 1;
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}