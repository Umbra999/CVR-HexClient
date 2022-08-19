using ABI_RC.Core.Networking;
using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using ABI_RC.Systems.MovementSystem;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using Dissonance;
using HarmonyLib;
using Hexed.Configs;
using Hexed.Modules;
using Hexed.Wrappers;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zettai;

namespace Hexed.Core
{
    internal static class Patching
    {
        private static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("Hexed");

        private static HarmonyMethod GetPatch(string name)
        {
            return new HarmonyMethod(typeof(Patching).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
        }

        private static MethodInfo GetFromMethod(this Type Type, string name)
        {
            return Type.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }

        private static void CreatePatch(MethodBase TargetMethod, HarmonyMethod Before = null, HarmonyMethod After = null)
        {
            try
            {
                Instance.Patch(TargetMethod, Before, After);
            }
            catch (Exception e)
            {
                Wrappers.Logger.LogError($"Failed to Patch {TargetMethod.Name} \n{e}");
            }
        }


        public static unsafe void ApplyPatches()
        {
            CreatePatch(typeof(UnityClient).GetMethods().Where(x => x.Name == "SendMessage").ToArray()[0], GetPatch(nameof(SendGameMessagePrefix)));
            CreatePatch(AccessTools.Property(typeof(SystemInfo), "deviceUniqueIdentifier").GetMethod, null, GetPatch(nameof(HWIDPostix)));
            //CreatePatch(AccessTools.Property(typeof(MetaPort), "matureContentAllowed").GetMethod, null, GetPatch(nameof(AllowBoolPostfix)));
            CreatePatch(typeof(MovementSystem).GetFromMethod(nameof(MovementSystem.ToggleFlight)), GetPatch(nameof(ToggleFlyPrefix)));
            CreatePatch(typeof(PlayerNameplate).GetFromMethod(nameof(PlayerNameplate.UpdateNamePlate)), null, GetPatch(nameof(NameplateUpdatePostix)));
            CreatePatch(typeof(DissonanceComms).GetFromMethod("Net_PlayerStartedSpeaking"), null, GetPatch(nameof(PlayerStartTalkingPostfix)));
            CreatePatch(typeof(DissonanceComms).GetFromMethod("Net_PlayerStoppedSpeaking"), null, GetPatch(nameof(PlayerStopTalkingPostfix)));
            CreatePatch(typeof(NetworkManager).GetFromMethod("OnGameNetworkMessageReceived"), GetPatch(nameof(OnGameMessageReceivedPrefix)));
            CreatePatch(typeof(PuppetMaster).GetFromMethod(nameof(PuppetMaster.AvatarInstantiated)), GetPatch(nameof(OnAvatarInstantiatedPrefix)));
            CreatePatch(typeof(SkipIntro).GetFromMethod("Start"), GetPatch(nameof(NoIntroPrefix)));
        }

        private static void AllowBoolPostfix(ref bool __result)
        {
            __result = true;
        }

        private static void DisallowBoolPostfix(ref bool __result)
        {
            __result = false;
        }

        private static bool ReturnFalsePrefix()
        {
            return false;
        }

        private static bool ToggleFlyPrefix()
        {
            Movement.ToggleFly();
            return false;
        }

        private static bool NoIntroPrefix()
        {
            SceneManager.LoadSceneAsync(1);
            return false;
        }

        private static void OnAvatarInstantiatedPrefix(PuppetMaster __instance)
        {
            AvatarFilter.AdjustAvatar(__instance.GetAvatarObject());
        }

        private static void PlayerStopTalkingPostfix(string __0)
        {
            NameplateHandler Handler = GameObject.Find($"/{__0}").transform.Find("[NamePlate]").gameObject.GetComponent<NameplateHandler>();
            Handler.BackgroundMask.color = new Color(Handler.BackgroundColor.r, Handler.BackgroundColor.g, Handler.BackgroundColor.b, 0.32f);
            Handler.BackgroundImageComp.color = new Color(Handler.BackgroundColor.r, Handler.BackgroundColor.g, Handler.BackgroundColor.b, 0.32f);
            Handler.Nametag.color = new Color(Handler.FontColor.r, Handler.FontColor.g, Handler.FontColor.b, 0.6f);
        }

        private static void PlayerStartTalkingPostfix(string __0)
        {
            NameplateHandler Handler = GameObject.Find($"/{__0}").transform.Find("[NamePlate]").gameObject.GetComponent<NameplateHandler>();
            Handler.BackgroundMask.color = new Color(Handler.BackgroundColor.r, Handler.BackgroundColor.g, Handler.BackgroundColor.b, 1f);
            Handler.BackgroundImageComp.color = new Color(Handler.BackgroundColor.r, Handler.BackgroundColor.g, Handler.BackgroundColor.b, 1f);
            Handler.Nametag.color = new Color(Handler.FontColor.r, Handler.FontColor.g, Handler.FontColor.b, 1);
        }

        private static void NameplateUpdatePostix(PlayerNameplate __instance)
        {
            __instance.gameObject.AddComponent<NameplateHandler>();
        }

        private static bool OnGameMessageReceivedPrefix(object __0, MessageReceivedEventArgs __1)
        {
            Tags tag = (Tags)__1.Tag;
            using (Message message = __1.GetMessage())
            {
                if (Variables.LogReceiveEvents)
                {
                    using (DarkRiftReader reader = message.GetReader())
                    {
                        byte[] Data = reader.ReadRaw(reader.Length);
                        Wrappers.Logger.LogGameEvent(tag, Data, false);
                    }
                }

                switch (tag)
                {
                    case Tags.DropPortalBroadcast:
                        if (Variables.AntiPortal) return false;
                        return EventValidation.CheckPortalSpawn(message);

                    case Tags.CreateSpawnableObject:
                        return EventValidation.CheckPropSpawn(message);

                    case Tags.UpdateSpawnableObject:
                        return EventValidation.CheckPropUpdate(message);

                    case Tags.ObjectSync:
                        return EventValidation.CheckObjectSync(message);

                    case Tags.ObjectSyncOnJoin:
                        return EventValidation.CheckObjectSync(message);

                    case Tags.PlayerDisconnection:
                        {
                            using (DarkRiftReader reader = message.GetReader())
                            {
                                string UserID = reader.ReadString();
                                var player = CVRPlayerManager.Instance.GetPlayer(UserID);
                                Wrappers.Logger.Log($"[ - ] {player.Username} [{UserID}]", Wrappers.Logger.LogsType.Clean);
                            }
                        }
                        break;

                    case Tags.UserAccountingData:
                        {
                            using (DarkRiftReader reader = message.GetReader())
                            {
                                string UserID = reader.ReadString();
                                string Username = reader.ReadString();
                                string StaffTag = reader.ReadString();
                                string ImageURL = reader.ReadString();
                                string Rank = reader.ReadString();
                                string AvatarID = reader.ReadString();

                                Wrappers.Logger.Log($"[ + ] {Username} [{UserID}]", Wrappers.Logger.LogsType.Clean);
                            }
                        }
                        break;

                    case Tags.SwitchIntoAvatar:
                        {
                            using (DarkRiftReader reader = message.GetReader())
                            {
                                string UserID = reader.ReadString();
                                string AvatarID = reader.ReadString();

                                var player = CVRPlayerManager.Instance.GetPlayer(UserID);

                                Wrappers.Logger.Log($"{player.GetUsername()} changed Avatar [{AvatarID}]", Wrappers.Logger.LogsType.Avatar);
                            }
                            break;
                        }
                }
            }
            return true;
        }

        private static void HWIDPostix(ref string __result)
        {
            __result = HWIDSpoof.FakeHWID;
        }

        private static bool SendGameMessagePrefix(ref Message __0, ref SendMode __1)
        {
            Tags EventCode = (Tags)__0.Tag;
            if (Variables.LogRaiseEvents)
            {
                using (DarkRiftReader reader = __0.GetReader())
                {
                    byte[] Data = reader.ReadRaw(reader.Length);
                    Wrappers.Logger.LogGameEvent(EventCode, Data, true);
                }
            }

            switch (EventCode)
            {
                case Tags.NetworkedRootData:
                    return !FakeSerialize.NoSerialize;
            }

            return true;
        }
    }
}
