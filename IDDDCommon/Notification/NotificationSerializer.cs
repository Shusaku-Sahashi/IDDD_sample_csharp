using Newtonsoft.Json;

namespace IDDDCommon.Test.Notification
{
    internal class NotificationSerializer
    {
        internal static string Serialize(IDDDCommon.Notification.Notification notification)
        {
            return JsonConvert.SerializeObject(notification);
        }
    }
}