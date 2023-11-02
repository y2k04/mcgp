using System;

namespace MainCharacterGenderPatcher
{
    public class ChoiceSel
    {
        public int selectedIndex;

        public int Choice(string question, string[] choices)
        {
            selectedIndex = 0;
            while (true)
            {
                Console.WriteLine($"{question}{Environment.NewLine}");
                for (var i = 0; i < choices.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[i]);
                        Console.ResetColor();
                    } else
                        Console.WriteLine(choices[i]);
                }

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.DownArrow && selectedIndex <= 1)
                    selectedIndex++;
                else if (key == ConsoleKey.UpArrow && selectedIndex >= 1)
                    selectedIndex--;
                else if (key == ConsoleKey.Enter)
                    return selectedIndex;

                Console.Clear();
            }
        }
    }
}
