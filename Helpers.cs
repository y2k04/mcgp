using AssetsTools.NET;
using System;
using System.IO;
using System.Security.Cryptography;

namespace MainCharacterGenderPatcher
{
    public static class Helpers
    {
        // Unmodified langen-us.cy SHA256 hash
        static readonly string cleanHash = "13113171b6241cad1a86f4e35d8b5ae32821e58ee5b172c42c77f0e6fc0cbd0f";
        
        // Returns the dialogue from the script at the provided indexes
        public static string GetDialogueValue(AssetTypeValueField script, int sceneIndex, int dialogueIndex)
            => script[0][sceneIndex][2][0][dialogueIndex].AsString;

        // Modify dialogue from script at the provided indexes
        public static void SetDialogueValue(AssetTypeValueField script, int sceneIndex, int dialogueIndex, string text)
            => script[0][sceneIndex][2][0][dialogueIndex].AsString = text;

        // Takes the first character of the string and capitalises it
        public static string CapitaliseWord(string word)
            => word[0].ToString().ToUpper() + word.Substring(1);

        // Create a hash from the bytes provided and convert to a lowercase string
        public static string ComputeHash(byte[] data)
            => string.Join("", Array.ConvertAll(SHA256.Create().ComputeHash(data), b => b.ToString("X2"))).ToLower();

        // Read file from path provided, compare the hash of the file and the hash above,
        // set data in byteArray variable and return the hash compare result
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
