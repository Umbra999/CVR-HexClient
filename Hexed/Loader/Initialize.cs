using Hexed.Configs;
using Hexed.Core;
using Hexed.Modules;
using Hexed.Wrappers;
using MelonLoader;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Hexed.Loader
{
    internal class Initialize
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCP(uint wCodePageID);

        public static void OnStart()
        {
            if (!File.Exists("Hexed\\Key.Hexed")) HexedServer.ForceExit();
            Console.Title = $"Hexed by Umbra | CONSOLE | {Utils.RandomString(20)}";
            SetConsoleOutputCP(65001);
            SetConsoleCP(65001);
            Console.OutputEncoding = Encoding.GetEncoding(65001);
            Console.InputEncoding = Encoding.GetEncoding(65001);

            HWIDSpoof.Init();
            FileHandler.Init();
            Patching.ApplyPatches();
            //HexedServer.DownloadAvatars();
            //HexedServer.CreateAndSendLog();
            DelayedStart().Start();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(@"
          
 ▄▀▀▄ ▄▄   ▄▀▀█▄▄▄▄  ▄▀▀▄  ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀█▄▄  
█  █   ▄▀ ▐  ▄▀   ▐ █    █   █ ▐  ▄▀   ▐ █ ▄▀   █ 
▐  █▄▄▄█    █▄▄▄▄▄  ▐     ▀▄▀    █▄▄▄▄▄  ▐ █    █ 
   █   █    █    ▌       ▄▀ █    █    ▌    █    █ 
  ▄▀  ▄▀   ▄▀▄▄▄▄       █  ▄▀   ▄▀▄▄▄▄    ▄▀▄▄▄▄▀ 
 █   █     █    ▐     ▄▀  ▄▀    █    ▐   █     ▐  
 ▐   ▐     ▐         █    ▐     ▐        ▐        

      _
     |u|
   __|m|__
   \+-b-+/       Beautiful girls, dead feelings...
    ~|r|~        one day the sun is gonna explode and all this was for nothing.
     |a|
      \|
");
        }

        public static void OnUpdate()
        {

        }

        public static void OnLevelLoaded(int level)
        {

        }

        public static void OnLevelInit(int level)
        {

        }

        public static void OnQuit()
        {
            MelonPreferences.Save();
            Process.GetCurrentProcess().Kill();
        }

        private static IEnumerator DelayedStart()
        {
            while (GameObject.Find("/Cohtml") == null) yield return null;

            GameObject Hexed = new GameObject()
            {
                name = "Hexed",
            };
            UnityEngine.Object.DontDestroyOnLoad(Hexed);
            if (!GeneralWrappers.IsInVr())
            {
                Hexed.AddComponent<GUIHandler>();
                Hexed.AddComponent<KeybindHandler>();
                Hexed.AddComponent<Thirdperson>();
            }
            Hexed.AddComponent<GUIPlayerList>();
            Hexed.AddComponent<Movement>();
            Hexed.AddComponent<Playerlist>();
        }
    }
}
