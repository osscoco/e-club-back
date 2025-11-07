namespace E_Club.Application.DTOs.Clubs.Response
{
    public class ClubDtoResponse
    {
        public Guid ClubId { get; set; }
        public required string Name { get; set; }
        public required decimal CA { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
