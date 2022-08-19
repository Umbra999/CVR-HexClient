using ABI_RC.Core.Networking.IO.Instancing;
using System.Collections.Generic;

namespace Hexed.Wrappers
{
    internal static class InstanceWrappers
    {
        public static InstanceDetails_t GetCurrentInstance() { return InstanceDetails_t.InstanceDetailsPool.GetObject(); }
        public static string GetInstanceID(this InstanceDetails_t Instance) { return Instance.InstanceId; }
        public static string GetInstanceName(this InstanceDetails_t Instance) { return Instance.InstanceName; }
        public static List<ABI_RC.Core.Networking.IO.Social.MinimalUser_t> GetInstanceUsers(this InstanceDetails_t Instance) { return Instance.Users; }
        public static int GetMaxPlayerCount(this InstanceDetails_t Instance) { return Instance.MaxPlayer; }
        public static int GetCurrentPlayerCount(this InstanceDetails_t Instance) { return Instance.CurrentPlayer; }
        public static string GetInstanceRegion(this InstanceDetails_t Instance) { return Instance.Region; }
        public static string GetInstancePrivacy(this InstanceDetails_t Instance) { return Instance.Privacy; }
        public static ABI_RC.Core.Networking.IO.Social.ExtendedUser_t GetInstanceOwner(this InstanceDetails_t Instance) { return Instance.Owner; }
    }
}
