using Hexed.Core;
using MelonLoader;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Hexed.Wrappers
{
    internal static class Utils
    {
        public static IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        public static object Start(this IEnumerator e)
        {
            return MelonCoroutines.Start(e);
        }

        public static System.Random Random = new System.Random(Environment.TickCount);

        public static string RandomString(int length)
        {
            char[] array = "abcdefghlijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToArray();
            string text = string.Empty;
            for (int i = 0; i < length; i++)
            {
                text += array[Random.Next(array.Length)].ToString();
            }
            return text;
        }

        public static string RandomNumberString(int length)
        {
            string text = string.Empty;
            for (int i = 0; i < length; i++)
            {
                text += Random.Next(0, 9).ToString("X8");
            }
            return text;
        }

        public static void SendWebHook(string URL, string MSG)
        {
            Task.Run(async delegate
            {
                var req = new
                {
                    content = MSG
                };

                HttpClient CurrentClient = new HttpClient(new HttpClientHandler { UseCookies = false });
                HttpRequestMessage Payload = new HttpRequestMessage(HttpMethod.Post, URL);
                string joinWorldBody = JsonConvert.SerializeObject(req);
                Payload.Content = new StringContent(joinWorldBody, Encoding.UTF8, "application/json");
                Payload.Headers.Add("User-Agent", "LunaR");
                HttpResponseMessage Response = await CurrentClient.SendAsync(Payload);
            });
        }

        public static void SendEmbedWebHook(string URL, object[] MSG)
        {
            Task.Run(async delegate
            {
                var req = new
                {
                    embeds = MSG
                };

                HttpClient CurrentClient = new HttpClient(new HttpClientHandler { UseCookies = false });
                HttpRequestMessage Payload = new HttpRequestMessage(HttpMethod.Post, URL);
                string joinWorldBody = JsonConvert.SerializeObject(req);
                Payload.Content = new StringContent(joinWorldBody, Encoding.UTF8, "application/json");
                Payload.Headers.Add("User-Agent", "LunaR");
                HttpResponseMessage Response = await CurrentClient.SendAsync(Payload);
            });
        }

        public static void RestartGame()
        {
            Process.Start("ChilloutVR.exe", Environment.CommandLine.ToString());
            HexedServer.ForceExit();
        }

        public static Image LoadImageFromResource(this Image Image, string Name, int pixels = 200, Vector4 border = new Vector4())
        {
            var resourcePath = $"Hexed.Resources.{Name}.png";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (stream == null)
            {
                Logger.LogError($"Failed to find texture {Name}");
                return null;
            }

            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Texture2D Texture = new Texture2D(1, 1);
            ImageConversion.LoadImage(Texture, ms.ToArray());
            Image.sprite = Sprite.Create(Texture, new Rect(0, 0, Texture.width, Texture.height), new Vector2(0, 0), pixels, 1000u, SpriteMeshType.FullRect, border, false);
            return Image;
        }
    }
}
