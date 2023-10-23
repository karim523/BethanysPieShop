namespace BethanysPieShop.InventoryManagement.NotificationContext
{
    public sealed class Notification
    {
        public Notification()
        {
            
        }
        public Notification( string message)
        {
            Message = message;
        }
        public string Message { get; set; }
        public override string ToString()
        {
            return $"{Message}";
        }
    }
}