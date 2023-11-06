namespace YGHMS.API.DTO.ResponseModels.PostDTOs
{
    public class ExtendPostExpirationDateRequest
    {
        public int PostId { get; set; }
        public DateTime ToDate { get; set; }
    }
}
