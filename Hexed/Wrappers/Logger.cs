using ABI_RC.Core.Networking;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Hexed.Wrappers
{
    internal class Logger
    {
        public enum LogsType
        {
            Clean,
            Info,
            Protection,
            Avatar,
            API,
            Bot
        }

        public static void Log(object obj, LogsType Type)
        {
            string log = obj.ToString().Replace("\a", "a").Replace("\u001B[", "u001B[");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] [");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Hexed");
            Console.ForegroundColor = ConsoleColor.Blue;
            if (Type != LogsType.Clean) Console.Write($"] [{Type}] {log}\n");
            else Console.Write($"] {log}\n");
        }

        public static void LogError(object obj)
        {
            string log = obj.ToString().Replace("\a", "");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] [");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Hexed");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"] ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{log}\n");
        }

        public static void LogWarning(object obj)
        {
            string log = obj.ToString().Replace("\a", "");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] [");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Hexed");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{log}\n");
        }

        public static void LogDebug(object obj)
        {
            string log = obj.ToString().Replace("\a", "");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] [");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Hexed");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{log}\n");
        }

        private static int[] IgnoredCodes = new int[] { 5019, 20 };

        public static void LogGameEvent(Tags Type, byte[] Bytes, bool RaiseInsteadReceive)
        {
            if (IgnoredCodes.Contains((int)Type)) return;

            string IfBytes = "  |  ";
            IfBytes += string.Join(", ", Bytes);
            IfBytes += $" [L: {Bytes.Length}]";

            LogDebug(string.Concat(new object[]
            {
                    Environment.NewLine,
                    $"======= {(RaiseInsteadReceive ? "RAISED" : "RECEIVED" )} GAME EVENT =======", Environment.NewLine,
                    $"TYPE: {Type} / {(int)Type} ", Environment.NewLine,
                    $"DATA SERIALIZED: {JsonConvert.SerializeObject(Bytes, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}{IfBytes}", Environment.NewLine,
                    "======= END =======",
            }));
        }
    }
}
