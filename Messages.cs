using System;
using System.Windows.Forms;

namespace MainCharacterGenderPatcher
{
    public static class Messages
    {
        public static string FILE_SELECT_CANCELLED = $"DDLC+ Main Character Gender Patcher v{Application.ProductVersion} \nBy Y2K4\n\nError: File selection was cancelled.\n\nPress any key to exit...";
        public static string FILE_HASH_MISMATCH = $"DDLC+ Main Character Gender Patcher v{Application.ProductVersion} \nBy Y2K4\n\nError: File hash does not match.\n\nPress any key to exit...";

        public static void TITLE()
        {
            Console.Clear();
            Console.WriteLine($"DDLC+ Main Character Gender Patcher v{Application.ProductVersion} \nBy Y2K4\n");
        }
        public static string FILE_SELECT = $"DDLC+ Main Character Gender Patcher v{Application.ProductVersion} \nBy Y2K4\n\n[Please select the langen-us.cy file]";
        public static string CUSTOM_PRONOUN = $"DDLC+ Main Character Gender Patcher v{Application.ProductVersion} \nBy Y2K4\n\n[Custom pronoun mode]";
        public static string PRONOUN_SELECT = $"DDLC+ Main Character Gender Patcher v{Application.ProductVersion} \nBy Y2K4\n\n[Select a pronoun]";
        public static string PATCH_COMPLETED = $"DDLC+ Main Character Gender Patcher v{Application.ProductVersion} \nBy Y2K4\n\nPatched file is located at '{Environment.CurrentDirectory}\\langen-us.patched.cy'\nPress any key to exit...";
    }
}
