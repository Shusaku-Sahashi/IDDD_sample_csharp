namespace IDDDCommon.Notification
{
    /// <summary>
    /// Notificationの最新情報
    /// </summary>
    public class PublishedNotificationTracker
    {
        public long MostLatestPublishedId { get; set; }
        public int CurrencyVersion { get; set; }
        public string TypeName { get; set; }
    }
}