using Hexed.Wrappers;
using MelonLoader;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Hexed.Modules
{
    internal class HWIDSpoof
    {
        public static readonly string FakeHWID = GenerateHWID();

        public static void Init()
        {
            SpoofSteam();
            Logger.Log("Hardware Spoofed", Logger.LogsType.Protection);
        }

        public static string GenerateHash(string Text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Text);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            string ComputeHash = string.Join("", from it in hash select it.ToString("x2"));
            return ComputeHash;
        }

        public static string GenerateHWID()
        {
            string OriginalHWID = UnityEngine.SystemInfo.deviceUniqueIdentifier;
            byte[] bytes = new byte[OriginalHWID.Length / 2];
            Utils.Random.NextBytes(bytes);
            return string.Join("", bytes.Select(it => it.ToString("x2")));
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        private static void SpoofSteam()
        {
            string path = "ChilloutVR_Data\\Plugins\\x86_64\\steam_api64.dll";
            if (!File.Exists(path)) return;
            IntPtr library = LoadLibrary(path);

            if (library == IntPtr.Zero) return;
            string[] names = new[]
            {
                "SteamAPI_Init",
                "SteamAPI_RestartAppIfNecessary",
                "SteamAPI_GetHSteamUser",
                "SteamAPI_RegisterCallback",
                "SteamAPI_UnregisterCallback",
                "SteamAPI_RunCallbacks",
                "SteamAPI_Shutdown"
            };

            foreach (string name in names)
            {
                unsafe
                {
                    IntPtr address = GetProcAddress(library, name);
                    if (address == IntPtr.Zero)
                    {
                        Logger.LogError($"{name} not found");
                        continue;
                    }
                    MelonUtils.NativeHookAttach((IntPtr)(&address), HarmonyLib.AccessTools.Method(typeof(HWIDSpoof), nameof(ReturnFalse)).MethodHandle.GetFunctionPointer());
                }
            }
        }

        private static bool ReturnFalse()
        {
            return false;
        }
    }
}
