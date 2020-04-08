using System.Collections.Generic;

namespace IDDDCommon.Notification
{
    /**
     * 送信済のNotificationを保持するためのクラス
     */
    public interface IPublishedNotificationTrackerStore
    {
        PublishedNotificationTracker PublishedNotificationTracker();
        /// <summary>
        /// PublishedNotificationTrackerを最新のNotificationで更新して保存する。
        /// </summary>
        /// <param name="publishedNotificationTracker"></param>
        /// <param name="notifications"></param>
        void TrackMotsResentPublishedNotification(
            PublishedNotificationTracker publishedNotificationTracker,
            IEnumerable<Notification> notifications);

        void Purge();
    }
}