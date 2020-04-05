using System.Collections.Generic;
using IDDDCommon.Test.Notification;

namespace IDDDCommon.Notification
{
    /**
     * 送信済のNotificationを保持するためのクラス
     */
    public interface IPublishedNotificationTrackerStore
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