using System.Collections.Generic;

namespace MainCharacterGenderPatcher
{
    public static class DialogueEntries
    {
        // All of the entries that contain words that reference the main character's gender
        public static List<Dialogue> genderedDialogue = new List<Dialogue>()
        {
            new Dialogue(10, 91), // ch0_main
            new Dialogue(12, 21),
            new Dialogue(12, 22),
            new Dialogue(13, 49), // ch1_end
            new Dialogue(13, 58),
            new Dialogue(13, 59),
            new Dialogue(24, 85), // ch20_main2
            new Dialogue(24, 94),
            new Dialogue(24, 105),
            new Dialogue(25, 10), // ch21_main
            new Dialogue(26, 49), // ch21_end
            new Dialogue(26, 58),
            new Dialogue(26, 59),
            new Dialogue(26, 88),
            new Dialogue(28, 32), // ch22_end
            new Dialogue(28, 147),
            new Dialogue(29, 22), // ch23_main
            new Dialogue(29, 86),
            new Dialogue(29, 104),
            new Dialogue(30, 58), // ch23_end
            new Dialogue(36, 75), // ch3_start_yuri
            new Dialogue(38, 104), // ch3_end
            new Dialogue(38, 106),
            new Dialogue(38, 116),
            new Dialogue(38, 118),
            new Dialogue(38, 119),
            new Dialogue(44, 10), // ch30_main
            new Dialogue(87, 12), // ch30_36
            new Dialogue(109, 26), // ch4_exclusive_natsuki
            new Dialogue(114, 167), // ch40_main
            new Dialogue(114, 175),
            new Dialogue(162, 9), // ch1_y_good
        };
    }
}
