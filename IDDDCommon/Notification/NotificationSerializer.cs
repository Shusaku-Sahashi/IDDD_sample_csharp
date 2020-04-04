using System.Text.Json;

namespace IDDDCommon.Notification
{
    internal static class NotificationSerializer
    {
        internal static string Serialize(Notification notification)
        {
            return JsonSerializer.Serialize(notification);
        }
    }
}