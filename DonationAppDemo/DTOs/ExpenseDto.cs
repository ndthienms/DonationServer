namespace DonationAppDemo.DTOs
{
    public class ExpenseDto
    {
        public string? Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public int OrganiserId { get; set; }
        public int CampaignId { get; set; }
    }
}