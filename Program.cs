using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.IO;
using System.Windows.Forms;

using static MainCharacterGenderPatcher.DialogueEntries;
using static MainCharacterGenderPatcher.Helpers;

namespace MainCharacterGenderPatcher
{
    class Program
    {
        static AssetsManager am = new AssetsManager();
        static string[] genderedWords = new string[10];
        static AssetTypeValueField script;

        [STAThread]
        static void Main()
        {
            DecompileCYFile();

            var assetInst = am.LoadAssetsFileFromBundle(am.LoadBundleFile("langen-us.unity3d", true), 0, true);
            var scriptContainer = am.GetBaseField(assetInst, assetInst.file.GetAssetsOfType(AssetClassID.MonoBehaviour)[0]);
            script = scriptContainer["linesdict"];

            SelectPronouns();
            ReplacePronouns();
            CompileUnity3DFile(ref assetInst, ref scriptContainer);
            Cleanup();

            Console.Clear();
            Console.WriteLine(Messages.PATCH_COMPLETED);
            Console.Read();
        }

        static void DecompileCYFile()
        {
            Console.WriteLine(Messages.FILE_SELECT);
            OpenFileDialog f = new OpenFileDialog() { Filter = "CY file|*.cy", Title = "Select DDLC+ English language file" };
            if (f.ShowDialog() == DialogResult.Cancel)
            {
                Console.Clear();
                Console.WriteLine(Messages.FILE_SELECT_CANCELLED);
                Console.ReadKey();
                Environment.Exit(0);
            }

            byte[] cyd;

            if (!CompareHash(f.FileName, out cyd))
            {
                Console.Clear();
                Console.WriteLine(Messages.FILE_HASH_MISMATCH);
                Console.ReadKey();
                Environment.Exit(0);
            }

            for (var i = 0; i < cyd.Length; i++)
            {
                cyd[i] ^= 0x28;
            }

            using (var u3d = new BinaryWriter(File.Open("langen-us.unity3d", FileMode.Create)))
                u3d.Write(cyd);
        }

        static void SelectPronouns()
        {
            Console.Clear();
            switch (new ChoiceSel().Choice(Messages.PRONOUN_SELECT, new string[] { "She/Her", "They/Them", "Custom" }))
            {
                case 0:
                    genderedWords = new string[] { "she", "she's", "she'd", "she'll", "her", "her", "girl", "girl", "girlfriend", "lady" };
                    break;
                case 1:
                    genderedWords = new string[] { "they", "they're", "they'd", "they'll", "them", "them", "person", "person", "partner", "gentlefolk" };
                    break;
                case 2:
                    Console.WriteLine(Messages.CUSTOM_PRONOUN);
                    string[] originalGenderedWords = new string[10] { "he", "he's", "he'd", "he'll", "him", "his", "boy", "guy", "boyfriend", "gentleman" };
                    for (var i = 0; i < originalGenderedWords.Length; i++)
                    {
                        Console.Write($"Replacement for {originalGenderedWords[i]}: ");
                        genderedWords[i] = Console.ReadLine();
                    }
                    break;
            }
        }

        static void ReplacePronouns()
        {
            for (var i = 0; i < genderedDialogue.Count; i++)
            {
                var words = GetDialogueValue(script, genderedDialogue[i].sceneIndex, genderedDialogue[i].dialogueIndex).Split(' ');
                if (i == 0 || i == 6)
                {
                    words[4] = $"{genderedWords[6]}?";
                    if (words[4] == "girl?")
                        words[3] = "another";
                }

                if (i == 2 || i == 3 || i == 10 || i == 22 || i == 24)
                    words[0] = CapitaliseWord(genderedWords[0]);
                if (i == 3 || i == 9 || i == 10 || i == 15)
                    words[4] = genderedWords[0];
                if (i == 5 || i == 12 || i == 29)
                    words[6] = genderedWords[0];
                if (i == 4 || i == 11)
                    words[11] = genderedWords[0];
                if (i == 15 || i == 19)
                    words[7] = genderedWords[4];
                if (i == 16 || i == 17)
                    words[4] = genderedWords[6];

                if (i == 1)
                {
                    words[4] = genderedWords[4];
                    words[9] = genderedWords[1];
                }
                else if (i == 7)
                    words[2] = $"{genderedWords[8]},";
                else if (i == 8)
                    words[9] = genderedWords[0];
                else if (i == 13)
                    words[2] = genderedWords[3];
                else if (i == 14)
                    words[12] = genderedWords[0];
                else if (i == 18)
                    words[6] = genderedWords[4];
                else if (i == 19)
                    words[10] = $"{genderedWords[4]}self.";
                else if (i == 20)
                    words[4] = $"{genderedWords[6]}.";
                else if (i == 21)
                    words[11] = genderedWords[4];
                else if (i == 23)
                    words[12] = genderedWords[2];
                else if (i == 25)
                    words[3] = genderedWords[1];
                else if (i == 26)
                    words[10] = $"{genderedWords[4]}.";
                else if (i == 27)
                    words[5] = $"{genderedWords[8]}~";
                else if (i == 28)
                    words[8] = $"{genderedWords[9]}?";
                else if (i == 30)
                    words[14] = genderedWords[4];
                else if (i == 31)
                    words[0] = $"<i>({CapitaliseWord(genderedWords[1])}";

                SetDialogueValue(script, genderedDialogue[i].sceneIndex, genderedDialogue[i].dialogueIndex, string.Join(" ", words));
            }
        }

        static void CompileUnity3DFile(ref AssetsFileInstance assetInst, ref AssetTypeValueField scriptContainer)
        {
            assetInst.file.GetAssetsOfType(AssetClassID.MonoBehaviour)[0].SetNewData(scriptContainer);

            using (AssetsFileWriter w = new AssetsFileWriter("langen-us.patched.unity3d"))
            {
                assetInst.file.Write(w);
            }

            byte[] data;

            using (var s = new FileStream("langen-us.patched.unity3d", FileMode.Open))
            {
                var len = (int)s.Length;
                data = new byte[len];
                s.Read(data, 0, len);
            }

            for (var i = 0; i < data.Length; i++)
            {
                data[i] ^= 0x28;
            }

            using (var cy = new BinaryWriter(File.Open("langen-us.patched.cy", FileMode.Create)))
                cy.Write(data);
        }

        static void Cleanup()
        {
            am.UnloadAll();
            File.Delete("langen-us.unity3d");
            File.Delete("langen-us.patched.unity3d");
        }
    }
}
