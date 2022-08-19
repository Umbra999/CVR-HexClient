using System.IO;
using System.Net;

namespace Hexed.Configs
{
    internal class FileHandler
    {
        public static void Init()
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Hexed");
            bool NeedsRestart = false;

            if (!File.Exists("BouncyCastle.Crypto.dll"))
            {
                webClient.DownloadFile("https://cdn.discordapp.com/attachments/855072998796296212/976553926000279642/BouncyCastle.Crypto.dll", "BouncyCastle.Crypto.dll");
                NeedsRestart = true;
            }

            if (!File.Exists("websocket-sharp2.dll"))
            {
                webClient.DownloadFile("https://cdn.discordapp.com/attachments/855072998796296212/895719711029993482/websocket-sharp2.dll", "websocket-sharp2.dll");
                NeedsRestart = true;
            }

            if (!File.Exists("Microsoft.CSharp.dll"))
            {
                webClient.DownloadFile("https://cdn.discordapp.com/attachments/855072998796296212/1002014736398954647/Microsoft.CSharp.dll", "Microsoft.CSharp.dll");
                NeedsRestart = true;
            }

            string HTMLPath = $"{Directory.GetCurrentDirectory()}/ChilloutVR_Data/StreamingAssets/Cohtml/UIResources/CVRTest";
            if (!File.Exists(HTMLPath + "/HexedMenu.png"))
            {
                new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/855072998796296212/997352311905075310/Background.jpg", HTMLPath + "/HexedMenu.png");
            }

            if (NeedsRestart) Wrappers.Utils.RestartGame();
        }
    }
}