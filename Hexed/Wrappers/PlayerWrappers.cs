using ABI.CCK.Components;
using ABI_RC.Core.Player;
using Dissonance.Integrations.DarkRift2;
using UnityEngine;

namespace Hexed.Wrappers
{
    internal static class PlayerWrappers
    {
        public static GameObject GetPlayerObject(this CVRPlayerEntity player) { return player.PlayerObject; }
        public static string GetAvatarID(this CVRPlayerEntity player) { return player.AvatarId; }
        public static DarkRift2Player GetDarkRiftPlayer(this CVRPlayerEntity player) { return player.DarkRift2Player; }
        public static PlayerNameplate GetNameplate(this CVRPlayerEntity player) { return player.PlayerNameplate; }
        public static string GetImageURL(this CVRPlayerEntity player) { return player.ApiProfileImageUrl; }
        public static string GetUsername(this CVRPlayerEntity player) { return player.Username; }
        public static string GetUserID(this CVRPlayerEntity player) { return player.Uuid; }
        public static PlayerMeta GetMetaData(this CVRPlayerEntity player) { return player.PlayerMetaData; }
        public static PlayerDescriptor GetPlayerDescriptor(this CVRPlayerEntity player) { return player.PlayerDescriptor; }
        public static PuppetMaster GetPuppetMaster(this CVRPlayerEntity player) { return player.PuppetMaster; }
        public static string GetStaffTag(this CVRPlayerEntity player) { return player.ApiUserStaffTag; }
        public static CVRPlayerEntity[] GetAllNetworkedPlayers(this CVRPlayerManager instance) { return instance.NetworkPlayers.ToArray(); }
        public static string GetUserID(this DarkRift2Player player) { return player.PlayerId; }
        public static string GetAvatarID(this PlayerDescriptor player) { return player.avtrId; }
        public static bool IsAvatarBlocked(this PlayerDescriptor player) { return player.avatarBlocked; }
        public static bool IsVoiceMuted(this PlayerDescriptor player) { return player.voiceMuted; }
        public static string GetClanTag(this PlayerDescriptor player) { return player.userClanTag; }
        public static string GetStaffTag(this PlayerDescriptor player) { return player.userStaffTag; }
        public static string GetOwnerID(this PlayerDescriptor player) { return player.ownerId; }
        public static string GetImageURL(this PlayerDescriptor player) { return player.profileImageUrl; }
        public static string GetUsername(this PlayerDescriptor player) { return player.userName; }
        public static GameObject GetAvatarObject(this PlayerSetup player) { return player._avatar; }
        public static GameObject GetAvatarObject(this PuppetMaster player) { return player.avatarObject; }
        public static GameObject GetAvatarObject(this CVRPlayerEntity player) { return player.AvatarHolder; }
        public static string GetAvatarID(this CVRAssetInfo player) { return player.guid; }

        public static PlayerDescriptor GetLocalPlayerDescriptor() { return GetLocalPlayer().GetComponent<PlayerDescriptor>(); }
        public static PlayerSetup GetLocalPlayerSetup() { return GetLocalPlayer().GetComponent<PlayerSetup>(); }

        private static GameObject LocalPlayer;
        public static GameObject GetLocalPlayer()
        {
            if (LocalPlayer == null) LocalPlayer = GameObject.Find("_PLAYERLOCAL");
            return LocalPlayer;
        }

        public static CVRPlayerEntity GetPlayer(this CVRPlayerManager Instance, string UserID)
        {
            foreach (CVRPlayerEntity player in Instance.GetAllNetworkedPlayers())
            {
                if (player.Uuid == UserID) return player;
            }
            return null;
        }

        public enum PlayerRank
        {
            None,
            User,
            Legend,
            Guide,
            Mod,
            Dev,
        }

        public static PlayerRank GetRank(string Rank)
        {
            switch (Rank)
            {
                case "User":
                    return PlayerRank.User;

                case "Legend":
                    return PlayerRank.Legend;

                case "Community Guide":
                    return PlayerRank.Guide;

                case "Moderator":
                    return PlayerRank.Mod;

                case "Developer":
                    return PlayerRank.Dev;
            }
            return PlayerRank.None;
        }

        public static PlayerRank GetRank(this CVRPlayerEntity player)
        {
            return GetRank(player.ApiUserRank);
        }

        public static PlayerRank GetRank(this PlayerDescriptor player)
        {
            return GetRank(player.userRank);
        }


        public static Color GetRankColor(this PlayerRank Rank)
        {
            switch (Rank)
            {
                case PlayerRank.User:
                    return Color.green;

                case PlayerRank.Legend:
                    return Color.white;

                case PlayerRank.Guide:
                    return Color.magenta;

                case PlayerRank.Mod:
                    return Color.red;

                case PlayerRank.Dev:
                    return Color.red;
            }
            return Color.black;
        }

        public static bool GetIsBot(this CVRPlayerEntity player)
        {
            return player.GetPlayerObject().transform.position == Vector3.zero;
        }
    }
}
