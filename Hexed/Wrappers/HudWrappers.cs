using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.UI;

namespace Hexed.Wrappers
{
    internal class HudWrappers
    {
        public static void TriggerAlert(string Title, string Content)
        {
            ViewManager.Instance.TriggerAlert(Title, Content, 1, true);
        }

        public static void TriggerPushAlter(string content)
        {
            ViewManager.Instance.TriggerPushNotification(content, 5);
        }

        public static void TriggerDropText(string Title, string content)
        {
            CohtmlHud.Instance.ViewDropText(Title, content);
        }
    }
}
