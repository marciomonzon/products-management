namespace EmailNotificationWorker.Events
{
    public class ProductCreatedEvent
    {
        public int ProductId { set; get; }
        public DateTime CreatedAt { set; get; }
    }
}
