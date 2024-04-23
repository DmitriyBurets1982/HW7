namespace BillingService.Dtos
{
    public class AccountDto : AccountBaseDto
    {
        public int Id { get; set; }
        public double? Balance { get; set; }
    }
}
