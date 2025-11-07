namespace E_Club.Application.DTOs.UserTypes.Response
{
    public class UserTypeDtoResponse
    {
        public Guid UserTypeId { get; set; }
        public required string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
