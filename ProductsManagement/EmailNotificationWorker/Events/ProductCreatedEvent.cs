﻿namespace EmailNotificationWorker.Events
{
    public class ProductCreatedEvent
    {
        public int ProductId { set; get; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { set; get; }
    }
}
