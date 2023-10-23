namespace BethanysPieShop.InventoryManagement.NotificationContext
{
    public abstract class Notifiable
    {
        private readonly List<Notification> _notifications;
        public bool IsInvalid => _notifications.Any();
        public List<string> Notifications => _notifications.Select(a => a.ToString()).ToList();
        public Notifiable()
        {
            _notifications = new List<Notification>();
        }
        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }
        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
    }
}