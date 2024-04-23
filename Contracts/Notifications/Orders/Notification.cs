namespace Contracts.Notifications.Orders
{
    public class Notification
    {
        public NotificationResult Result { get; set; }
        public int AccountId { get; set; }
        public double Price { get; set; }
    }
}
