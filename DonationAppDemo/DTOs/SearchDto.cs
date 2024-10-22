namespace DonationAppDemo.DTOs
{
    public class SearchDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set;}
        public string? Id { get; set; }
        public int PageIndex { get; set; }
    }
}
