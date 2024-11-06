using System;
using System.Media;
namespace MarketMaster1.Classes;

public class MenuClass
{
    public static void StartMenu()
    {
        Console.Clear();
        Console.WriteLine("****************************************");
        Console.WriteLine("   * Välkommen till Market Master! *");
        Console.WriteLine("****************************************");
        Thread.Sleep(4500);
        Console.Clear();
        Console.WriteLine("\n");
        TypeWrite("I en värld där guld skimrar, ");
        Thread.Sleep(360);
        TypeWrite("silver lockar");
        Thread.Sleep(360);
        TypeWrite(" och platina står på spel ");
        Thread.Sleep(360);
        TypeWrite("finns det mycket som kan gå fel...\n");
        Thread.Sleep(1800);
        TypeWrite("Står du redo för att göra ditt drag?\n");
        Thread.Sleep(1200);
        TypeWrite("Var försiktig; marknaden kan vara nyckfull, ");
        Thread.Sleep(360);
        TypeWrite("men för den listige väntar stora vinster!\n");
        Thread.Sleep(2100);
        Console.WriteLine("Press [Enter] för att kliva in i marknadens djungel...");
        Console.ReadKey();
        Thread.Sleep(1200);
        Console.Clear();
        // string audioFile = @"C:\Users\jonat\AProject\MarketMaster1\MarketPirate.wav";
        // using (SoundPlayer player = new SoundPlayer(audioFile))
        // {
        //     player.Load();    // Load the file
        //     player.PlayLooping();    // Play the audio (PlaySync() to wait until it's finished)
        // }
        TypeWrite("Du står vid marknadens port,");
        Thread.Sleep(900);
        TypeWrite(" en tyngd av mynt klirrar i fickan.\n");
        Thread.Sleep(900);
        TypeWrite("Handlare viskar om dagens bästa fynd, men vem kan du lita på?\n");
        Thread.Sleep(900);
        TypeWrite("'Kom och köp,' ropar en man. 'Endast de smartaste överlever här!'.\n");
        Thread.Sleep(900);
        TypeWrite("Törs du satsa stort eller väljer du att spela försiktigt? \n");
        Thread.Sleep(900);
        TypeWrite("Press [Enter] för att pröva lyckan...");
        Console.ReadKey();
    }
        public static void TypeWrite(string text, int delay = 45)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }
}