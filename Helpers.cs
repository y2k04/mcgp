using AssetsTools.NET;
using System;
using System.IO;
using System.Security.Cryptography;

namespace MainCharacterGenderPatcher
{
    public static class Helpers
    {
        static readonly string cleanHash = "13113171b6241cad1a86f4e35d8b5ae32821e58ee5b172c42c77f0e6fc0cbd0f";
        
        public static string GetDialogueValue(AssetTypeValueField script, int sceneIndex, int dialogueIndex)
            => script[0][sceneIndex][2][0][dialogueIndex].AsString;

        public static void SetDialogueValue(AssetTypeValueField script, int sceneIndex, int dialogueIndex, string text)
            => script[0][sceneIndex][2][0][dialogueIndex].AsString = text;

        public static string CapitaliseWord(string word)
            => word[0].ToString().ToUpper() + word.Substring(1);

        public static string ComputeHash(byte[] data)
            => string.Join("", Array.ConvertAll(SHA256.Create().ComputeHash(data), b => b.ToString("X2"))).ToLower();

        public static bool CompareHash(string filePath, out byte[] byteArray)
        {
            using (var s = new FileStream(filePath, FileMode.Open))
            {
                var len = (int)s.Length;
                byteArray = new byte[len];
                s.Read(byteArray, 0, len);
            }

            return ComputeHash(byteArray) == cleanHash;
        }
    }
}
