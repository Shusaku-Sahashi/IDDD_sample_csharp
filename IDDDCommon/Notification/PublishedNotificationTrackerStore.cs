using System.Collections.Generic;

namespace IDDDCommon.Test.Notification
{
    /**
     * 送信済の
     */
    public interface PublishedNotificationTrackerStore
    {
        PublishedNotificationTracker PublishedNotificationTracker();
        /// <summary>
        /// PublishNotificationTrackerを更新して保存する
        /// </summary>
        /// <param name="publishedNotificationTracker"></param>
        /// <param name="notifications"></param>
        void TrackPublishedNotificationTracker(
            PublishedNotificationTracker publishedNotificationTracker,
            IEnumerable<IDDDCommon.Notification.Notification> notifications);

        long MostLatestPublishedNotificationTracker();
    }
}