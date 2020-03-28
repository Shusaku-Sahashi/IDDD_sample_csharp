namespace IDDDCommon.Test.Notification
{
    /// <summary>
    /// Notificationの最新情報
    /// </summary>
    public class PublishedNotificationTracker
    {
        public long Id { get; set; }
        public long MostLatestPublishedId { get; set; }
        public int CurrencyVersion { get; set; }
        public string TypeName { get; set; }
    }
}