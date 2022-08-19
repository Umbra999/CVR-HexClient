using ABI_RC.Core.EventSystem;
using ABI_RC.Core.Networking;
using DarkRift;
using UnityEngine.XR;

namespace Hexed.Wrappers
{
    internal static class GeneralWrappers
    {
        public static void SwitchAvatar(string ID) { AssetManagement.Instance.LoadLocalAvatar(ID); }

        public static bool IsInVr()
        {
            return XRDevice.isPresent;
        }

        public static bool IsConnected()
        {
            return NetworkManager.Instance.GameNetwork.ConnectionState == ConnectionState.Connected;
        }
    }
}
