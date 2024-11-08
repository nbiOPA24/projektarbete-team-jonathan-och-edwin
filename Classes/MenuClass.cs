using System;
using System.Media;
namespace MarketMaster1.Classes;


public class MenuClass
{
    // public static void StartMenu()
    // {
    //     string[] menuArray =
    //     [
    //         "****************************************",
    //         "   * Välkommen till Market Master! *",
    //         "****************************************",
    //         "I en värld där guld skimrar, silver lockar och platina står på spel...\n", 
    //         "finns det mycket som kan gå fel\n",
    //         "Står du redo att göra ditt drag? Var försiktig;\n", 
    //         "marknaden kan vara nyckfull men för den listige väntar stora vinster\n",
    //         "Du står vid marknadens port...\n",
    //         "en tyngd av mynt klirrar i fickan...\n",
    //         "Handlare viskar om dagens bästa fynd, men vem kan du lita på?\n",
    //         "'Kom och köp,' ropar en man. 'Endast de smartaste överlever här!'.\n",
    //         "Törs du satsa stort eller väljer du att spela försiktigt? \n",
    //         "Tryck [ENTER] för att äntra spelplanen..."
    //     ];

    //     string audioFile = @"C:\Users\jonat\AProject\MarketMaster1\MarketPirate.wav";
    //     using (SoundPlayer player = new SoundPlayer(audioFile))
    //     {
    //         player.Load();    // Load the file
    //         player.PlayLooping();    // Play the audio (PlaySync() to wait until it's finished)
    //     }

    //     int j = 0;
    //     for (int i = 0; i < 3; i++)
    //     {
    //         Console.SetCursorPosition(35, j);
    //         System.Console.WriteLine(menuArray[i]);
    //         j++;
    //     }
        
    //     for (int i = 3; i < menuArray.Length; i++)
    //     {
    //         TypeWrite(menuArray[i]);
    //     }

    // }

    public static void TypeWrite(string text, int delay = 45)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }
}

